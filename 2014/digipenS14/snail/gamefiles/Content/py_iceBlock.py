#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_iceBlock
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This script controls all functions of the ice block
#               The ice block will kill any players or enemies it contacts
####################################################################################################
import Zero
import Events
import Property
import VectorMath

#shortcut
Vec3 = VectorMath.Vec3


class py_iceBlock:
    def Initialize(self, initializer):
        self.playerTimer = 1000
        self.snailObject = self.Space.FindObjectByName("Snail")
        #cog variable holding the snail object
        playerCog = self.Space.FindObjectByName("Snail")
        #calculate the direction of the player
        moveDirection = playerCog.Transform.Translation.x - self.Owner.Transform.Translation.x
        #if the player is to the right of the owner, push owner to the left
        if(moveDirection >= 0):
            self.Owner.RigidBody.ApplyLinearImpulse(Vec3(-15, 0, 0))
        #if the player is to the left of the owner, push owner to the right
        else:
            self.Owner.RigidBody.ApplyLinearImpulse(Vec3(15, 0, 0))
        
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted);
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate);

    def OnCollisionStarted(self, CollisionEvent):
        #shortcut
        otherObject = CollisionEvent.OtherObject
        #if the other object is not a tilemap
        if(otherObject.py_objectSettings):
            #if the other object is a player object, damage the player
            if(otherObject.py_objectSettings.objectClass == "Player" and self.playerTimer > 120):
                pushDirection = (otherObject.Transform.Translation - self.Owner.Transform.Translation) * 5
                otherObject.RigidBody.ApplyLinearVelocity(pushDirection)
                Zero.Disconnect(Zero.Keyboard, Events.KeyDown, otherObject.py_input_manager.OnKeyDown)
                otherObject.py_platformer_mechanics.Active = False
                otherObject.DynamicMotor.MoveInDirection(Vec3(0,0,0), Vec3(0,0,0))
                self.iceCog = self.Space.CreateAtPosition("arc_iceOverlay", otherObject.Transform.Translation - Vec3(0, 0.35, -1))
                self.playerTimer = 0
            #avoid taking any actions if it's another Ice Block
            elif(otherObject.Name == "IceBlock"):
                pass
            #if the other object is an enemy object, kill the enemy and create a new ice block
            elif(otherObject.py_objectSettings.objectClass == "Enemy"):
                self.Space.py_globalVariables.enemyList.remove(otherObject.Transform)
                otherObject.Destroy()
            #if the other object is a Projectile, propel the owner in its direction
            elif(otherObject.py_objectSettings.objectClass == "Projectile"):
                projDirection = otherObject.RigidBody.Velocity
                print(projDirection)
                otherObject.Destroy()
                self.Owner.RigidBody.ApplyLinearImpulse(projDirection)
        #prevent the ice block from reaching a Y velocity so high it warps through walls
        if(self.Owner.RigidBody.Velocity.y <= -25):
            self.Owner.RigidBody.Velocity = Vec3(0,-24,0)
            
    def OnLogicUpdate(self, UpdateEvent):
        if(self.playerTimer > 120):
            return
        elif(self.playerTimer < 120):
            self.playerTimer += 1
            self.iceCog.Transform.Translation = self.snailObject.Transform.Translation - Vec3(0, 0.35, -1)
        elif(self.playerTimer == 120):
            self.playerTimer += 1
            self.iceCog.Destroy()
            Zero.Connect(Zero.Keyboard, Events.KeyDown, self.snailObject.py_input_manager.OnKeyDown)
            self.snailObject.py_platformer_mechanics.Active = True

Zero.RegisterComponent("py_iceBlock", py_iceBlock)