/***************************************************************************************/
/*
	filename   	Level1.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:

*/        	 
/***************************************************************************************/
package GamePlay
{
	import Engine.ObjectManager;
	import Engine.InputManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import Engine.State;
	import Engine.ObjectManager;
	import flash.ui.Keyboard;
	import flash.display.DisplayObject;
	import flash.text.TextField;
	import flash.display.MovieClip;

	public class Level1 extends State
	{
		private var iTimer:int;
		static public var iScore:int;
		static public var bLevelStarted:Boolean;
		
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
		{
			ObjectManager.AddObject(new ShipSP(50 , 200, 10),"Ship" , ObjectManager.OM_DYNAMICOBJECT);
			ObjectManager.AddObject(new CountdownMC(275,200),"Countdown" , ObjectManager.OM_STATICOBJECT);
			
			var textfieldShipHealth:TextField = new TextField();
			textfieldShipHealth.scaleX = 2;
			textfieldShipHealth.scaleY = 2;
			textfieldShipHealth.text = "Health = 100";
			ObjectManager.AddObject(new GameObject(textfieldShipHealth,300, 0, GamePlayGlobals.GO_UI_TEXT), "HealthText" , ObjectManager.OM_STATICOBJECT);
			
			var textfieldScore:TextField = new TextField();
			textfieldScore.x = 100;
			textfieldScore.scaleX = 2;
			textfieldScore.scaleY = 2;
			textfieldScore.text = "Score = 0";
			ObjectManager.AddObject(new GameObject(textfieldScore, 100, 0, GamePlayGlobals.GO_UI_TEXT), "ScoreText" , ObjectManager.OM_STATICOBJECT);
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
			TextField((ObjectManager.GetObjectByName("HealthText" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Health = 100";
			TextField((ObjectManager.GetObjectByName("ScoreText" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Score = 0";
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
			if( (iTimer % 10) == 0 )
			{
				ObjectManager.AddObject(new EnemySP(550, Math.random()*350 + 25 , Math.random()*5 + 10) , "Enemy" , ObjectManager.OM_DYNAMICOBJECT);
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
