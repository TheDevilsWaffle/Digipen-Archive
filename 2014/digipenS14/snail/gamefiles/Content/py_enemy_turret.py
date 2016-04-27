#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_enemy_turret
#AUTHOR:        Christopher Christensen
#DESCRIPTION:   This function holds all the functions necessary for controlling turrets
#               The turret will be rotated dependent upon it's chosen direction
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import random;
import math;

# Shortcut for Vector 3
Vec3 = VectorMath.Vec3;

# Different angles to be attached from
firingDirection = ["Up", "Down", "Left", "Right"];
wallDirection = ["Up", "Down", "Left", "Right"];


class py_enemy_turret:
    
    # Set the initial angle to fire from
    DirectionToFire = Property.Enum(enum = firingDirection);
    WhereIsWall = Property.Enum(enum = wallDirection);
    
    def Initialize(self, initializer):
        # Set const variables
        self.TIME_LIMIT_IDLE = 1.5;
        self.TIME_LIMIT_IDLE_SHOOTING = .2;
        self.NUM_SHOTS = 3;
        
        # Create variables
        self.wallAdjustmentX = 0;
        self.wallAdjustmentY = 0;
        self.bubbleTime = 300;
        
        # Create AntTurret Variable
        self.antTurret = self.Owner.FindChildByName("AntTurret");
        
        ##############################################
        #
        # Hill/Base Information
        #
        ##############################################
        
        # Ensure that the turret remains on top and doesn't rotate
        self.Owner.Sprite.OnTop = True;
                                
        # Alter physical location dependant on wall location
        if(self.WhereIsWall == "Down"):
            self.wallAdjustmentX = -.5;
            self.vCurrentRotation = 0;
            
        if(self.WhereIsWall == "Up"):
            self.wallAdjustmentX = .5;
            self.vCurrentRotation = 180;
            
        if(self.WhereIsWall == "Left"):
            self.wallAdjustmentY = .5;
            self.vCurrentRotation = 270;
            
        if(self.WhereIsWall == "Right"):
            self.wallAdjustmentY = -.5;
            self.vCurrentRotation = 90;
            
        # Submit the current direction to the rotation of the Turret
        self.Owner.Transform.Rotation = VectorMath.Quat.EulerXYZ(0, 0, 
                                        math.radians(self.vCurrentRotation));
        
        ##############################################
        #
        # Turret Information
        #
        ##############################################
        
        # Current State
        self.turretState = "Idle";
        
        # Timer variable that determines the current phase
        self.timerState = 0;
        
        # Force Z location to be 0
        self.vCurrentLoc = Vec3(self.Owner.Transform.Translation.x + self.wallAdjustmentX, 
                                self.Owner.Transform.Translation.y + self.wallAdjustmentY, 
                                0);
                                
        # Set the initial rotation dependant on the direction stated
        if(self.WhereIsWall == "Up"):
            if(self.DirectionToFire == "Left"):
                self.vTurretRotation = -25;
                self.antTurret.Sprite.FlipX = True;
                
            if(self.DirectionToFire == "Right"):
                self.vTurretRotation = 205;
                self.antTurret.Sprite.FlipY = True;
                self.antTurret.Sprite.FlipX = True;
                
            if(self.DirectionToFire == "Down"):
                self.vTurretRotation = 283; #283
                
        if(self.WhereIsWall == "Down"):
            if(self.DirectionToFire == "Left"):
                self.vTurretRotation = 0;
                
            if(self.DirectionToFire == "Right"):
                self.vTurretRotation = 180;
                self.antTurret.Sprite.FlipY = True;
                    
            if(self.DirectionToFire == "Up"):
                self.vTurretRotation = 285;
            
        if(self.WhereIsWall == "Left"):
            if(self.DirectionToFire == "Right"):
                self.vTurretRotation = 270;
                self.antTurret.Sprite.FlipY = True;
                    
            if(self.DirectionToFire == "Up"):
                self.vTurretRotation = 10;
                
            if(self.DirectionToFire == "Down"):
                self.vTurretRotation = 170;
                self.antTurret.Sprite.FlipY = True;
                    
        if(self.WhereIsWall == "Right"):
            if(self.DirectionToFire == "Left"):
                self.vTurretRotation = 270;
                    
            if(self.DirectionToFire == "Up"):
                self.vTurretRotation = 347;
                self.antTurret.Sprite.FlipX = True;
                
            if(self.DirectionToFire == "Down"):
                self.vTurretRotation = 192;
                self.antTurret.Sprite.FlipX = True;
                self.antTurret.Sprite.FlipY = True;
                
        self.Owner.Transform.Translation = self.vCurrentLoc;
            
        self.antTurret.Transform.Rotation = VectorMath.Quat.EulerXYZ(0, 0, 
                                        math.radians(self.vTurretRotation));

        # Run the update loop every frame
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate);
        
    ########################################################
    #   Function  - OnLogicUpdate
    #
    #   Purpose   - Run other functions based upon a timer
    #
    #   Parameters
    #           - Self
    #           - UpdateEvent: Update loop variable
    ########################################################
    def OnLogicUpdate(self, UpdateEvent):

        self.antTurret.Sprite.OnTop = True;
        
        # Check to see if the turret was hit by a bubble
        # Find the turret and check to se
        if(self.Owner.py_objectSettings.inBubble == True):
            self.turretState = "Hit";

    # Run through current states
        # Idle State
        if(self.turretState == "Idle"):
            
            # If the turret is still Idle...
            if(self.timerState <= self.TIME_LIMIT_IDLE):
                # ... Then update the timer.
                self.timerState += (UpdateEvent.Dt);
                
            # Otherwise, we're no longer Idle.
            else:
                # Reset the timer
                self.timerState = 0;
                
                # Set the number of shots fired to 0
                self.shotsFired = 0;
                
                # Set ShootingState to 'Fire'
                self.shootingState = "Fire";
                
                # Set the turretState to shooting
                self.turretState = "Shooting";
            
        # Shooting State
        if(self.turretState == "Shooting"):
            
            # Determine that we have shot fewer than the current number of shots
            if(self.shotsFired < self.NUM_SHOTS):
                
                # FIRE THE TURRET, not idle
                if(self.shootingState == "Fire"):
                    
                    # Fire a projectile
                    self.OnShooting();
                    
                    # Set the idleTimer to 0;
                    self.fireIdleTimer = 0;
                    
                    # Set 'Firing State' to idle
                    self.shootingState = "Idle";
                
                # THE TURRET IS TAKING A SHORT BREAK, not firing
                if(self.shootingState == "Idle"):
                    
                    # This is the short break between shots
                    if(self.fireIdleTimer < self.TIME_LIMIT_IDLE_SHOOTING):
                        
                        # Increment the timer
                        self.fireIdleTimer += UpdateEvent.Dt;
                        
                    # We're done taking a short break. Fire the turret.
                    else:
                        # Set the Shooting State (Fires the Turret)
                        self.shootingState = "Fire";
                        
                        # Increment the number of shots fired
                        self.shotsFired += 1;
            
            else:
                # We've broken out of the 'Shooting' turretState
                self.turretState = "Idle";
        
        # Disable the turret while it's hit
        if(self.turretState == "Hit"):
                # Don't flip the base
                self.Owner.Sprite.FlipY = False;
                self.Owner.Sprite.FlipX = False;
                
                # Actions to perform if the timer expires
                if(self.Owner.py_objectSettings.iTimer >= self.bubbleTime):
                    
                    # Turret is no longer restricted
                    self.Owner.py_objectSettings.inBubble = False;
                    
                    # Reset the timer
                    self.Owner.py_objectSettings.iTimer = 0;
                    
                    # Destroy the goo object
                    self.Owner.FindChildByName("Gooed").Destroy();
                    
                    # Change the turretState
                    self.turretState = "Idle";
                    
                    # Don't flip the base
                    self.Owner.Sprite.FlipY = False;
                    self.Owner.Sprite.FlipX = False;
                    
                #add 1 to the timer every logic update that it's in a bubble.
                self.Owner.py_objectSettings.iTimer += 1
                
    ########################################################
    #   Function  - OnShooting
    #
    #   Purpose   - Fire a projectile
    #
    #   Parameters
    #           - Self
    ########################################################
    def OnShooting(self):
        
        # self.antTurret.py_enemy_turret_fire.OnShooting(self.DirectionToFire, self.Owner.Transform.Translation + self.antTurret.Transform.Translation);
        # VectorMath.Quat.EulerXYZ(0, 0, math.radians(self.vCurrentRotation))
        self.antTurret.py_enemy_turret_fire.OnShooting(self.DirectionToFire, Vec3(self.antTurret.Transform.WorldTranslation.x, 
                                                        self.antTurret.Transform.WorldTranslation.y, 
                                                        0));

Zero.RegisterComponent("py_enemy_turret", py_enemy_turret)