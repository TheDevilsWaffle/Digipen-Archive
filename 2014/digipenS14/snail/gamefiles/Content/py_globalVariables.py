####################################################################################################
#COPYRIGHT:     All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
#FILENAME:      py_globalVariables
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This is attached to the space, and handles all functions that need to be
#               controlled across levels or while loading a level
####################################################################################################
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
        #create a list that will store the enemies in the level
        self.enemyList = []
        #set the players health to the determined maximum. This is done so that we can change the maximum health from inside the game
        self.PlayerHealth = self.PlayerMaxHealth
        #set the players lives to the determined maximum
        self.playerLives = self.playerMaxLives
        #default the player upgrade
        self.playerUpgrade = "Default"
        #initialize the players score
        self.score = 0
        #keep track of what level to start on continue.
        self.storeLevel = "lvl_MainMenu"
        self.startVector = Vec3(0,0,0)
        #Play background music.
        #self.Space.SoundSpace.PlayCue("BackgroundMusic")
        #Used for Pausing the game.
        self.bPaused = False
        #Connect a function that will run every logic update
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        #a series of events that will happen whenever a new level is loaded
        Zero.Connect(self.Space, Events.LevelStarted, self.OnLevelStarted)
        
    def OnLevelStarted(self, Event):
        
        #if(self.Space.CurrentLevel.Name == "lvl_INVIS_HUD"):
        #    self.Space.FindObjectByName("BackgroundMusic").SoundEmitter.Cue = "GameMusic"
            
        if((self.Space.CurrentLevel.Name != "lvl_Credits")
        and (self.Space.CurrentLevel.Name != "lvl_howPlay")
        and (self.Space.CurrentLevel.Name != "lvl_WinScreen")
        and (self.Space.CurrentLevel.Name != "lvl_HUD")
        and (self.Space.CurrentLevel.Name != "lvl_MainMenu")
        and (self.Space.CurrentLevel.Name != "lvl_LoseScreen")):
            self.storeLevel = self.Space.CurrentLevel
        snailObject = self.Space.FindObjectByName("Snail")
        #This will store the start location of the player, so that we can reset it when the snail dies.
        self.startLocation = snailObject.Transform.Translation
        
        #Load the proper upgrades for what the player has picked up
        if(snailObject.py_shoot):
            #alert the py_snail_animations pyscript so we can change to the right snail type
            self.Space.FindObjectByName("Snail").py_snail_animations.ChangeSnailType()
            #swap out the snail idle sprite right away
            self.Space.FindObjectByName("Snail").py_snail_animations.SnailIdle()
            snailObject.py_platformer_mechanics.WalkableSlopeAngle = 65
            if(self.playerUpgrade == "Default"):
                snailObject.py_shoot.GooSprite = "arc_bubble"
                snailObject.py_shoot.GooSpeed = 15
            if(self.playerUpgrade == "Ice"):
                snailObject.py_shoot.GooSprite = "arc_ice"
            if(self.playerUpgrade == "Fire"):
                snailObject.py_shoot.GooSprite = "arc_firebreath"
            if(self.playerUpgrade == "Lightning"):
                snailObject.py_shoot.GooSprite = "arc_lightning"

    def OnLogicUpdate(self, UpdateEvent):
        #Determine if the players health has fallen below 0,
        if(self.PlayerHealth <= 0):
            self.hasDied = True
            #if the current level is not main menu
            if(self.Space.CurrentLevel.Name != "lvl_MainMenu"):
                #remove one life from the player
                self.playerLives -= 1
            #find the player object
            playerObject = self.Space.FindObjectByName("Snail")
            self.snailTrail = self.Space.CreateAtPosition("arc_snailTrail", playerObject.Transform.Translation)
            playerObject.Transform.Translation = Vec3(100,100,0)
            #give the player full health
            self.PlayerHealth = self.PlayerMaxHealth
            
            #Michael's Code
            #get rid of the players upgrade
            self.playerUpgrade = "Default"
            self.Owner.FindObjectByName("Snail").py_shoot.GooSprite = "arc_bubble"
            self.Owner.FindObjectByName("Snail").py_shoot.GooSpeed = 15
            
            #Travis' Code
            #alert the py_snail_animations pyscript so we can change to the right snail type
            self.Space.FindObjectByName("Snail").py_snail_animations.ChangeSnailType()
            self.Space.FindObjectByName("Snail").py_snail_animations.SnailIdle()
        else:
            self.hasDied = False
        if(self.Space.FindObjectByName("SnailTrail")):
            startVector = self.startLocation - self.snailTrail.Transform.Translation
            distanceToStart = startVector.length()
            startVector.normalize()
            if(self.hasDied == True):
                self.moveSpeed = distanceToStart
            self.snailTrail.Transform.Translation += startVector * UpdateEvent.Dt * self.moveSpeed
            if(distanceToStart <= 0.1):
                snailObject = self.Space.FindObjectByName("Snail")
                snailObject.Transform.Translation = self.startLocation
                self.snailTrail.Destroy()
                #define a script event
                toSend = Zero.ScriptEvent()
                #use that script event to send "unlock" into the game space, triggering anything listening
                self.Space.DispatchEvent("Invincible", toSend)

        
        # Garrett's Code
        #If the lives are below zero, reload the level and restore lives and health
        if(self.playerLives <= 0):
            self.Space.LoadLevel("lvl_LoseScreen")
            self.PlayerHealth = self.PlayerMaxHealth
            self.playerLives = self.playerMaxLives
            
Zero.RegisterComponent("py_globalVariables", py_globalVariables)