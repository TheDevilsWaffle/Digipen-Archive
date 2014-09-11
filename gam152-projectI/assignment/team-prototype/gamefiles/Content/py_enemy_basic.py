import Zero
import Events
import Property
import VectorMath
import random
#shorthand for VectorMath.Vec3
Vec3 = VectorMath.Vec3

class py_enemy_basic():
    #timer for the length the object will stay in a bubble
    bubbleTime = Property.Int(300)
    
    def Initialize(self, init):
        #set variable NextJump = 2
        self.NextJump = 2.0
        #set variable JumpTimer = 0
        self.JumpTimer = 0.0
        #set variable TimeInDirection = 0
        self.TimeInDirection = 0.0
        #set variable CurrentDirection = Vec3 (will be updated in OnUpdateCharacterInput
        self.CurrentDirection = Vec3(0.0, 0.0, 0.0)
        #set variable target = None (will eventually be player (or changed if need be)
        self.Target = None
        #set variable Random = random.Random()
        self.Random = random.Random()
        
        #listen to space every frame and perform OnLogicUpdate
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        #listen to owner and update CurrentDirection through OnUpdateCharacterInput
        Zero.Connect(self.Owner, "UpdateCharacterInput", self.OnUpdateCharacterInput)
        #Sets up collision to kill player
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
    
    ################################################################################################
    #FUNCTION - OnAllObjectsCreated
    #DESCRIPTION - responsible for setting the enemy to look out for Player
    ################################################################################################
    def OnAllObjectsCreated(self):
        #set target to Player
        self.Target = self.Space.FindObjectByName("Snail")

    ################################################################################################
    #FUNCTION - OnUpdateCharacterInput
    #PARAMTERS - _e: event
    #DESCRIPTION - responsible for setting and updating enemy's current direction every frame
    ################################################################################################
    def OnUpdateCharacterInput(self, _e):
        #set enemy's current direction based on move direction
        self.Owner.py_character_movement.MoveDirection = self.CurrentDirection

    ################################################################################################
    #FUNCTION - OnLogicUpdate
    #DESCRIPTION - responsible for determing enemy's movement and chasing
    ################################################################################################
    def OnLogicUpdate(self, updateEvent):
        #set JumpTimer based on game timer
        self.JumpTimer += updateEvent.Dt
        #set TimeInDirection based on game timer
        self.TimeInDirection += updateEvent.Dt

        #ENEMY RANDOM JUMPING
        #if enemy's JumpTimer has exceeded NextJump varaible (stars at 2 seconds)
        if(self.Owner.py_objectSettings.inBubble == False):
            if(self.JumpTimer > self.NextJump):
                #set NextJump to a random number between 0.5 and 3 seconds
                self.NextJump = self.Random.uniform(0.5, 3.0)
                #make enemy jump
                self.Owner.py_character_movement.BeginJump()
                #reset the JumpTimer
                self.JumpTimer = 0.0

            #ENEMY RANDOM PACING
            #if enemy's TimeInDirection exceeds 2 seconds
            if(self.TimeInDirection > 2.0):
                #change enemy's x direction randomly (-1 for left, 1 for right)
                self.CurrentDirection = Vec3(self.Random.uniform(-1.0, 1.0), 0.0, 0.0)
                #reset TimeInDirection
                self.TimeInDirection = 0.0

            #CHASING TARGET
            #if a target has been set for the enemy
            if(self.Target):
                #set local varible TargetPos = (currently player's) translation
                TargetPos = self.Target.Transform.Translation
                #set local varaible ToTarget = where the target is and where the enemy is
                ToTarget = TargetPos - self.Owner.Transform.Translation

                #if the target is within range (currently 2.5)
                if(ToTarget.length() < 2.5):
                    #chase!
                    ToTarget = Vec3(ToTarget.x, 0.0, 0.0)
                    #normalize
                    self.CurrentDirection = ToTarget.normalized()
        
        #if the enemy is inside a bubble, remove the direction it's moving in
        elif(self.Owner.py_objectSettings.inBubble == True):
            self.CurrentDirection = Vec3(0,0,0)
            if(self.Owner.py_objectSettings.iTimer <= self.bubbleTime):
                self.Owner.py_objectSettings.iTimer += 1
            else:
                self.Owner.py_objectSettings.inBubble = False
                self.Owner.py_objectSettings.iTimer = 0
                
    #Collision with player decrements health
    def OnCollisionStarted(self, CollisionEvent):
        # This block damages the player and destroys the pellet.
        otherObject = CollisionEvent.OtherObject
        if(otherObject.py_objectSettings):
            if(otherObject.py_objectSettings.objectClass == "Player"):
                self.Space.py_globalVariables.PlayerHealth -= self.Owner.py_objectSettings.damageToPlayer

Zero.RegisterComponent("py_enemy_basic", py_enemy_basic)