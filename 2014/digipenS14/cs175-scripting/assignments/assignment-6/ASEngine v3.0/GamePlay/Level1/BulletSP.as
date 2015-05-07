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
package GamePlay.Level1
{
	import Engine.ObjectManager;
	import Engine.GameObject;
	import GamePlay.GamePlayGlobals;
	import Engine.CollisionInfo;
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
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			switch(CInfo_.gameobjectCollidedWith.iType)
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