#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_sCargo
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This is a box that will drop, killing the player if he's underneath.
#               It will also explode on contact, dropping salt pellets all around.
####################################################################################################
import Zero
import Events
import Property
import VectorMath
#shortcut
Vec3 = VectorMath.Vec3


class py_sCargo:
    #number of salt pellets this will drop
    numOfSalt = Property.Int(5)
    
    def Initialize(self, initializer):
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted);

    def OnCollisionStarted(self, CollisionEvent):
        #shortcuts
        otherObject = CollisionEvent.OtherObject
        bodyLocation = CollisionEvent.FirstPoint.BodyPoint
        
        #if it's getting hit on its bottom
        if(bodyLocation.y <= -0.25):
            #run this loop, creating the proper number of salt pellets
            for x in range(0, self.numOfSalt):
                saltCog = self.Space.CreateAtPosition("arc_saltPellet", self.Owner.Transform.Translation)
                #thrown up in air on creation
                saltCog.RigidBody.Velocity = Vec3(0,5,0)
                #set life to 10 seconds
                saltCog.py_objectSettings.iTimer = 600
            self.Owner.Destroy()

Zero.RegisterComponent("py_sCargo", py_sCargo)