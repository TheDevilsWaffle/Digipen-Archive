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
package GamePlay.Level3
{
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	import flash.geom.Point;

	public class ShinyBulletSP extends GameObject
	{		
		public var nSpeed:Number;
		private var pDirection:Point;
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		public function ShinyBulletSP(nPosX_:Number, 
							   nPosY_:Number,
							   pDirection_:Point,
							   nSpeed_:Number,
							   iCollisionType_:int)
		{
			super(new ShinyBullet(), nPosX_, nPosY_, GamePlayGlobals.GO_SHINYBULLET, iCollisionType_);
			nSpeed = nSpeed_;
			pDirection = pDirection_;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Disable the bullet once it goes outside the right side of the viewport
				
			Parameters:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			displayobject.x += pDirection.x * nSpeed;
			displayobject.y += pDirection.y * nSpeed;
			if( displayobject.x > 640 || displayobject.x < 0 || displayobject.y < 0 || displayobject.y > 480)
			{
				bIsDead = true;
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				Bullet Collision Reaction
			
			Parameters:
				- None
		*/
		/*******************************************************************************/
		override public function CollisionReaction(CInfo:CollisionInfo):void
		{
			switch(CInfo.gameobjectCollidedWith.iType)
			{
				case GamePlayGlobals.GO_RAINBOWSTAR:
				{
					bIsDead = true;
				}
				break;
			}
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
			super.Destroy();
		}
	}
}