#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_mainMenu
#AUTHOR:        Garrett Huxtable
#DESCRIPTION:   script that controls the main menu and the controls for highlighting options
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import Keys
Vec3 = VectorMath.Vec3


class py_mainMenu:
    def Initialize(self, initializer):
        #when a key is pressed, go to OnKeyDown
        Zero.Connect(Zero.Keyboard, Events.KeyDown, self.OnKeyDown)

    def OnKeyDown(self, _keyboardEvent):
        Highlight = self.Space.FindObjectByName("Highlight")
        
        #if player is pressing W or up, move the highlight up one
        if(_keyboardEvent.Key == Keys.W or _keyboardEvent.Key == Keys.Up):
            Highlight.Transform.Translation += Vec3(0, 2.5, 0)
            #move the highlight to the bottom if it is at the top
            if(Highlight.Transform.Translation.y > 0):
                Highlight.Transform.Translation = Vec3(16,-10,0)
            
        #if player is pressing S or down, move the highlight down one
        if(_keyboardEvent.Key == Keys.S or _keyboardEvent.Key == Keys.Down):
            Highlight.Transform.Translation -= Vec3(0, 2.5, 0)
            #move the highlight to the top if it is at the bottom
            if(Highlight.Transform.Translation.y < -10.0):
                Highlight.Transform.Translation = Vec3(16,0,0)
                
        #to last loaded level
        if(Highlight.Transform.Translation == self.Space.FindObjectByName("ContinueButton").Transform.Translation):
            self.Space.FindObjectByName("spr_ui_continue_high").Sprite.Visible = True
            #if player is pressing enter or space, take them to the highlated level
            if(_keyboardEvent.Key == Keys.Enter):
                self.Space.LoadLevel(self.Space.py_globalVariables.storeLevel)
        else:
            self.Space.FindObjectByName("spr_ui_continue_high").Sprite.Visible = False
            
        #to the first level
        if(Highlight.Transform.Translation == self.Space.FindObjectByName("StartButton").Transform.Translation):
            self.Space.FindObjectByName("spr_ui_newgame_high").Sprite.Visible = True
            #if player is pressing enter or space, take them to the highlated level
            if(_keyboardEvent.Key == Keys.Enter):
                self.Space.LoadLevel("lvl_world01")
        else:
            self.Space.FindObjectByName("spr_ui_newgame_high").Sprite.Visible = False
            
        #to the How to play screen
        if(Highlight.Transform.Translation == self.Space.FindObjectByName("HowPlayButton").Transform.Translation):
            self.Space.FindObjectByName("spr_ui_howtoplay_high").Sprite.Visible = True
            #if player is pressing enter or space, take them to the highlated level
            if(_keyboardEvent.Key == Keys.Enter):
                self.Space.LoadLevel("lvl_howPlay")
        else:
            self.Space.FindObjectByName("spr_ui_howtoplay_high").Sprite.Visible = False
            
        #to the credits
        if(Highlight.Transform.Translation == self.Space.FindObjectByName("CreditsButton").Transform.Translation):
            self.Space.FindObjectByName("spr_ui_credits_high").Sprite.Visible = True
            #if player is pressing enter or space, take them to the highlated level
            if(_keyboardEvent.Key == Keys.Enter):
                self.Space.LoadLevel("lvl_Credits")
        else:
            self.Space.FindObjectByName("spr_ui_credits_high").Sprite.Visible = False
            
        #quit game
        if(Highlight.Transform.Translation == self.Space.FindObjectByName("QuitButton").Transform.Translation):
            self.Space.FindObjectByName("spr_ui_quit_high").Sprite.Visible = True
            #if player is pressing enter or space, take them to the highlated level
            if(_keyboardEvent.Key == Keys.Enter):
                Zero.Game.Quit()
        else:
            self.Space.FindObjectByName("spr_ui_quit_high").Sprite.Visible = False

Zero.RegisterComponent("py_mainMenu", py_mainMenu)