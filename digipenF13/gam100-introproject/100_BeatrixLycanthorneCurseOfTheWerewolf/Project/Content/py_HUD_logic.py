import Zero
import Events
import Property
import VectorMath
import Color

Vec3 = VectorMath.Vec3


class py_HUD_logic:
    werewolfIsTrue = False
    
    def Initialize(self, initializer):
        #used to update the seconds passed and the score for every logic update
        Zero.Connect(self.Space, Events.LogicUpdate, self.UpdateHUD)
        Zero.Connect(self.Space, "wereWolfStateIsTrue", self.findWerewolfState)
        Zero.Connect(self.Space, "wereWolfStateIsFalse", self.findWerewolfStateFalse)
        #keep track of seconds that pass by for werewolf transformation
        self.TimerWerewolf = 5.0
        
        #keep track of keys collected
        self.Keys = 0

        #keep track of ingredients collected
        self.Ingredients = 0
        
        #keep track of lives
        self.Lives = 3
        
        #Keep track of the cooldown timer
        self.CooldownTimer = 5.0
    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-UpdateHUD
    #-----------------------------------------------------------------------------------------------------------#
    def UpdateHUD(self, UpdateEvent):
        #update the time passed with delta time
        

        #create variable for Level_HUD_Beatrix
        HUDBeatrix = Zero.Game.FindSpaceByName("Level_HUD_Beatrix")
        #create variables to use for ingredients, keys, and timers
        Potion = HUDBeatrix.FindObjectByName("HUDPotion")
        Key = HUDBeatrix.FindObjectByName("HUDKey")
        HUDWerewolf = HUDBeatrix.FindObjectByName("WereHUD")
        HumanHUD = HUDBeatrix.FindObjectByName("HUDBeatrix")
        Hearts3 = HUDBeatrix.FindObjectByName("Hearts3")
        Hearts2 = HUDBeatrix.FindObjectByName("Hearts2")
        Hearts1 = HUDBeatrix.FindObjectByName("Hearts1")
        CD5sec = HUDBeatrix.FindObjectByName("CD5sec")
        CD4sec = HUDBeatrix.FindObjectByName("CD4sec")
        CD3sec = HUDBeatrix.FindObjectByName("CD3sec")
        CD2sec = HUDBeatrix.FindObjectByName("CD2sec")
        CD1sec = HUDBeatrix.FindObjectByName("CD1sec")
        Timer5s = HUDBeatrix.FindObjectByName("Timer5s")
        Timer4s = HUDBeatrix.FindObjectByName("Timer4s")
        Timer3s = HUDBeatrix.FindObjectByName("Timer3s")
        Timer2s = HUDBeatrix.FindObjectByName("Timer2s")
        Timer1s = HUDBeatrix.FindObjectByName("Timer1s")
        
        if(HUDBeatrix):
            #declare a variable to stand for beatrix object
            Player = self.Space.FindObjectByName("BeatrixLycanthorne")
            #if player is exists (True)
            if(Player):
                #Find and set the keys, ingredients and lives
                self.Keys = Player.py_beatrix_controller.KeysCollected
                self.Ingredients = Player.py_beatrix_controller.IngredientsCollected
                self.Lives = Player.py_beatrix_controller.LivesCount
                
                #Change the heart sprite depending on the lives
                if(self.Lives == 2):
                    Hearts3.Sprite.Visible = True
                    Hearts2.Sprite.Visible = False
                    Hearts1.Sprite.Visible = False
                    
                if(self.Lives == 1):
                    Hearts3.Sprite.Visible = False
                    Hearts2.Sprite.Visible = True
                    Hearts1.Sprite.Visible = False
                    
                if(self.Lives == 0):
                    Hearts3.Sprite.Visible = False
                    Hearts2.Sprite.Visible = False
                    Hearts1.Sprite.Visible = True
                
                #Show or hide the key sprite depending on if the player has a key or not
                if(self.Keys == 1):
                    Key.Sprite.Visible = True
                if(self.Keys == 0):
                    Key.Sprite.Visible = False
                    
                #Show or hide the potion sprite depending on if the player has a key or not
                if(self.Ingredients == 1):
                    Potion.Sprite.Visible = True
                if(self.Ingredients == 0):
                    Potion.Sprite.Visible = False
                
                #Check if the player is a werewolf
                if(self.werewolfIsTrue):
                    #Activate the werewolf HUD and grey out the items
                    HumanHUD.Sprite.Visible = False
                    HUDWerewolf.Sprite.Visible = True
                    Key.Sprite.Color = Color.DarkGray
                    Potion.Sprite.Color = Color.DarkGray
                    #Activate the werewolf timer and reset the cooldown Timer
                    self.CooldownTimer = 5.0
                    self.TimerWerewolf -= UpdateEvent.Dt
                    #Depending on the timer, show the appropriate time sprite
                    if(self.TimerWerewolf >= 4):
                        Timer5s.Sprite.Visible = True
                        
                    if(self.TimerWerewolf >= 3 and self.TimerWerewolf <= 4):
                        Timer5s.Sprite.Visible = False
                        Timer4s.Sprite.Visible = True
                        
                    if(self.TimerWerewolf >= 2 and self.TimerWerewolf <= 3):
                        Timer5s.Sprite.Visible = False
                        Timer4s.Sprite.Visible = False
                        Timer3s.Sprite.Visible = True
                        
                    if(self.TimerWerewolf >= 1 and self.TimerWerewolf <= 2):
                        Timer5s.Sprite.Visible = False
                        Timer4s.Sprite.Visible = False
                        Timer3s.Sprite.Visible = False
                        Timer2s.Sprite.Visible = True
                        
                    if(self.TimerWerewolf >= 0 and self.TimerWerewolf <= 1):
                        Timer5s.Sprite.Visible = False
                        Timer4s.Sprite.Visible = False
                        Timer3s.Sprite.Visible = False
                        Timer2s.Sprite.Visible = False
                        Timer1s.Sprite.Visible = True
                    
                    if(self.TimerWerewolf <= 0):
                        Timer5s.Sprite.Visible = False
                        Timer4s.Sprite.Visible = False
                        Timer3s.Sprite.Visible = False
                        Timer2s.Sprite.Visible = False
                        Timer1s.Sprite.Visible = False
                
                #If the player is not a werewolf
                if(self.werewolfIsTrue == False):
                    #Reset the werewolf timer and decrement the cooldown timer
                    self.CooldownTimer -= UpdateEvent.Dt
                    Timer1s.Sprite.Visible = False
                    self.TimerWerewolf = 5.0
                    #Check what second the cooldown timer is at and display the right sprite
                    if(self.CooldownTimer >= 4):
                        CD5sec.Sprite.Visible = True
                        
                    if(self.CooldownTimer >= 3 and self.CooldownTimer <= 4):
                        CD5sec.Sprite.Visible = False
                        CD4sec.Sprite.Visible = True
                        
                    if(self.CooldownTimer >= 2 and self.CooldownTimer <= 3):
                        CD5sec.Sprite.Visible = False
                        CD4sec.Sprite.Visible = False
                        CD3sec.Sprite.Visible = True
                        
                    if(self.CooldownTimer >= 1 and self.CooldownTimer <= 2):
                        CD5sec.Sprite.Visible = False
                        CD4sec.Sprite.Visible = False
                        CD3sec.Sprite.Visible = False
                        CD2sec.Sprite.Visible = True
                        
                    if(self.CooldownTimer >= 0 and self.CooldownTimer <= 1):
                        CD5sec.Sprite.Visible = False
                        CD4sec.Sprite.Visible = False
                        CD3sec.Sprite.Visible = False
                        CD2sec.Sprite.Visible = False
                        CD1sec.Sprite.Visible = True
                        
                    if(self.CooldownTimer <= 0):
                        CD5sec.Sprite.Visible = False
                        CD4sec.Sprite.Visible = False
                        CD3sec.Sprite.Visible = False
                        CD2sec.Sprite.Visible = False
                        CD1sec.Sprite.Visible = False
                    
                    HumanHUD.Sprite.Visible = True
                    HUDWerewolf.Sprite.Visible = False
                    Key.Sprite.Color = Color.White
                    Potion.Sprite.Color = Color.White
                    
                
    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-findWerewolfState
    #Find whether or not there player is a werewolf
    #-----------------------------------------------------------------------------------------------------------#
    def findWerewolfState(self, werewolfStateIsTrue):
        self.werewolfIsTrue = True
    
    #############################################################################################################

    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-findWerewolfState
    #-----------------------------------------------------------------------------------------------------------#
    def findWerewolfStateFalse(self, werewolfStateIsFalse):
        self.werewolfIsTrue = False
        
    
    #############################################################################################################
Zero.RegisterComponent("py_HUD_logic", py_HUD_logic)