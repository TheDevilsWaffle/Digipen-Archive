import Zero
import Events
import Property
import VectorMath

Vec3 = VectorMath.Vec3

class py_camera_controller:
    #object to follow
    targetObject = Property.Cog()

    def Initialize(self, initializer):
        #we will update the camera every logic update
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)

    def OnLogicUpdate(self, UpdateEvent):
        #get the current translation of the camera
        currentTranslation = self.Owner.Transform.Translation
        #get the target objects's translation
        targetTranslation = self.targetObject.Transform.Translation
        #use x compnent of target translation with our y and z translation
        newTranslation = Vec3(targetTranslation.x, targetTranslation.y, currentTranslation.z)
        #update our translation with the newly found translation
        self.Owner.Transform.Translation = newTranslation

Zero.RegisterComponent("py_camera_controller", py_camera_controller)