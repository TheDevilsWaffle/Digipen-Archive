####################################################################################################
#COPYRIGHT:     All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
#FILENAME:      py_platformer_mechanics
#AUTHOR:        Travis Moore
#DESCRIPTION:   This script contains basic platformer mechanics (movement and
#               jumping) for all player and ai characters in the game.
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import Keys
import Action
import math
#shothand for VectorMath.Vec3
Vec3 = VectorMath.Vec3
#states to be used for character
States = ["Idle", "Walking", "Falling", "Jumping"]
#set starting state for character
CharacterState = Property.DeclareEnum("CharacterState", States);


class py_platformer_mechanics():
    #character is active and now dead/sleeping
    Active = Property.Bool(default = True)
    #The up vector of the character. You will jump in this direction
    Up = Property.Vector3(default = Vec3(0.0, 1.0, 0.0))
    
    #MOVEMENT
    #set variable MaxSpeed = the maximum speed the character
    MaxSpeed = Property.Float(default = 7.0)
    #set variable MovePower = the acceleration of the character
    MovePower = Property.Float(default = 2.0)
    #set variable InAirControl = the amount of movement a character can have in the air ( 0 = no control, 1 = full control)
    InAirControl = Property.Float(default = 0.2)
    
    #GROUND
    #set variable WalkableSlopeAngle = max angle of surface that is walkable for the player
    WalkableSlopeAngle = Property.Float(default = 65.0)
    
    #JUMPING
    #set variable InitialJumpVelocity = starting force of a jump
    InitialJumpVelocity = Property.Float(default = 6.0)
    #set variable AdditiveJumpVelocity = force applied after jump has started (if player is holding on to jump)
    AdditiveJumpVelocity = Property.Float(default = 150.0)
    #set variable AdditiveJumpTime = how long the AdditiveJumpVelocity can be applied in the air
    AdditiveJumpTime = Property.Float(default = 0.0)
    #set variable LateJumpTimer = the brief allowance of time a player has to press jump if off a surface
    LateJumpTimer = Property.Float(default = 0.23)

    def Initialize(self, init):
        #set CurrentY equal to the character's current y-axis position
        CurrentY = self.Owner.Transform.Translation.y
        #set PreviousY equal to the CurrentY value (this is updated last)
        self.PreviousY = CurrentY
        #set variable Traction = the amount of control the player has on a surface ( 0 = no control 1 = full control (and inbetween))
        self.Traction = 1.0
        #set boolean Jumping = if character is currently Jumping
        self.Jumping = False
        #set boolean InAirFromJump = if the player is in the air because of jumping or not
        self.InAirFromJump = False
        #set boolean OnGround = if character is currently on the ground (if true, then Traction is how much control the character has)
        self.OnGround = False
        #set variable TimeSinceLastDirectContact = how long (Dt) character has been off the ground
        self.TimeSinceLastDirectContact = 0.0
        #set variable VelocityOfGround = the movement velocity of the ground the character is on (used to maintain velocity when jumping off of moving ground)
        self.VelocityOfGround = Vec3(0.0, 0.0, 0.0)
        #set variable JumpTimer = cooldown for jumping
        self.JumpTimer = 2.0
        #set variable MoveDirection = the current left/right movement of the player
        self.MoveDirection = Vec3(0.0, 0.0, 0.0)
        #set variable CharacterFlipRight = used to keep track of which way the sprite is facing (right == true or left == false)
        self.CharacterFlipRight = True
        #set variable CharacterState = current state of the character (currently Idle)
        self.CharacterState = CharacterState.Idle

        # We need to update the character's movement every logic update then call OnLogicUpdate
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)

    ################################################################################################
    #FUNCTION - OnLogicUpdate
    #DESCRIPTION - responsible for updating the character's movement every logic update
    ################################################################################################
    def OnLogicUpdate(self, updateEvent):
        #if character is not active, do not do the loop
        if(self.Active == False):
            return

        #send this event to update our input. Whoever listens to this should update the MoveDirection vector that on us. The receiver of the event will be able to immediately update our state.
        toSend = Zero.ScriptEvent()
        self.Owner.DispatchEvent("UpdateCharacterInput", toSend)

        #update whether or not character is on ground, as well as getting the traction of the surface
        self.UpdateGroundState(updateEvent.Dt)
        
        #all logic for jumping is contained in this function
        self.UpdateJumpState(updateEvent.Dt)

        #get character's current control (value between 0-1)
        controlScalar = self.GetCurrentControlScalar()

        #set the amount of force we can apply to reach our desired maximum speed, we're multiplying by the object's mass so that the character can easily be scaled without having to re-adjust MovePower
        moveForce = self.MovePower * self.Owner.RigidBody.Mass
        #apply the control scalar (air control / traction / etc...)
        moveForce *= controlScalar
        #set the final force
        self.Owner.DynamicMotor.MaxMoveForce = moveForce

        #get our current max speed
        maxSpeed = self.GetMaxSpeed()

        #move in the given direction with our current max speed
        self.Owner.DynamicMotor.MoveInDirection(self.MoveDirection * maxSpeed, self.Up)
        self.UpdateCurrentState(self.MoveDirection)
    
    ################################################################################################
    #FUNCTION - UpdateCurrentState
    #PARAMETERS - movement = MoveDirection: Vec3 used to determine idle/walking
    #DESCRIPTION - responsible for changing the state of the character based on available states
    ################################################################################################
    def UpdateCurrentState(self, _movement):
        #set currentY based on owner
        self.CurrentY = self.Owner.Transform.Translation.y
        #character is on OnGround
        if(self.OnGround and self.CurrentY > self.PreviousY - 0.05):
            #if it is our player
            if(self.Owner.py_objectSettings.objectClass == "Player"):
                #fire off the snail idle sprite/animation in py_snail_animations
                self.Owner.py_snail_animations.SnailIdle()
            pass

            #character is Jumping
            if(self.Jumping):
                #update state to Jumping
                self.CharacterState = CharacterState.Jumping
                #if it is our player
                if(self.Owner.py_objectSettings.objectClass == "Player"):
                    #fire off the snail jumping sprite/animation in py_snail_animations
                    self.Owner.py_snail_animations.SnailJump()
                pass

            #character not Jumping
            else:
                #set local variable Speed = _movement to determine if idle or not
                Speed = _movement.length()
                #if Speed == 0
                if(Speed == 0.0):
                    #update character to Idle
                    self.CharacterState = CharacterState.Idle
                    #if it is our player
                    if(self.Owner.py_objectSettings.objectClass == "Player"):
                        #fire off the snail idle sprite/animation in py_snail_animations
                        self.Owner.py_snail_animations.SnailIdle()
                    pass
                #if Speed != 0
                else:
                    #update character to Walking
                    self.CharacterState = CharacterState.Walking

        #character is not OnGround
        else:
            #character is Jumping
            if(self.Jumping):
                #update state to Jumping
                self.CharacterState = CharacterState.Jumping
                if(self.Owner.py_objectSettings.objectClass == "Player"):
                    #fire off the snail jumping sprite/animation in py_snail_animations
                    self.Owner.py_snail_animations.SnailJump()
                pass
            #character is no longer jumping
            else:
                #update state to Falling
                self.CharacterState = CharacterState.Falling
                #if it is our player
                if(self.Owner.py_objectSettings.objectClass == "Player"):
                    #player is jumping, switch to jumping sprite
                    if(self.CurrentY > self.PreviousY):
                        #fire off the snail jumping sprite/animation in py_snail_animations
                        self.Owner.py_snail_animations.SnailJump()
                    #player is falling, switch to falling sprite
                    if(self.CurrentY < self.PreviousY):
                        #fire off the snail falling sprite/animation in py_snail_animations
                        self.Owner.py_snail_animations.SnailFall()
                    pass
                    
        #fix the burping
        if(self.Space.FindObjectByName("Snail").py_input_manager.IsBurping == True):
            #fire off the snail jumping sprite/animation in py_snail_animations
            self.Space.FindObjectByName("Snail").py_snail_animations.SnailBurp()
        
        #update the PreviousY based on CurrentY
        self.PreviousY = self.CurrentY

    ################################################################################################
    #FUNCTION - GetCurrentControlScaler
    #DESCRIPTION - responsible for determining which control type the character currently has
    ################################################################################################
    def GetCurrentControlScalar(self):
        #use character's current traction if OnGround
        if(self.OnGround):
            return self.Traction

        #character not OnGround, use InAirControl
        return self.InAirControl

    ################################################################################################
    #FUNCTION - GetMaxSpeed
    #DESCRIPTION - responsible for determining speed of character depending if OnGround or not
    ################################################################################################
    def GetMaxSpeed(self):
        #set local variable Speed = MaxSpeed
        Speed = self.MaxSpeed
        #use Speed normally if OnGround
        if(self.OnGround):
            return Speed

        #not on ground, set local variable Vel = character's Velocity
        Vel = self.Owner.RigidBody.Velocity
        #project out the up vector (we only want velocity on our plane of movement)
        Vel = Vel - self.Up * Vel.dot(self.Up)
        #set local variable CurrSpeed = new value of Vel
        CurrSpeed = Vel.length()
        #return whichever is greater
        return max(Speed, CurrSpeed)

    ################################################################################################
    #FUNCTION - UpdateGroundState
    #PARAMETERS - _dt: used as a timer variable
    #DESCRIPTION - responsible for determining whether the character is grounded or not
    ################################################################################################
    def UpdateGroundState(self, _dt):
        #update the timer for late jumps
        self.TimeSinceLastDirectContact += _dt
        
        #iterate through all objects we're in contact with in order to determine whether or not we have any objects under us (ground)
        for contactHolder in self.Owner.Collider.Contacts:
            #ignore ghost objects
            if(contactHolder.IsGhost):
                continue

            #get the object character is in contact with
            objectHit = contactHolder.OtherObject

            # We need the normal of the surface (the normal that points from the object hit to us) to determine whether or not it's walkable
            contactPoint = contactHolder.FirstPoint
            surfaceNormal = -contactPoint.WorldNormalTowardsOther

            #if the object is considered walkable
            if(self.IsGround(surfaceNormal)):
                #contact is valid ground
                self.OnGround = True
                
                if(self.Jumping == False):
                    self.InAirFromJump = False

                #set variable TimeSinceLastDirectContact = 0
                self.TimeSinceLastDirectContact = 0.0
                
                # set the reference frame to the object if it's valid
                if(self.ShouldChangeReferenceFrame(objectHit)):
                    self.Owner.DynamicMotor.SetReferenceFrameToObject(objectHit)
                
                #store the object's velocity so that we can jump with the object's velocity taken into account
                if(objectHit.RigidBody):
                    self.VelocityOfGround = objectHit.RigidBody.Velocity

        #if TimeSinceLastDirectContact > LateJumpTimer
        if(self.TimeSinceLastDirectContact > self.LateJumpTimer):
            #reset all values
            self.OnGround = False
            self.VelocityOfGround = Vec3(0.0, 0.0, 0.0)

            # By default, always set our reference frame to the world. if we're in contact with another object that is considered walkable, we can change to its reference frame (see loop below)
            self.Owner.DynamicMotor.SetReferenceFrameToWorld()

    ################################################################################################
    #FUNCTION - ChouldChangeReference
    #PARAMETERS - __object: used to keep track of object hit
    #DESCRIPTION - responsible for setting reference frame based on object on
    ################################################################################################
    def ShouldChangeReferenceFrame(self, _object):
        #for now, all objects are valid you could only accept kinematic objects you could only accept objects with a specific component (e.g. a Platform component)
        return True

    ################################################################################################
    #FUNCTION - GetDegreeDifference
    #PARAMETERS - _surfaceNormal: used to store surface normal value
    #DESCRIPTION - responsible for returning the degree difference
    ################################################################################################
    def GetDegreeDifference(self, _surfaceNormal):
        #returns the angle between the surface normal and the up vector of the character
        cosTheta = _surfaceNormal.dot(self.Up)
        radians = math.acos(cosTheta)
        degrees = math.degrees(radians)
        return degrees

    ################################################################################################
    #FUNCTION - IsGround
    #PARAMETERS - _surfaceNormal: used to determine if character can be on this surface
    #DESCRIPTION - responsible for determining if character is on ground
    ################################################################################################
    def IsGround(self, surfaceNormal):
        #if the angle of the surface's normal is less than the specified value, character is considered to be on ground
        degrees = self.GetDegreeDifference(surfaceNormal)
        return degrees < self.WalkableSlopeAngle

    ################################################################################################
    #FUNCTION - BeginJump
    #DESCRIPTION - responsible for checking if character can jump and performing jump
    ################################################################################################
    def BeginJump(self):
        #call CanJump and check if character can currently jump
        if(self.CanJump()):
            #if CanJump returns true, perform Jump()
            self.Jump()
            self.OnGround = False

    ################################################################################################
    #FUNCTION - EndJump
    #DESCRIPTION - responsible for setting Jumping boolean to false
    ################################################################################################
    def EndJump(self):
        #set Jumping boolean to false
        self.Jumping = False

    ################################################################################################
    #FUNCTION - UpdateJumpState
    #PARAMETERS - _dt: used as a time variable
    #DESCRIPTION - responsible for continuing jumping if key/button is held while in the air
    ################################################################################################
    def UpdateJumpState(self, _dt):
        #update CurrentY
        self.CurrentY = self.Owner.Transform.Translation.y
        #if character is currently Jumping
        if(self.Jumping):
            #keep adding the AdditiveJumpVelocity while less than the JumpTimer
            if(self.JumpTimer < self.AdditiveJumpTime):
                #increment JumpTimer through passed _dt paramter
                self.JumpTimer += _dt
                #add to character's velocity
                self.Owner.RigidBody.Velocity += self.Up * self.AdditiveJumpVelocity * _dt
            
            #Jump is no longer available (button/key depress or timer is up)
            else:
                #set Jumping boolean to false
                self.Jumping = False
    
    ################################################################################################
    #FUNCTION - OnKeyUp
    #DESCRIPTION - responsible for determining if the character can jump
    ################################################################################################
    def CanJump(self):
        #return OnGround to determine if character can jump or not
        return self.OnGround
        
    
    ################################################################################################
    #FUNCTION - Jump
    #DESCRIPTION - responsible for performing jumping for characters
    ################################################################################################
    def Jump(self):
        #get only horizontal element of our velocity (none in the direction of our Up vector)
        currVelocity = self.Owner.RigidBody.Velocity
        newVelocity = currVelocity - self.Up * currVelocity.dot(self.Up)

        # Add velocity upward by the initial jump strength
        newVelocity += self.Up * self.InitialJumpVelocity

        #we want to add the velocity of the surface we're currently on
        #this allows us to get an extra boost from jumping off moving objects (e.g. platforms moving upwards)
        newVelocity += self.Up * self.VelocityOfGround

        #set the velocity
        self.Owner.RigidBody.Velocity = newVelocity

        #we're no longer on the ground
        self.OnGround = False

        #we're now jumping (used for the additive jump)
        self.Jumping = True
        self.InAirFromJump = True

        #set the additive jump timer to 0
        self.JumpTimer = 0.0

        #because we're now off the ground, we want to attach ourselves back to the world
        self.Owner.DynamicMotor.SetReferenceFrameToWorld()
        
Zero.RegisterComponent("py_platformer_mechanics", py_platformer_mechanics)