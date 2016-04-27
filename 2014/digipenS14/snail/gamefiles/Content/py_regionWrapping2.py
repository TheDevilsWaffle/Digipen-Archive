#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_regionWrapping2
#AUTHOR:        Garrett Huxtable
#DESCRIPTION:   region wrapping so enemies/snail can use warp zones. Differs from the other by
#               being attached to a parent and child archetype.
####################################################################################################
import Zero
import Events
import Property
import VectorMath
Vec3 = VectorMath.Vec3


class py_regionWrapping2:
    #Object to follow
    targetObject = Property.Cog()
    
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
            
    def OnCollisionStarted(self, collisionEvent):
        otherObject = collisionEvent.OtherObject
        #move the object entering the region to the location of the regions child.
        otherObject.Transform.Translation = self.targetObject.Transform.WorldTranslation

Zero.RegisterComponent("py_regionWrapping2", py_regionWrapping2)