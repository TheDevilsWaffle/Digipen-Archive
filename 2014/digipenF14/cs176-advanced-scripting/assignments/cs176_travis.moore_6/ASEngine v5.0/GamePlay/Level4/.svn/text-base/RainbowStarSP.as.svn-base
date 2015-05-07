/***************************************************************************************/
/*
	filename   	RainbowStar.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	This class handles the bullet movement.
*/        	 
/***************************************************************************************/
package GamePlay.Level4
{
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	import flash.geom.Point;

	public class RainbowStarSP extends GameObject
	{		
		public var nSpeed:Number;
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		public function RainbowStarSP(nPosX_:Number, 
							   nPosY_:Number,
							   nSpeed_:Number,
							   iCollisionType_:int)
		{
			super(new RainbowStar(), nPosX_, nPosY_,GamePlayGlobals.GO_RAINBOWSTAR,iCollisionType_);
			nSpeed = nSpeed_;
			EnablePhysics();
			physicsinfo.AddForce(3, new Point(0,1), 200);
			physicsinfo.AddForce(Infinity, new Point(Math.random()*2-1,0), 100);
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
			displayobject.rotation += 5;
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
			switch(CInfo.gameobjectCollidedWith.iID)
			{
				case GamePlayGlobals.GO_SHINYBULLET:
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
			super.Destroy()
		}
	}
}