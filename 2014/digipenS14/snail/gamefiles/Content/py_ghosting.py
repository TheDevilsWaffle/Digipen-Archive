#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_ghosting
#AUTHOR:        Garrett Huxtable
#DESCRIPTION:   Ghosting is used by the player and enemies to move up 
#               through floors
####################################################################################################

import Zero
import Events
import Property
import VectorMath

##################################################################

#   Ghosting is used by the player and enemies to move up 
#   through floors

##################################################################
class py_ghosting:
    def Initialize(self, initializer):
        currentY = self.Owner.Transform.Translation.y
        self.previousY = currentY
        
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
        
    ##################################################################

    #   OnLogicUpdate is used to chech if the owner is moving
    #   positively on the x-axis. If the owner is moving positively,
    #   it ghosts through floors

    ##################################################################
    def OnLogicUpdate(self, updateEvent):
        self.currentY = self.Owner.Transform.Translation.y
        
        if(self.currentY > self.previousY + 0.05):
            self.Owner.Collider.Ghost = True
            
        else:
            self.Owner.Collider.Ghost = False
            
        self.previousY = self.currentY
        
    ##################################################################

    #   OnCollisionStarted is used for prevent the owner from ghosting
    #   through parts of the environment they are not supposed to,
    #   such as the ceiling, walls, and floors.

    ##################################################################
    def OnCollisionStarted(self, collisionEvent):
        if(collisionEvent.OtherObject.py_objectSettings):
            if(collisionEvent.OtherObject.py_objectSettings.objectClass == "Barrier"):
                self.Owner.RigidBody.Velocity = VectorMath.Vec3(0,0,0)


Zero.RegisterComponent("py_ghosting", py_ghosting)