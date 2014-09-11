import Zero
import Events
import Property
import VectorMath
import Keys
import Action
import math
#shorthand for VectorMath.Vec3
Vec3 = VectorMath.Vec3

class py_player_input():
    #boolean varaible to check for input type (set to true)
    InputType = Property.Bool(default = True)
    #?????
    GamepadIndex = Property.Int(default = 0)
    
    def Initialize(self, init):
        #Attempt to get the gamepad at the given index
        self.Gamepad = Zero.Gamepads.GetGamePad(self.GamepadIndex)
        
        #Listen for when the character wants input to be updated (to reduce lag)
        Zero.Connect(self.Owner, "UpdateCharacterInput", self.OnUpdateCharacterInput)
        
        #Listen for keyboard buttons up and down events
        if(self.InputType == True):
            #when keyboard key is down -> OnKeyDown()
            Zero.Connect(Zero.Keyboard, Events.KeyDown, self.OnKeyDown)
            #when keyboard key is up -> OnKeyUp()
            Zero.Connect(Zero.Keyboard, Events.KeyUp, self.OnKeyUp)
            
        #if keyboard is inactive and using the gamepad
        elif(self.InputType == False and self.Gamepad):
            #when gamepad button is pressed -> OnButtonDown()
            Zero.Connect(self.Gamepad, Events.ButtonDown, self.OnButtonDown)
            #when gamepad button is depressed -> OnButtonUp()
            Zero.Connect(self.Gamepad, Events.ButtonUp, self.OnButtonUp)
    
    def OnUpdateCharacterInput(self, e):
        #Get our movement direction (zero out the y axis so we can't fly)
        movement = self.GetMovementDirection()
        movement.y = 0.0
        
        #set the movement direction on the player
        self.Owner.py_character_movement.MoveDirection = movement
    
    def GetMovementDirection(self):
        #get our movement direction depending on whether
        #we use keyboard or gamepad input
        if(self.InputType):
            return self.GetKeyboardMovement()
        elif(self.Gamepad):
            return self.GetGamepadMovement()
        
        return Vec3(0.0, 0.0, 0.0)
    
    def OnKeyDown(self, keyboardEvent):
        #if player is pressing w to jump
        if(keyboardEvent.Key == Keys.W):
            self.Owner.py_character_movement.BeginJump()
        #if player is pressing space to shoot
        if(keyboardEvent.Key == Keys.Space):
            self.Owner.py_shoot.Shoot()
    
    def OnKeyUp(self, keyboardEvent):
        if(keyboardEvent.Key == Keys.W):
            self.Owner.py_character_movement.EndJump()
    
    def OnButtonDown(self, gamepadEvent):
        if(gamepadEvent.Button == Buttons.A):
            self.Owner.py_character_movement.BeginJump()
    
    def OnButtonUp(self, gamepadEvent):
        if(gamepadEvent.Button == Buttons.A):
            self.Owner.py_character_movement.EndJump()
    
    def GetKeyboardMovement(self):
        #set dir to VectorMath.Vec3 to zeros to add onto
        dir = Vec3(0.0, 0.0, 0.0)
        
        #if the player is pressing "A" (going left)
        if(Zero.Keyboard.KeyIsDown(Keys.A)):
            #move left (add to dir by -x)
            dir += Vec3(-1.0, 0.0, 0.0)
            #set sprite to False so it flips left (faces the right direction)
            self.Owner.py_character_movement.CharacterFlipRight = False
        
        #if the player is pressing "D" (going right)
        if(Zero.Keyboard.KeyIsDown(Keys.D)):
            #move right (add to dir by x)
            dir += Vec3(1.0, 0.0, 0.0)
            #set sprite to True so it flips right (faces the left direction)
            self.Owner.py_character_movement.CharacterFlipRight = True
        
        #if the player is pressing "W" (moving up/jumping)
        if(Zero.Keyboard.KeyIsDown(Keys.W)):
            #move upward (add to dir by y)
            dir += Vec3(0.0, 1.0, 0.0)
        
        #if the player is pressing "S" (moving down)
        if(Zero.Keyboard.KeyIsDown(Keys.S)):
            #move downward (add to dir by -y)
            dir += Vec3(0.0, -1.0, 0.0)
        
        #return the newly updated dir
        return dir
    
    
    def GetGamepadMovement(self):
        return Vec3(self.Gamepad.LeftStick, 0.0)
    
Zero.RegisterComponent("py_player_input", py_player_input)