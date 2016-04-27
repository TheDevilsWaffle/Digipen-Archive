#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME: py_HUDCreator
#AUTHOR: Garrett Huxtable
#DESCRIPTION: Creates the HUD on levels
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import Keys

Vec3 = VectorMath.Vec3

class py_HUDCreator:
    def DefineProperties(self):
        #The level with our hud objects
        self.LevelForHud = Property.Resource("Level")
        
    def Initialize(self, initializer):
        self.bPaused = False
        self.HUDSpace = Zero.Game.CreateNamedSpace("HUDSpace", "Space")
        self.textHUD = self.HUDSpace.FindObjectByName("HudText")
        
        # background music
        self.SplashMusicObject = self.Space.FindObjectByName("SplashMusic")
        self.GameMusicObject = self.Space.FindObjectByName("GameMusic")
        if(self.SplashMusicObject):
            self.SplashMusicObject.Destroy()
        if (not self.GameMusicObject):
            self.GameMusicObject = self.Space.Create("arc_gameMusic")
            self.GameMusicObject.Persistent = True
        if (self.GameMusicObject.SoundEmitter.IsPlaying() == False):
            self.GameMusicObject.SoundEmitter.Play()
        
        Zero.Connect(Zero.Keyboard, Events.KeyDown, self.OnKeyDown)
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        
        #Load the level with our HUD objects into the HUD space
        self.HUDSpace.LoadLevel(self.LevelForHud)
        
    def OnLogicUpdate(self, UpdateEvent):
        self.textHUD = self.HUDSpace.FindObjectByName("HudText")
        if(self.textHUD):
            self.textHUD.SpriteText.Text = "Lives " + str(round(self.Space.py_globalVariables.playerLives))
        
    def OnKeyDown(self, _keyboardEvent):
        if(self.bPaused == False):
            if(_keyboardEvent.Key == Keys.P):
                self.bPaused = True
                self.Space.CreateAtPosition("arc_pauseMenu", Vec3(0,0,1))
        
    def Destroyed(self):
        #Destroy the HUD space and all objects inside it
        self.HUDSpace.Destroy()
        

Zero.RegisterComponent("py_HUDCreator", py_HUDCreator)