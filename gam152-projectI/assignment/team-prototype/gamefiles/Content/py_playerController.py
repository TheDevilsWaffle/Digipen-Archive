#####       This is the controller for all the controls used after exiting the tank object      #####
#####################################################################################################
import Zero
import Events
import Property
import VectorMath
import math
import Color

Vec3 = VectorMath.Vec3

class py_playerController:
    #declare the playermans move speed
    moveSpeed = Property.Float(3.0)
    #declare the playermans jump force/speed
    jumpSpeed = Property.Float(3.0)
    #Integer that limits the amount of times a player can jump before landing on the ground
    maxNumberOfJumps = Property.Int(1)
    #User defined integer that will limit the speed that a player can shoot
    ProjectileLimiter = Property.Int(20)
    
    def Initialize(self, initializer):
        #connect the OnLogicUpdate function
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        #connect the OnCollisionStarted function
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
        #the player is not on the ground by default
        self.OnGround = False
        #the player has no jumps by default
        self.jumpsRemaining = 0
        ###Collision with an enemy on entering a level causes errors if these are not initialized
        self.moveRight = False
        self.moveLeft = False
        #a boolean telling the player object if it can fire
        self.CanFire = True
        #a timer that will be used to count until the player can fire again
        self.ProjectileTimer = 0
        #create a variable for the camera object
        cameraCog = self.Space.FindObjectByName("Camera")
        #connect the OnMouseDown function
        Zero.Connect(cameraCog.Camera.Viewport, Events.MouseDown, self.OnMouseDown)
        #connect the RightMouseDown function
        Zero.Connect(cameraCog.Camera.Viewport, Events.RightMouseDown, self.RightMouseDown)
        
    def OnLogicUpdate(self, UpdateEvent):
        #run the appropriate functions every logic update
        self.UpdatePlayerInput()
        self.UpdateGroundState()
        self.UpdateJumpState()
        self.ApplyMovement()
        
        #ifthe man's health falls below 0, reset its health and reload the level
        if(self.Space.py_globalVariables.PlayerHealth <= 0.0):
            self.Space.py_globalVariables.PlayerHealth = self.Space.py_globalVariables.PlayerMaxHealth
            self.Space.ReloadLevel()
            
        #if the man has picked up an upgrade, allow it to double jump
        if(self.Space.py_globalVariables.DoubleJump):
            self.maxNumberOfJumps = 2
            
        #if the Projectile timer has not yet hit the user defined limit, increase by 1
        if(self.ProjectileTimer < self.ProjectileLimiter):
            self.ProjectileTimer += 1
        #if it has, then allow the player object to fire
        else:
            self.CanFire = True
        
    def UpdatePlayerInput(self):
        #create all the player movement variables for the appropriate buttons
        self.moveRight = Zero.Keyboard.KeyIsDown(Zero.Keys.D)
        self.moveLeft = Zero.Keyboard.KeyIsDown(Zero.Keys.A)
        self.jumpIsPressed = Zero.Keyboard.KeyIsPressed(Zero.Keys.Space)
        self.enterTank = Zero.Keyboard.KeyIsPressed(Zero.Keys.Z)
        
    def ApplyMovement(self):
        #initialize a variable for the player movement
        moveDirection = Vec3(0,0,0)
        #find the player object as save it as a variable
        tank = self.Space.FindObjectByName("Player")
        
        CanJump = (self.OnGround) or (self.jumpsRemaining > 1)
        
        #track the proper movement for input
        if(self.moveRight):
            moveDirection += Vec3(1,0,0)
        if(self.moveLeft):
            moveDirection += Vec3(-1,0,0)
            
        #allow the player to jump if they have the ability
        if(CanJump and self.jumpIsPressed):
            self.jumpsRemaining -= 1
            if(self.jumpsRemaining < 0):
                self.jumpsRemaining = 0
            self.Owner.RigidBody.ApplyLinearImpulse(Vec3(0,1,0) * self.jumpSpeed)
            
        #allow the player to enter the tank, and destroy the Man
        if(self.enterTank):
            self.Space.FindObjectByName("Camera").CameraLogic.targetObject = tank
            self.Owner.Destroy()
            
        #normalize the player movement
        moveDirection.normalize()
        #apply force based movement
        self.Owner.RigidBody.ApplyForce(moveDirection * self.moveSpeed)
        
    #def CanJump(self):
        #CanJump = (self.OnGround) or (self.jumpsRemaining > 1)
        #return CanJump
        
    def UpdateJumpState(self):
        if(self.OnGround):
            self.jumpsRemaining = self.maxNumberOfJumps
            
    def UpdateGroundState(self):
        self.OnGround = False
        for ContactHolder in self.Owner.Collider.Contacts:
            if(ContactHolder.IsGhost):
                continue
                
            objectHit = ContactHolder.OtherObject
            
            surfaceNormal = -ContactHolder.FirstPoint.WorldNormalTowardsOther
            
            if(self.IsGround(surfaceNormal)):
                self.OnGround = True
                return
            
    def GetDegreeDifference(self, surfaceNormal):
        UpDirection = Vec3(0,1,0)
        cosTheta = surfaceNormal.dot(UpDirection)
        cosTheta = min(max(cosTheta, -1.0), 1.0)
        radians = math.acos(cosTheta)
        degrees = math.degrees(radians)
        return degrees
        
    def IsGround(self, surfaceNormal):
        degrees = self.GetDegreeDifference(surfaceNormal)
        return degrees < 60.0
        
    def OnMouseDown(self, ViewportMouseEvent):
        #if the player can shoot, perform all shoot functions
        if(self.CanFire):
            #set the mouses position on the world stage
            mousePosition = ViewportMouseEvent.ToWorldZPlane(0.0)
            #set the projectile to be located at the centerpoint of the player
            projectilePosition = self.Owner.Transform.Translation
            #create a projectile, and set it as a variable
            projectileCog = self.Space.CreateAtPosition("arc_projectile", projectilePosition)
            #calculate the direction towards the mouse
            direction = mousePosition - projectilePosition
            #normalize that direction
            direction.normalize()
            #create an event to send into the space
            clickEvent = Zero.ScriptEvent()
            #store the direction inside the event
            clickEvent.direction = direction
            #send that event to the projectile
            projectileCog.DispatchEvent("clickEvent", clickEvent)
            #tell the player object it cannot shoot again right away
            self.CanFire = False
            #reset the timer that limits player shooting
            self.ProjectileTimer = 0
    
    #################################################################################################
    #####           This space reserved for an alt-fire.                            #####
    #################################################################################################
    def RightMouseDown(self, ViewportMouseEvent):
        #if the player can shoot, perform all shoot functions
        if(self.CanFire):
            #set the mouses position on the world stage
            mousePosition = ViewportMouseEvent.ToWorldZPlane(0.0)
            #set the projectile to be located at the centerpoint of the player
            projectilePosition = self.Owner.Transform.Translation
            #create a projectile, and set it as a variable
            projectileCog = self.Space.CreateAtPosition("arc_bubble", projectilePosition)
            #calculate the direction towards the mouse
            direction = mousePosition - projectilePosition
            #normalize that direction
            direction.normalize()
            #create an event to send into the space
            clickEvent = Zero.ScriptEvent()
            #store the direction inside the event
            clickEvent.direction = direction
            #send that event to the projectile
            projectileCog.DispatchEvent("clickEvent", clickEvent)
            #tell the player object it cannot shoot again right away
            self.CanFire = False
            #reset the timer that limits player shooting
            self.ProjectileTimer = 0
        
    def OnCollisionStarted(self, CollisionEvent):
        #shortcut
        otherObject = CollisionEvent.OtherObject
        
        #if the other object is an Enemy class
        if(otherObject.py_objectSettings):
            if(otherObject.py_objectSettings.objectClass == "Enemy"):
                #
                #The following if statements are a quick fix, and should not be considered final.
                #repelling the enemy should be based on which way the player IS moving, not how they want to.
                #
                #if the player is currently trying to move to the right
                if(self.moveRight):
                    #repel the enemy on the X axis
                    otherObject.RigidBody.ApplyLinearVelocity(Vec3(1,0,0) * 10)
                    #repel the player on the X axis
                    self.Owner.RigidBody.ApplyLinearVelocity(Vec3(-1,0,0) * 10)
                #if the player is currently trying to move to the left
                if(self.moveLeft):
                    #repel the enemy on the X axis
                    otherObject.RigidBody.ApplyLinearVelocity(Vec3(-1,0,0) * 10)
                    #repel the player on the X axis
                    self.Owner.RigidBody.ApplyLinearVelocity(Vec3(1,0,0) * 10)
                #damage the tanks health
                self.Space.py_globalVariables.PlayerHealth -= otherObject.py_objectSettings.damageToPlayer
            
Zero.RegisterComponent("py_playerController", py_playerController)