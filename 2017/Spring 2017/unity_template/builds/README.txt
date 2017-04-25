CLASS: 		GAT 261 — Spring 2017
PROFESSOR: 	Richard Rowan
STUDENT: 	Travis Moore
PROJECT:	Super Space Race

XBOX GAMEPAD CONTROLS
MENUS:
	CONFIRM  - A Button
	BACK 	 - B Button

IN-GAME:
	PITCH/YAW - Left Analog Stick
	ROLL - Right Analog Stick
	THROTTLE - Right Trigger
	SPEED BOOST - Right Bumper
	SHOOT LASERS - Left Trigger
	BRAKE/RECHARGE LASERS & SHIELDS	 - Left Bumper
	
	
	PAUSE - Start/Menu Button

DETAILS:
	While the game is not complete, the idea is to make a space flight racing game with limited energy management between your shields, lasers, and throttle. Holding throttle will slowly drain the power from your shields/lasers, while letting go of throttle and breaking will recharge your shields/lasers. Boosting to go faster will dramatically increase the drain rate of your shields/lasers.

That's all for now.

WHAT CHANGED:
	Menu System:
		- Added controls and credits menus
	Controls:
		- boost to RB (previously was R3)
		- brake to LB (previously was RB)
		- added multiple sensitivities to flight so it is hard to make sharp turns when you are going really fast
		- throttle speeds are a lot slower now
		- 
	HUD:
		- lasers and shields rotated (mimics menus), a little larger, and out to the sides more.
		- shader to help reticle UI stick out on top of in-game objects
		- lasers and shields do not animate their divider lines any more
		- throttle has been moved to the bottom center of the screen
	Game:
		- lasers move faster
		- lasers explode upon impact
		- shield bubble is gone, now the shield is a mesh of the ship
		- updated shader for both normal shields and danger (no shield)
		- tweaked camera to appear further back
		- throttle based particle system to show speed better
		- throttle based chromatic abberation
		- throttle based vignetting
		- throttle based depth of field
		- throttle based gamepad rumble
		- laser shot rumble
		- initial jolt when changing speed states (brake->cruise->max->boost)
		- rumble only on throttle being held
		- slow camera bobbing
		- pivot point of ship is now on the nose of the ship (previously was center of the model
		
	SFX:
		- laser explosion sound effect
		- many new engine noises based on throttle
		- warning noises when lasers/shields are drained
		