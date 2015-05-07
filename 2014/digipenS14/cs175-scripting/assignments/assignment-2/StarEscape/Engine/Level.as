/***************************************************************************************/
/*
	filename   	Level.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date    	01/28/2014 
	
	brief:
	This file contains the variables and functions that are a part of the Level class.
	This file contains the constructor, PlayerMovement(), GenerateEnemies(),
	DestroyEnemy(), CollisionCheck(), RemoveDeadEnemies(), HUDDisplay(), and
	Destroy() functions. This file is responsible for accessing and updating the stage.
*/        	 
/***************************************************************************************/
package Engine
{
	//access as files in Objects
	import Objects.*;
	//access files in Enemies
	import Objects.Enemies.*;
	//access the stage to display things
	import flash.display.Stage;
	//access mouseevents for tracking the mouse
	import flash.events.MouseEvent;
	//access events for updating every frame
	import flash.events.Event;
	//access textfield for displaying text
	import flash.text.TextField;
	
	public class Level
	{
		//variable that is used to access the stage class
		private var sMyStage:Stage;
		//variable that the player controls based on the Hero class
		private var heroMainCharacter:Hero;
		//variable to hold and keep track of enemies
		private var aEnemies:Array;
		//variable to keep track of frames passed
		private var iTimer:int;
		//variable to display text of how many items are on the stage
		private var tNumOfObjects:TextField;
		//variable to display text of how much health the player has
		private var tHeroHealth:TextField;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for initializing the Level class with its 
				functions and variables.
			
			Parameters:
				- sStage_:Stage - used to access the stage from the .fla file
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Level(sStage_:Stage):void
		{
			//link to the stage via the passed parameter
			sMyStage = sStage_;
			
			//create hero and initialize with x, y, and health parameters
			heroMainCharacter = new Hero(50,200,100);
			//initialize aEnemies array
			aEnemies = new Array();
			
			//create text fields
			tNumOfObjects = new TextField();
			tHeroHealth = new TextField();
			
			//set width of texts
			tNumOfObjects.width = 400;
			tHeroHealth.width = 400;
			
			//set x scales to 2
			tNumOfObjects.scaleX = 2;
			tHeroHealth.scaleX = 2
			
			//set y scales to 2
			tNumOfObjects.scaleY = 2;
			tHeroHealth.scaleY = 2;
			
			//set texts x position
			tNumOfObjects.x = 200;
			tHeroHealth.x = 200;
			
			//set texts y position
			tHeroHealth.y = 25;
			
			//set texts to red
			tNumOfObjects.textColor = 0XFF0000;
			tHeroHealth.textColor = 0XFF0000;

			//set tNumOfObjects equal to uiNumberOfObjects
			tNumOfObjects.text = String("Number of Objects " + GameObject.uiNumberOfObjects);
			//set tHeroHealth equal to heroMainCharacter.iHealth
			tHeroHealth.text = String("Hero Health " + heroMainCharacter.iHealthValue);
			
			//add number of objects text
			sMyStage.addChild(tNumOfObjects);
			//add hero health text
			sMyStage.addChild(tHeroHealth);
			
			//add event listeners
			sMyStage.addChild(heroMainCharacter);
			sMyStage.addEventListener(MouseEvent.MOUSE_MOVE, PlayerMovement);
			sMyStage.addEventListener(Event.ENTER_FRAME, GenerateEnemies);
			sMyStage.addEventListener(Event.ENTER_FRAME, CollisionCheck);
			sMyStage.addEventListener(Event.EXIT_FRAME, RemoveDeadEnemies);
			sMyStage.addEventListener(Event.ENTER_FRAME, HUDDisplay);
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for updating player movement
			Parameters:
				- me_:MouseEvent - used to track mouse events (movement in our case)
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function PlayerMovement(me_:MouseEvent):void
		{
			//call the hero's movement function
			heroMainCharacter.Movement(me_);
			
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for generating enemies based on a timer
			Parameters:
				- e_:Event - used to update every frame
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function GenerateEnemies(e_:Event):void
		{
			//increment uiTimer
			++iTimer;
			
			//check for every 60 frames
			if(iTimer % 60 == 0)
			{
				//randomly pick an enemy type (black, yellow, red)
				var iRandomEnemy:int = 10 * Math.random();
				//insert random enemy
				switch(iRandomEnemy)
				{
					//create black enemy
					case 0:
					case 1:
					case 2:
					case 3:
						//push in black enemy and initalize in array
						aEnemies.push(new BlackEnemy(470,(25 + (375 - 25) * Math.random()),5,15));
						break;
					//create yellow enemy
					case 4:
					case 5:
					case 6:
						//push in yellow enemy and initalize in array
						aEnemies.push(new YellowEnemy(470,(25 + (375 - 25) * Math.random()),15,10,5));
						break;
					//create red enemy
					case 7:
					case 8:
					case 9:
						//push in red enemy and initalize in array
						aEnemies.push(new RedEnemy(470,(25 + (375 - 25) * Math.random()),10,5));
						break;
				}
				//add the enemy to the stage
				sMyStage.addChild(aEnemies[(aEnemies.length - 1)]);
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for Destroying enemies and removing them
			Parameters:
				- iEnemyIndex_:int - used to specify which enemy in the aEnemies array
									 to destroy.
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function DestroyEnemy(iEnemyIndex_:int):void
		{
			//call Destroy() function for the enemy
			aEnemies[iEnemyIndex_].Destroy();
			//remove the enemy from the stage
			sMyStage.removeChild(aEnemies[iEnemyIndex_])
			//replace the enemy with a null value
			aEnemies[iEnemyIndex_] = null;
			//remove the null value from the aEnemies array
			aEnemies.splice(iEnemyIndex_, 1);
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for checking for collision and taking action based
				on it.
			Parameters:
				- e_:Event - used to update every frame
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function CollisionCheck(e_:Event)
		{
			//cycle through Enemies in aEnemies
			for(var iIndex:int = 0; iIndex < aEnemies.length; ++iIndex)
			{
				//if Hero hits an Enemy in aEnemies[iIndex]
				if(heroMainCharacter.hitTestObject(aEnemies[iIndex]))
				{
					//call hero's GotHit() function and send in damage
					heroMainCharacter.GotHit(aEnemies[iIndex].iDamage)
					//change bIsDead of object that hit the hero
					aEnemies[iIndex].bIsDead = true;

					//check if hero's health < 0
					if(heroMainCharacter.iHealthValue <= 0)
					{
						//call the destroy function
						Destroy();
					}
				}
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for updating the bIsDead variable for enemies and
				calling the DestroyEnemies() function
			Parameters:
				- e_:Event - used to update every frame
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function RemoveDeadEnemies(e_:Event):void
		{
			//cycle through aEnemies
			for(var iIndex:int = 0; iIndex < aEnemies.length; ++iIndex)
			{
				//if a Enemy is bIsDead == true
				if(aEnemies[iIndex].bIsDead == true)
				{
					//call the DestroyEnemies function
					DestroyEnemy(iIndex);
				}
			}
			
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for updating the text fields
			Parameters:
				- e_:Event - used to update every frame
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function HUDDisplay(e_:Event)
		{
			//update text for GameObjects.uiNumberOfObjects
			tNumOfObjects.text = String("Number of Objects " + GameObject.uiNumberOfObjects);
			//update text for Hero.iHealth
			tHeroHealth.text = String("Hero Health " + heroMainCharacter.iHealthValue);
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for ending the game when the player dies
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function Destroy()
		{
			//remove the hero from the stage
			sMyStage.removeChild(heroMainCharacter);
			//remove the text for hero health
			sMyStage.removeChild(tHeroHealth);
			//remove the text for objects
			sMyStage.removeChild(tNumOfObjects);
			//remove all objects from the array from the stage
			for(var iIndex:int = 0; iIndex < aEnemies.length; ++iIndex)
			{
				//remove from the stage
				sMyStage.removeChild(aEnemies[iIndex]);
			}
			
			//remove all event listeners
			sMyStage.removeEventListener(MouseEvent.MOUSE_MOVE, PlayerMovement);
			sMyStage.removeEventListener(Event.ENTER_FRAME, GenerateEnemies);
			sMyStage.removeEventListener(Event.ENTER_FRAME, CollisionCheck);
			sMyStage.removeEventListener(Event.EXIT_FRAME, RemoveDeadEnemies);
			sMyStage.removeEventListener(Event.ENTER_FRAME, HUDDisplay);
		}
	}
}