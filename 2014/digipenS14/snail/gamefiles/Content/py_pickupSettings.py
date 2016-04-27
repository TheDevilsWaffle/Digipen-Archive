#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_pickupSettings
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   information about creating a pickup object, and having it effect the player
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import Color
Vec3 = VectorMath.Vec3


#list of upgrade types
typeList = ["Ice", "Fire", "Lightning"]

class py_pickupSettings:
    #pulldown list for ease of use
    PickUpType = Property.Enum(enum = typeList)
    
    def Initialize(self, initializer):
        #change the sprite of the pickup depending on its type
        if(self.PickUpType == "Ice"):
            self.Owner.Sprite.SpriteSource = "spr_potion_blue"
        if(self.PickUpType == "Fire"):
            self.Owner.Sprite.SpriteSource = "spr_potion_red"
        if(self.PickUpType == "Lightning"):
            self.Owner.Sprite.SpriteSource = "spr_potion_yellow"
            
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted);


    def OnCollisionStarted(self, CollisionEvent):
        otherObject = CollisionEvent.OtherObject
        
        #ignore all collision except for player
        if(otherObject.py_objectSettings.objectClass == "Player"):
            #update the global variable so it stores across levels
            self.Space.py_globalVariables.playerUpgrade = self.PickUpType
            #depending on type, change projectile
            if(self.PickUpType == "Ice"):
                self.Space.FindObjectByName("Snail").py_shoot.GooSpeed = 15
                self.Space.FindObjectByName("Snail").py_shoot.GooSprite = "arc_ice"
                
                #Travis' Code
                #alert the py_snail_animations pyscript so we can change to the right snail type
                self.Space.FindObjectByName("Snail").py_snail_animations.ChangeSnailType()
                #swap out the snail idle sprite right away
                self.Space.FindObjectByName("Snail").py_snail_animations.SnailIdle()
            
            #change to fire
            if(self.PickUpType == "Fire"):
                self.Space.FindObjectByName("Snail").py_shoot.GooSprite = "arc_firebreath"
                self.Space.FindObjectByName("Snail").py_shoot.GooSpeed = 30
                
                #Travis' Code
                #alert the py_snail_animations pyscript so we can change to the right snail type
                self.Space.FindObjectByName("Snail").py_snail_animations.ChangeSnailType()
                #swap out the snail idle sprite right away
                self.Space.FindObjectByName("Snail").py_snail_animations.SnailIdle()
            
            #change to lightning
            if(self.PickUpType == "Lightning"):
                self.Space.FindObjectByName("Snail").py_shoot.GooSprite = "arc_lightning"
                self.Space.FindObjectByName("Snail").py_shoot.GooSpeed = 2
                
                #Travis' Code
                #alert the py_snail_animations pyscript so we can change to the right snail type
                self.Space.FindObjectByName("Snail").py_snail_animations.ChangeSnailType()
                #swap out the snail idle sprite right away
                self.Space.FindObjectByName("Snail").py_snail_animations.SnailIdle()
                
            #remove the upgrade item from the screen
            self.Owner.Destroy()

Zero.RegisterComponent("py_pickupSettings", py_pickupSettings)