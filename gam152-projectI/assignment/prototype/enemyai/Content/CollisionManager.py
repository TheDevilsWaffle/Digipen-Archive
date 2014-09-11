import Zero
import Events
import Property
import VectorMath
import Keys
Vec3 = VectorMath.Vec3

class CollisionManager:
    def Initialize(self, init):
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
    
    def OnCollisionStarted(self, collisionEvent):
        #Get the other object in the collision
        objectHit = collisionEvent.OtherObject
        
        #basic enemy dispatching
        if(objectHit.py_enemyAI_basic):
            contactPoint = collisionEvent.FirstPoint
            bodyPoint = contactPoint.BodyPoint
            
            #if player is above the enemy (a.k.a. mario jump)
            if(bodyPoint.y < -0.3):
                #if player is holding on to W still
                if(Zero.Keyboard.KeyIsDown(Keys.W)):
                    #make the player jump again without killing the enemy
                    self.Owner.py_character_movement.Jump()
                    
                #player has let go of jump, kill the enemy
                else:
                    #kill the enemy
                    objectHit.Destroy()
            else:
                self.Respawn()
                
    def Respawn(self):
        self.Space.ReloadLevel();

Zero.RegisterComponent("CollisionManager", CollisionManager)