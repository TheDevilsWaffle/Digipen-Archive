import Zero
import Events
import Property
import math
import VectorMath
import Color
import time
import sys

#shorthand for VectorMath.Vec3
Vec3 = VectorMath.Vec3

#global variables
gtransformToWerewolf = "transformToWerewolf"
gtransformToHuman = "transformToHuman"
gItemKey = False
gItemIngredient = False
playerIsCrouching = False

class py_beatrix_controller:
    #human properties
    HumanAcceleration = Property.Float(1.0)
    HumanMaxSpeed = Property.Float(5.0)
    HumanCrouchAcceleration = Property.Float(1.0)
    HumanMaxCrouchSpeed = Property.Float(3.0)
    HumanJumpSpeed = Property.Float(4.5)
    HumanMaxNumberOfJumps = Property.Int(1)

    #werewolf properties
    WerewolfAcceleration = Property.Float(2.0)
    WerewolfMaxSpeed = Property.Float(10.0)
    WerewolfJumpSpeed = Property.Float(6.0)
    WerewolfMaxNumberOfJumps = Property.Int(2)
    
    #timer properties
    gTimerCurrentValue = Property.Float(5.0)
    CooldownTimer = Property.Float(5.0)
    #global variables
    gWerewolfState = False
    MoveDrag = Property.Float(0.15)
    
    def Initialize(self, initializer):
        #we want to apply controls every logic update
        Zero.Connect(self.Space, Zero.Events.LogicUpdate, self.OnLogicUpdate)

        #apply collision logic to items and barrels (which are really crates?)
        Zero.Connect(self.Owner, Zero.Events.CollisionStarted, self.PickUpItem)
        Zero.Connect(self.Owner, Zero.Events.CollisionEnded, self.ClearBarrel)

        #global variable to check if player is in the air
        self.OnGround = False

        #global variable to check how many times a player jumps
        self.JumpsRemaining = 0
        
        #global variable to see how many keys the player has
        self.KeysCollected = 0

        #global variable to see how many ingredients the player has
        self.IngredientsCollected = 0

        #global variable that controls transforming
        self.InputTransform = False

        #global variable that controls crouching
        self.InputCrouch = False
        
        #variable for which way the player is facing
        self.playerDirection = False
        #Set barrels, doors, cauldrons to None so we can't interact with them yet
        self.Barrel = None
        self.LockedDoor = None
        self.Cauldron = None
        self.HubDoor = None
        self.CaveDoor = None
        self.VillageDoor = None
        
        #Set livescount, cauldroncount, and checkpoints to default
        self.LivesCount = 2
        self.CauldronCount = 0
        self.checkpointReached = False
        self.checkpoint2Reached = False
        self.Godmode = False
        
        #Find checkpoints, player, cauldron, and block objects for later use
        self.cauldron = self.Space.FindObjectByName("Cauldron")
        self.Block = self.Space.FindObjectByName("Block")
        self.Block2 = self.Space.FindObjectByName("Block2")
        self.Checkpoint = self.Space.FindObjectByName("Checkpoint")
        self.Checkpoint2 = self.Space.FindObjectByName("Checkpoint2")
        self.player = self.Space.FindObjectByName("BeatrixLycanthorne")
        if (self.player == None):
            print("[py_beatrix_controller] Initialize:  Failed to find player.")

    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-OnLogicUpdate
    #-----------------------------------------------------------------------------------------------------------#
    def OnLogicUpdate(self, UpdateEvent):
        #get all input from the player
        self.UpdatePlayerInput()

        #logic for player movement
        self.UpdateMovement()

        #logic for player crouching
        self.UpdateCrouch()

        #logic for player jumping
        self.UpdateJumping()

        #logic for updating "JumpsRemaining"
        self.UpdateJumpState()

        #logic to check "on ground" status
        self.UpdatePlayerGroundState()

        #logic for player using the actionKey
        self.UpdateAction()

        #logic for transforming
        self.UpdateTransform(UpdateEvent)
        
        #logic for flipping the sprite
        self.Owner.Sprite.FlipX = self.playerDirection

    #############################################################################################################

    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-UpdatePlayerInput
    #-----------------------------------------------------------------------------------------------------------#
    def UpdatePlayerInput(self):
        #the player is attempting to move left
        self.InputRight = Zero.Keyboard.KeyIsDown(Zero.Keys.D) or Zero.Keyboard.KeyIsDown(Zero.Keys.Right)

        #the player is attempting to move right
        self.InputLeft = Zero.Keyboard.KeyIsDown(Zero.Keys.A) or Zero.Keyboard.KeyIsDown(Zero.Keys.Left)

        #the player is attempting to enter a door or climb a ladder
        self.InputClimbOrEnter = Zero.Keyboard.KeyIsPressed(Zero.Keys.W)

        #the player is attemping to crouch
        self.InputCrouch = Zero.Keyboard.KeyIsDown(Zero.Keys.S) or Zero.Keyboard.KeyIsDown(Zero.Keys.Down)

        #the player is attempting to perform an action
        self.InputActionKey = Zero.Keyboard.KeyIsPressed(Zero.Keys.E) or Zero.Keyboard.KeyIsPressed(Zero.Keys.Enter)

        #the player is attemping to jump
        self.InputJump = Zero.Keyboard.KeyIsPressed(Zero.Keys.W) or Zero.Keyboard.KeyIsPressed(Zero.Keys.Up)
        
        #the player is attempting to go godmode
        self.InputGodmode = Zero.Keyboard.KeyIsPressed(Zero.Keys.G)
        
        #the player is attempting to get a key
        self.InputKey = Zero.Keyboard.KeyIsPressed(Zero.Keys.K)
        
        #global gItemKey
        #cheats to give the player a key
        #if(self.InputKey):
            #gItemKey = True
            #self.Space.SoundSpace.PlayCue("PickupKey")
            #increment the key value
            #self.KeysCollected = +1
        
        #the player is attempting to get a potion
        #self.InputPotion = Zero.Keyboard.KeyIsPressed(Zero.Keys.P)
        
        #global gItemIngredient
        #cheats to give the player an ingredient
        #if(self.InputPotion):
            #gItemIngredient = True
            #self.Space.SoundSpace.PlayCue("PickupPotion")
            #increment the ingredient value
            #self.IngredientsCollected += 1

        #the player is attemping to change human/werewolf states
        if(Zero.Keyboard.KeyIsPressed(Zero.Keys.Space)):
            #keep variable true for as long as this button is pressed
            self.InputTransform = True

    #############################################################################################################

    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-UpdateMovement
    #-----------------------------------------------------------------------------------------------------------#
    def UpdateMovement(self):
        #moveDirection-constrain movement to x and y axis, no z axis
        direction = Vec3(0, 0, 0)
        #apply movement based on player input
        #moving right
        
        #apply movement based on player input - moving right
        if(self.InputRight):
            #apply right direction
            direction += Vec3(0.25, 0, 0)

            #Set the player's sprite to the right
            self.playerDirection = False

            #animation of walking
            self.Owner.Sprite.AnimationActive = True
            
        if(Zero.Keyboard.KeyIsPressed(Zero.Keys.H)):
            self.player.Transform.Translation = Vec3(-35.642, -8.3, 0)
            
        if(Zero.Keyboard.KeyIsPressed(Zero.Keys.J)):
            self.player.Transform.Translation = Vec3(-18.639, -19.306, 0)
            
        if(Zero.Keyboard.KeyIsPressed(Zero.Keys.K)):
            self.player.Transform.Translation = Vec3(118.293, -20.848, 0)
            
        #apply movement based on player input - moving left
        if(self.InputLeft):
            #apply left direction
            direction += Vec3(-0.25, 0, 0)

            #Set the player's sprite to the left
            self.playerDirection = True

            #animation of walking
            self.Owner.Sprite.AnimationActive = True
            
        #If the player presses g
        if(self.InputGodmode):
            #Activate godmode
            self.Godmode = True
        #if not moving
        if(self.InputLeft == False and self.InputRight == False):
            #stop movement animation
            self.Owner.Sprite.AnimationActive = False

        #Only want unit direction
        direction.normalize()

        #apply linear velocity based on gWerewolfState - if werewolf
        if(self.gWerewolfState == True):
            #cap the movespeed to that of WerewolfMaxSpeed
            if(self.Owner.RigidBody.Velocity.length() < self.WerewolfMaxSpeed):
                #apply acceleration
                self.Owner.RigidBody.ApplyLinearVelocity(direction * self.WerewolfAcceleration)

                #debug
                #print("WerewolfMaxSpeed = " + str(self.WerewolfMaxSpeed))
                #print("Werewolf Current Speed =" + str(self.Owner.RigidBody.Velocity.x))

        #apply linear velocity based on gWerewolfState - if human and standing
        if(self.gWerewolfState == False and (self.InputCrouch == False)):
            #cap the movespeed to that of HumanMaxCrouchSpeed
            if(self.Owner.RigidBody.Velocity.length() < self.HumanMaxSpeed):
                #apply acceleration
                self.Owner.RigidBody.ApplyLinearVelocity(direction * self.HumanAcceleration)

                #debug
                #print("HumanMaxSpeed = " + str(self.HumanMaxSpeed))
                #print("Human Walking Current Speed =" + str(self.Owner.RigidBody.Velocity.x))

        #apply linear velocity based on gWerewolfState - if human and crouching
        else:
            #cap the movespeed to that of HumanMaxCrouchSpeed
            if(self.Owner.RigidBody.Velocity.length() < self.HumanMaxCrouchSpeed):
                #apply acceleration
                self.Owner.RigidBody.ApplyLinearVelocity(direction * self.HumanCrouchAcceleration)

                #debug
                #print("HumanCrouchMaxSpeed = " + str(self.HumanMaxCrouchSpeed))
                #print("Human Crouch Current Speed =" + str(self.Owner.RigidBody.Velocity.x))
                
        #slow down movement speed of character
        self.Owner.RigidBody.Velocity *= Vec3(self.MoveDrag, 1, 1);

    #############################################################################################################

    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-UpdateCrouch
    #-----------------------------------------------------------------------------------------------------------#
    def UpdateCrouch(self):

        #access global variables
        global gWerewolfState
        global playerIsCrouching

        #create local variables
        currentPlayerSize = self.Owner.Transform.Scale
        playerCrouch = Vec3(1, 1, 1)
        playerCrouchPositionFix = Vec3(0, 0.38, 0)
        playerNormal = Vec3(1, 1, 1)

        #if control is pressed
        if(self.InputCrouch):

            #debug
            #print("*FUNCTION*InputCrouch*\n|_self.InputCrouch == " + str(self.InputCrouch) + "\n|_self.CanJump == " + str(self.InputCrouch))

            #check to see if player is human
            if(self.gWerewolfState == False):

                #debug
                #print("|_ENTERED 'if gWerewolfState == FALSE'\n  |_gWerewolfState == " + str(gWerewolfState) + "\n  |_currentPlayerSize == " + str(currentPlayerSize))

                #if player is already crouching size do not get smaller
                if(playerIsCrouching == True):

                    #debug
                    #print("already crouching")
                    #Adjust the collider
                    self.Owner.CapsuleCollider.Height = .5
                    self.Owner.CapsuleCollider.Offset = Vec3(.2, -.7, 0)
                    #ensure we are using the crouch sprite
                    self.Owner.Sprite.SpriteSource = "beatrix-human-crouching-ver01"

                #apply crouching size to player
                if(playerIsCrouching == False):
                    
                    #ensure we are using the crouch sprite
                    self.Owner.Sprite.SpriteSource = "beatrix-human-crouching-ver01"

                    #debug
                    #print("crouching!\nplayerIsCrouching = " + str(playerIsCrouching))

                    #switch playerIsCrouching to True
                    playerIsCrouching = True
                    self.Owner.Transform.Scale = Vec3(1, 1, 1)

            #if player is currently a werewolf
            if(self.gWerewolfState):

                #check to make sure they are the right size and correct if they are not
                if(playerIsCrouching == True):
                
                    #correct playerIsCrouching
                    playerIsCrouching = False
                    self.Owner.CapsuleCollider.Height = 1.8
                    self.Owner.CapsuleCollider.Offset = Vec3(.2, -.1, 0)
                    #debug
                    #print("changing back to right height value")

        #reverse the crouching when button is no longer pressed
        else:
            self.Owner.Transform.Scale = Vec3(0.8, 1, 1)
            #if player is crouch size at control button release
            
            if(playerIsCrouching == True):
                #Set the collider back
                self.Owner.CapsuleCollider.Height = 1.8
                self.Owner.CapsuleCollider.Offset = Vec3(.2, -.1, 0)
                #swap out the crouch sprite for the standing sprite
                self.Owner.Sprite.SpriteSource = "BeatrixAnimationSheet"

                #reset playerIsCrouching
                playerIsCrouching = False

    #############################################################################################################

    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-UpdateJumping
    #-----------------------------------------------------------------------------------------------------------#
    def UpdateJumping(self):
        #apply an instant force upward for jumping based on player input
        if(self.CanJump() and self.InputJump):
            #subtract from JumpsRemaining
            self.SubtractJumpsRemaining()
            
            #apply jumping based on gWerewolfState
            #if human, so normal jump
            if(self.gWerewolfState == False):

                #debug
                #print(str(self.gWerewolfState) + " = gWerewolfState")
                #print("human jump")
                #if they're crouching, halve the speed to normalize the jump
                if(playerIsCrouching == True):
                    self.Owner.RigidBody.ApplyLinearImpulse(Vec3(0,.5,0) * self.HumanJumpSpeed)
                else:
                    #apply jumping
                    self.Owner.RigidBody.ApplyLinearImpulse(Vec3(0,1,0) * self.HumanJumpSpeed)

            #else werewolf, so jump x 1.5
            if(self.gWerewolfState):
                #debug
                #print("werewolf jump")
            
                #apply jumping
                self.Owner.RigidBody.ApplyLinearImpulse(Vec3(0,1,0) * self.WerewolfJumpSpeed)

    #----------------------------------------------------------------#
    #----SUBFUNCTION-CanJump
    #----Called from: ApplyJumping
    #----------------------------------------------------------------#
    def CanJump(self):
        #is true if on the ground or jumps remain
        canJump = self.OnGround or (self.JumpsRemaining > 1)
        return canJump

    #----------------------------------------------------------------#
    #----SUBFUNCTION-UpdatePlayerGroundState
    #----Used in: CanJump
    #----------------------------------------------------------------#
    def UpdatePlayerGroundState(self):

        #reset varaible before evaluating
        self.OnGround = False
        #check to see if player is in contact with ground
        for ContactHolder in self.Owner.Collider.Contacts:

            #ignore ghost objects
            if(ContactHolder.IsGhost):
                continue

            #find out what object the player is colliding with
            objectHit = ContactHolder.OtherObject

            #checking normals to determine if object is walkable
            surfaceNormal = -ContactHolder.FirstPoint.WorldNormalTowardsOther

            #if the object is walkable
            if(self.IsGround(surfaceNormal)):

                #object is valid ground
                self.OnGround = True
                return
    #----------------------------------------------------------------#
    #----SUBUNCTION-SubtractJumpsRemaining
    #----Called from: UpdateJumping
    #----------------------------------------------------------------#
    def SubtractJumpsRemaining(self):

        #keep track of how many jumps are available
        self.JumpsRemaining -= 1

        #prevent negative jumps from happening
        if(self.JumpsRemaining < 0):
            self.JumpsRemaining = 0

    #----------------------------------------------------------------#
    #----SUBFUNCTION-UpdateJumpState
    #----Called from: UpdateJumping
    #----------------------------------------------------------------#
    def UpdateJumpState(self):

        #reset the available jumps when player is back on ground
        if(self.OnGround):

            #if player is a werewolf
            if(self.gWerewolfState):
                #Set the sprite back to the werewolf
                #self.Owner.Sprite.SpriteSource = "beatrix-werewolf-ver01"
                #add extra jump if currently a werewolf
                self.JumpsRemaining = self.WerewolfMaxNumberOfJumps
                #make sure player is upright if on ground
                self.Owner.Transform.SetEulerAnglesXYZ(0,0,0)

                #debug string
                #print("Player is currently on ground as Werewolf\n-setting maxNumberOfJumps to " + str(self.MaxNumberOfJumps))

            #else player is human
            else:
                #Set the sprite back to the human
                #self.Owner.Sprite.SpriteSource = "BeatrixAnimationSheet"
                #self.Owner.Sprite.AnimationActive = True
                #normal jumps
                self.JumpsRemaining = self.HumanMaxNumberOfJumps
                #make sure player is upright if on ground
                self.Owner.Transform.SetEulerAnglesXYZ(0,0,0)

                #debug string
                #print("Player is currently on ground as Human\n-setting maxNumberOfJumps to " + str(self.MaxNumberOfJumps + 1))
        #If they are not touching the ground, set it to the jump sprite
        #else:
         #   self.Owner.Sprite.SpriteSource = "Crate"
    #----------------------------------------------------------------#
    #FUNCTION-IsGround
    #----Called from: UpdatePlayerGroundState
    #----------------------------------------------------------------#
    def IsGround(self, surfaceNormal):
        #if the angle of surface's normal is less than specified value, the player is on the ground
        degrees = self.GetDegreeDifference(surfaceNormal)
        return degrees < 60.0

    #----------------------------------------------------------------#
    #----SUBFUNCTION-GetDegreeDifference
    #----Called from: IsGround
    #----------------------------------------------------------------#
    def GetDegreeDifference(self, surfaceNormal):

        upDirection = Vec3(0,1,0)

        #returns the angle between the surface normal and the up vector of the player
        cosTheta = surfaceNormal.dot(upDirection)

        #playing it safe with math
        cosTheta = min(max(cosTheta, -1.0),1.0)

        #rest of the function
        radians = math.acos(cosTheta)
        degrees = math.degrees(radians)

        #return
        return degrees
    #############################################################################################################

    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-UpdateTransform
    #-----------------------------------------------------------------------------------------------------------#
    def UpdateTransform(self, UpdateEvent):
        #Decrement cooldown timer
        self.CooldownTimer -= UpdateEvent.Dt
        
        #NOT SURE WHAT EXACTLY THIS IS (variable of what?)- NEED COMMENT EXPLANATION HERE
        wereWolfStateIsTrue = Zero.ScriptEvent()
        wereWolfStateIsFalse = Zero.ScriptEvent()
        gWerewolfState = False
        
        #create local variables
        #Find and assign the darkness object
        darkness = self.Space.FindObjectByName("Darkness")

        if(self.InputTransform and (self.InputCrouch == False) and self.CooldownTimer <= 0):
            #debug
            #print("***FUNCTION UpdatePlayerState***\nPASSED 'if self.InputTransform == TRUE' and 'self.CurrentState() == TRUE'\n|_VARIABLES: 'changeToWerewolf' = " + str(changeToWerewolf) + " and 'changeToHuman' = " + str(changeToHuman))

            if(self.gWerewolfState == False and self.CooldownTimer <= 0):
                #change werewolfState to TRUE
                self.gWerewolfState = True

                #Send the event that the player is a werewolf
                self.Space.DispatchEvent("wereWolfStateIsFalse", wereWolfStateIsFalse)

                #ensure we are using the werewolf sprite
                self.Owner.Sprite.SpriteSource = "WolfAnimationboxed"

                #update HUD text
                self.MaxNumberOfJumps = self.WerewolfMaxNumberOfJumps

                #Turn the darkness to visible
                darkness.Sprite.Visible = True
                
                self.Space.SoundSpace.PlayCue("WerewolfHowl")

                #change gWerewolfState to TRUE so we exit this loop
                self.gWerewolfState = True

                #debug
                #print("|_ENTERED 'gWerewolfState == FALSE'\n|_'werewolfState' now set to TRUE")

            if(self.gWerewolfState == True):

                #print("entered else gWerewolfState == TRUE")
                if(self.gTimerCurrentValue > 0):
                    #debug
                    #print("entered else gTimerCurrentValue >= 0")

                    self.gTimerCurrentValue -= UpdateEvent.Dt
                    #gWerewolfState = False
                    self.Space.DispatchEvent("wereWolfStateIsTrue", wereWolfStateIsTrue)
                else:

                    #debug
                    #print("gTimerCurrentValue less than 0, change gWerewolfState == False, change InputTransform == False")

                    #ensure we are using the werewolf sprite
                    self.Owner.Sprite.SpriteSource = "BeatrixAnimationSheet"
                    
                    #set the darkness back to invisible
                    darkness.Sprite.Visible = False

                    #reset variables
                    self.gWerewolfState = False
                    self.InputTransform = False
                    self.gTimerCurrentValue = 5.0
                    self.CooldownTimer = 5.0

                    #gWerewolfState = False
                    self.Space.DispatchEvent("wereWolfStateIsFalse", wereWolfStateIsFalse)

        #If the cooldown timer is still going, don't allow the transform
        if(self.InputTransform and (self.InputCrouch == False) and self.CooldownTimer > 0):
            self.InputTransform = False
    ##################################################################

    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-UpdateAction
    #-----------------------------------------------------------------------------------------------------------#
    def UpdateAction(self):

        #check to see what the current state of the player upon button press
        if(self.InputActionKey):

            #debug
            #print("***FUNCTION UpdateAction***\n|_PASSED 'if self.InputActionKey == TRUE' and 'self.CurrentState() == TRUE'\n|_VARIABLES: 'gWerewolfState' = " + str(self.gWerewolfState))

            #if player is currently human
            if(self.gWerewolfState == False):

                #debug
                #print("|_ENTERED 'if gWerewolfState == FALSE'\n  |_Proceeding to FUNCTION UseItem()\n")

                #perform FUNCTION UseItem
                self.UseItem()

            #else, player is werewolf
            else:

                #debug
                #print("|_ENTERED 'else gWerewolfState == TRUE'\n  |_Proceeding to FUNCTION UseSlash()\n")

                #perform FUNCTION UseBreak
                self.UseBreak()
    #############################################################################################################

    #-----------------------------------------------------------------------------------------------------------#
    #FUNCTION-PickUpItem
    #-----------------------------------------------------------------------------------------------------------#
    def PickUpItem(self, CollisionEvent):

        #accessing global variables
        global gItemKey
        global gItemIngredient

        #starting up the collision
        targetObject = CollisionEvent.OtherObject
        #checking to see if the item is a key
        if(targetObject.Name == "gItemKey"):
            
            #if it is, set the key to true
            gItemKey = True

            #destroy the key
            targetObject.Destroy()
            
            self.Space.SoundSpace.PlayCue("PickupKey")

            #increment the key value
            self.KeysCollected += 1
            #Remove the block so that players can unlock the door
            self.Block.Transform.Translation = Vec3(0,0,-20)
            self.Block.Sprite.Visible = False
            #debug
            #print("Key picked up\nKeys in inventory == " + str(self.KeysCollected))

        #checking to see if the item is an ingredient
        if(targetObject.Name == "gItemIngredient"):

            #if it is, set the ingredient to true
            gItemIngredient = True

            #destroy the ingredient
            targetObject.Destroy()
            
            self.Space.SoundSpace.PlayCue("PickupPotion")

            #increment the ingredient value
            self.IngredientsCollected += 1

            #debug
            #print("Ingredient picked up\nIngredients in inventory == " + str(self.IngredientsCollected))
        #Logic for doors
        if(targetObject.Name == "HubDoor"):
            self.Space.LoadLevel("Level")
        if(targetObject.Name == "CaveDoor"):
            self.Space.LoadLevel("Cave")
            
        #Checkpoint logic
        if(targetObject.Name == "Checkpoint"):
            if(self.checkpointReached == False):
                self.Space.SoundSpace.PlayCue("Checkpoint")
            self.Checkpoint.Sprite.SpriteSource = "FlagsGreen"
            self.checkpointReached = True
        if(targetObject.Name == "Checkpoint2"):
            if(self.checkpoint2Reached == False):
                self.Space.SoundSpace.PlayCue("Checkpoint")
            self.Checkpoint2.Sprite.SpriteSource = "FlagsGreen"
            self.checkpoint2Reached = True
        #Logic for moving the player to the correct position when they lose a life
        #This depends on the current level and if they have reached a checkpoint
        if(targetObject.Name == "Hazard" and self.Godmode == False):
            if(self.LivesCount < 1):
                self.Space.LoadLevel("LoseScreen")
            if(self.Space.CurrentLevel.Name == "Cave"):
                if(self.gWerewolfState == False):
                    self.Space.SoundSpace.PlayCue("DeathBeatrix")
                if(self.gWerewolfState == True):
                    self.Space.SoundSpace.PlayCue("WerewolfDeath")
                if(self.checkpointReached == False and self.checkpoint2Reached == False):
                    self.player.Transform.Translation = Vec3(-15.083, -2.031, 0)
                if(self.checkpointReached == True):
                    self.player.Transform.Translation = Vec3(-30.796,-34.477, 0)
                if(self.checkpoint2Reached == True):
                    self.player.Transform.Translation = Vec3(42.505,-9.598, 0)
                self.LivesCount -= 1
            if(self.Space.CurrentLevel.Name == "Level"):
                if(self.gWerewolfState == False):
                    self.Space.SoundSpace.PlayCue("DeathBeatrix")
                if(self.gWerewolfState == True):
                    self.Space.SoundSpace.PlayCue("WerewolfDeath")
                if(self.checkpointReached == False):
                    self.player.Transform.Translation = Vec3(-14.233, 48.781, 0)
                if(self.checkpointReached == True):
                    self.player.Transform.Translation = Vec3(20.266,81.774, 0)
                self.LivesCount -= 1
            if(self.Space.CurrentLevel.Name == "Tutorial"):
                if(self.gWerewolfState == False):
                    self.Space.SoundSpace.PlayCue("DeathBeatrix")
                if(self.gWerewolfState == True):
                    self.Space.SoundSpace.PlayCue("WerewolfDeath")
                if(self.checkpointReached):
                    self.player.Transform.Translation = Vec3(-26.496, -3.513, 0)
                if(self.checkpointReached == False):
                    self.player.Transform.Translation = Vec3(-43.215, -3.223, 0)
                self.LivesCount -= 1
    #############################################################################################################

    #----------------------------------------------------------------#
    #----SUBFUNCTION-UseItem
    #----Called by: UpdateAction
    #----------------------------------------------------------------#
    def UseItem(self):
        #access global variables
        global gItemKey
        global gItemIngredient

        #debug
        #print("\n***FUNCTION UseItem***")

        #perform check to see what item the player has
        #if player has a key
        if(gItemKey == True and self.LockedDoor):
            
            #debug
            #print("|_ENTERED 'if gItemKey == TRUE'\n  |_Using Key\n|_Removing Key from inventory\n|_Resetting gItemKey = FALSE")

            #decrement the key counter to remove the item from the inventory
            self.KeysCollected -= 1

            #find the door
            self.LockedDoor.Destroy()
            
            self.Space.SoundSpace.PlayCue("UnlockDoor")

            #reset the key
            if(self.KeysCollected == 0):

                #set gItemKey to False
                gItemKey = False

        #elif player has an Ingredient
        elif(gItemIngredient == True and self.Cauldron):

            #debug
            #print("|_ENTERED 'if gItemIngredient == TRUE'\n  |_Using Ingredient\n|_Removing Ingredient from inventory\n|_Resetting gItemIngredient = FALSE")

            #remove ingredient from inventory
            self.IngredientsCollected -= 1
            #Increment the cauldron count
            self.CauldronCount += 1
            #Remove the block so that players can use the cauldron
            if(self.CauldronCount == 1):
                self.Block2.Transform.Translation = Vec3(0,0,-20)
            self.Space.SoundSpace.PlayCue("Cauldron")
            #If the player has no ingredients, set the boolean to false
            if(self.IngredientsCollected == 0):
                gItemIngredient = False
            #Change the color of the cauldron depending on the level
            if(self.CauldronCount == 1 and self.Space.CurrentLevel.Name == "Tutorial"):
                self.cauldron.Sprite.SpriteSource = "CauldronRed"
            if(self.CauldronCount == 1 and self.Space.CurrentLevel.Name == "Level"):
                self.cauldron.Sprite.SpriteSource = "CauldronPurple"
            #If it's the last level, load the winscreen
            if(self.CauldronCount == 1 and self.Space.CurrentLevel.Name == "Cave"):
                self.Space.LoadLevel("WinScreen")
        #Door logic
        if(self.HubDoor):
            self.Space.LoadLevel("Level")
        if(self.CaveDoor):
            self.Space.LoadLevel("Cave")

        #else no item to be used
        else:

            print("|_There is no item to be used\n")
    #############################################################################################################

    #----------------------------------------------------------------#
    #----SUBFUNCTION-UseBreak
    #----Called by: UpdateAction
    #----------------------------------------------------------------#
    def UseBreak(self):
        #debug
        #print("\n***FUNCTION UseBreak***")
        #print("Check barrel: ", self.Barrel)
        #If we are touching the barrel
        if (self.Barrel):

            #sound - slashing
            self.Space.SoundSpace.PlayCue("WerewolfBreak")
            self.Space.SoundSpace.PlayCue("BreakSound")

            # Break the barrel by making it invisible and moving it back on the z axis
            self.Barrel.Sprite.Visible = False
            self.Barrel.Transform.Translation = Vec3(0,0,-10)

            #debug
            #print("Player broke ", self.Barrel.Name)
        else:
            print("Player used Slash.")

    #Clear the barrel and doors 
    def ClearBarrel(self, OnCollisionEnded):
        targetObject = OnCollisionEnded.OtherObject
        #checking if the player is next to the barrel
        if (targetObject == self.Barrel):
            self.Barrel = None;

    def ClearLockedDoor(self, OnCollisionEnded):
        targetObject = OnCollisionEnded.OtherObject
        #checking if the player is next to the barrel
        if (targetObject == self.LockedDoor):
            self.LockedDoor = None;

    #############################################################################################################

Zero.RegisterComponent("py_beatrix_controller", py_beatrix_controller)