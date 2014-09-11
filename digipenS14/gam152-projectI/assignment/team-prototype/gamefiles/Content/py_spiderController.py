#####       This handles all the Basic enemies AI functions     #####
#####################################################################
import Zero
import Events
import Property
import VectorMath
import Color

Vec3 = VectorMath.Vec3

class py_spiderController:
    #a variable to control the speed at which it's pacing
    PaceSpeed = Property.Float(4.0)
    #The maximum distance the enemy will pace from its origin
    MaxMoveDistance = Property.Float(5.0)
    #The object the enemy will chase after
    targetObject = Property.Cog()
    #how close the object can get before the enemy will chase it
    ChaseTriggerDistance = Property.Float(7.0)
    #the speed at which the owner chases the object
    ChaseSpeed = Property.Float(4.0)
    
    def Initialize(self, initializer):
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        #the owner will pace on the X axis
        self.PaceDirection = Vec3(1,0,0)
        #records the location where owner is created
        self.StartPosition = self.Owner.Transform.Translation
        #initialize the distance from the targetobject
        self.DistanceFromTarget = 0.0
        #initialize the chase direction
        self.ChaseDirection = Vec3(0,0,0)
        #record the owners color
        self.OriginalColor = self.Owner.Sprite.Color
        
        self.bubbleTimer = 0
        
    def OnLogicUpdate(self, UpdateEvent):
        #initialize a boolean saying if the object is in range.
        targetIsWithinRange = False
        
        #if a target object is declared the object is not in a bubble
        if(self.targetObject and self.Owner.py_objectSettings.inBubble == False):
            #run a function
            self.CalculateChaseDirectionAndDistance()
            #set boolean to true if the target is within the specified trigger distance
            targetIsWithinRange = (self.DistanceFromTarget <= self.ChaseTriggerDistance)
            #if the target is being chased, run the proper function
            if(targetIsWithinRange):
                self.ChaseTarget(UpdateEvent)
            #if the target is not being chased, run the pace function
            else:
                self.BasicMovement(UpdateEvent)
                
        elif(self.Owner.py_objectSettings.inBubble == True):
            if(self.bubbleTimer == 0):
                self.Owner.Transform.RotateByAngles(Vec3(180,0,0))
            
            if(self.bubbleTimer < self.Owner.py_objectSettings.bubbleTimer):
                self.bubbleTimer += 1
            else:
                self.Owner.py_objectSettings.inBubble = False
                self.Owner.Transform.RotateByAngles(Vec3(-180,0,0))
                self.bubbleTimer = 0
        
        #if no target object is declared, run the pace function
        else:
            self.BasicMovement(UpdateEvent)
            
        #if the owners health is less than 0, destroy it.
        if(self.Owner.py_objectSettings.objectHealth <= 0.0):
            self.Owner.Destroy()
        
    def BasicMovement(self, UpdateEvent):
        #calculate how far the object has moved, is a vector
        displacement = self.Owner.Transform.Translation - self.StartPosition
        #normalize displacement into a number
        distanceFromStart = displacement.length()
        
        #if it's outside the user specified distance, move it in the opposite direction
        if(distanceFromStart >= self.MaxMoveDistance):
            self.PaceDirection = -displacement
        
        #normalize the pace direction
        self.PaceDirection.normalize()
        
        #make sure the color is the sprited default color
        self.Owner.Sprite.Color = self.OriginalColor
        
        #update the owners location with the direction and speed it's supposed to be moving in.
        self.Owner.Transform.Translation += self.PaceDirection * UpdateEvent.Dt * self.PaceSpeed
        
    def ChaseTarget(self, UpdateEvent):
        #move the owner in the direction of the object it is chasing.
        self.Owner.Transform.Translation += self.ChaseDirection * UpdateEvent.Dt * self.ChaseSpeed
        
    def CalculateChaseDirectionAndDistance(self):
        #calculate the direction on the X axis of the target object from the owner
        self.ChaseDirection.x = self.targetObject.Transform.Translation.x - self.Owner.Transform.Translation.x
        #calculate the direction on the X axis of the target object from the owner
        self.ChaseDirection.y = self.targetObject.Transform.Translation.y - self.Owner.Transform.Translation.y
        #calculate the distance to the target, and then normalize
        self.DistanceFromTarget = self.ChaseDirection.length()
        self.ChaseDirection.normalize()

Zero.RegisterComponent("py_spiderController", py_spiderController)