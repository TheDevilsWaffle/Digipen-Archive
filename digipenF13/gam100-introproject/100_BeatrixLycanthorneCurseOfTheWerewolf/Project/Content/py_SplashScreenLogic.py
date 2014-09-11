import Zero
import Events
import Property
import VectorMath

class py_SplashScreenLogic:
    def Initialize(self, initializer):
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        self.timer = 2.0
        pass
        
    def OnLogicUpdate(self, UpdateEvent):
        #Decrement the timer
        self.timer -= UpdateEvent.Dt
        #If the timer reaches 0
        if(self.timer <= 0):
            #If the level is on the DigiPen splash, change it to the TSC logo
            if(self.Space.CurrentLevel.Name == "DigiSplash"):
                self.Space.LoadLevel("LogoScreen")
            #If the level is on the TSC logo, change it to the menu screen
            if(self.Space.CurrentLevel.Name == "LogoScreen"):
                self.Space.LoadLevel("MenuScreen")

Zero.RegisterComponent("py_SplashScreenLogic", py_SplashScreenLogic)