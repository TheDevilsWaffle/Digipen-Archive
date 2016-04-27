####################################################################################################
#COPYRIGHT:     All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
#FILENAME:      py_input_manager
#AUTHOR:        Travis Moore
#DESCRIPTION:   This script listens for player input from keyboards/gamepads and performs actions
#               based on which key is pressed/depressed
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import Keys
import Action
import math
import Keys
import DebugDraw
#shorthand for VectorMath.Vec3
Vec3 = VectorMath.Vec3


class py_input_manager():
    #set boolean InputType = to check for input type (set to true)
    InputType = Property.Bool(default = True)
    #set 
    GamepadIndex = Property.Int(default = 0)
    
    def Initialize(self, init):
        #variable to keep is burping active
        self.IsBurping = False
        
        #timer for shot clock
        self.timer = 0
        
        #attempt to get the gamepad at the given index
        self.Gamepad = Zero.Gamepads.GetGamePad(self.GamepadIndex)
        cameraCog = self.Space.FindObjectByName("Camera")
        
        #listen for when the character wants input to be updated (to reduce lag)
        Zero.Connect(self.Owner, "UpdateCharacterInput", self.OnUpdateCharacterInput)
        
        #update every frame
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnUpdateShotClock)
        
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
        #DEBUG
        #print(math.degrees(self.Owner.Orientation.AbsoluteAngle))
        #DEBUG
        #print(self.Owner.Orientation.WorldForward)
        
        #set movement equal to button pressed
        movement = self.GetMovementDirection()
        
        #set the movement direction on the player
        self.Owner.py_platformer_mechanics.MoveDirection = movement
        
    ################################################################################################
    #Scripter Garrett
    #FUNCTION - OnUpdateShotClock
    #PARAMETERS - UpdateEvent: used to update ever frame
    #DESCRIPTION - responsible for keeping track of shot clock
    ################################################################################################
    def OnUpdateShotClock(self, UpdateEvent):
        self.timer += UpdateEvent.Dt

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
    #FUNCTION - OnKeyDown
    #PARAMETERS - _keyboardEvent: used to keep track of keyboard presses every frame
    #DESCRIPTION - responsible for performing the correct functions when keys are down
    ################################################################################################
    def OnKeyDown(self, _keyboardEvent):
        #if the game is not paused
        if(self.Space.FindObjectByName("LevelSettings").py_HUDCreator.bPaused == False):
            #if player is pressing Control to jump
            if(_keyboardEvent.Key == Keys.Control or _keyboardEvent.Key == Keys.Up or _keyboardEvent.Key == Keys.W):
                #perfom the jump
                self.Owner.py_platformer_mechanics.BeginJump()
                
                #goto py_snail_animation pyscript to check for correct sprite to use
                self.Owner.py_snail_animations.SnailJump()
            
            #Scripter Garrett
            #Timer based lightning shots.
            if(self.Space.py_globalVariables.playerUpgrade == "Lightning"):
                if(self.timer >= 1):
                    #if player is pressing space to shoot
                    if(_keyboardEvent.Key == Keys.Space):
                        #player burp sound
                        self.Owner.SoundEmitter.Play()
                        
                        #perform shoot function
                        self.Owner.py_shoot.Shoot()
                        
                        #go to py_snail_animation pyscript to check for correct sprite to use
                        self.Owner.py_snail_animations.SnailBurp()
                        
                        #update IsBurping to true
                        self.IsBurping = True
                        
                        #reset timer
                        self.timer = 0
            ##### End Garrett Code #####
                        
            #if player is pressing space to shoot
            else:
                if(_keyboardEvent.Key == Keys.Space):
                    #player burp sound
                    self.Owner.SoundEmitter.Play()
                    
                    #perform shoot function
                    self.Owner.py_shoot.Shoot()
                    
                    #go to py_snail_animation pyscript to check for correct sprite to use
                    self.Owner.py_snail_animations.SnailBurp()
                    
                    #update IsBurping to true
                    self.IsBurping = True
    
    ################################################################################################
    #FUNCTION - OnKeyUp
    #PARAMETERS - _keyboardEvent: used to keep track of keyboard presses every frame
    #DESCRIPTION - responsible for performing correct sprite swap when key is no longer down
    ################################################################################################
    def OnKeyUp(self, _keyboardEvent):
        #if player has depressed Control
        if(_keyboardEvent.Key == Keys.Control or _keyboardEvent.Key == Keys.Up):
            #end the jump
            self.Owner.py_platformer_mechanics.EndJump()
        
        #if player was pressing space to shoot
        if(_keyboardEvent.Key == Keys.Space):
            #end the burp noise
            self.Owner.SoundEmitter.Stop()
            #end burp by going to py_snail_animation pyscript to check for correct sprite to use
            self.Owner.py_snail_animations.SnailIdle()
            #update IsBurping to false
            self.IsBurping = False
    
    ################################################################################################
    #FUNCTION - OnButtonDown
    #PARAMETERS - _gamepadEvent: used to keep track of gamepad button presses every frame
    #DESCRIPTION - responsible for performing BeginJump when gamepad button is down
    ################################################################################################
    def OnButtonDown(self, _gamepadEvent):
        #if player is pressing A to jump
        if(_gamepadEvent.Button == Buttons.A):
            self.Owner.py_platformer_mechanics.BeginJump()
        
        #if player is pressing X to shoot
        if(_gamepadEvent.Button == Buttons.X):
            #player burp sound
            self.Owner.SoundEmitter.Play()
            
            #perform the shooting
            self.Owner.py_shoot.Shoot()
            
            #go to py_snail_animation pyscript to check for correct sprite to use
            self.Owner.py_snail_animations.SnailBurp()

    ################################################################################################
    #FUNCTION - OnButtonUp
    #PARAMETERS - _gamepadEvent: used to keep track of gamepad button presses every frame
    #DESCRIPTION - responsible for performing EndJump when gamepad button is no longer down
    ################################################################################################
    def OnButtonUp(self, _gamepadEvent):
        #if player has depressed A
        if(_gamepadEvent.Button == Buttons.A):
            self.Owner.py_platformer_mechanics.EndJump()
            
        #if player is pressing X to shoot
        if(_gamepadEvent.Button == Buttons.X):
            #end the burp noise
            self.Owner.SoundEmitter.Stop()
            
            #go to py_snail_animation pyscript to check for correct sprite to use
            self.Owner.py_snail_animations.SnailIdle()

    ################################################################################################
    #FUNCTION - GetKeyboardMovement
    #DESCRIPTION - responsible for performing movement based on which key is pressed
    ################################################################################################
    def GetKeyboardMovement(self):
        #set localvariable Dir to VectorMath.Vec3 (currently all zeros)
        Dir = Vec3(0.0, 0.0, 0.0)
        
        #IF Paused, dont take commands
        if(self.Space.FindObjectByName("LevelSettings").py_HUDCreator.bPaused == False):
            #print("LOCAL Y! = " + str(self.Owner.Orientation.LocalForward))
            
            #if the player is pressing "A" (going left)
            if(Zero.Keyboard.KeyIsDown(Keys.A) or Zero.Keyboard.KeyIsDown(Keys.Left)):
                
                #check if player is on flat ground
                #if(math.degrees(self.Owner.Orientation.AbsoluteAngle) >= -45 and math.degrees(self.Owner.Orientation.AbsoluteAngle) <= 45):
                #set sprite to False so it flips left (faces the right direction)
                self.Owner.Sprite.FlipX = False
                #set local forward to the left direction
                Orientation = self.Owner.Orientation.WorldForward
                self.Owner.Orientation.LocalForward = Vec3(-1,0,0)
                #move left (add to Dir by -x)
                Dir += Orientation
                
                #else player is on a wall, so 0 out this movement for now
                #else:
                #    #set Dir to zeros
                #    Dir = Vec3(0.0, 0.0, 0.0)
                
                
                
            #if the player is pressing "D" (going right)
            if(Zero.Keyboard.KeyIsDown(Keys.D) or Zero.Keyboard.KeyIsDown(Keys.Right)):
                
                #check if player is on flat ground
                #if(math.degrees(self.Owner.Orientation.AbsoluteAngle) >= -45 and math.degrees(self.Owner.Orientation.AbsoluteAngle) <= 45):
                #set sprite to True so it flips right (faces the left direction)
                self.Owner.Sprite.FlipX = True
                #set local forward to the right direction
                Orientation = self.Owner.Orientation.WorldForward
                self.Owner.Orientation.LocalForward = Vec3(1,0,0)
                #move left (add to Dir by x)
                Dir += Orientation
            
                #else player is on a wall, so 0 out this movement for now
                #else:
                #    #set Dir to zeros
                #    Dir = Vec3(0.0, 0.0, 0.0)
            
            #if the player is pressing "W" (moving up)
            if(Zero.Keyboard.KeyIsDown(Keys.W)):
                
                #check if player is on wall
                if(math.degrees(self.Owner.Orientation.AbsoluteAngle) >= 85 and math.degrees(self.Owner.Orientation.AbsoluteAngle) <= 175):
                    #move upward (add to Dir by y)
                    Dir += Vec3(0.0, 1.0, 0.0)
                
                #else player is not on a wall, so 0 out this movement for now
                else:
                    #set Dir to zeros
                    Dir = Vec3(0.0, 0.0, 0.0)
        
        
        
        ##if the player is pressing "S" (moving down)
        if(Zero.Keyboard.KeyIsDown(Keys.S)):
            
            #check if player is on wall
            if(math.degrees(self.Owner.Orientation.AbsoluteAngle) >= 85 and math.degrees(self.Owner.Orientation.AbsoluteAngle) <= 175):
                #move upward (add to Dir by y)
                Dir += Vec3(0.0, -1.0, 0.0)
            
            #else player is not on a wall, so 0 out this movement for now
            else:
                #set Dir to zeros
                Dir = Vec3(0.0, 0.0, 0.0)
                
        return Dir

    ################################################################################################
    #FUNCTION - UpdateCurrentState
    #DESCRIPTION - responsible for gamepad movement
    ################################################################################################
    def GetGamepadMovement(self):
        #movement based off of gamepad's LeftStick
        return Vec3(self.Gamepad.LeftStick, 0.0)
        
Zero.RegisterComponent("py_input_manager", py_input_manager)
        
Zero.RegisterComponent("py_input_manager", py_input_manager)