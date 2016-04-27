#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_saltPellet
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This script is attached to the salt pellets that enemies will use to damage the player
####################################################################################################
import Zero
import Events
import Property
import VectorMath


class py_saltPellet:
    def Initialize(self, initializer):
        
        # Added by Chris for use in Turret
        self.Owner.BoxCollider.Ghost = True;
        
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
        
        # Added by Chris for use in Turret
        Zero.Connect(self.Owner, Events.CollisionEnded, self.OnCollisionEnded);
        
    # Added by Chris for use in Turret
    def OnCollisionEnded(self, CollisionEvent):
        self.Owner.BoxCollider.Ghost = False;
        
    def OnLogicUpdate(self, UpdateEvent):
        # This block of code uses the timer to countdown until when the pellet will be deleted
        if(self.Owner.py_objectSettings.iTimer == 0):
            self.Owner.Destroy()
        else:
            self.Owner.py_objectSettings.iTimer -= 1
            
    def OnCollisionStarted(self, CollisionEvent):
        # This block damages the player and destroys the pellet.
        otherObject = CollisionEvent.OtherObject
        if(otherObject.py_objectSettings):
            if(otherObject.py_objectSettings.objectClass == "Player"):
                if(otherObject.py_objectSettings.canBeKilled == True):
                    self.Space.py_globalVariables.PlayerHealth -= self.Owner.py_objectSettings.damageToPlayer
                self.Owner.Destroy()

Zero.RegisterComponent("py_saltPellet", py_saltPellet)