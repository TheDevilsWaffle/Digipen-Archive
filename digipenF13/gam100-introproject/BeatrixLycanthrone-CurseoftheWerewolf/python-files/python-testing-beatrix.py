import Zero
import Events
import Property
import math
import VectorMath
import Color
import time
import sys

Vec3 = VectorMath.Vec3
#global variable to set human/werewolf state
werewolfState = False
#global variables to allow changing between human/werewolf
changeToWerewolf = False
changeToHuman = False
#global variable to store cooldown between form changes
cooldownNotActive = True

class beatrix_controller:

    #Properties
    moveSpeed = Property.Float(0.25)
    jumpSpeed = Property.Float(30.0)
    maxNumberOfJumps = Property.Int(1)

    #INITIALIZE
    def Initialize(self, initializer):
        #we want to apply controls every logic update
        Zero.Connect(self.Space, Zero.Events.LogicUpdate, self.OnLogicUpdate)
        #global variable to check if player is in the air
        self.OnGround = False
        #global variable to check how many times a player jumps
        self.JumpsRemaining = 0

    #FUNCTION OnLogicUpdate
    def OnLogicUpdate(self, UpdateEvent):
        #get all input from the player
        self.UpdatePlayerInput()
        #Logic for player movement
        self.ApplyMovement()
        #logic for player jumping
        self.ApplyJumping()
        #logic to check "on ground" status
        self.UpdatePlayerGroundState()
        #logic for updating "JumpsRemaining"
        self.UpdateJumpState()
        #logic for updating player to and from werewolf state
        self.UpdatePlayerState()

    #FUNCTION UpdatePlayerInput
    def UpdatePlayerInput(self):
        #the player is attempting to move left or right
        self.moveRight = Zero.Keyboard.KeyIsDown(Zero.Keys.D)
        self.moveLeft = Zero.Keyboard.KeyIsDown(Zero.Keys.A)
        #the player is attemping to jump
        self.moveJump = Zero.Keyboard.KeyIsPressed(Zero.Keys.Space)
        #the player is attemping to change human/werewolf states
        self.changeState = Zero.Keyboard.KeyIsPressed(Zero.Keys.Enter)

    #FUNCTION ApplyMovement
    def ApplyMovement(self):
        moveDirection = Vec3(0,0,0)
        #apply movement based on player input
        if(self.moveRight):
            moveDirection += Vec3(1,0,0)
        if(self.moveLeft):
            moveDirection += Vec3(-1,0,0)
        #Only want unit direction
        moveDirection.normalize()
        #applying movement linear velocity based on werewolfState
        #if human, normal speed
        if(werewolfState == False):
            self.Owner.RigidBody.ApplyLinearVelocity(moveDirection * self.moveSpeed)
        #else werewolf speed x 2
        else:
            self.Owner.RigidBody.ApplyLinearVelocity(moveDirection * self.moveSpeed * 2)

    #FUNCTION GetDegreeDifference
    def GetDegreeDifference(self, surfaceNormal):
        UpDirection = Vec3(0,1,0)
        #returns the angle between the surface normal and the up vector of the player
        cosTheta = surfaceNormal.dot(UpDirection)
        #playing it safe with math
        cosTheta = min(max(cosTheta, -1.0),1.0)
        #rest of the function
        radians = math.acos(cosTheta)
        degrees = math.degrees(radians)
        return degrees

    #FUNCTION IsGround
    def IsGround(self, surfaceNormal):
        #if the angle of surface's normal is less than specified value, the player is on the ground
        degrees = self.GetDegreeDifference(surfaceNormal)
        return degrees < 60.0

    #FUNCTION UpdatePlayerGroundState
    def UpdatePlayerGroundState(self):
        #reset varaible before evaluating
        self.OnGround = False
        #check to see if player is in contact with ground
        for ContactHolder in self.Owner.Collider.Contacts:
            #ignore ghost objects
            if(ContactHolder.IsGhost):
                continue
            #find out what object the player is colliding with
            objectHit = ContactHolder.OtherObject
            #checking normals to determine if object is walkable
            surfaceNormal = -ContactHolder.FirstPoint.WorldNormalTowardsOther
            #if the object is walkable
            if(self.IsGround(surfaceNormal)):
                #object is valid ground
                self.OnGround = True
                return

    #FUNCTION SubtractJumpsRemaining
    def SubtractJumpsRemaining(self):
        #keep track of how many jumps are available
        self.JumpsRemaining -= 1
        if(self.JumpsRemaining < 0):
            self.JumpsRemaining = 0

    #FUNCTION UpdateJumpState
    def UpdateJumpState(self):
        #reset the available jumps when player is back on ground
        if(self.OnGround):
            if(werewolfState):
                #add extra jump if currently a werewolf
                self.JumpsRemaining = self.maxNumberOfJumps + 1
                #make sure player is upright if on ground
                self.Owner.Transform.SetEulerAnglesXYZ(0,0,0)
                #debug
                #print("Player is currently on ground as Werewolf\n-setting maxNumberOfJumps to " + str(self.maxNumberOfJumps))
            #else player is in human form, use normal number of jumps
            else:
                #normal jumps
                self.JumpsRemaining = self.maxNumberOfJumps
                #make sure player is upright if on ground
                self.Owner.Transform.SetEulerAnglesXYZ(0,0,0)
                #debug
                #print("Player is currently on ground as Human\n-setting maxNumberOfJumps to " + str(self.maxNumberOfJumps + 1))

    #FUNCTION CanJump
    def CanJump(self):
        #is true if on the ground or jumps remain
        canJump = self.OnGround or (self.JumpsRemaining > 1)
        return canJump

    #FUNCTION ApplyJumping
    def ApplyJumping(self):
        #apply an instant force upward for jumping based on player input
        if(self.CanJump() and self.moveJump):
            #subtract from JumpsRemaining
            self.SubtractJumpsRemaining()
            #apply jumping based on werewolfState
            #if human, normal jump
            if(werewolfState == False):
                self.Owner.RigidBody.ApplyLinearImpulse(Vec3(0,1,0) * self.jumpSpeed)
            #else werewolf, jump x 1.5
            else:
                self.Owner.RigidBody.ApplyLinearImpulse(Vec3(0,1,0) * self.jumpSpeed * 1.5)

    #FUNCTION CanChangeState
    def CanChangeState(self):
        #reset variables to evaluate depending on current state
        global changeToWerewolf
        global changeToHuman
        global cooldownActive
        transformReady = False
        #debug
        print("DOING: CanChangeState\n-setting 'changeToWerewolf to FALSE\n-setting 'changeToHuman' to FALSE")
        #if player is currently a human
        if(werewolfState == False):
            changeToWerewolf = True
            changeToHuman = False
            transformReady = True
            cooldownNotActive = False
            #start FUNCTION CooldownTimer
            #self.CooldownTimer()
            #debug
            print("entered 'if self.werewolfState == False'\nchanging 'changeToWerewolf' to TRUE\n and changeToHuman to FALSE\nand transformReady to TRUE")
        #else player is a werewolf so change to human
        else:
            changeToWerewolf = False
            changeToHuman = True
            transformReady = True
            #debug
            print("entered 'if self.werewolfState == TRUE'\nchanging 'changeToHuman' to TRUE\n and 'changeToWerewolf' to FALSE\nand transformReady to TRUE")
        return transformReady

    #FUNCTION CooldownTimer
    #def CooldownTimer(self):
        #debug
        #print("DOING CooldownTimer")
        #variables to store time
        #global cooldownNotActive
        #cooldown = 50000
        #timer = 0
        #while(timer < cooldown):
            #cooldownNotActive = False
            #timer = timer + 1
            #debug
            #print("timer is currently at: " + str(timer) + "out of: " +str(cooldown))
            #if(timer == 50000):
                #cooldownNotActive = True
                #debug
                #print("timer is now equal to: " + str(timer) + "\n-setting cooldownNotActive to: " + str(cooldownNotActive))
            #else:
                #continue
        #return cooldownNotActive


    #FUNCTION UpdatePlayerState
    def UpdatePlayerState(self):
        #access global variables
        global changeToWerewolf
        global changeToHuman
        global werewolfState
        global moveSpeed
        if(self.changeState and self.CanChangeState()):
            #debug
            print("DOING: UpdatePlayerState\n entering if condition\nCURRENT STATE OF VARIABLES:\n changeToWerewolf = " + str(changeToWerewolf) + "\nchangeToHuman = " + str(changeToHuman))
            if(changeToWerewolf):
                self.Owner.Sprite.Color = Color.Red
                #change werewolfState to TRUE
                werewolfState = True
                #adjust size of player and move player up so they don't clip the ground
                self.Owner.Transform.Scale += Vec3(0,0.25,0)
                self.Owner.Transform.Translation += Vec3(0,.15,0)
                #change moveSpeed
                moveSpeed = Property.Float(10.0)
                #debug
                print("changing to werewolf form\n-setting werewolfState to " + str(werewolfState))
            elif(changeToHuman):
                self.Owner.Sprite.Color = Color.White
                #adjust size of player back to normal and move player up so they don't clip the ground
                self.Owner.Transform.Scale -= Vec3(0,0.25,0)
                self.Owner.Transform.Translation -= Vec3(0,0.15,0)
                #change werewolfState to FALSE
                werewolfState = False
                #debug
                print("changing back to human form\n-setting werewolfState to " + str(werewolfState))
            else:
                print("Something went horribly wrong")

Zero.RegisterComponent("beatrix_controller", beatrix_controller)