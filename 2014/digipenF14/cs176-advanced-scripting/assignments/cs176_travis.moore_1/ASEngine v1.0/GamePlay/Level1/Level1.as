/***************************************************************************************/
/*
	filename   	Level1.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		09/09/2014 
	
	brief:
	The Level1 class contains all the properties and methods of the Level1 class and
	basically is the stage for our game.
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import Engine.ObjectManager;
	import Engine.InputManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import Engine.State;
	import Engine.ObjectManager;
	import GamePlay.MainMenu.MainMenu;
	import GamePlay.Level1.*;
	import GamePlay.GamePlayGlobals;
	import flash.ui.Keyboard;
	import flash.display.DisplayObject;
	import flash.text.TextField;
	import flash.display.MovieClip;

	public class Level1 extends State
	{
		private var uiTimer:uint;
		static public var iScore:int;
		static public var bLevelStarted:Boolean;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor creates GameObjects like the turret, countdown, and score 
				text for use to set in Initialize().
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Create():void
		{
			ObjectManager.AddObject(new TurretMC(380, 490, 10),"the_turret" , ObjectManager.OM_DYNAMICOBJECT);
			ObjectManager.AddObject(new CountDownMC(380, 200),"the_countdown" , ObjectManager.OM_STATICOBJECT);

			var textfieldScore:TextField = new TextField();
			textfieldScore.scaleX = 2;
			textfieldScore.scaleY = 2;
			textfieldScore.text = "Score = 0";
			ObjectManager.AddObject(new GameObject(textfieldScore, 50, 50, GamePlayGlobals.GO_SCORE), "the_score" , ObjectManager.OM_STATICOBJECT);
		}
		
		/*******************************************************************************/
		/*
			Description:
				Initialize() sets Level1's default attribute values and starting score
				text.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/	
		override public function Initialize():void
		{
			//set attributes
			bLevelStarted = false;
			iScore = 0;
			uiTimer = 0;
			
			//set score text
			TextField((ObjectManager.GetObjectByName("the_score" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Score = 0";
		}

		/*******************************************************************************/
		/*
			Description:
				GenerateEnemies() creates a new enemy with a random starting x, y == 50,
				a random speed between 1 - 5, and sets the name and OM type.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		private function GenerateEnemies():void
		{
			//give the enemy a random starting x, y = 50, and a random speed from 1 - 5.
			ObjectManager.AddObject(new EnemyMC((0 + (760 - 0) * Math.random()), 50, (1 + (5 - 1) * Math.random())), "the_enemy" , ObjectManager.OM_DYNAMICOBJECT);
		}
		
		/*******************************************************************************/
		/*
			Description:
				Update() is used to perform actions based on player input, as well as
				populate the world with GenerateEnemies() based on uiTimer.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			//M key returns to the MainMenu
			if(InputManager.IsTriggered(Keyboard.M))
			{
				GameStateManager.GotoState(new MainMenu());
			}
			
			//R key restarts Level1
			if(InputManager.IsTriggered(Keyboard.R))
			{
				GameStateManager.RestartState();
			}
			
			//if the level has started, call GenerateEnemies()
			if(bLevelStarted == true)
			{	
				//if the uiTimer % 50
				if( (uiTimer % 50) == 0 )
				{
					GenerateEnemies();
				}
				
				//increment uiTimer
				++uiTimer;
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				Unitialize() performs all default GameObject class functions.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Uninitialize():void
		{
		}
	}
}
