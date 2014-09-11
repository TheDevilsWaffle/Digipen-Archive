import Zero
import Events
import Property
import VectorMath

Events.MouseDownSomewhere = "MouseDownSomewhere"
Events.MouseUpSomewhere = "MouseUpSomewhere"

class py_MenuButtonLogic:
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, Events.LevelStarted, self.OnLevelStarted)
        
    #######
    # Events
    #######

    def OnLevelStarted(self, GameEvent):
        #Hook up commands
        Zero.Connect(self.Owner, Events.MouseDown, self.OnMouseDown)
        Zero.Connect(self.Owner, Events.MouseUp, self.OnMouseUp)

    def OnMouseDown(self, MouseEvent):
        #Send the event that the mouse is down
        self.Owner.DispatchEvent(Events.MouseDownSomewhere, Zero.ScriptEvent())

    def OnMouseUp(self, MouseEvent):
        #Send the event that the mouse is up
        self.Owner.DispatchEvent(Events.MouseUpSomewhere, Zero.ScriptEvent())
Zero.RegisterComponent("py_MenuButtonLogic", py_MenuButtonLogic)