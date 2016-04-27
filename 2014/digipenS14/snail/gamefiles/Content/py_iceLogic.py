#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_iceLogic
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This script controls all functions of the ice projectile
####################################################################################################
import Zero
import Events
import Property
import VectorMath


class py_iceLogic:
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted);

    def OnCollisionStarted(self, CollisionEvent):
        otherObject = CollisionEvent.OtherObject
        
        if(otherObject.py_objectSettings):
            if(otherObject.py_objectSettings.objectClass == "Enemy"):
                self.Space.CreateAtPosition("arc_iceBlock", otherObject.Transform.Translation)
                #remove it from the global variables list
                self.Space.py_globalVariables.enemyList.remove(otherObject.Transform)
                otherObject.Destroy()

Zero.RegisterComponent("py_iceLogic", py_iceLogic)