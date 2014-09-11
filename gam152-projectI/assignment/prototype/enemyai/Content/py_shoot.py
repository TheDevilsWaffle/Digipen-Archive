import Zero
import Events
import Property
import VectorMath
#shorthand for VectorMath.Vec3
Vec3 = VectorMath.Vec3

class py_shoot:
    #varaible to set the archetype for the projectile
    GooSprite = Property.Archetype()
    #variable to set the speed for the projectile
    GooSpeed = Property.Float(10.0)
    #variable to set the angle of the projectile
    GooAngle = Property.Float(5.0)
    
    def Initialize(self, initializer):
        pass
    ##############################################################
    #FUNCTION-SHOOT
    ##############################################################
    def Shoot(self):
        #Create the goo object
        goo = self.Space.Create(self.Goo)
        
        #if player is facing left
        if(self.Owner.py_character_movement.CharacterFlipRight == False):
            #Move it to the left of our position
            goo.Transform.Translation = self.Owner.Transform.Translation - Vec3(1.0, 0.0, 0.0)
            #Give the bullet an initial left velocity
            goo.RigidBody.Velocity = Vec3(-self.GooSpeed, self.GooAngle, 0.0)
        
        #if player is facing right
        if(self.Owner.py_character_movement.CharacterFlipRight == True):
            #Move it to the right of our position
            goo.Transform.Translation = self.Owner.Transform.Translation + Vec3(1.0, 0.0, 0.0)
            #Give the bullet an initial right velocity
            goo.RigidBody.Velocity = Vec3(self.GooSpeed, self.GooAngle, 0.0)

Zero.RegisterComponent("py_shoot", py_shoot)