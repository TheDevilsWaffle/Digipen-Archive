#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
################################################################################################
#FILENAME:      py_rotation_tracker
#AUTHOR:        Travis Moore
#DESCRIPTION:   responsible for keeping track of and setting bools for the player
#               to be used by py_wall_sticky
################################################################################################
import Zero
import Events
import Property
import VectorMath


class py_rotation_tracker:
    
    ################################################################################################
    #FUNCTION - Initialize
    #DESCRIPTION - sets variables and even listeners
    ################################################################################################
    def Initialize(self, initializer):
        #VARIABLES
        #variables used for determining if the player is on the floor/ceiling/leftwall/rightwall
        self.IsOnFloor = True
        self.IsOnCeiling = False
        self.IsOnLeftWall = False
        self.IsOnRightWall = False
        #variable to determine if player is falling
        self.IsFalling = False
        
        #listen to space every frame and perform OnLogicUpdate
        Zero.Connect(self.Space, Events.LogicUpdate, self.TrackRotation)
        pass
    
    ################################################################################################
    #FUNCTION - TrackRotation
    #DESCRIPTION - responsible for determing the current rotation of the player
    ################################################################################################
    def TrackRotation(self, UpdateEvent):
        #get player's current rotation
        pass
        
        
        

Zero.RegisterComponent("py_rotation_tracker", py_rotation_tracker)