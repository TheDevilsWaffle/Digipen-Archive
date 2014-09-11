import Zero
import Events
import Property
import VectorMath


class py_barrels:
    
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, Zero.Events.CollisionStarted, self.SetCollidedObject)
        
        self.player = self.Space.FindObjectByName("BeatrixLycanthorne")
        if (self.player == None):
            #Make sure we can always find the player
            print("[py_beatrix_controller] Initialize:  Failed to find player.")

    def SetCollidedObject(self, OnCollisionEvent):
        print("Set the barrel on player.")
        targetObject = OnCollisionEvent.OtherObject
        #checking if the player is next to the barrel
        if (targetObject == self.player):
            self.player.py_beatrix_controller.Barrel = self.Owner;

Zero.RegisterComponent("py_barrels", py_barrels)