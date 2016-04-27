#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_wall_sticky
#AUTHOR:        Travis Moore
#DESCRIPTION:   Used to make snail stick to walls. Was abandoned when py_ghosting was created.
####################################################################################################
import Zero
import Events
import Property
import Keys
import math
import VectorMath
import DebugDraw
Vec3 = VectorMath.Vec3


class py_wall_sticky:
    ##################################################################
    #FUNCTION - Initialize
    #DESCRIPTION - Constructor for py_wall_sticky
    ##################################################################
    def Initialize(self, init):
        #listen for collision events and perform OnCollisionStarted
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
        Zero.Connect(self.Owner, Events.CollisionEnded, self.OnCollisionEnded)
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        pass
        
        #create variable to track if player is on wall
        self.OnWall = False;
    
    ################################################################################################
    #FUNCTION - OnCollisionStarted
    #DESCRIPTION - Performs wall sticky actions when the player hits a tilemap
    ################################################################################################
    def OnCollisionStarted(self, collisionEvent):
        #set variable ObjectHit to the other object in the collision
        ObjectHit = collisionEvent.OtherObject
        #set a variable for the location on the players body that fits
        BodyLocation = collisionEvent.FirstPoint.BodyPoint
        
        if(self.Owner.GravityEffect.Direction.y == -1):
            #if player contacts with a tile
            if(ObjectHit.Name == "DefaultTile"):
                #cycle through the objects that the player is in contact with.
                for ContactHolder in self.Owner.Collider.Contacts:
                    
                    #DEBUG
                    #print(ContactHolder)
                    
                    #set ContactObject to the player's object that it is in contact with
                    ContactObject = ContactHolder.OtherObject
                    #set SurfaceNormal equal to NEGATIVE worldnormal stuffs
                    SurfaceNormal = -ContactHolder.FirstPoint.WorldNormalTowardsOther
                
                #set UpDirection to a vector pointing up
                UpDirection = Vec3(0,1,0)
                #set CosTheta based on surface normal of up
                CosTheta = SurfaceNormal.dot(UpDirection)
                #set variable Radians based on cosign of CosTheta
                Radians = math.acos(CosTheta)
                
                
                #if player is moving left
                if(self.Owner.py_platformer_mechanics.CharacterFlipRight):
                    #set the player's rotation based on Radians
                    self.Owner.Transform.SetEulerAnglesXYZ(0, 0, Radians)
                    #set the player's gravity effect based on some maths
                    self.Owner.GravityEffect.Direction = Vec3((2 * Radians/(math.pi)),0 ,0)
                    #apply some linear velocity to the player to stop real gravity's momentum (stop sliding down a wall)
                    self.Owner.RigidBody.ApplyLinearVelocity(-self.Owner.RigidBody.Velocity)
                    #print(self.Owner.GravityEffect.Direction)
                    
                
                #if player is moving right
                if(self.Owner.py_platformer_mechanics.CharacterFlipRight == False):
                    #set the player's rotation based on Radians
                    self.Owner.Transform.SetEulerAnglesXYZ(0, 0, -Radians)
                    #set the player's gravity effect based on some maths
                    self.Owner.GravityEffect.Direction = Vec3((-2 * Radians/(math.pi)),0 ,0)
                    #apply some linear velocity to the player to stop real gravity's momentum (stop sliding down a wall)
                    self.Owner.RigidBody.ApplyLinearVelocity(-self.Owner.RigidBody.Velocity)
                    #print(self.Owner.GravityEffect.Direction)
    
    ################################################################################################
    #FUNCTION - OnCollisionEnded
    #DESCRIPTION - performs gravity resetting on the player when no longer on a tile
    ################################################################################################
    def OnCollisionEnded(self, collisionEvent):
        if(collisionEvent.OtherObject.Name == "DefaultTile"):
            self.Owner.GravityEffect.Direction = Vec3(0,-1,0)
            #reset OnWall back to False
            self.OnWall = False
            
    
    ################################################################################################
    #FUNCTION - OnLogicUpdate
    #DESCRIPTION - Raycasting funsies
    ################################################################################################
    def OnLogicUpdate(self, updateEvent):
        #print(ObjectHit)
        Ray = VectorMath.Ray()
        Ray.Start = self.Owner.Transform.Translation + self.Owner.Orientation.WorldForward + .5 * self.Owner.Orientation.WorldUp
        Ray.Direction = self.Owner.Orientation.WorldForward + .25 * self.Owner.Orientation.WorldForward - .8 * self.Owner.Orientation.WorldUp
        Direction = Ray.Direction.normalize()
        
        RayCast = self.Space.PhysicsSpace.CastRayFirst(Ray)
        #print(RayCast)
        
        #Ray.Distance = 1.0
        DebugDraw.DrawArrow(Ray.Start, Ray.Start + Ray.Direction)
        
        
    
Zero.RegisterComponent("py_wall_sticky", py_wall_sticky)