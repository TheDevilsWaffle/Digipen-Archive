/***************************************************************************************/
/*
	filename   	missile.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		09/04/2014 
	
	brief:
	This class handles the missile's movement and keeps track of its health value
*/        	 
/***************************************************************************************/
package GamePlay
{
	import Engine.InputManager;
	import Engine.ObjectManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import flash.ui.Keyboard;
	import flash.text.TextField;
	import flash.geom.Point;
	import flash.display.DisplayObject;

	public class missile extends GameObject
	{
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
		public var nSpeed:Number;
		public var nRotationSpeed:Number;
		public var pPoint:Point;
		public var doPlayer:DisplayObject;
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function missile(nPosX_:Number, nPosY_:Number, nSpeed_:Number, nRotationSpeed_:Number)
		{
			super(new myMissile(), nPosX_, nPosY_, GamePlayGlobals.GO_MISSILE);
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
			nSpeed = nSpeed_;
			nRotationSpeed = nRotationSpeed_;
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
			
			//set player to be a standin variable for the player object
			doPlayer = ObjectManager.GetObjectByName("the_player" , ObjectManager.OM_DYNAMICOBJECT).displayobject;
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
			if(!bIsDead)
			{
				pPoint = new Point(doPlayer.x - displayobject.x, doPlayer.y - displayobject.y)
				pPoint.normalize(1);
				
				displayobject.x += pPoint.x * nSpeed;
				displayobject.y += pPoint.y * nSpeed;
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
		override public function CollisionReaction(iCollidedWithObjectType_:int):void
		{
			switch(iCollidedWithObjectType_)
			{
				case GamePlayGlobals.GO_PLAYER:
				{
					bIsDead = true;
				}
				break;
			}
		}
	}
}