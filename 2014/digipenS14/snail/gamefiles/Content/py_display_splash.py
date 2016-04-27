#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_display_splash
#AUTHOR:        Travis Moore
#DESCRIPTION:   Used to display the digipen splash screen and team screens at the beginning of the
#               starting the game.
####################################################################################################
import Zero
import Events
import Property


class py_display_splash:
    def Initialize(self, initializer):
        #listen to space and perform DisplaySplashScreens
        Zero.Connect(self.Space, Events.LogicUpdate, self.DisplaySplashScreens)
        #set variable DisplayTimer to 2 seconds (how long each splash screen will show)
        self.DisplayTimer = 2.0
        pass

    ################################################################################################
    #FUNCTION - DisplaySplashScreens
    #PARAMTERS - _UpdateEvent: used to keep track of timer value every frame
    #DESCRIPTION - responsible for displaying the splash screens before the game starts
    ################################################################################################
    def DisplaySplashScreens(self, _UpdateEvent):
        #decrement timer
        self.DisplayTimer -= _UpdateEvent.Dt

        #when the timer reaches 0, cycle through splash screens depending on which screen is currently loaded
        if(self.DisplayTimer <= 0):
            #once the digipen splash screen is done, display the team logo screen
            if(self.Space.CurrentLevel.Name == "Splash_Digipen"):
                #load Splash_TeamLogo
                self.Space.LoadLevel("Splash_TeamLogo")
            #once team logo screen is done, display the menu screen
            if(self.Space.CurrentLevel.Name == "Splash_TeamLogo"):
                #load Menu_MainMenu
                self.Space.LoadLevel("Menu_MainMenu")

Zero.RegisterComponent("py_display_splash", py_display_splash)