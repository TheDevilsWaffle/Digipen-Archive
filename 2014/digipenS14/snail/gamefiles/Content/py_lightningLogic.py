#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_lightningLogic
#AUTHOR:        Garrett Huxtable
#DESCRIPTION:   controls lightning projectiles
####################################################################################################
import Zero
import Events
import Property
import VectorMath
Vec3 = VectorMath.Vec3


class py_lightningLogic:
    def Initialize(self, initializer):
        # Set the initial timer
        self.timer = 0
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted);
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate);
        
    def OnLogicUpdate(self, UpdateEvent):
        # Timer
        self.timer += UpdateEvent.Dt
        if(self.timer >= 0.2):
            self.Owner.Destroy()

    def OnCollisionStarted(self, CollisionEvent):
        otherObject = CollisionEvent.OtherObject
        
        if(otherObject.py_objectSettings):
            if(otherObject.py_objectSettings.objectClass == "Enemy"):
                self.Space.py_globalVariables.enemyList.remove(otherObject.Transform)
                otherObject.Destroy()

Zero.RegisterComponent("py_lightningLogic", py_lightningLogic)