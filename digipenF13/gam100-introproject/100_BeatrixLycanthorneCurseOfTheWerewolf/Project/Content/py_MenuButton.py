
import Zero
import Events
import Property
import VectorMath
Vec4  = VectorMath.Vec4
class py_MenuButton:
    
    #Set the different colors and whether or not the button should be invisible
    DefaultColor = Property.Vector4(default = Vec4(1, 1, 1, 1))
    HoverColor = Property.Vector4(default = Vec4(1, 1, 1, 64))
    DownColor = Property.Vector4(default = Vec4(1, 1, 1, 128))
    UICommand = Property.String()
    Invisible = Property.Bool(default = True)
    
    def Initialize(self, initializer):
        #Set all mouseover states to default false
        self.MouseIsDown = False
        self.IsFocus = False
        self.IsHovered = False
        
        #Hook up events
        Zero.Connect(self.Owner, Events.MouseEnter, self.OnMouseEnter)
        Zero.Connect(self.Owner, Events.MouseExit, self.OnMouseExit)
        Zero.Connect(self.Owner, Events.MouseUp, self.OnMouseUp)
        Zero.Connect(self.Owner, Events.MouseDown, self.OnMouseDown)
        Zero.Connect(self.Space, Events.MouseDownSomewhere, self.OnMouseDownSomewhere)
        Zero.Connect(self.Space, Events.MouseUpSomewhere, self.OnMouseUpSomewhere)
        
        #Initialize state
        self.DefaultState()
    
    ############
    # Viewport Mouse Events
    ############
    
    def OnMouseDownSomewhere(self, unusedEvent):
        #If the mouse is down
        self.MouseIsDown = True
        
    def OnMouseUpSomewhere(self, unusedEvent):
        #If the mouse is up
        self.MouseIsDown = False
        
        #Make sure the mouse isn't hovering over the button
        if(self.IsHovered == False):
            self.IsFocus = False
    
    ############
    # Local Mouse Events
    ############
    
    def OnMouseEnter(self, MouseEvent):
        #The mouse is hovering over the button
        self.IsHovered = True
        
        #If the mouse is over the button and the mouse is clicked, go to downstate
        if(self.IsFocus == True):
            self.DownState()
        #If the mouse is not over the button and the mouse is clicked, go to hoverstate
        else:
            self.HoverState()
            if(self.MouseIsDown == False):
                self.IsFocus = True
    
    def OnMouseExit(self, MouseEvent):
        #Mouse is not hovering
        self.IsHovered = False
        #Return to default
        self.DefaultState()
        
        #If the mouse is not down, button is no longer the focus
        if(self.MouseIsDown == False):
            self.IsFocus = False
    
    def OnMouseUp(self, MouseEvent):
        #Activate hoverstate
        self.HoverState()
        
        #If the button is the focus, click
        if(self.IsFocus == True):
            self.MouseClick()
        #Button is the focus
        else:
            self.IsFocus = True
    
    def OnMouseDown(self, MouseEvent):
        #Activate downstate
        self.DownState()
        
    ############
    # Behavior
    ############
    #The default state of the button
    def DefaultState(self):
        if(self.Invisible == True):
            self.Owner.Sprite.Visible = False
        if(self.Invisible == False):
            self.Owner.Sprite.Visible = True
        self.Owner.Sprite.Color = self.DefaultColor
    #How the button looks when being hovered
    def HoverState(self):
        self.Space.SoundSpace.PlayCue("ButtonHover")
        self.Owner.Sprite.Color = self.HoverColor
        self.Owner.Sprite.Visible = True

    #How the button looks when being clicked
    def DownState(self):
        self.Owner.Sprite.Color = self.DownColor
    
    #Perform the command and play the click sound
    def MouseClick(self):
        self.Space.UIManager.Do(self.UICommand)
        self.Space.SoundSpace.PlayCue("ButtonClick")

Zero.RegisterComponent("py_MenuButton", py_MenuButton)