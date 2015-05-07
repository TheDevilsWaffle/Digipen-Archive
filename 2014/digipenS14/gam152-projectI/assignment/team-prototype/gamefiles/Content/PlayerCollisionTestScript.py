import Zero
import Events
import Property
import VectorMath

class PlayerCollisionTestScript:
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, "Blah", self.OnBlah);

    def OnBlah(self, Event):
        
        pass

Zero.RegisterComponent("PlayerCollisionTestScript", PlayerCollisionTestScript)