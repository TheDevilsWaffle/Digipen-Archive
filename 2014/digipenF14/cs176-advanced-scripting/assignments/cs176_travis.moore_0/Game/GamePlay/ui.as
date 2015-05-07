/***************************************************************************************/
/*
	filename   	ui.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		09/04/2014 
	
	brief:
	This class handles the ui class
*/        	 
/***************************************************************************************/
package GamePlay
{
	import Engine.InputManager;
	import Engine.ObjectManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import flash.text.TextField;

	public class ui extends GameObject
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
		public function ui(nPosX_:Number, nPosY_:Number)
		{
			super(new myUi(), nPosX_, nPosY_, GamePlayGlobals.GO_UI);
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
		/*
		override public function CollisionReaction(iCollidedWithObjectType_:int):void
		{
			switch(iCollidedWithObjectType_)
			{
				case GamePlayGlobals.GO_Player:
				{
					
				}
				break;
			}
		}
		*/
	}
}