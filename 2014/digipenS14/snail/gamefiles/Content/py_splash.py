#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_splash
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This script counts and displays the splash screens for digipen and our team
####################################################################################################
import Zero
import Events
import Property
import VectorMath


class py_splash:
    #point to next level
    nextLevel = Property.Level()
    
    def Initialize(self, initializer):
        #timer until screen is removed
        self.removeTime = 0
        
        # background music
        self.SplashMusicObject = self.Space.FindObjectByName("SplashMusic")
        self.GameMusicObject = self.Space.FindObjectByName("GameMusic")
        if(self.GameMusicObject):
            self.GameMusicObject.Destroy()
        if (not self.SplashMusicObject):
            self.SplashMusicObject = self.Space.Create("arc_splashMusic")
            self.SplashMusicObject.Persistent = True
        if (self.SplashMusicObject.SoundEmitter.IsPlaying() == False):
            self.SplashMusicObject.SoundEmitter.Play()
        
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate);

    def OnLogicUpdate(self, Event):
        #if the timer has reached 200 logic updates, move to the next level
        if(self.removeTime >= 180):
            self.Space.LoadLevel(self.nextLevel)
        #add one to the timer if we didn't exit the level.
        self.removeTime += 1

Zero.RegisterComponent("py_splash", py_splash)