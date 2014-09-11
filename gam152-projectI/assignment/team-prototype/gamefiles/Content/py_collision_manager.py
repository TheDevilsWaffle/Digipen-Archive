import Zero
import Events
import Property
import Keys


class py_collision_manager:

    def Initialize(self, init):
        #listen for collision events and perform OnCollisionStarted
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
        pass

    ################################################################################################
    #FUNCTION - OnCollisionStarted
    #DESCRIPTION - responsible for performing actions when the player hits an enemy/object
    ################################################################################################
    def OnCollisionStarted(self, collisionEvent):
        #set variable ObjectHit to the other object in the collision
        ObjectHit = collisionEvent.OtherObject
        
        #if enemy hit was a basic enemy (currently spiders)
        if(ObjectHit.py_enemy_basic):
            #set variable ContactPoint = the first point of contact
            PointOfContact = collisionEvent.FirstPoint
            #set variable BodyPoint = the body point of the 
            BodyLocation = PointOfContact.BodyPoint

            #if player is above the enemy (a.k.a. mario jump)
            if(BodyLocation.y < -0.3):
                #if player is holding on to jump (W) while contact is made
                if(Zero.Keyboard.KeyIsDown(Keys.W)):
                    #make the player jump again without killing the enemy
                    self.Owner.py_character_movement.Jump()

                #player has let go of jump when contact was made, kill the enemy
                else:
                    #kill the enemy
                    ObjectHit.Destroy()
                

Zero.RegisterComponent("py_collision_manager", py_collision_manager)