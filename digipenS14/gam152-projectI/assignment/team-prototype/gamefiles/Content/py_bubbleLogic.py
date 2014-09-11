import Zero
import Events
import Property
import VectorMath

class py_bubbleLogic:
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, Events.CollisionStarted, self.CaptureEnemy)
        
    def CaptureEnemy(self, CollisionEvent):
        #this determines if the bubble has hit an enemy class, and turns it's "inBubble" variable to true
        otherObject = CollisionEvent.OtherObject
        
        if(otherObject.py_objectSettings):
            if(otherObject.py_objectSettings.objectClass == "Enemy"):
                otherObject.py_objectSettings.inBubble = True
        

Zero.RegisterComponent("py_bubbleLogic", py_bubbleLogic)