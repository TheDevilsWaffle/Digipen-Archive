/***************************************************************************************/
/*
	filename   	Level1.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		09/12/2013 
	
	brief:
	This class contains level1's gameplay. Mainly handles the enemy creation, score 
	update and uninitializing the righ objects objects when restarting the level.
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import Engine.*;
	import GamePlay.MainMenu.MainMenu;
	import GamePlay.Level2.Level2;
	import flash.ui.Keyboard;
	import flash.text.TextField;
	import flash.geom.Point;
	
	public class Level1 extends State
	{
		/* Integer Timer used to decide when an enemy is created */
		private var iTimer:int;
		/* This unsigned integer is used as the score value. It is static in 
		order to */
		static public var uiScore:uint;
		private var textfieldScore:TextField;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor that is only responsible for instantiating the level.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Level1() 
		{
		}
		
		/*******************************************************************************/
		/*
			Description:
				The create function is responsible for adding the turret (player)
				and the hud objects (score)
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Create():void
		{
			ObjectManager.AddObject(new TurretSP(380,500,2), "Turret", ObjectManager.OM_DYNAMICOBJECT);
			
			textfieldScore = new TextField();
			textfieldScore.text = "Score = 0";
			textfieldScore.scaleX = 3;
			textfieldScore.scaleY = 3;
			textfieldScore.mouseEnabled = false;
			
			ObjectManager.AddObject(new GameObject(textfieldScore,20,20), "ScoreText", ObjectManager.OM_STATICOBJECT);
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for resetting the timer and the score 
				when we first go to this level or when we restart it.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Initialize():void
		{
			iTimer = 0;
			uiScore = 0;
		}

		/*******************************************************************************/
		/*
			Description:
				Most of the game logic is done here. An enemy is created every 50 
				frames. The score is updated every frame. Pressing 'R' restarts the
				level, pressing 'M' goes to the main menu and finally pressing 'N' 
				goes to Level2.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			if(InputManager.IsTriggered(Keyboard.R))
			{
				GameStateManager.RestartState();
			}
			
			if(InputManager.IsTriggered(Keyboard.N))
			{
				GameStateManager.GotoState(new Level2());
			}
			
			if(InputManager.IsTriggered(Keyboard.M))
			{
				GameStateManager.GotoState(new MainMenu());
			}
			
			if( (iTimer % 50) == 0)
			{
				var turret:GameObject = ObjectManager.GetObjectByName("Turret", ObjectManager.OM_DYNAMICOBJECT);
				var pEnemyPosition:Point = new Point(Math.random()*700 + 50, 50);
				
				var pDirection:Point = new Point();
				pDirection.x = turret.displayobject.x - pEnemyPosition.x;
				pDirection.y = turret.displayobject.y - pEnemyPosition.y;
				pDirection.normalize(1);
				
				ObjectManager.AddObject(new EnemySP(pEnemyPosition.x, pEnemyPosition.y, Math.random() * 5 + 5, pDirection),
										"Enemy", ObjectManager.OM_DYNAMICOBJECT);
				
				pEnemyPosition = null;
				pDirection = null;
			}
			
			++iTimer;
			
			textfieldScore.text = "Score = " + uiScore;
		}

		/*******************************************************************************/
		/*
			Description:
				This method is responsible for uninitializing the level. The main 
				task is to remove all enemies and bullets when we decide to 
				uninitialize.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Uninitialize():void
		{
			ObjectManager.RemoveAllObjectsByName("Enemy", ObjectManager.OM_DYNAMICOBJECT);
			ObjectManager.RemoveAllObjectsByName("Bullet", ObjectManager.OM_DYNAMICOBJECT);
		}
	}
}
