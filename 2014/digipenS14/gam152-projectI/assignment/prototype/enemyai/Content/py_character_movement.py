import Zero
import Events
import Property
import VectorMath
import Keys
import Action
import math
Vec2 = VectorMath.Vec2
Vec3 = VectorMath.Vec3
Vec4 = VectorMath.Vec4
Quat = VectorMath.Quat

States = ["Idle", "Walking", "Falling", "Jumping"]
CharacterState = Property.DeclareEnum("CharacterState", States);

class py_character_movement():
    Active = Property.Bool(default = True)
    #The up vector of the character. You will jump in this direction
    Up = Property.Vector3(default = Vec3(0.0, 1.0, 0.0))
    
    #------------------------------------------------------------------ Movement
    # The maximum speed the character can achieve on its own
    MaxSpeed = Property.Float(default = 7.0)
    # Determines the max force you can apply to get to your target speed (acceleration)
    # Also determines how much you can push or pull objects (strength)
    # Like horsepower
    # NOTE* This does not increase the max speed of the character
    MovePower = Property.Float(default = 2.0)
    # A scalar for the amount of force the player can apply to move
    # while not on the ground
    # Value should be between 0-1
    # 0: No control while in the air
    # 1: Full control while in the air
    InAirControl = Property.Float(default = 0.2)
    
    #-------------------------------------------------------------------- Ground
    # Used to determine whether or not the objects we're in contact with are
    # considered to be walkable (you're on ground and can jump).  If the
    # angle (degrees) between the normal of the surface and the players up
    # vector is less than this value, it is walkable
    WalkableSlopeAngle = Property.Float(default = 40.0)
    
    #------------------------------------------------------------------- Jumping
    # The velocity applied when a jump is started (in the up direction)
    # This is applied only once, immediately when a jump is started
    InitialJumpVelocity = Property.Float(default = 6.0)
    # Extra velocity upward that is applied after a jump is initiated, every frame
    # for a specified amount of time (AdditiveJumpTime)
    AdditiveJumpVelocity = Property.Float(default = 150.0)
    # The amount of time (seconds) that the AdditiveJumpVelocity will be applied
    # while the Jump action is still held
    AdditiveJumpTime = Property.Float(default = 0.0)
    # Sometimes you may leave the surface just slightly (micro hops) or have just 
    # barely run off an edge, and we still want to be able to jump for a short 
    # amount of time after
    LateJumpTimer = Property.Float(default = 0.23)
    
    def Initialize(self, init):
        # A scalar for the amount of force the player can apply to move
        # Should be between 0-1
        # 0: No control while on the ground
        # 1: Full control while on the ground
        # NOTE: This value will be updated based on specified traction of the
        # surface it is standing on (through the Traction component)
        self.Traction = 1.0
        ## Wether or not we're currently in the middle of a jump
        self.Jumping = False
        self.InAirFromJump = False
        # Whether or not we're considered to be on the ground
        # This is used to determine how much control the player has
        # If we're on the ground, our control is dependant on the traction of the surface
        # If we're in the air, our control is dependant on the InAirControl property
        # It's set in the UpdateGroundState function
        self.OnGround = False
        # The time since we were in last direct contact with the ground
        # See the LateJumpTimer property description
        self.TimeSinceLastDirectContact = 0.0
        # The velocity of the ground we're standing on
        # This is used for to maintain velocity when jumping off of moving ground
        self.VelocityOfGround = Vec3(0.0, 0.0, 0.0)
        self.CharacterState = CharacterState.Idle
        self.JumpTimer = 0.0
        
        # The direction that we're moving. This is set from
        # another script that is updated every frame.
        self.MoveDirection = Vec3(0.0, 0.0, 0.0)
        
        #flip the sprite 
        self.CharacterFlipRight = True
        
        # We need to update the character's movement every logic update
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
    
    def OnLogicUpdate(self, updateEvent):
        if(self.Active == False):
            return
        
        # Send this event to update our input. Whoever listens to this should update
        # the MoveDirection vector that on us. The reason we send an event out here
        #is to reduce character lag. The receiver of the event will be able
        #to immediately update our state.
        toSend = Zero.ScriptEvent()
        self.Owner.DispatchEvent("UpdateCharacterInput", toSend)
        
        # Update whether or not we are on ground, as well as getting 
        # the traction (slipperyness) of the surface we're on
        self.UpdateGroundState(updateEvent.Dt)
        
        # All logic for jumping is contained in this function
        self.UpdateJumpState(updateEvent.Dt)
        
        # Get our current control (value between 0-1)
        controlScalar = self.GetCurrentControlScalar()
        
        # We want to set the amount of force we can apply to reach our desired maximum speed
        # We're multiplying by the object's mass so that the character can easily be scaled
        # without having to re-adjust MovePower
        moveForce = self.MovePower * self.Owner.RigidBody.Mass
        # Apply the control scalar (air control / traction / etc...)
        moveForce *= controlScalar
        # Set the final force
        self.Owner.DynamicMotor.MaxMoveForce = moveForce
        
        # Get our current max speed
        maxSpeed = self.GetMaxSpeed()
        
        # Move in the given direction with our current max speed
        self.Owner.DynamicMotor.MoveInDirection(self.MoveDirection * maxSpeed, self.Up)
        
        self.UpdateCurrentState(self.MoveDirection)
        
        #sets character sprite direction
        self.Owner.Sprite.FlipX = self.CharacterFlipRight
    
    def UpdateCurrentState(self, movement):
        if(self.OnGround):
            if(self.Jumping):
                self.CharacterState = CharacterState.Jumping
            else:
                speed = movement.length()
                if(speed == 0.0):
                    self.CharacterState = CharacterState.Idle
                else:
                    self.CharacterState = CharacterState.Walking
        else:
            if(self.Jumping):
                self.CharacterState = CharacterState.Jumping
            else:
                self.CharacterState = CharacterState.Falling
    
    def GetCurrentControlScalar(self):
        # Use our current traction if we're on the ground
        if(self.OnGround):
            return self.Traction
        # Otherwise, use the air control
        return self.InAirControl
    
    def GetMaxSpeed(self):
        speed = self.MaxSpeed
        # If we're on the ground simply use the default max speed
        if(self.OnGround):
            return speed
        
        # If we're in the air, we want to be able to move faster than the
        # specified MaxSpeed.  This allows objects to hit us and accelerate
        # us to faster speeds than the specified max
        # To accomplish this, we want to set our current maximum speed
        # to the maximum of the specified MaxSpeed and our current velocity
        # This way, the DynamicMotor will not try and slow us down to the max
        # specified speed while in the air, we will simply remain at whatever
        # speed we're currently at
        
        # Get our current velocity
        vel = self.Owner.RigidBody.Velocity
        
        # Project out the up vector (we only want velocity on our plane of movement)
        # In 2D, this could simply be: abs(self.Owner.RigidBody.Velocity.x)
        vel = vel - self.Up * vel.dot(self.Up)
        currSpeed = vel.length()
        
        # Return whichever is greater
        return max(speed, currSpeed)
    
    #-------------------------------------------------------------- Ground State
    def UpdateGroundState(self, dt):
        # Update the timer for late jumps
        self.TimeSinceLastDirectContact += dt
        
        # We want to iterate through all objects we're in contact with in order
        # to determine whether or not we have any objects under us (ground)
        for contactHolder in self.Owner.Collider.Contacts:
            # Ignore ghost objects
            if(contactHolder.IsGhost):
                continue
            
            # Get the object we're in contact with
            objectHit = contactHolder.OtherObject
            
            # We need the normal of the surface (the normal that points from
            # the object hit to us) to determine whether or not it's walkable
            contactPoint = contactHolder.FirstPoint
            surfaceNormal = -contactPoint.WorldNormalTowardsOther
            
            # If the object is considered walkable
            if(self.IsGround(surfaceNormal)):
                # Contact is valid ground
                self.OnGround = True
                if(self.Jumping == False):
                    self.InAirFromJump = False
                
                self.TimeSinceLastDirectContact = 0.0
                
                # Set the reference frame to the object if it's valid
                if(self.ShouldChangeReferenceFrame(objectHit)):
                    self.Owner.DynamicMotor.SetReferenceFrameToObject(objectHit)
                
                # We want to store the object's velocity so that we can
                # jump with the object's velocity taken into account
                if(objectHit.RigidBody):
                    self.VelocityOfGround = objectHit.RigidBody.Velocity
                
              
        
        if(self.TimeSinceLastDirectContact > self.LateJumpTimer):
            # Reset all values
            self.OnGround = False
            self.VelocityOfGround = Vec3(0.0, 0.0, 0.0)
            
            # By default, always set our reference frame to the world
            # If we're in contact with another object that is considered walkable, 
            # we can change to its reference frame (see loop below)
            self.Owner.DynamicMotor.SetReferenceFrameToWorld()
    
    def ShouldChangeReferenceFrame(self, object):
        # For now, all objects are valid
        # You could only accept kinematic objects
        # You could only accept objects with a specific component (e.g. a Platform component)
        return True
    
    def GetDegreeDifference(self, surfaceNormal):
        # Returns the angle between the surface normal and the up vector of the character
        cosTheta = surfaceNormal.dot(self.Up)
        radians = math.acos(cosTheta)
        degrees = math.degrees(radians)
        return degrees
    
    def IsGround(self, surfaceNormal):
        # If the angle of the surface's normal is less than the specified value,
        # we're considered to be on ground
        degrees = self.GetDegreeDifference(surfaceNormal)
        return degrees < self.WalkableSlopeAngle
    
    #------------------------------------------------------------------- Jumping
    def BeginJump(self):
        # Start jumping if we can
        if(self.CanJump()):
            self.Jump()
    
    def EndJump(self):
        self.Jumping = False
    
    def UpdateJumpState(self, dt):
        # If we're currently jumping, we want to continue adding an upward velocity while
        # they still have the jump action held down
        # This allows the player to hold the jump button longer to jump higher
        if(self.Jumping):
            # Keep adding the additive jump velocity while Jump is still held and until we
            # have reached the additive jump timer
            if(self.JumpTimer < self.AdditiveJumpTime):
                # Increment the timer
                self.JumpTimer += dt
                # Add to our velocity
                self.Owner.RigidBody.Velocity += self.Up * self.AdditiveJumpVelocity * dt
            # Otherwise jump is not still held (or we rand out of time)
            else:
                # If the player has released the jump button or we've reached the
                # end of the timer, we're no longer jumping
                self.Jumping = False
    
    def CanJump(self):
        # We need to be on the ground to be allowed to jump
        # We could also add logic for jumping if we're holding
        # onto something like a rope or ledge
        return self.OnGround
    
    def Jump(self):
        # Get only horizontal element of our velocity (none in the direction of our Up vector)
        currVelocity = self.Owner.RigidBody.Velocity
        newVelocity = currVelocity - self.Up * currVelocity.dot(self.Up)
        
        # Add velocity upward by the initial jump strength
        newVelocity += self.Up * self.InitialJumpVelocity
        
        # We want to add the velocity of the surface we're currently on
        # This allows us to get an extra boost from jumping off moving objects (e.g. platforms moving upwards)
        newVelocity += self.Up * self.VelocityOfGround
        
        # Set the velocity
        self.Owner.RigidBody.Velocity = newVelocity
        
        # We're no longer on the ground
        self.OnGround = False
        
        # We're now jumping (used for the additive jump)
        self.Jumping = True
        self.InAirFromJump = True
        
        # Set the additive jump timer to 0
        self.JumpTimer = 0.0
        
        # Because we're now off the ground, we want to attach ourselves back to the world
        self.Owner.DynamicMotor.SetReferenceFrameToWorld()
    
Zero.RegisterComponent("py_character_movement", py_character_movement)