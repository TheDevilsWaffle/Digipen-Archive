import Zero
import Events
import Property
import VectorMath


class py_HUD_beatrix:
    #the level that cotains the HUD for beatrix
    LevelHUDBeatrix = Property.Resource("Level")
    
    def Initialize(self, initializer):
        #create the space for the HUD objects
        self.HUD_beatrix = Zero.Game.CreateNamedSpace("Level_HUD_Beatrix", "Space")
        
        #load the level with our objects into the hud space
        self.HUD_beatrix.LoadLevel(self.LevelHUDBeatrix)
        
    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-HUDDestroy
    #-----------------------------------------------------------------------------------------------------------#
    def HUDDestroy(self):
        #destroy the hud space and all objects inside of it
        self.HUD_beatrix.Destroy
        
    #############################################################################################################

Zero.RegisterComponent("py_HUD_beatrix", py_HUD_beatrix)