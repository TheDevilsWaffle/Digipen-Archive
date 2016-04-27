#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
#################################################################################
#FILENAME:      py_cameraLogic
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This is a currently unused script. It was originally designed to
#               "lerp" towards the snail, but then altered to pan the camera
#               evenly. It was then altered again to zoom on the snails eye,
#               before being dropped entirely.
#################################################################################

import Zero
import Events
import Property
import VectorMath

Vec3 = VectorMath.Vec3

class py_cameraLogic:
    #create a target object for the camera to focus on
    targetObject = Property.Cog()
    #boolean to say if we want the camera to move.
    moveCamera = Property.Bool(True)
    #Boolean to say whether or not to allow lerping
    smoothMovement = Property.Bool(False)
    #create a variable that will store information on where the camera is moving to
    newTranslation = Vec3(0,0,0)
    #stores the horizontal distance the object can move before the camera follows
    horizontalTriggerDistance = Property.Float(5)
    #stores the vertical distance the object can move before the camera follows
    verticalTriggerDistance = Property.Float(5)
    #the speed at which the camera will adjust
    smoothMoveSpeed = Property.Float(1.0)
    
    levelTimer = Property.Float(300)
    
    nextLevel = Property.Level()
    
    def Initialize(self, initializer):
        #initializes variable with current camera location
        self.newTranslation = self.Owner.Transform.Translation
        #this will be used as a timer to count when the next level will load after killing enemies
        self.isStarting = True
        
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        
    def OnLogicUpdate(self, UpdateEvent):
        #set the cameras current location
        currentTranslation = self.Owner.Transform.Translation
        #set the cameras target location to a specified object
        targetTranslation = self.targetObject.Transform.Translation
        #calculate the horizontal distance to the specified object
        horizontalDistance = targetTranslation.x - currentTranslation.x
        #calculate the vertical distance to the specified object
        verticalDistance = targetTranslation.y - currentTranslation.y
        #creates and initializes two variables telling the camera not to move.
        followTargetHorizontal = False
        followTargetVertical = False
        
        cameraCog = self.Space.FindObjectByName("Camera")
        self.MoveDirection = Vec3(0,0,0)
        
        #The following 4 if statements check to see if the camera should move towards the specified object
        if(horizontalDistance > self.horizontalTriggerDistance):
            followTargetHorizontal = True
        if(horizontalDistance < -self.horizontalTriggerDistance):
            followTargetHorizontal = True
        if(verticalDistance > self.verticalTriggerDistance):
            followTargetVertical = True
        if(verticalDistance < -self.verticalTriggerDistance):
            followTargetVertical = True
        
        self.CalculateDirection()
        #if we want the camera to move. This can be turned off by selecting the object.
        if(self.moveCamera and verticalDistance < -0.1):
            #if the target object is outside the vertical AND horizontal limits, move camera on the X and Y
            if(followTargetHorizontal == True and followTargetVertical == True):
                self.newTranslation = Vec3(targetTranslation.x, targetTranslation.y, currentTranslation.z)
            #if the target object is outside the horizontal limit, move the camera on the X axis
            elif(followTargetHorizontal == True and followTargetVertical == False):
                self.newTranslation = Vec3(targetTranslation.x, currentTranslation.y, currentTranslation.z)
            #if the target object is outside the vertical limit, move the camera on the Y axis
            elif(followTargetHorizontal == False and followTargetVertical == True):
                self.newTranslation = Vec3(currentTranslation.x, targetTranslation.y, currentTranslation.z)
                
            #if smoothMovement is turned on, use lerping to determine camera movement
            if(self.smoothMovement):
                self.Owner.Transform.Translation = currentTranslation.lerp(self.newTranslation, UpdateEvent.Dt * self.smoothMoveSpeed)
            #otherwise, move the camera without lerping.
            else:
                self.Owner.Transform.Translation += self.MoveDirection * UpdateEvent.Dt * self.smoothMoveSpeed
                
        #obsolete code that would have worked if all "levels" were on the same level
        #if(self.targetObject.Name == "lvl1Center"):
            #if(len(self.Space.py_globalVariables.enemyList) == 2):
                #self.Space.FindObjectByName("Snail").Transform.Translation = Vec3(0, -31.5, 0)
                #self.targetObject = self.Space.FindObjectByName("lvl2Center")
            
            #if the level has reached a point where the stage is no longer in view, load the proper level
            if(self.levelTimer >= 130 and self.targetObject.Name == "LevelBottom"):
                self.Space.LoadLevel(self.nextLevel)
                
        #If all enemies are dead, pan the camera off the screen.
        #if(len(self.Space.py_globalVariables.enemyList) == 0 and self.targetObject.Name == "LevelCenter"):
            #self.targetObject = self.Space.FindObjectByName("LevelBottom")
            #reset the level timer
            #self.levelTimer = 0
        #if the level is starting, start the camera off zooming out
        if(self.isStarting == True):
            self.isStarting = False
            cameraCog.Camera.Size = 0
        #if there are no enemies left, zoom in on the snails eye and start the counter
        if(len(self.Space.py_globalVariables.enemyList) == 0):
            self.levelTimer -= 1
            cameraCog.py_cameraLogic.targetObject = self.Space.FindObjectByName("Snail")
            cameraCog.Camera.Size -= UpdateEvent.Dt * 10
            self.Owner.Transform.Translation = Vec3(targetTranslation.x + 0.75, targetTranslation.y + 1, currentTranslation.z)
        #if the camera's size is under 26 and there are enemies, zoom the camera out
        elif(cameraCog.Camera.Size < 26):
            cameraCog.py_cameraLogic.targetObject = self.Space.FindObjectByName("LevelCenter")
            cameraCog.Camera.Size += UpdateEvent.Dt * 10
        #load the next level when the timer runs out
        if(self.levelTimer <= 0):
            self.Space.LoadLevel(self.nextLevel)
            
    def CalculateDirection(self):
        #calculate the direction on the X axis of the target object from the owner
        self.MoveDirection.x = self.targetObject.Transform.WorldTranslation.x - self.Owner.Transform.Translation.x
        #calculate the direction on the X axis of the target object from the owner
        self.MoveDirection.y = self.targetObject.Transform.WorldTranslation.y - self.Owner.Transform.Translation.y
        #normalize
        self.MoveDirection.normalize()

Zero.RegisterComponent("py_cameraLogic", py_cameraLogic)