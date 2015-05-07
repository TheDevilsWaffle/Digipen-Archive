/***************************************************************************************/
/*
	filename   	BulletSP.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		09/10/2013
	
	brief:
	This class handles the bullet's creation and behavior.
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import GamePlay.GamePlayGlobals;
	import Engine.GameObject;
	import Engine.CollisionInfo;
	import Engine.CollisionManager;
	import flash.geom.Point;
	
	public class BulletSP extends GameObject
	{		
		/* Used as speed value when moving the bullet */
		private var nSpeed:Number;
		/* Used to move the bullet in the right direction */
		private var pDirection:Point;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for creating/initializing the bullet's variables.
			
			Parameters:
				- nPosX_: the bullet's x position
				
				- nPosY_: the bullet's y position
				
				- nSpeed_: the bullet's speed value.
				
				- pDirection_: the bullet's direction. It is used with the speed variable
							   in order to move the bullet
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function BulletSP(nPosX_:Number, nPosY_:Number, nSpeed_:Number, pDirection_:Point)
		{
			super(new Bullet(),nPosX_, nPosY_, GamePlayGlobals.GO_BULLET);
			iCollisionType = CollisionManager.CO_DYNAMIC;
			nSpeed = nSpeed_;
			pDirection =  new Point(pDirection_.x, pDirection_.y);
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for moving the bullet and deciding when it is
				out of the viewport to destroy it.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			displayobject.x += pDirection.x * nSpeed;
			displayobject.y += pDirection.y * nSpeed;
			
			if(displayobject.x < -30 || displayobject.x > 790 || displayobject.y < -30)
			{
				bIsDead = true;
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				This function decides the bullet's behavior when collided with other 
				objects.
			
			Parameters:
				- CInfo_: The collision information that contains the object that we are 
						  colliding with.
				
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
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for cleanly destroying the bullet instance.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Destroy():void
		{
			pDirection = null;
		}
	}
}