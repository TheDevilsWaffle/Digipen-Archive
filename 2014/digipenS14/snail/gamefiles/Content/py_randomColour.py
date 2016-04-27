#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_randomColour
#AUTHOR:        Michael Van Zant
#DESCRIPTION:   creates a random color for paired warp zones
####################################################################################################
import Zero
import Events
import Property
import VectorMath
import random

class py_randomColour:
    def Initialize(self, initializer):
        redValue = random.randint(0,5) * .2
        greenValue = random.randint(0,5) * .2
        blueValue = random.randint(0,5) * .2
        if(redValue == 0 and greenValue == 0 and blueValue == 0):
            redValue = random.randint(1,5) * .2
        
        self.Owner.SpriteParticleSystem.Tint = VectorMath.Vec4(redValue, greenValue, blueValue, 0.5)
        self.Owner.FindChildByName("Wrapping Effect").SpriteParticleSystem.Tint = VectorMath.Vec4(redValue, greenValue, blueValue, 0.5)

Zero.RegisterComponent("py_randomColour", py_randomColour)