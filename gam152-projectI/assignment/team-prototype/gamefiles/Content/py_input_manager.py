#will this be tracked?

import Zero
import Events
import Property
import VectorMath
import Keys
import Action
import math
import Keys
#shorthand for VectorMath.Vec3
Vec3 = VectorMath.Vec3


class py_input_manager():
    #set boolean InputType = to check for input type (set to true)
    InputType = Property.Bool(default = True)
    #set 
    GamepadIndex = Property.Int(default = 0)
    
    def Initialize(self, init):
        #attempt to get the gamepad at the given index
        self.Gamepad = Zero.Gamepads.GetGamePad(self.GamepadIndex)
        
        #listen for when the character wants input to be updated (to reduce lag)
        Zero.Connect(self.Owner, "UpdateCharacterInput", self.OnUpdateCharacterInput)
        
        #listen for keyboard buttons up and down events
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

    ################################################################################################
    #FUNCTION - OnUpdateCharacterInput
    #PARAMETERS - _e: used to update ever frame
    #DESCRIPTION - responsible for setting movement to GetMoveDirection() and py_character_movememnt
    ################################################################################################
    def OnUpdateCharacterInput(self, _e):
        #Get our movement direction (zero out the y axis so we can't fly)
        movement = self.GetMovementDirection()
        movement.y = 0.0
        
        #set the movement direction on the player
        self.Owner.py_character_movement.MoveDirection = movement

    ################################################################################################
    #FUNCTION - GetMovementDirection
    #DESCRIPTION - responsible for determing if keyboard or gamepad is used, returns empty Vec3
    ################################################################################################
    def GetMovementDirection(self):
        #get our movement direction depending on keyboard or gamepad
        #keyboard
        if(self.InputType):
            return self.GetKeyboardMovement()
        
        #gamepad
        elif(self.Gamepad):
            return self.GetGamepadMovement()
        return Vec3(0.0, 0.0, 0.0)

    ################################################################################################
    #FUNCTION - OnKeyUp
    #PARAMETERS - _keyboardEvent: used to keep track of keyboard presses every frame
    #DESCRIPTION - responsible for performing the correct functions when keys are down
    ################################################################################################
    def OnKeyDown(self, _keyboardEvent):
        #if player is pressing w to jump
        if(_keyboardEvent.Key == Keys.W):
            self.Owner.py_character_movement.BeginJump()
        #if player is pressing space to shoot
        if(_keyboardEvent.Key == Keys.Space):
            self.Owner.py_shoot.Shoot()

    ################################################################################################
    #FUNCTION - OnKeyUp
    #PARAMETERS - _keyboardEvent: used to keep track of keyboard presses every frame
    #DESCRIPTION - responsible for performing EndJump when key is no longer down
    ################################################################################################
    def OnKeyUp(self, _keyboardEvent):
        #if player has depressed W
        if(_keyboardEvent.Key == Keys.W):
            self.Owner.py_character_movement.EndJump()

    ################################################################################################
    #FUNCTION - OnButtonDown
    #PARAMETERS - _gamepadEvent: used to keep track of gamepad button presses every frame
    #DESCRIPTION - responsible for performing BeginJump when gamepad button is down
    ################################################################################################
    def OnButtonDown(self, _gamepadEvent):
        #if player is pressing A to jump
        if(_gamepadEvent.Button == Buttons.A):
            self.Owner.py_character_movement.BeginJump()
        
        #if player is pressing X to shoot
        if(_gamepadEvent.Button == Buttons.X):
            self.Owner.py_shoot.Shoot()

    ################################################################################################
    #FUNCTION - OnButtonUp
    #PARAMETERS - _gamepadEvent: used to keep track of gamepad button presses every frame
    #DESCRIPTION - responsible for performing EndJump when gamepad button is no longer down
    ################################################################################################
    def OnButtonUp(self, _gamepadEvent):
        #if player has depressed A
        if(_gamepadEvent.Button == Buttons.A):
            self.Owner.py_character_movement.EndJump()

    ################################################################################################
    #FUNCTION - GetKeyboardMovement
    #DESCRIPTION - responsible for performing movement based on which key is pressed
    ################################################################################################
    def GetKeyboardMovement(self):
        #set localvariable Dir to VectorMath.Vec3 (currently all zeros)
        Dir = Vec3(0.0, 0.0, 0.0)

        #if the player is pressing "A" (going left)
        if(Zero.Keyboard.KeyIsDown(Keys.A)):
            #move left (add to Dir by -x)
            Dir += Vec3(-1.0, 0.0, 0.0)
            #set sprite to False so it flips left (faces the right direction)
            self.Owner.py_character_movement.CharacterFlipRight = False
        #if the player is pressing "D" (going right)
        if(Zero.Keyboard.KeyIsDown(Keys.D)):
            #move right (add to Dir by x)
            Dir += Vec3(1.0, 0.0, 0.0)
            #set sprite to True so it flips right (faces the left direction)
            self.Owner.py_character_movement.CharacterFlipRight = True
        #if the player is pressing "W" (moving up/jumping)
        if(Zero.Keyboard.KeyIsDown(Keys.W)):
            #move upward (add to Dir by y)
            Dir += Vec3(0.0, 1.0, 0.0)
        #if the player is pressing "S" (moving down)
        if(Zero.Keyboard.KeyIsDown(Keys.S)):
            #move downward (add to Dir by -y)
            Dir += Vec3(0.0, -1.0, 0.0)

        #return the newly updated Dir
        return Dir

    ################################################################################################
    #FUNCTION - UpdateCurrentState
    #DESCRIPTION - responsible for gamepad movement
    ################################################################################################
    def GetGamepadMovement(self):
        #movement based off of gamepad's LeftStick
        return Vec3(self.Gamepad.LeftStick, 0.0)

Zero.RegisterComponent("py_input_manager", py_input_manager)