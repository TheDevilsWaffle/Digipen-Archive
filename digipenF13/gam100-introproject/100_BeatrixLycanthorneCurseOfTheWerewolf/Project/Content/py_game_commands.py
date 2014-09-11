import Zero
import Events
import Property
import VectorMath

class py_game_commands:
    def Initialize(self, initializer):
        #listen for logic updates
        Zero.Connect(self.Space, Events.LogicUpdate, self.GameCommands)
        
        
    def GameCommands(self, UpdateEvent):
        #reload the level using "R"
        if(Zero.Keyboard.KeyIsPressed(Zero.Keys.Tab)):
            if(self.Space.CurrentLevel.Name == "Tutorial"):
                self.Space.LoadLevel("Level")
            if(self.Space.CurrentLevel.Name == "Level"):
                self.Space.LoadLevel("Cave")
            if(self.Space.CurrentLevel.Name == "GameTitle"):
                self.Space.LoadLevel("HighConcept")
            if(self.Space.CurrentLevel.Name == "HighConcept"):
                self.Space.LoadLevel("DigiSplash")

Zero.RegisterComponent("py_game_commands", py_game_commands)