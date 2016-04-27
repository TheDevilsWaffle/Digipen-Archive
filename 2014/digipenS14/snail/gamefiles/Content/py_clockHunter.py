#       All content Copyright 2014 DigiPen (USA) Corporation, all rights reserved.
####################################################################################################
#FILENAME:      py_clockHunter
#AUTHOR:        Garrett Huxtable
#DESCRIPTION:   this script controls the clockhunter/crow and includes its behaviors like stalking
#               and chasing.
####################################################################################################
import Zero
import Events
import Property
import VectorMath

Vec3 = VectorMath.Vec3

class py_clockHunter:
    ChaseSpeed = Property.Float(8.0)
    #If we reach zero health should we call destroy on ourself?    DestroyAtZeroHealth = Property.Bool(default = True)
    
    #Pacing Properties
    PaceSpeed = Property.Float(5.0)
    MaxMoveDistance = Property.Float(10.0)
    
    def Initialize(self, initializer):
        self.ChaseDirection = Vec3(0,0,0)
        self.timer = 0
        self.stalkCenter = Vec3(0,9,0)
        self.PaceDirection = Vec3(0,1,0)
        self.Owner.SoundEmitter.Play()
        
        currentX = self.Owner.Transform.Translation.x
        self.previousX = currentX
        
        Zero.Connect(self.Space, Events.LogicUpdate, self.OnLogicUpdate)
        Zero.Connect(self.Owner, Events.CollisionStarted, self.OnCollisionStarted)
        
    def OnLogicUpdate(self, UpdateEvent):
        if(self.Space.FindObjectByName("Snail")):
            self.CalculateChaseDirectionAndDistance()
            
            self.timer += UpdateEvent.Dt
            
            self.currentX = self.Owner.Transform.Translation.x
        
            if(self.currentX > self.previousX):
                self.Owner.Sprite.FlipX = True
            else:
                self.Owner.Sprite.FlipX = False
            
            self.previousX = self.currentX
            
            if(self.timer <= 5):
                self.StalkTarget(UpdateEvent)
            
            if(self.timer >= 5):
                self.ChaseTarget(UpdateEvent)
                
        if(self.Space.py_globalVariables.hasDied == True):
            self.Space.FindObjectByName("LevelSettings").py_level_logic.timer = 0
            self.Space.FindObjectByName("LevelSettings").py_level_logic.crow = False
            self.Owner.Destroy()
                
    def StalkTarget(self, UpdateEvent):
        displacement = self.Owner.Transform.Translation - self.stalkCenter
        self.Owner.Transform.Translation += self.PaceDirection * UpdateEvent.Dt * self.PaceSpeed
        distanceFromStart = displacement.length()
        
        if(distanceFromStart >= self.MaxMoveDistance):
            self.PaceDirection = -displacement
        
        self.PaceDirection.normalize()
                
    def ChaseTarget(self, UpdateEvent):
        self.Owner.Transform.Translation += self.ChaseDirection * UpdateEvent.Dt * self.ChaseSpeed
        
    def CalculateChaseDirectionAndDistance(self):
        self.ChaseDirection = self.Space.FindObjectByName("Snail").Transform.Translation - self.Owner.Transform.Translation
        self.DistanceFromTarget = self.ChaseDirection.length()
        self.ChaseDirection.normalize()
        
    def OnCollisionStarted(self, collisionEvent):
        target = self.Space.FindObjectByName("Snail")
        if(collisionEvent.OtherObject == target):
            self.Space.FindObjectByName("LevelSettings").py_level_logic.timer = 0
            self.Space.FindObjectByName("LevelSettings").py_level_logic.crow = False
            self.Owner.Destroy()
            self.Space.py_globalVariables.PlayerHealth = 0

Zero.RegisterComponent("py_clockHunter", py_clockHunter)