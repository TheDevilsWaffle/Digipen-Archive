/***************************************************************************************/
/*
	filename   	ShipSP.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	This class handles the ship's movement and keeps track of its health value
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import Engine.InputManager;
	import Engine.ObjectManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import GamePlay.MainMenu.MainMenu;
	import GamePlay.GamePlayGlobals;
	import Engine.CollisionInfo;
	import flash.ui.Keyboard;
	import flash.text.TextField;
	

	public class ShipSP extends GameObject
	{
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
		public var nSpeed:Number;
		private var iHealth:int;
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function ShipSP(nPosX_:Number, nPosY_:Number, nSpeed_:Number)
		{
			super(new ship(), nPosX_, nPosY_, GamePlayGlobals.GO_SHIP);
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
			
			iHealth = 100;
			nSpeed = nSpeed_;
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
			displayobject.x = nInitialPosX;
			displayobject.y = nInitialPosY;
			iHealth = 100;
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
		public function get iHealthValue():int
		{
			return iHealth;
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
		public function set iHealthValue(iValue_:int):void
		{
			if(iValue_ < 0 || iValue_ > 100)
			{
				iValue_ = 0;
			}
			
			iHealth = iValue_;
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
		private function Shoot():void
		{
			ObjectManager.AddObject(new BulletSP(displayobject.x + 50, displayobject.y, 10), "Bullet" , ObjectManager.OM_DYNAMICOBJECT);
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
			if(Level1.bLevelStarted == true)
			{
				if(InputManager.IsPressed(Keyboard.UP))
				{
					displayobject.y -= nSpeed;
				}
				else if(InputManager.IsPressed(Keyboard.DOWN))
				{
					displayobject.y += nSpeed;
				}
				
				if(InputManager.IsTriggered(Keyboard.SPACE))
				{
					Shoot();
				}
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
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				case GamePlayGlobals.GO_ENEMY:
				{
					iHealth -= 10;
					
					var goHealth:GameObject = ObjectManager.GetObjectByName("HealthText" , ObjectManager.OM_STATICOBJECT);
					TextField(goHealth.displayobject).text = "Health = " + String(iHealth); 
					
					if(iHealth == 0)
					{
						GameStateManager.GotoState(new MainMenu());
					}
				}
				break;
			}
		}
	}
}