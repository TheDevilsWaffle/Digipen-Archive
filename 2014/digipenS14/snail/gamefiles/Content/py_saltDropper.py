#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_saltDropper
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This is the controller for the Salt Shaker enemy type
#               It will wait for the player to get close, and then run around dropping salt pellets
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import math
import random
Vec3 = VectorMath.Vec3


class py_saltDropper:
    #This bool will wake up the enemy at the appropriate time.
    isAwake = Property.Bool(False)
    #a cog that will be set to the player object
    targetObject = Property.Cog()
    #how close the object can get before the enemy will chase it
    ChaseTriggerDistance = Property.Float(7.0)
    #A timer that will determine how quickly to drop salt pellets.
    dropLimiter = Property.Float(40)
    #the speed at which the owner chases the object
    moveSpeed = Property.Float(7.0)
    #this variable stores the amount of time that the enemy will stay in a bubble
    bubbleTime = Property.Int(300)
    
    def Initialize(self, initializer):
        #initialize the distance from the targetobject
        self.DistanceFromTarget = 0.0
        #initialize the chase direction
        self.MoveDirection = Vec3(0,0,0)
        #initialize a timer that will count untill it's able to drop salt
        self.dropTimer = 0
        #initialize a timer that will count untill it's able to change direction
        self.directionTimer = 0
        #ensure that the saltDropper is following the snail
        self.targetObject = self.Space.FindObjectByName("Snail")
        #connect a script that will run every logic update
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        
    def OnLogicUpdate(self, UpdateEvent):
        #initialize a boolean saying if the object is in range.
        targetIsWithinRange = False
        #calculate displacement between the saldDropper and the target object
        displacement  = self.targetObject.Transform.Translation - self.Owner.Transform.Translation
        #set the distance to an integer.
        DistanceFromTarget = displacement.length()
        #if a target object is declared the object is not in a bubble
        if(self.targetObject and self.Owner.py_objectSettings.inBubble == False):
            #set boolean to true if the target is within the specified trigger distance
            targetIsWithinRange = (DistanceFromTarget <= self.ChaseTriggerDistance)
            #if the targetObject is within range, wake up the owner
            if(targetIsWithinRange):
                self.isAwake = True
            #if the owner is awake, have it run randomly
            if(self.isAwake == True):
                self.RunCrazy(UpdateEvent)
        #if the object is in a bubble
        if(self.Owner.py_objectSettings.inBubble == True):
            #remove all movement from the owner
            self.Owner.DynamicMotor.MoveInDirection(Vec3(0,0,0), Vec3(0,0,0))
            #if the timer has run out for the bubble, tell the owner it's not in it anymore and re-initialize the timer
            if(self.Owner.py_objectSettings.iTimer >= self.bubbleTime):
                self.Owner.py_objectSettings.inBubble = False
                self.Owner.py_objectSettings.iTimer = 0
                self.Owner.Sprite.FlipY = False
                self.Owner.FindChildByName("Gooed").Destroy()
            #add 1 to the timer every logic update that it's in a bubble.
            self.Owner.py_objectSettings.iTimer += 1
        
    def PlayerDistance(self):
        #calculate the direction on the X axis of the target object from the owner
        self.DistanceFromTarget  = self.targetObject.Transform.Translation - self.Owner.Transform.Translation
        
    def RunCrazy(self, UpdateEvent):
        #store the objects current position
        currentPosition = self.Owner.Transform.Translation
        #if the timer determines it's ok to drop a pellet, do so, in the proper direction
        if(self.dropTimer % self.dropLimiter == 0):
            if(self.MoveDirection == Vec3(1, 0, 0)):
                projectileCog = self.Space.CreateAtPosition("arc_saltPellet", currentPosition + Vec3(-0.5,0.25,0))
            elif(self.MoveDirection == Vec3(-1, 0, 0)):
                projectileCog = self.Space.CreateAtPosition("arc_saltPellet", currentPosition + Vec3(0.5,0.25,0))
        #add one to the drop timer if it's running around
        self.dropTimer += 1
        #create a variable that stores either a 1 or 2 randomly
        directionInt = random.randint(1, 2)
        
        #if it's time to change directions
        if(self.directionTimer % 100 == 0):
            #if the random integer is a 1, run in the + direction
            if(directionInt == 1):
                self.MoveDirection = Vec3(1, 0, 0)
                #flip saltshaker sprite according to direction it is facing
                self.Owner.Sprite.FlipX = False
                
            #if the random integer is a 2, run in the - direction.
            elif(directionInt == 2):
                self.MoveDirection = Vec3(-1, 0, 0)
                #flip saltshaker sprite according to direction it is facing
                self.Owner.Sprite.FlipX = True
                
            #add 1 to the timer
            self.directionTimer += 1
        #otherwise, just add 1 to the timer
        else:
            self.directionTimer += 1
            
        #move the owner according to the moveDirection and moveSpeed.
        self.Owner.DynamicMotor.MoveInDirection(self.MoveDirection * self.moveSpeed, Vec3(0,0,0))
        
Zero.RegisterComponent("py_saltDropper", py_saltDropper)