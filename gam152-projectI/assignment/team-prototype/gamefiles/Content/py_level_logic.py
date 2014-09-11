import Zero
import Events
import Property
import VectorMath

class py_level_logic:
    def Initialize(self, initializer):
        #We want to check for input every logic update
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        
    def OnLogicUpdate(self, UpdateEvent):
            
        #Switch levels when the space key is pressed
        if(Zero.Keyboard.KeyIsPressed(Zero.Keys.Enter)):
            self.Space.LoadLevel("lvl_world1")
        if(Zero.Keyboard.KeyIsPressed(Zero.Keys.One)):
            self.Space.LoadLevel("lvl_world1")
        if(Zero.Keyboard.KeyIsPressed(Zero.Keys.Two)):
            self.Space.LoadLevel("lvl_world2")
        if(Zero.Keyboard.KeyIsPressed(Zero.Keys.Back)):
            self.Space.LoadLevel("lvl_StartScreen")
        

Zero.RegisterComponent("py_level_logic", py_level_logic)