/***************************************************************************************/
/*
	filename   	Level1.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:

*/        	 
/***************************************************************************************/
package GamePlay.Level3
{
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	
	import flash.ui.Keyboard;
	import flash.display.DisplayObject;
	import flash.text.TextField;
	import flash.display.MovieClip;
	import flash.geom.Point;
	import GamePlay.MainMenu.MainMenu;
	import GamePlay.Level4.Level4;

	public class Level3 extends State
	{
		private var iTimer:int;
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		public function Level3()
		{
		}
		
		override public function Create():void
		{
			ObjectManager.AddObject(new SmallShipSP(50 , 200, 50, CollisionManager.CO_DYNAMIC), "SmallShip", ObjectManager.OM_DYNAMICOBJECT);
		}
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/		
		override public function Initialize():void
		{
			iTimer = 0;
		}
		
		public function GenerateStars()
		{
			if( (iTimer % 30) == 0 )
			{
				ObjectManager.AddObject(new RainbowStarSP(Math.random()*220 + 220, 10, Math.random()*25 + 50, CollisionManager.CO_DYNAMIC) , "RainbowStar" , ObjectManager.OM_DYNAMICOBJECT);
			}
			iTimer++;
		}
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
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
			
			if(InputManager.IsTriggered(Keyboard.N))
			{
				GameStateManager.GotoState(new Level4());
			}
			
			if(InputManager.IsTriggered(Keyboard.W))
			{
				PhysicsManager.AddGlobalForce(Infinity, new Point(-1,0), 25);
			}
			
			if(InputManager.IsTriggered(Keyboard.Q))
			{
				PhysicsManager.RemoveAllGlobalForces();
			}
			
			GenerateStars();
		}
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		override public function Uninitialize():void
		{
			ObjectManager.RemoveAllObjectsByName("ShinyBullet", ObjectManager.OM_DYNAMICOBJECT);
			ObjectManager.RemoveAllObjectsByName("RainbowStar", ObjectManager.OM_DYNAMICOBJECT);
			PhysicsManager.RemoveAllGlobalForces();
		}
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		override public function Destroy():void
		{
		}
	}// End of class 
}// End of package
