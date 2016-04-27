#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_regionWrapping
#AUTHOR:        Garrett Huxtable
#DESCRIPTION:   region wrapping so enemies/snail can use warp zones. Differs from the other by
#               being attached to a single object.
####################################################################################################
import Zero
import Events
import Property
import VectorMath
Vec3 = VectorMath.Vec3


class py_regionWrapping:
    #Object to follow
    targetObject = Property.Cog()
    #Which way the object exits the region
    exitLeft = Property.Bool()
    exitRight = Property.Bool()
    exitTop = Property.Bool()
    exitBottom = Property.Bool()
    
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
            
    ##################################################################

    #   OnCollisionStarted checks to see if an object has hit wrapping
    #   region and sends it to the connected region.

    ##################################################################
    def OnCollisionStarted(self, collisionEvent):
        otherObject = collisionEvent.OtherObject
        
        if(self.exitLeft == True):
            otherObject.Transform.Translation = self.targetObject.Transform.Translation - Vec3(3,0,0)
        
        if(self.exitRight == True):
            otherObject.Transform.Translation = self.targetObject.Transform.Translation + Vec3(3,0,0)
        
        if(self.exitBottom == True):
            otherObject.Transform.Translation = self.targetObject.Transform.Translation - Vec3(0,3,0)
        
        if(self.exitTop == True):
            otherObject.Transform.Translation = self.targetObject.Transform.Translation + Vec3(0,3,0)

Zero.RegisterComponent("py_regionWrapping", py_regionWrapping)