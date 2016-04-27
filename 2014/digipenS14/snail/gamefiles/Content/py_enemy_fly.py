#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_enemy_basic
#AUTHOR:        Christopher Christensen
#DESCRIPTION:   This script holds all the functions necissary to controlling the fly enemy
#               The enemy will fly around the level and try to collide with the player
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import random

# Shortcut for Vector 3
Vec3 = VectorMath.Vec3;


class py_enemy_fly:
    #Timer that the player can input # of logic updates before the owner is no longer in bubble
    bubbleTimer = Property.Int(300)
    
    def Initialize(self, initializer):
        
        # Set the fly to be a ghost
        self.Owner.Collider.Ghost = False;
        self.Owner.Sprite.OnTop = True;
        self.Owner.RigidBody.RotationLocked = True;
        
        # Records the players current location
        self.vCurrentLoc = Vec3(self.Owner.Transform.Translation.x, self.Owner.Transform.Translation.y, 0);
        
        # Set the objects initial locations
        self.Owner.Transform.Translation = self.vCurrentLoc;
        
        # Set variable for where the fly is going
        self.vPreviousLoc = self.vCurrentLoc;
        
        # Set the fly's speed
        self.iSpeed = 2;
        
        # Set initial state (Idle, Moving)
        self.enemyState = "Idle";
        self.idleTimer = random.randint(0, 3);
        
        # Set the initial returnDir
        self.returnDir = Vec3(0, 0, 0);
        
        # Set the initial timer
        self.timer = 0;
        
        # Set level edges
        self.SCREEN_LEFT = -25;
        self.SCREEN_RIGHT = 25;
        self.SCREEN_TOP = -12;
        self.SCREEN_BOTTOM = 12;
        
        # Run an update loop every frame
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate);
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnHitPlayer);
        pass
        
    ########################################################
    #   Function  - OnLogicUpdate
    #
    #   Purpose   - Run other functions based upon a timer
    #
    #   Parameters
    #           - Self
    ########################################################
    def OnLogicUpdate(self, UpdateEvent):
        
        # Timer
        self.timer += UpdateEvent.Dt;
        #if the fly has been gooed
        if(self.Owner.py_objectSettings.inBubble == True):
            #start the timer
            self.Owner.py_objectSettings.iTimer += 1
            #send the enemy moving into the ground
            self.Owner.RigidBody.ApplyImpulse(Vec3(0,-0.5,0), Vec3(0,0,0))
            #alter the flies current location
            self.vCurrentLoc = self.Owner.Transform.Translation
            #limit terminal velocity
            if(self.Owner.RigidBody.Velocity.y <= -25):
                self.Owner.RigidBody.Velocity = Vec3(0, -24, 0)
            #if the timer has reached the amount of the bubble timer
            if(self.Owner.py_objectSettings.iTimer >= self.bubbleTimer):
                #take fly out of bubble
                self.Owner.py_objectSettings.inBubble = False
                #reset timer for future use
                self.Owner.py_objectSettings.iTimer = 0
                #remove the gooed overlay
                self.Owner.FindChildByName("Gooed").Destroy()
        
        # If the fly is moving
        elif(self.enemyState == "Fly"):
            #ensure the fly is rightside up while moving.
            self.Owner.Sprite.FlipY = False
            # Determine random time that the fly will move
            self.movementLength = (random.random() + .25) * 3;
            
            # After the fly has remained idle for a certain amount of time...
            if(self.timer <= self.movementLength):
                
                if(self.Owner.Transform.Translation.x < self.SCREEN_LEFT or 
                    self.Owner.Transform.Translation.x > self.SCREEN_RIGHT):
                        self.flyVector.x *= -1;
                if(self.Owner.Transform.Translation.y < self.SCREEN_TOP or
                    self.Owner.Transform.Translation.y > self.SCREEN_BOTTOM):
                        self.flyVector.y *= -1;
                        
                # The fly is moving
                self.vCurrentLoc.x += self.flyVector.x * UpdateEvent.Dt * self.iSpeed;
                self.vCurrentLoc.y += self.flyVector.y * UpdateEvent.Dt * self.iSpeed;
                
            else:
                # Reset Timer
                self.timer = 0;
                
                # Set the enemyState
                self.enemyState = "Idle";
                
                # Randomize how long to be idle
                self.idleTimer = random.randint(0, 3);
                pass
            # Update the current location
            self.Owner.Transform.Translation = self.vCurrentLoc;
            
        # Otherwise, the fly is not moving
        elif(self.enemyState == "Idle"):
            # This only runs when the timer has passed the set amount of time
            if(self.timer >= self.idleTimer):
                # Reset the Timer
                self.timer = 0;
                
                # Set the enemyState
                self.enemyState = "Fly";
                
                # Run the FlyLocation function
                self.flyVector = self.FlyLocation(random.randint(1, 4));
            # Update the current location
            self.Owner.Transform.Translation = self.vCurrentLoc;
            
            pass
        
        pass
        
    ########################################################
    #   Function  - FlyLocation
    #
    #   Purpose   - Determine the direction the fly will move
    #
    #   Parameters
    #           - Self
    #           - findLocation - An int between 1 and 4
    ########################################################
    def FlyLocation(self, findLocation):
        # Set the direction the fly will move (Default Up & Right)
        self.xDir = 1;
        self.yDir = 1;
        if(findLocation == 1):
            self.xDir = 1;
            self.yDir = 1;
        if(findLocation == 2):
            self.xDir = -1;
            self.yDir = 1;
        if(findLocation == 3):
            self.xDir = -1;
            self.yDir = -1;
        if(findLocation == 4):
            self.xDir = 1;
            self.yDir = -1;
            
        # Multiply by the speed of the fly
        self.xDir *= 4;
        self.yDir *= 1;
        
        # Sort all information
        self.returnDir = Vec3(self.xDir, self.yDir, 0);
        
        # Return final solution
        return self.returnDir;
        
    ########################################################
    #   Function  - OnHitPlayer
    #
    #   Purpose   - If the fly hits the player
    #
    #   Parameters
    #           - Self
    #           - findLocation - An int between 1 and 4
    ########################################################
    def OnHitPlayer(self, collisionObject):
        otherObject = collisionObject.OtherObject;
        if(self.Owner.py_objectSettings.inBubble == False):
            if(otherObject.py_objectSettings):
                if(otherObject.py_objectSettings.objectClass == "Player"):
                #if(otherObject == self.Space.FindObjectByName("Snail")):
                    #self.Space.FindObjectByName("Snail").Destroy();
                    self.Space.py_globalVariables.enemyList.remove(self.Owner.Transform)
                    self.Owner.Destroy()
                    pass
                #if(otherObject.py_
        pass

Zero.RegisterComponent("py_enemy_fly", py_enemy_fly)
