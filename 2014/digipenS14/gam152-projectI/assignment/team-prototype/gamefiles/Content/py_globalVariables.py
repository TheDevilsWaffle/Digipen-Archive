#####       This is attached to the space, and handles all functions that need to be        #####
#####       controlled across levels or while loading a level                               #####
#################################################################################################
import Zero
import Events
import Property
import VectorMath

#Shortcut
Vec3 = VectorMath.Vec3

class py_globalVariables:
    #tells the game the maximum amount of health the player can have.
    PlayerMaxHealth = Property.Float(1.0)
    
    #sets the lives
    playerMaxLives = Property.Float(5.0)
    
    #if true, allows the player to double jump
    DoubleJump = Property.Bool(False)
    
    #tells the game if the player is currently in a shell
    inShell = Property.Bool(True)
    
    #####       The following are performed whenever the game starts        #####
    def Initialize(self, initializer):
        #set the players health to the determined maximum
        self.PlayerHealth = self.PlayerMaxHealth
        self.playerLives = self.playerMaxLives
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        
        
    def OnLogicUpdate(self, UpdateEvent):
        #Determine if the players health has fallen below 0, and reload the level if it has.
        if(self.PlayerHealth <= 0):
            self.playerLives -= 1
            playerLocation = self.Space.FindObjectByName("Snail")
            playerLocation.Transform.Translation = Vec3(0,-8.5,0)
            self.PlayerHealth = self.PlayerMaxHealth
        
        #If the lives are below zero, reload the level
        if(self.playerLives <= 0):
            self.Space.ReloadLevel()
            self.PlayerHealth = self.PlayerMaxHealth
            self.playerLives = self.playerMaxLives

Zero.RegisterComponent("py_globalVariables", py_globalVariables)