#####       This script will control normal camera movement     #####
#####################################################################

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
    smoothMovement = Property.Bool(True)
    #create a variable that will store information on where the camera is moving to
    newTranslation = Vec3(0,0,0)
    #stores the horizontal distance the object can move before the camera follows
    horizontalTriggerDistance = Property.Float(5)
    #stores the vertical distance the object can move before the camera follows
    verticalTriggerDistance = Property.Float(5)
    #the speed at which the camera will adjust
    smoothMoveSpeed = Property.Float(1.0)
    
    def Initialize(self, initializer):
        #initializes variable with current camera location
        self.newTranslation = self.Owner.Transform.Translation
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
        
        #The following 4 if statements check to see if the camera should move towards the specified object
        if(horizontalDistance > self.horizontalTriggerDistance):
            followTargetHorizontal = True
        if(horizontalDistance < -self.horizontalTriggerDistance):
            followTargetHorizontal = True
        if(verticalDistance > self.verticalTriggerDistance):
            followTargetVertical = True
        if(verticalDistance < -self.verticalTriggerDistance):
            followTargetVertical = True
            
        #if we want the camera to move. This can be turned off by selecting the object.
        if(self.moveCamera):
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
                self.Owner.Transform.Translation = self.newTranslation

Zero.RegisterComponent("py_cameraLogic", py_cameraLogic)