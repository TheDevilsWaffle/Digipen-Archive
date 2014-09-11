import Zero
import Events
import Property
import VectorMath

class py_Cauldron:
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, Zero.Events.CollisionStarted, self.SetCollidedObject)
        
        self.player = self.Space.FindObjectByName("BeatrixLycanthorne")
        if (self.player == None):
            #Make sure we can always find the player
            print("[py_beatrix_controller] Initialize:  Failed to find player.")

    def SetCollidedObject(self, OnCollisionEvent):
        #debug
        print("Set the cauldron on player.")
        targetObject = OnCollisionEvent.OtherObject
        #checking if the player is next to the barrel
        if (targetObject == self.player):
            self.player.py_beatrix_controller.Cauldron = self.Owner;

Zero.RegisterComponent("py_Cauldron", py_Cauldron)