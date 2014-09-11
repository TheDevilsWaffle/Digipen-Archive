import Zero
import Events
import Property
import VectorMath

class UIManager:
    def Initialize(self, initializer):
        pass
        
    def Do(self, command):
        #Checks what the command is. Commands are put in the component list on the object
        if(command == "StartGame"):
            self.Owner.LoadLevel("GameIntro1")
        elif(command == "Credits"):
            self.Owner.LoadLevel("Credits")
        elif(command == "Controls" or command == "SwitchToHuman"):
            self.Owner.LoadLevel("ControlsHuman")
        elif(command == "SwitchToWerewolf"):
            self.Owner.LoadLevel("ControlsWerewolf")
        elif(command == "ReturnToMenu"):
            self.Owner.LoadLevel("MenuScreen")
        elif(command == "NextScreen2"):
            self.Owner.LoadLevel("GameIntro2")
        elif(command == "NextScreen3"):
            self.Owner.LoadLevel("GameIntro3")
        elif(command == "NextScreen4"):
            self.Owner.LoadLevel("GameIntro4")
        elif(command == "NextScreen5"):
            self.Owner.LoadLevel("GameIntro5")
        elif(command == "Start"):
            self.Owner.LoadLevel("Tutorial")
        elif(command == "Quit"):
            Zero.Game.Quit()
        #If the command isn't valid, print this so we don't get an error
        else:
            print("UIManager Do: " + command + " is not a UI command.")

Zero.RegisterComponent("UIManager", UIManager)