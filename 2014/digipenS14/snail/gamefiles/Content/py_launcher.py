#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_launcher
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This is attached to an object, and will have that object propel the player
####################################################################################################
import Zero
import Events
import Property
import VectorMath
Vec3 = VectorMath.Vec3

class py_launcher:
    #a variable determining launch force to apply
    launchForce = Property.Float(22)
    #should this launcher push the player upwards?
    launchUp = Property.Bool(True)
    #downwards?
    launchDown = Property.Bool(False)
    #move to the left?
    launchLeft = Property.Bool(False)
    #move to the right?
    launchRight = Property.Bool(False)
    
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted);

    def OnCollisionStarted(self, CollisionEvent):
        #shortcut
        otherObject = CollisionEvent.OtherObject
        #if the object is not a tilemap
        if(otherObject.py_objectSettings):
            #if the object is a player
            if(otherObject.py_objectSettings.objectClass == "Player"):
                #apply the proper force based on the chosen booleans
                otherObject.RigidBody.Velocity = Vec3(0,0,0)
                if(self.launchUp == True):
                    otherObject.RigidBody.ApplyLinearVelocity(Vec3(0, self.launchForce, 0))
                if(self.launchLeft == True):
                    otherObject.RigidBody.ApplyLinearVelocity(Vec3(-self.launchForce, 0, 0))
                if(self.launchRight == True):
                    otherObject.RigidBody.ApplyLinearVelocity(Vec3(self.launchForce, 0, 0))
                if(self.launchDown == True):
                    otherObject.RigidBody.ApplyLinearVelocity(Vec3(0, -self.launchForce, 0))

Zero.RegisterComponent("py_launcher", py_launcher)