/***************************************************************************************/
/*
	filename   	Level2.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu, abichahine@digipen.edu
	date		04/01/2014 
	
	brief:
	This is the Level2 as file that creates, initializes, updates, uninitalizes, and
	otherwise runs everything that makes up Level2.
*/        	 
/***************************************************************************************/

package GamePlay.Level2
{
	//import necessary files
	import Engine.*;
	import Engine.ObjectManager;
	import Engine.InputManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import Engine.State;
	import Engine.ObjectManager;
	import GamePlay.Level2.Level2;
	import GamePlay.GamePlayGlobals;
	import GamePlay.MainMenu.MainMenu;
	import flash.ui.Keyboard;
	import flash.display.DisplayObject;
	import flash.text.TextField;
	import flash.display.MovieClip;
	
	public class Level2 extends State
	{
		//create a frame timer variable to keep track of frames
		private var iFrames:int;
		//create a timer variable to change player color
		private var iTimer:int;
		//create a score variable to keep track of player's points
		static public var iScore:int;
		//create a true/false state for if the game is over
		static public var bGameOver:Boolean;
		
		/*******************************************************************************/
		/*
			Description:
				Function used to create the initial game state
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Create():void
		{
			//create the player, give it a name, give it a type
			ObjectManager.AddObject(new Player(320, 240, 10), "Player", ObjectManager.OM_DYNAMICOBJECT);
			
			//text variable to keep track of the time
			var textfieldTimer:TextField = new TextField();
			textfieldTimer.scaleX = 2;
			textfieldTimer.scaleY = 2;
			textfieldTimer.text = "Timer = 10";
			//create the player TimerText object, give it an x position, give it a y position, give it a type, the add it to object manager, give it a name, give it a type 
			ObjectManager.AddObject(new GameObject(textfieldTimer,300, 0, GamePlayGlobals.GO_UI_TEXT), "TimerText" , ObjectManager.OM_STATICOBJECT);
			
			//text variable to keep track of the player's score
			var textfieldScore:TextField = new TextField();
			textfieldScore.x = 100;
			textfieldScore.scaleX = 2;
			textfieldScore.scaleY = 2;
			textfieldScore.text = "Score = 0";
			//create the player score text object, give it an x position, give it a y position, give it a type, the add it to object manager, give it a name, give it a type
			ObjectManager.AddObject(new GameObject(textfieldScore, 100, 0, GamePlayGlobals.GO_UI_TEXT), "ScoreText" , ObjectManager.OM_STATICOBJECT);
		}
		/*******************************************************************************/
		/*
			Description:
				Initialize function to set values to created variables
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/	
		override public function Initialize():void
		{
			//set level started to false
			bGameOver = false;
			//set score to 0
			iScore = 0;
			//set timer to 0
			iTimer = 10;
			//set frame timer
			iFrames = 0;
			
			//set timer text
			TextField((ObjectManager.GetObjectByName("TimerText" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Timer = " + iTimer;
			//set score text
			TextField((ObjectManager.GetObjectByName("ScoreText" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Score = 0";
		}
		/*******************************************************************************/
		/*
			Description:
				Create enemies of varying color types that spawn in a random location
				along the radius of a circle that travel towards the center.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		private function GenerateEnemies():void
		{
			//every 25 frames
			if((iFrames % 25) == 0)
			{
				//temporary variables to establish position and speed
				var iPosX:int;
				var iPosY:int;
				var iSpeed:int;
				
				//store a random number to be shared as the angle for both x and y
				var nRandom:Number = Math.random() * Math.PI * 2
				
				//get x angle * radius of 400
				iPosX = Math.cos(nRandom) * 400;
				//get y angle * radius of 400
				iPosY = Math.sin(nRandom) * 400;
				//get iSpeed randomly between 10 and 5
				iSpeed += Math.random() * ((10 - 5 + 1) + 5);
				
				//add this dot object to the game
				ObjectManager.AddObject(new Dot(iPosX, iPosY, iSpeed), "Dot", ObjectManager.OM_DYNAMICOBJECT);
			}
		}
		/*******************************************************************************/
		/*
			Description:
				Function that keeps track of game state based on player input and also
				keeps track of frames passed, timers, and if the game is over or not.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			//if game is over
			if(bGameOver == true)
			{
				//perform uninitialize
				Uninitialize();
				//exit out of Update loop
				return;
			}
			
			//if player wants a new game
			if(InputManager.IsTriggered(Keyboard.N))
			{
				GameStateManager.GotoState(new Level2());
			}
			//if player wants the main menu
			if(InputManager.IsTriggered(Keyboard.M))
			{
				GameStateManager.GotoState(new MainMenu());
			}
			//if player wants to restart
			if(InputManager.IsTriggered(Keyboard.R))
			{
				GameStateManager.RestartState();
			}
			
			//generate a Dot
			GenerateEnemies();
			
			//update our frame counter
			++iFrames;
			
			//every second that passes
			if(iFrames % 24 == 0)
			{
				//if the iTimer has reached zero, reset it back to 10
				if(iTimer == 1)
				{
					//reset iTimer to 10
					iTimer = 10;
					//set iTimer text
					TextField((ObjectManager.GetObjectByName("TimerText" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Timer = " + iTimer;
					
					//create a player variable
					var thePlayer = ObjectManager.GetObjectByName("Player", ObjectManager.OM_DYNAMICOBJECT);
					//call player's random color
					thePlayer.RandomColor();
				}
				//else keep counting down to zero
				else
				{
					//decrement iTimer
					--iTimer;
					//set iTimer text
					TextField((ObjectManager.GetObjectByName("TimerText" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Timer = " + iTimer;
				}
			}
		}
		/*******************************************************************************/
		/*
			Description:
				Function that removes objects from the screen when called
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Uninitialize():void
		{
			//remove the dots
			ObjectManager.RemoveAllObjectsByName("Dot", ObjectManager.OM_DYNAMICOBJECT);
			//remove the player
			ObjectManager.RemoveAllObjectsByName("Player", ObjectManager.OM_DYNAMICOBJECT);
			//remove the timer text
			ObjectManager.RemoveAllObjectsByName("TimerText", ObjectManager.OM_STATICOBJECT);
		}
	}
}
