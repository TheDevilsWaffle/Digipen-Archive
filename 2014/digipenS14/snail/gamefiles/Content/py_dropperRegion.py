#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_dropperRegion
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   Attached to a region that will drop S-Cargo when the player enters it.
####################################################################################################


import Zero
import Events
import Property
import VectorMath


class py_dropperRegion:
    #the object that this script will drop
    dropObject = Property.Cog()
    
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted);

    def OnCollisionStarted(self, CollisionEvent):
        #shortcut
        otherObject = CollisionEvent.OtherObject
        
        #if the other object is not a wall
        if(otherObject.py_objectSettings):
            #if the other object is a player object
            if(otherObject.py_objectSettings.objectClass == "Player"):
                #make the drop object no longer static
                self.dropObject.RigidBody.Static = False
                #destroy the region (for cleaning)
                self.Owner.Destroy()

Zero.RegisterComponent("py_dropperRegion", py_dropperRegion)