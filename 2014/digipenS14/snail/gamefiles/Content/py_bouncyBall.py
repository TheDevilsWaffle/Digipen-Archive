#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
#################################################################################
#FILENAME:      py_bouncyBall
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This is for an enemy class that moves at diagonals, bouncing off
#               anything in its path.
#################################################################################

import Zero
import Events
import Property
import VectorMath

#shortcut
Vec3 = VectorMath.Vec3

class py_bouncyBall:
    #Timer that the player can input # of logic updates before the owner is no longer in bubble
    bubbleTimer = Property.Int(300)
    
    def Initialize(self, initializer):
        self.ownVelocity = Vec3(0,0,0)
        
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)

    def OnLogicUpdate(self, Event):
        #if the object is not in a bubble
        if(self.Owner.py_objectSettings.inBubble == False):
            #store its velocity for later use
            self.ownVelocity = self.Owner.RigidBody.Velocity
            
            #normalize velocity
            if(self.ownVelocity.x < 0):
                self.Owner.RigidBody.Velocity = Vec3(-5, self.ownVelocity.y, 0)
                self.ownVelocity = self.Owner.RigidBody.Velocity
            if(self.ownVelocity.x >= 0):
                self.Owner.RigidBody.Velocity = Vec3(5, self.ownVelocity.y, 0)
                self.ownVelocity = self.Owner.RigidBody.Velocity
            if(self.ownVelocity.y < 0):
                self.Owner.RigidBody.Velocity = Vec3(self.ownVelocity.x, -5, 0)
                self.ownVelocity = self.Owner.RigidBody.Velocity
            if(self.ownVelocity.y >= 0):
                self.Owner.RigidBody.Velocity = Vec3(self.ownVelocity.x, 5, 0)
                self.ownVelocity = self.Owner.RigidBody.Velocity
        
        #if the enemy is in a bubble
        else:
            #start the timer
            self.Owner.py_objectSettings.iTimer += 1
            #send the enemy moving into the ground
            self.Owner.RigidBody.ApplyImpulse(Vec3(0,-0.5,0), Vec3(0,0,0))
            if(self.Owner.RigidBody.Velocity.y <= -25):
                self.Owner.RigidBody.Velocity = Vec3(0, -24, 0)
            #disconnect the Collision event. This is so it won't kill the player still
            Zero.Disconnect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
        #when the timer runs out, take the player out of the bubble, and undo everything we've done above
        if(self.Owner.py_objectSettings.iTimer >= self.bubbleTimer):
            self.Owner.py_objectSettings.inBubble = False
            self.Owner.py_objectSettings.iTimer = 0
            self.Owner.RigidBody.Velocity = self.ownVelocity
            self.Owner.FindChildByName("Gooed").Destroy()
            Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)

    def OnCollisionStarted(self, CollisionEvent):
        #shortcuts
        otherObject = CollisionEvent.OtherObject
        bodyLocation = CollisionEvent.FirstPoint.BodyPoint
        
        #if the other object is a player object, hurt the player
        if(otherObject.py_objectSettings):
            self.Owner.RigidBody.Velocity = Vec3(self.ownVelocity.x, self.ownVelocity.y, 0)
        #if the other object is a wall
        else:
            #if the enemy hit its top or bottom, mirror in that direction
            if(bodyLocation.y > 0.25 or bodyLocation.y < -0.25):
                self.Owner.RigidBody.Velocity = Vec3(self.ownVelocity.x, -self.ownVelocity.y, 0)
            #if the enemy hit its left or right, mirror in that direction
            if(bodyLocation.x > 0.25 or bodyLocation.x < -0.25):
                self.Owner.RigidBody.Velocity = Vec3(-self.ownVelocity.x, self.ownVelocity.y, 0)

Zero.RegisterComponent("py_bouncyBall", py_bouncyBall)