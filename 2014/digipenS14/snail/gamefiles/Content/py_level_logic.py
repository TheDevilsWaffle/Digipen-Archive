#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_level_logic
#AUTHOR:        Garrett Huxtable
#DESCRIPTION:   used to swtich between levels
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import Keys
Vec3 = VectorMath.Vec3


class py_level_logic:
    #allows us to set which level is next
    nextLevel = Property.Level()
        
    def Initialize(self, initializer):
        #empty the global variables enemy list
        self.Space.py_globalVariables.enemyList = []
        
        # Set the initial timer
        self.timer = 0
        self.crow = False
        
        #We want to check for input every logic update
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        Zero.Connect(Zero.Keyboard, Events.KeyDown, self.OnKeyDown)
        
    ##################################################################

    #   OnLogicUpdate is used to check if all enemies have been
    #   defeated. If all enemies have been defeated, the level
    #   switches based on the nextLevel Cog.

    ##################################################################
    def OnLogicUpdate(self, UpdateEvent):
        # Timer
        self.timer += UpdateEvent.Dt
        
        if(self.Space.py_globalVariables.hasDied == True):
            self.timer = 0
        
        #this if statement prevents an the enemy count from coming into affect in levels without enemies
        if((self.Space.CurrentLevel.Name != "lvl_MainMenu") 
            and (self.Space.CurrentLevel.Name != "lvl_Credits") 
            and (self.Space.CurrentLevel.Name != "lvl_howPlay") 
            and (self.Space.CurrentLevel.Name != "lvl_WinScreen") 
            and (self.Space.CurrentLevel.Name != "lvl_LoseScreen")):
            
            # this if statement checks to see if there are no more enemies and moves on if there are none
            if(len(self.Space.py_globalVariables.enemyList) == 0):
                    self.Space.LoadLevel(self.nextLevel)
                    #empty the global variables enemy list
                    self.Space.py_globalVariables.enemyList = []
                    #reset level timer
                    self.timer = 0
                    
            #Spawns crow after 45 seconds
            if(self.crow == False):
                if(self.timer >= 45):
                    stalkingPosition = Vec3(16, 16, 10)
                    self.Space.CreateAtPosition("Crow", stalkingPosition)
                    self.timer = 0
                    self.crow = True
                    
        if((self.Space.CurrentLevel.Name == "lvl_howPlay") 
            or (self.Space.CurrentLevel.Name == "lvl_Credits")
            or (self.Space.CurrentLevel.Name == "lvl_WinScreen")
            or (self.Space.CurrentLevel.Name == "lvl_LoseScreen")):
            if(Zero.Keyboard.KeyIsPressed(Zero.Keys.Back)):
                self.Space.LoadLevel("lvl_MainMenu")
                self.Space.py_globalVariables.playerLives = self.Space.py_globalVariables.playerMaxLives
                self.Space.py_globalVariables.playerUpgrade = "Default"
                #alert the py_snail_animations pyscript so we can change to the right snail type
                    #self.Space.FindObjectByName("Snail").py_snail_animations.ChangeSnailType()
                #swap out the snail idle sprite right away
                    #self.Space.FindObjectByName("Snail").py_snail_animations.SnailIdle()

        if(Zero.Keyboard.KeyIsPressed(Zero.Keys.Back)):
            self.Space.LoadLevel("lvl_MainMenu")
            self.Space.py_globalVariables.playerLives = self.Space.py_globalVariables.playerMaxLives
            self.Space.py_globalVariables.playerUpgrade = "Default"
            
    ##################################################################

    #   OnKeyDown is used to cheat our way to other levels.
    #   Pressing N goes to the next level
    #   Pressing L goes to the Lose Screen
    #   Pressing P goes to the Win Screen
    #   Pressing R reloads the current level

    ##################################################################
    def OnKeyDown(self, _keyboardEvent):
        #If the game is not Paused:
        if(self.Space.FindObjectByName("LevelSettings").py_HUDCreator.bPaused == False):
            if(_keyboardEvent.Key == Keys.N):
                self.Space.LoadLevel(self.nextLevel)
            if(_keyboardEvent.Key == Keys.L):
                self.Space.LoadLevel("lvl_LoseScreen")
            if(_keyboardEvent.Key == Keys.V):
                self.Space.LoadLevel("lvl_WinScreen")
            if(_keyboardEvent.Key == Keys.R):
                self.Space.ReloadLevel()
            if(_keyboardEvent.Key == Keys.U):
                self.Space.LoadLevel(self.Space.py_globalVariables.storeLevel)

Zero.RegisterComponent("py_level_logic", py_level_logic)



