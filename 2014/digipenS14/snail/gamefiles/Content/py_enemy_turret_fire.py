#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_enemy_turret_fire
#AUTHOR:        Christopher Christensen
#DESCRIPTION:   This function creates the projectile that shoots according to the DirectionToFire
####################################################################################################
import Zero
import Events
import Property
import VectorMath
Vec3 = VectorMath.Vec3;


class py_enemy_turret_fire:
    def Initialize(self, initializer):
        pass
        
    # Launch a projectile
    def OnShooting(self, DirectionToFire, TurretLocation):
        self.projectileCog = self.Space.CreateAtPosition("arc_saltPellet", TurretLocation);
        
        if(DirectionToFire == "Up"):
            #send the enemy moving into the ground
            self.projectileCog.RigidBody.ApplyImpulse(Vec3(0,1.5,0), Vec3(0,0,0));
            
        if(DirectionToFire == "Down"):
            # Fire the projectile toward the ground
            self.projectileCog.RigidBody.ApplyImpulse(Vec3(0,-1,0), Vec3(0,0,0));
            
        if(DirectionToFire == "Left"):
            #send the enemy moving into the ground
            self.projectileCog.RigidBody.ApplyImpulse(Vec3(-1,0.3,0), Vec3(0,0,0));
            
        if(DirectionToFire == "Right"):
            #send the enemy moving into the ground
            self.projectileCog.RigidBody.ApplyImpulse(Vec3(1,0.3,0), Vec3(0,0,0));

        pass

Zero.RegisterComponent("py_enemy_turret_fire", py_enemy_turret_fire)