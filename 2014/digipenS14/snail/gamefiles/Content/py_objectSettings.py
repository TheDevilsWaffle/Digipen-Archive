#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_objectSettings
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   This is attached to all game objects, determines the Class, damage to player
#               and health if applicable (not a player object)  
####################################################################################################
import Zero
import Events
import Property
import VectorMath


#a list (array) that holds a series of strings for use by the games class system
classList = ["Player", "Enemy", "Barrier", "Gate", "PickUp", "Projectile", "Crow"]

class py_objectSettings:
    #User defined class system to make detection easier
    objectClass = Property.Enum(enum = classList)
    #can the object be killed?
    canBeKilled = Property.Bool()
    #how much health does the object have (DO NOT USE FOR PLAYER OBJECTS, use global variable instead)
    objectHealth = Property.Int()
    #how much damage does the object do to the player?
    damageToPlayer = Property.Int()
    #setting that tells object if it's in a bubble.
    inBubble = Property.Bool(False)
    #timer that the object will stay in a bubble
    iTimer = Property.Int()
    
    def Initialize(self, initializer):
        #if(self.objectClass == "Player"):
            #print(self.Space.py_globalVariables.PlayerHealth)
        #if the object we're creating is an enemy, add it to the enemy list global variable.
        if(self.objectClass == "Enemy"):
            self.Space.py_globalVariables.enemyList.append(self.Owner.Transform)
            

Zero.RegisterComponent("py_objectSettings", py_objectSettings)