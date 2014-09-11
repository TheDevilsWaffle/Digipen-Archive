/***************************************************************************************/
/*
	filename   	Level1.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu, eabichahine@digipen.edu
	date		04/18/2014 
	
	brief:

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
	import GamePlay.GamePlayGlobals;
	import GamePlay.MainMenu.MainMenu;
	import flash.ui.Keyboard;
	import flash.display.DisplayObject;
	import flash.text.TextField;
	import flash.display.MovieClip;
	import flash.media.SoundChannel;
	import GamePlay.Win.WinMenu;

	public class Level1 extends State
	{
		private var iTimer:int;
		static public var iScore:int;
		static public var bLevelStarted:Boolean;
		static public var textfieldShipHealth:TextField; 
		var timerStuff:int;
		var gameTimer:int;
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Create():void
		{	//add in the background
			ObjectManager.AddObject(new MainBg(380, 300), "MainBG", ObjectManager.OM_STATICOBJECT);
			//add top wall
			ObjectManager.AddObject(new HorizontalWall(380, 0), "HorizontalWallTop", ObjectManager.OM_DYNAMICOBJECT);
			//add bottom wall
			ObjectManager.AddObject(new HorizontalWall(380, 590), "HorizontalWallBottom", ObjectManager.OM_DYNAMICOBJECT);
			//add left wall
			ObjectManager.AddObject(new VerticalWall(0, 400), "VericalWallLeft", ObjectManager.OM_DYNAMICOBJECT);
			//add right wall
			ObjectManager.AddObject(new VerticalWall(760, 400), "VeritcallWallRight", ObjectManager.OM_DYNAMICOBJECT);
			//add in the ship
			ObjectManager.AddObject(new ShipSP(60, 320, 10),"MyShip" , ObjectManager.OM_DYNAMICOBJECT);
			//add in the countdown
			ObjectManager.AddObject(new CountdownMC(320,240),"Countdown" , ObjectManager.OM_STATICOBJECT);
			textfieldShipHealth = new TextField();
			textfieldShipHealth.x = 20;
			textfieldShipHealth.y = 20;
			textfieldShipHealth.textColor = 0xFFFFFF;
			textfieldShipHealth.scaleX = 2;
			textfieldShipHealth.scaleY = 2;
			textfieldShipHealth.text = "Health = 50";
			ObjectManager.AddObject(new GameObject(textfieldShipHealth,300, 20, GamePlayGlobals.GO_UI_TEXT), "HealthText" , ObjectManager.OM_STATICOBJECT);
			
			var textfieldScore:TextField = new TextField();
			textfieldScore.x = 100;
			textfieldScore.y = 20;
			textfieldScore.textColor = 0xFFFFFF;
			textfieldScore.scaleX = 2;
			textfieldScore.scaleY = 2;
			textfieldScore.text = "Timer = 60";
			ObjectManager.AddObject(new GameObject(textfieldScore, 100, 20, GamePlayGlobals.GO_UI_TEXT), "TimerText" , ObjectManager.OM_STATICOBJECT);
		
			
		}
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/	
		override public function Initialize():void
		{
			bLevelStarted = false;
			iScore = 0;
			iTimer = 0;
			timerStuff = 60;
			gameTimer = 0;
			TextField((ObjectManager.GetObjectByName("HealthText" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Health = 100";
			TextField((ObjectManager.GetObjectByName("TimerText" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Timer = 60";
			
			//start bg music
			var levelMusic = new bgmusic;
			var channel:SoundChannel = levelMusic.play(0, 9999);
		}

		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		private function GenerateEnemies():void
		{
			if( (iTimer % 48) == 0 )
			{
				ObjectManager.AddObject(new EnemySP(Math.random() * (640 - 600 + 1) + 600, Math.random() * (440 - 40 + 1) + 40, Math.random() * (20 - 10 + 1) + 10) , "Enemy" , ObjectManager.OM_DYNAMICOBJECT);
			}
			++iTimer;
		}
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			if(InputManager.IsTriggered(Keyboard.M))
			{
				GameStateManager.GotoState(new MainMenu());
			}
			
			if(InputManager.IsTriggered(Keyboard.R))
			{
				GameStateManager.RestartState();
			}
			
			if(bLevelStarted == true)
			{	
				GenerateEnemies();
				if((gameTimer % 24) == 0)
				{
					timerStuff -= 1;
					var gameobjectTime:GameObject = ObjectManager.GetObjectByName("TimerText" , ObjectManager.OM_STATICOBJECT);
					TextField(gameobjectTime.displayobject).text = "Timer = " + String(timerStuff);
				}
				++gameTimer;
			}
			//win condition
			if(iTimer == 1440)
			{
				GameStateManager.GotoState(new WinMenu);
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Uninitialize():void
		{
			ObjectManager.RemoveAllObjectsByName("Bullet", ObjectManager.OM_DYNAMICOBJECT);
			ObjectManager.RemoveAllObjectsByName("Enemy", ObjectManager.OM_DYNAMICOBJECT);
		}
	}
}
