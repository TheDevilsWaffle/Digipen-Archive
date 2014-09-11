#####       This is all of the functions necessary for projectiles of all types     #####
#########################################################################################
import Zero
import Events
import Property
import VectorMath

class py_projectileLogic:
    #set the projectile speed
    Speed = Property.Float(20.0)
    #set the amount of damage the projectile does
    Damage = Property.Float(2.0)
    #initial value of lifespan timer
    LifeSpan = Property.Int(0)
    #Maximum lifetime of a given projectile
    MaxLifeSpan = Property.Int(100)
    
    def Initialize(self, initializer):
        #create an event listener for "TargetAcquiredEvent"
        Zero.Connect(self.Owner, "clickEvent", self.OnClick)
        #event listener for collision
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
        #Zero.Connect(self.Space, Events.LogicUpdate, self.LifeTime)
        
    def OnClick(self, clickEvent):
        #move the projectile in the direction of the mouse
        self.Owner.RigidBody.ApplyLinearVelocity(self.Speed * clickEvent.direction)
        
    def OnCollisionStarted(self, CollisionEvent):
        #shortcut
        otherObject = CollisionEvent.OtherObject
        #if the other object has ObjectSettings
        if(otherObject.py_objectSettings):
            #if the other objects class is Enemy
            if(otherObject.py_objectSettings.objectClass == "Enemy"):
                #destroy the projectile
                self.Owner.Destroy()
                #if it's hitting an enemy class, damage the enemy
                otherObject.py_objectSettings.objectHealth -= self.Damage
        #if the other object does not have ObjectSettings, destroy the projectile
        else:
            self.Owner.Destroy()
    #
    ##### LIFETIME is made primarily  for death field, but can determine the life span of ANY bullet
    #
    def LifeTime(self, UpdateEvent):
        if(self.LifeSpan >= self.MaxLifeSpan):
            self.Owner.Destroy()
        self.LifeSpan += 1

Zero.RegisterComponent("py_projectileLogic", py_projectileLogic)