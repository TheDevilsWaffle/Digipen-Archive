import Zero
import Events
import Property
import VectorMath
#shorthand for VectorMath.Vec3
Vec3 = VectorMath.Vec3


class py_shoot:

    #varaible to set the sprite archetype for the projectile
    GooSprite = Property.Archetype()
    #variable to set the speed for the projectile
    GooSpeed = Property.Float(10.0)
    #variable to set the angle of the projectile
    GooAngle = Property.Float(5.0)

    def Initialize(self, initializer):
        pass

    ################################################################################################
    #FUNCTION - Shoot
    #DESCRIPTION - responsible for creating a goo projectile and shooting it based on direction
    ################################################################################################
    def Shoot(self):
        #Create the goo object
        Goo = self.Space.Create(self.GooSprite)

        #if player's sprite is facing left
        if(self.Owner.py_character_movement.CharacterFlipRight == False):
            #move the goo to the left of our position
            Goo.Transform.Translation = self.Owner.Transform.Translation - Vec3(1.0, 0.0, 0.0)
            #give the goo an initial left velocity
            Goo.RigidBody.Velocity = Vec3(-self.GooSpeed, self.GooAngle, 0.0)

        #if player's sprite is facing right
        if(self.Owner.py_character_movement.CharacterFlipRight == True):
            #move the goo to the right of our position
            Goo.Transform.Translation = self.Owner.Transform.Translation + Vec3(1.0, 0.0, 0.0)
            #give the goo an initial right velocity
            Goo.RigidBody.Velocity = Vec3(self.GooSpeed, self.GooAngle, 0.0)

Zero.RegisterComponent("py_shoot", py_shoot)