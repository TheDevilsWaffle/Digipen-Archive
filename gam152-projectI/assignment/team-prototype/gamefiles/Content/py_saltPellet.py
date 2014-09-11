#####       This script is attached to the salt pellets that enemies will use to damage the player      #####
#############################################################################################################
import Zero
import Events
import Property
import VectorMath

class py_saltPellet:
    def Initialize(self, initializer):
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
        
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
                self.Space.py_globalVariables.PlayerHealth -= self.Owner.py_objectSettings.damageToPlayer
                self.Owner.Destroy()

Zero.RegisterComponent("py_saltPellet", py_saltPellet)