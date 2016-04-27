#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
#################################################################################
#FILENAME:      py_collision_manager
#AUTHOR:        Travis Moore
#DESCRIPTION:   This handles all the collision events that happen to the snail.
#################################################################################

import Zero
import Events
import Property
import Keys
import math
import VectorMath

Vec3 = VectorMath.Vec3

class py_collision_manager:

    def Initialize(self, init):
        self.invTimer = 0
        #listen for collision events and perform OnCollisionStarted
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
        #Update event, used for invincibility functions.
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate);
        #listener for "Invincible" to come through the game space, will reset the invinsibility timer
        Zero.Connect(self.Space, "Invincible", self.InvincibleTimer)

    ################################################################################################
    #FUNCTION - OnCollisionStarted
    #DESCRIPTION - responsible for performing actions when the player hits an enemy/object
    ################################################################################################
    def OnCollisionStarted(self, collisionEvent):
        #set variable ObjectHit to the other object in the collision
        ObjectHit = collisionEvent.OtherObject
        #set a variable for the location on the players body that fits
        BodyLocation = collisionEvent.FirstPoint.BodyPoint
        
        #if the other object has objectsetting and is an enemy
        if(ObjectHit.py_objectSettings):
            
            if(ObjectHit.py_objectSettings.objectClass == "Enemy"):
                #if the other object is in a bubble
                if(ObjectHit.py_objectSettings.inBubble == True):
                    #if the player hits the object anywhere but the top
                    #if(BodyLocation.y > -0.25):
                    #destroy the object the player hit
                    ObjectHit.Destroy()
                    #remove it from the global variables list
                    self.Space.py_globalVariables.enemyList.remove(ObjectHit.Transform)
                    #give the player score for killing it
                    self.Space.py_globalVariables.score += 1
                    #if the player is hitting the enemy on the top, have it bounce off
                    #else:
                        #self.Owner.RigidBody.ApplyLinearImpulse(Vec3(0,20,0))
                #if the player is not in a bubble, have it damage the player
                elif(ObjectHit.py_objectSettings.inBubble == False and ObjectHit.Name != "SaltDropper" and self.Owner.py_objectSettings.canBeKilled == True):
                    self.Space.py_globalVariables.PlayerHealth -= ObjectHit.py_objectSettings.damageToPlayer
                    
    def OnLogicUpdate(self, Event):
        if(self.invTimer % 10 == 0):
            self.Owner.Sprite.Visible = True
        if(self.invTimer % 10 == 5):
            self.Owner.Sprite.Visible = False
        if(self.invTimer < 60):
            self.invTimer += 1
        elif(self.invTimer >= 60):
            self.Owner.py_objectSettings.canBeKilled = True
        
        
    def InvincibleTimer(self, SendEvent):
        self.invTimer = 0
        self.Owner.py_objectSettings.canBeKilled = False
    
Zero.RegisterComponent("py_collision_manager", py_collision_manager)