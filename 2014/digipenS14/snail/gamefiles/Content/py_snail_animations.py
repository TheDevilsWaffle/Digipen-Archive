####################################################################################################
#COPYRIGHT:     All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
#FILENAME:      py_snail_animations
#AUTHOR:        Travis Moore
#DESCRIPTION:   This script contains functions which control when snail sprites are swapped in/out.
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import Keys
import Action
import math

#shothand for VectorMath.Vec3
Vec3 = VectorMath.Vec3


class py_snail_animations:
    #initializer
    def Initialize(self, initializer):
        #set default SnailType to Normal and evaulate SnailType based off the value of SnailType
        self.SnailType = "Normal"
        pass
        
    ################################################################################################
    #FUNCTION - SnailIdle
    #DESCRIPTION - responsible for determining which idle sprite to use for an idling snail
    ################################################################################################
    def SnailIdle(self):
        #check SnailType to apply the correct sprite to the snail
        
        #if the SnailType is currently "Ice"
        if(self.SnailType == "Ice"):
            #change to spr_snail_ice_idle
            self.Owner.Sprite.SpriteSource = "spr_snail_ice_idle"
            
        #if the SnailType is currently "Fire"
        if(self.SnailType == "Fire"):
            #change to spr_snail_fire_idle
            self.Owner.Sprite.SpriteSource = "spr_snail_fire_idle"
            
        #if the SnailType is currently "Lightning"
        if(self.SnailType == "Lightning"):
            #change to spr_snail_lightning_idle
            self.Owner.Sprite.SpriteSource = "spr_snail_lightning_idle"
            
        #else, the snail is supposed to be normal
        if(self.SnailType == "Normal"):
            #change to spr_snail_idle
            self.Owner.Sprite.SpriteSource = "spr_snail_idle"
    
    ################################################################################################
    #FUNCTION - SnailBurp
    #DESCRIPTION - responsible for determining which burp sprite to use for a burping snail
    ################################################################################################
    def SnailBurp(self):
        #check SnailType to apply the correct sprite to the snail
        
        #if the SnailType is currently "Ice"
        if(self.SnailType == "Ice"):
            #change to spr_snail_ice_burp
            self.Owner.Sprite.SpriteSource = "spr_snail_ice_burp"
            
        #if the SnailType is currently "Fire"
        if(self.SnailType == "Fire"):
            #change to spr_snail_fire_burp
            self.Owner.Sprite.SpriteSource = "spr_snail_fire_burp"
            
        #if the SnailType is currently "Lightning"
        if(self.SnailType == "Lightning"):
            #change to spr_snail_lightning_burp
            self.Owner.Sprite.SpriteSource = "spr_snail_lightning_burp"
            
        #else, the snail is supposed to be normal
        if(self.SnailType == "Normal"):
            #change to spr_snail_burp
            self.Owner.Sprite.SpriteSource = "spr_snail_burp"
    
    ################################################################################################
    #FUNCTION - SnailEat
    #DESCRIPTION - responsible for determining which Eat sprite to use for eating enemies
    ################################################################################################
    def SnailEat(self):
        #check SnailType to apply the correct sprite to the snail
        
        #if the SnailType is currently "Ice"
        if(self.SnailType == "Ice"):
            #change to spr_snail_ice_eat
            self.Owner.Sprite.SpriteSource = "spr_snail_ice_eat"
            
        #if the SnailType is currently "Fire"
        if(self.SnailType == "Fire"):
            #change to spr_snail_fire_eat
            self.Owner.Sprite.SpriteSource = "spr_snail_fire_eat"
            
        #if the SnailType is currently "Lightning"
        if(self.SnailType == "Lightning"):
            #change to spr_snail_lightning_eat
            self.Owner.Sprite.SpriteSource = "spr_snail_lightning_eat"
            
        #else, the snail is supposed to be normal
        if(self.SnailType == "Normal"):
            #change to spr_snail_eat
            self.Owner.Sprite.SpriteSource = "spr_snail_eat"
    
    ################################################################################################
    #FUNCTION - SnailJump
    #DESCRIPTION - responsible for determining which jump sprite to use for a jumping snail
    ################################################################################################
    def SnailJump(self):
        #check SnailType to apply the correct sprite to the snail
        
        #if the SnailType is currently "Ice"
        if(self.SnailType == "Ice"):
            #change to spr_snail_ice_jump
            self.Owner.Sprite.SpriteSource = "spr_snail_ice_jumping"
            
        #if the SnailType is currently "Fire"
        if(self.SnailType == "Fire"):
            #change to spr_snail_fire_jump
            self.Owner.Sprite.SpriteSource = "spr_snail_fire_jumping"
            
        #if the SnailType is currently "Lightning"
        if(self.SnailType == "Lightning"):
            #change to spr_snail_lightning_jump
            self.Owner.Sprite.SpriteSource = "spr_snail_lightning_jumping"
            
        #else, the snail is supposed to be normal
        if(self.SnailType == "Normal"):
            #change to spr_snail_jump
            self.Owner.Sprite.SpriteSource = "spr_snail_jumping"
    
    ################################################################################################
    #FUNCTION - SnailFall
    #DESCRIPTION - responsible for determining which fall sprite to use for a falling snail
    ################################################################################################
    def SnailFall(self):
        #check SnailType to apply the correct sprite to the snail
        
        #if the SnailType is currently "Ice"
        if(self.SnailType == "Ice"):
            #change to spr_snail_ice_fall
            self.Owner.Sprite.SpriteSource = "spr_snail_ice_falling"
            
        #if the SnailType is currently "Fire"
        if(self.SnailType == "Fire"):
            #change to spr_snail_fire_fall
            self.Owner.Sprite.SpriteSource = "spr_snail_fire_falling"
            
        #if the SnailType is currently "Lightning"
        if(self.SnailType == "Lightning"):
            #change to spr_snail_lightning_fall
            self.Owner.Sprite.SpriteSource = "spr_snail_lightning_falling"
            
        #else, the snail is supposed to be normal
        if(self.SnailType == "Normal"):
            #change to spr_snail_fall
            self.Owner.Sprite.SpriteSource = "spr_snail_falling"
    
    ################################################################################################
    #FUNCTION - ChangeSnailType
    #DESCRIPTION - responsible for checking py_globalVariables's playerUpgrade variable in order
    #              correctly determine which powerup version of the snail sprites to use
    ################################################################################################
    def ChangeSnailType(self):
        #determine what powerup has been picked up
        #if powerup is ice
        if(self.Space.py_globalVariables.playerUpgrade == "Ice"):
            #set SnailType to "Ice"
            self.SnailType = "Ice"
            
        #if powerup is fire
        if(self.Space.py_globalVariables.playerUpgrade == "Fire"):
            #set SnailType to "Fire"
            self.SnailType = "Fire"
            
        #if powerup is lightning
        if(self.Space.py_globalVariables.playerUpgrade == "Lightning"):
            #set SnailType to "Lightning"
            self.SnailType = "Lightning"
            
        #if it is not ice, fire, or lightning, then change to normal
        if(self.Space.py_globalVariables.playerUpgrade == "Default"):
            #set SnailType to "Normal"
            self.SnailType = "Normal"

Zero.RegisterComponent("py_snail_animations", py_snail_animations)

