/***************************************************************************************/
/*
	filename   	BulletSP.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	This class handles the bullet movement.
*/        	 
/***************************************************************************************/
package GamePlay
{
	import Engine.ObjectManager;
	import Engine.GameObject;
	import flash.text.TextField;

	public class BulletSP extends GameObject
	{		
		public var nSpeed:Number;
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function BulletSP(nPosX_:Number, 
							   nPosY_:Number, 
							   nSpeed_:Number)
		{
			super(new bullet() , nPosX_, nPosY_, GamePlayGlobals.GO_BULLET);
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
		override public function Update():void
		{
			displayobject.x += nSpeed;
			if( displayobject.x > 560 )
			{
				bIsDead = true;
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
				case GamePlayGlobals.GO_ENEMY:
				{
					bIsDead = true;
				}
				break;

			}
		}
	}
}