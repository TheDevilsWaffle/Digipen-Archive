import Zero
import Events
import Property
import VectorMath
import math

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
    ChaseSpeed = Property.Float(4.0)
    
    bubbleTime = Property.Int(300)
    
    def Initialize(self, initializer):
        #initialize the distance from the targetobject
        self.DistanceFromTarget = 0.0
        #initialize the chase direction
        self.PlayerDirection = Vec3(0,0,0)
        self.dropTimer = 0
        self.switchDirection = False
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        Zero.Connect(self.Owner, Events.CollisionStarted, self.HitObject)
        
    def OnLogicUpdate(self, UpdateEvent):
        #initialize a boolean saying if the object is in range.
        targetIsWithinRange = False
        #if a target object is declared the object is not in a bubble
        if(self.targetObject and self.Owner.py_objectSettings.inBubble == False):
            if(self.dropTimer % self.dropLimiter == 0):
                #run a function
                self.PlayerDistance()
            #set boolean to true if the target is within the specified trigger distance
            targetIsWithinRange = (self.DistanceFromTarget <= self.ChaseTriggerDistance)
            #if the target is being chased, run the proper function
            if(targetIsWithinRange):
                self.isAwake = True
            if(self.isAwake == True):
                self.RunCrazy(UpdateEvent)
        if(self.Owner.py_objectSettings.inBubble == True):
            if(self.Owner.py_objectSettings.iTimer >= self.bubbleTime):
                print("HEY")
                self.Owner.py_objectSettings.inBubble = False
                self.Owner.py_objectSettings.iTimer = 0
            self.Owner.py_objectSettings.iTimer += 1
        
    def PlayerDistance(self):
        #calculate the direction on the X axis of the target object from the owner
        self.PlayerDirection.x = self.targetObject.Transform.Translation.x - self.Owner.Transform.Translation.x
        #calculate the distance to the target, and then normalize
        self.DistanceFromTarget = self.PlayerDirection.length()
        self.PlayerDirection.normalize()
        
    def RunCrazy(self, UpdateEvent):
        #store the objects current position
        currentPosition = self.Owner.Transform.Translation
        #if the timer determines it's ok to drop a pellet, do so
        if(self.dropTimer % self.dropLimiter == 0):
            projectileCog = self.Space.CreateAtPosition("arc_saltPellet", currentPosition)
        self.dropTimer += 1
        #Initially, the object will run away from the player, until it hits a wall
        if(self.switchDirection == False):
            self.Owner.Transform.Translation -= self.PlayerDirection * UpdateEvent.Dt * self.ChaseSpeed
        #after it hits a wall, it is free to chase after the player.
        if(self.switchDirection == True):
            self.Owner.Transform.Translation += self.PlayerDirection * UpdateEvent.Dt * self.ChaseSpeed
        
    def HitObject(self, CollisionEvent):
        #shortcut for other object
        otherObject = CollisionEvent.OtherObject
        #store the location where the collision event started
        PointOfContact = CollisionEvent.FirstPoint
        #determine the location on the object where the two objects collided
        BodyLocation = PointOfContact.BodyPoint
        #if the object on the sides, then tell the object to turn around.
        if(BodyLocation.y > 0.0):
            self.switchDirection = True
        
Zero.RegisterComponent("py_saltDropper", py_saltDropper)