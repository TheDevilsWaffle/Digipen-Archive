#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
#############################################################################################
#FILENAME:      py_fire_logic
#AUTHOR:        Travis Moore
#DIRECTION:     Controls the fire projectile, and creates ash animations where enemies used to be
#############################################################################################

import Zero
import Events
import Property
import VectorMath
import Action

#shorthand for VectorMath.Vec3
Vec3 = VectorMath.Vec3


class py_fire_logic:
    def Initialize(self, initializer):
        # Set the initial timer
        self.timer = 0.0
        #variable set to the Snail
        self.Snail = self.Space.FindObjectByName("Snail")
        #events
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted);
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate);
        
    def OnLogicUpdate(self, UpdateEvent):
        self.timer += UpdateEvent.Dt
        #keep flame going for as long as player is holding down the button or until timer is up
        if(self.Snail.py_input_manager.IsBurping == False and self.timer >=0.5):
            #destroy fire sprite on screen
            self.Owner.Destroy()
    
    def OnCollisionStarted(self, CollisionEvent):
        otherObject = CollisionEvent.OtherObject
        
        #If the other object has ObjectSettings, and is an enemy, tell it that it's been BURNED BABY!
        if(otherObject.py_objectSettings):
            if(otherObject.py_objectSettings.objectClass == "Enemy"):
                #create ash sprite
                ashCog = self.Space.Create("arc_ash")
                #set ash sprite to former location of enemy
                ashCog.Transform.Translation = otherObject.Transform.Translation
                #remove enemy from enemies array
                self.Space.py_globalVariables.enemyList.remove(otherObject.Transform)
                #destroy the enemy
                otherObject.Destroy()
                #adjust size of ash sprite
                ashCog.Transform.Scale = Vec3(0.75, 0.75, 0)
                #move the ash sprite down slightly
                ashCog.Transform.Translation -= Vec3(0, 0.25, 0)

Zero.RegisterComponent("py_fire_logic", py_fire_logic)