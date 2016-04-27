#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_pauseMenu
#AUTHOR:        Garrett Huxtable
#DESCRIPTION:   script that controls the pause menu and highlighting options on the pause menu
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import Keys
Vec3 = VectorMath.Vec3


class py_pauseMenu:
    def Initialize(self, initializer):
        self.bCredits = False
        self.bControls = False
        
        #when a key is pressed, go to OnKeyDown
        Zero.Connect(Zero.Keyboard, Events.KeyDown, self.OnKeyDown)
        #need to pause time in the game
        self.Space.TimeSpace.TogglePause()
        
    def OnKeyDown(self, _keyboardEvent):
        Highlight = self.Space.FindObjectByName("Highlight")
        
        if(self.bCredits == False and self.bControls == False):
            ##########         Move Highlight           ###########
            #if player is pressing W or up, move the highlight up one
            if(_keyboardEvent.Key == Keys.W or _keyboardEvent.Key == Keys.Up):
                if(Highlight.Transform.Translation.y == 7.5):
                    Highlight.Transform.Translation -= Vec3(0, 15, 0)
                else:
                    Highlight.Transform.Translation += Vec3(0, 5, 0)
                    
            #if player is pressing S or down, move the highlight down one
            if(_keyboardEvent.Key == Keys.S or _keyboardEvent.Key == Keys.Down):
                if(Highlight.Transform.Translation.y == -7.5):
                    Highlight.Transform.Translation += Vec3(0, 15, 0)
                else:
                    Highlight.Transform.Translation -= Vec3(0, 5, 0)
            ###########                               ###########
            
            #if player is pressing enter, take them to the highlated level
            if(_keyboardEvent.Key == Keys.Enter):
                #to last loaded level
                if(Highlight.Transform.Translation == self.Space.FindObjectByName("ContinueButton").Transform.Translation):
                    #need to set bPaused in HUD creator to false
                    self.Space.FindObjectByName("LevelSettings").py_HUDCreator.bPaused = False
                    self.Space.TimeSpace.TogglePause()
                    self.Owner.Destroy()
                
                #to the How to play screen
                if(Highlight.Transform.Translation == self.Space.FindObjectByName("HowPlayButton").Transform.Translation):
                    self.Space.CreateAtPosition("arc_controls", Vec3(0, 0, 5))
                    self.wordHighlight = self.Space.FindObjectByName("HowToPlayHighlight")
                    self.wordHighlight.Sprite.Visible = False
                    self.bControls = True
                
                #to the credits
                if(Highlight.Transform.Translation == self.Space.FindObjectByName("CreditsButton").Transform.Translation):
                    self.Space.CreateAtPosition("arc_credits", Vec3(0, 0, 5))
                    self.wordHighlight = self.Space.FindObjectByName("CreditsHighlight")
                    self.wordHighlight.Sprite.Visible = False
                    self.bCredits = True
                    
                #Return to menu
                if(Highlight.Transform.Translation == self.Space.FindObjectByName("MenuButton").Transform.Translation):
                    self.Space.TimeSpace.TogglePause()
                    self.Space.LoadLevel("lvl_MainMenu")
        
            else:
                ###########         Word Highlight          ###########
                if(Highlight.Transform.Translation == self.Space.FindObjectByName("ContinueButton").Transform.Translation):
                    self.wordHighlight = self.Space.FindObjectByName("ContinueHighlight")
                    self.wordHighlight.Sprite.Visible = True
                else:
                    self.wordHighlight = self.Space.FindObjectByName("ContinueHighlight")
                    self.wordHighlight.Sprite.Visible = False
                    
                if(Highlight.Transform.Translation == self.Space.FindObjectByName("HowPlayButton").Transform.Translation):
                    self.wordHighlight = self.Space.FindObjectByName("HowToPlayHighlight")
                    self.wordHighlight.Sprite.Visible = True
                else:
                    self.wordHighlight = self.Space.FindObjectByName("HowToPlayHighlight")
                    self.wordHighlight.Sprite.Visible = False
                    
                if(Highlight.Transform.Translation == self.Space.FindObjectByName("CreditsButton").Transform.Translation):
                    self.wordHighlight = self.Space.FindObjectByName("CreditsHighlight")
                    self.wordHighlight.Sprite.Visible = True
                else:
                    self.wordHighlight = self.Space.FindObjectByName("CreditsHighlight")
                    self.wordHighlight.Sprite.Visible = False
                    
                if(Highlight.Transform.Translation == self.Space.FindObjectByName("MenuButton").Transform.Translation):
                    self.wordHighlight = self.Space.FindObjectByName("MenuHighlight")
                    self.wordHighlight.Sprite.Visible = True
                else:
                    self.wordHighlight = self.Space.FindObjectByName("MenuHighlight")
                    self.wordHighlight.Sprite.Visible = False
                ###########                               ###########
                
        if(_keyboardEvent.Key == Keys.Back):
            if(self.bControls == True):
                self.wordHighlight.Sprite.Visible = True
                self.wordHighlight = self.Space.FindObjectByName("MenuHighlight")
                self.wordHighlight.Sprite.Visible = False
                arcControls = self.Space.FindObjectByName("Controls")
                arcControls.Destroy()
                self.bControls = False
            if(self.bCredits == True):
                self.wordHighlight.Sprite.Visible = True
                self.wordHighlight = self.Space.FindObjectByName("MenuHighlight")
                self.wordHighlight.Sprite.Visible = False
                arcCredits = self.Space.FindObjectByName("Credits")
                arcCredits.Destroy()
                self.bCredits = False
            else:
                pass

Zero.RegisterComponent("py_pauseMenu", py_pauseMenu)