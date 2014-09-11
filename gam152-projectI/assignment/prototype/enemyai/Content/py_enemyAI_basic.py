import Zero
import Events
import Property
import VectorMath
import random
Vec3 = VectorMath.Vec3

class py_enemyAI_basic():
    def Initialize(self, init):
        self.NextJump = 2.0
        self.JumpTimer = 0.0
        self.TimeInDirection = 0.0
        self.CurrentDirection = Vec3(0.0, 0.0, 0.0)
        self.Player = None
        self.Random = random.Random()
        
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        Zero.Connect(self.Owner, "UpdateCharacterInput", self.OnUpdateCharacterInput)
    
    def OnAllObjectsCreated(self):
        self.Player = self.Space.FindObjectByName("Player")
    
    def OnUpdateCharacterInput(self, e):
        self.Owner.py_character_movement.MoveDirection = self.CurrentDirection
    
    def OnLogicUpdate(self, updateEvent):
        self.JumpTimer += updateEvent.Dt
        self.TimeInDirection += updateEvent.Dt
        
        if(self.JumpTimer > self.NextJump):
            self.NextJump = self.Random.uniform(0.5, 3.0)
            self.Owner.py_character_movement.BeginJump()
            self.JumpTimer = 0.0
        
        if(self.TimeInDirection > 2.0):
            self.TimeInDirection = 0.0
            self.CurrentDirection = Vec3(self.Random.uniform(-1.0, 1.0), 0.0, 0.0)
        
        if(self.Player):
            playerPos = self.Player.Transform.Translation
            toPlayer = playerPos - self.Owner.Transform.Translation
            
            if(toPlayer.length() < 2.5):
                toPlayer = Vec3(toPlayer.x, 0.0, 0.0)
                self.CurrentDirection = toPlayer.normalized()
    
Zero.RegisterComponent("py_enemyAI_basic", py_enemyAI_basic)