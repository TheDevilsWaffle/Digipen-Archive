#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
#################################################################################
#FILENAME:      py_bubbleLogic
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This is for the primary goo that the snail will use to trap
#               enemies for eating.
#################################################################################
import Zero
import Events
import Property
import VectorMath

Vec3 = VectorMath.Vec3

class py_bubbleLogic:
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, Events.CollisionStarted, self.CaptureEnemy)
        
    def CaptureEnemy(self, CollisionEvent):
        #this determines if the bubble has hit an enemy class, and turns it's "inBubble" variable to true
        otherObject = CollisionEvent.OtherObject
        
        #If the other object has ObjectSettings, and is an enemy, tell it that it's in a bubble
        if(otherObject.py_objectSettings):
            if(otherObject.py_objectSettings.objectClass == "Enemy"):
                if(otherObject.py_objectSettings.inBubble == False):
                    otherObject.py_objectSettings.inBubble = True
                    otherObject.Sprite.FlipY = True
                    gooCog = self.Space.Create("arc_gooed")
                    #adjust where on the enemy sprite appears
                    gooCog.Transform.Translation = Vec3(0,0.3,20)
                    #adjust size of goo'd sprite
                    gooCog.Transform.Scale = Vec3(0.75, 0.75, 0)
                    gooCog.AttachTo(otherObject)
        

Zero.RegisterComponent("py_bubbleLogic", py_bubbleLogic)