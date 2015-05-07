/***************************************************************************************/
/*
	filename   	fuel.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		09/04/2014 
	
	brief:
	This class handles the fuel's movement and keeps track of its health value
*/        	 
/***************************************************************************************/
package GamePlay
{
	import Engine.InputManager;
	import Engine.ObjectManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import flash.ui.Keyboard;

	public class fuel extends GameObject
	{
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function fuel(nPosX_:Number, nPosY_:Number)
		{
			super(new myFuel(), nPosX_, nPosY_, GamePlayGlobals.GO_STAR);
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
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
			//code here.
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
		override public function CollisionReaction(iCollidedWithObjectType_:int):void
		{
			switch(iCollidedWithObjectType_)
			{
				case GamePlayGlobals.GO_PLAYER:
				{
					Level1.iFuel += 50;
					if(Level1.iFuel > 100)
					{
						Level1.iFuel = 100;
					}
					bIsDead = true;
					ObjectManager.AddObject(new fuel((20 + (740 - 20) * Math.random()),(50 + (500 - 50) * Math.random())),"the_fuel" , ObjectManager.OM_DYNAMICOBJECT);
				}
				break;
			}
		}
	}
}