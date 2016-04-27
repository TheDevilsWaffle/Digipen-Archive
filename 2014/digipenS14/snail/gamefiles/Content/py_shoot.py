#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_shoot
#AUTHOR:        Travis Moore
#DESCRIPTION:   used to set attributes on basic goo and fire projectiles created by the snail
####################################################################################################
import Zero
import Events
import Property
import VectorMath

#shorthand for VectorMath.Vec3
Vec3 = VectorMath.Vec3


class py_shoot:
    #varaible to set the sprite archetype for the projectile
    GooSprite = Property.Archetype(default="arc_bubble")
    
    #variable to set the speed for the projectile
    GooSpeed = Property.Float(10.0)
    #variable to set the angle of the projectile
    GooAngle = Property.Float(3.0)
    #variable to set the starting y position of the projectile
    GooStartingYPos = Property.Float(0.5)
    #variable to set the starting x position of the projectile
    GooStartingXPos = Property.Float(0.5)
    
    #variable to set the speed for the projectile
    FireSpeed = Property.Float(5.0)
    #variable to set the angle of the fire
    FireAngle = Property.Float(0.0)
    #variable to set the starting y position of the fire
    FireStartingYPos = Property.Float(0.9)
    #variable to set the starting x position of the fire
    FireStartingXPos = Property.Float(2.0)
    #variable to set the starting y position of the fire
    FireBlowBackForceXPos = Property.Float(25.0)
    #variable to set the starting x position of the fire
    FireBlowBackForceYPos = Property.Float(2.5)

    def Initialize(self, initializer):
        #variable set to the Snail
        self.Snail = self.Space.FindObjectByName("Snail")
        pass

    ################################################################################################
    #FUNCTION - Shoot
    #DESCRIPTION - responsible for creating a goo projectile and shooting it based on direction
    ################################################################################################
    def Shoot(self):
        #if the current powerup is normal
        if(self.Snail.py_shoot.GooSprite == "arc_firebreath"):
            #Create the fire object
            Fire = self.Space.Create(self.GooSprite)
            #if player is facing left
            if(self.Owner.Sprite.FlipX):
                #flip the fire sprite for the correct direction (left)
                Fire.Sprite.FlipX = False
                
                #move the fire to the right of our position
                Fire.Transform.Translation = self.Snail.Transform.Translation + Vec3(self.FireStartingXPos, self.FireStartingYPos, 0.0)
                #give the fire an initial right velocity
                Fire.RigidBody.Velocity = Vec3(self.FireSpeed, self.FireAngle, 0.0)
                
                #blast the player back to the right
                self.Snail.RigidBody.Velocity += (Vec3(-self.FireBlowBackForceXPos, self.FireBlowBackForceYPos, 0))
            
            #if player is facing right
            else:
                #flip the fire sprite for the correct direction (right)
                Fire.Sprite.FlipX = True
                
                #move the fire to the left of our position
                Fire.Transform.Translation = self.Snail.Transform.Translation + Vec3(-self.FireStartingXPos, self.FireStartingYPos, 0.0)
                #give the fire an initial left velocity
                Fire.RigidBody.Velocity = Vec3(-self.FireSpeed, self.FireAngle, 0.0)
                
                #blast the player back to the left
                self.Snail.RigidBody.Velocity += (Vec3(self.FireBlowBackForceXPos, self.FireBlowBackForceYPos, 0))
        
        #currently using the normal goo
        else:
            #Create the goo object
            Goo = self.Space.Create(self.GooSprite)
            
            #if player's sprite is facing left
            if(self.Owner.Sprite.FlipX):
                #move the goo to the right of our position
                Goo.Transform.Translation = self.Owner.Transform.Translation + Vec3(self.GooStartingXPos, self.GooStartingYPos, 0.0)
                Goo.RigidBody.ApplyAngularVelocity(Vec3(0, 0, 5))
                #give the goo an initial right velocity
                Goo.RigidBody.Velocity = Vec3(self.GooSpeed, self.GooAngle, 0.0)
                
            #if player's sprite is facing right
            else:
                #move the goo to the left of our position
                Goo.Transform.Translation = self.Owner.Transform.Translation + Vec3(-self.GooStartingXPos, self.GooStartingYPos, 0.0)
                #give the goo an initial left velocity
                Goo.RigidBody.Velocity = Vec3(-self.GooSpeed, self.GooAngle, 0.0)
                Goo.RigidBody.ApplyAngularVelocity(Vec3(0, 0, 5))

Zero.RegisterComponent("py_shoot", py_shoot)