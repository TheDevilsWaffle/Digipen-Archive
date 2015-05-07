/***************************************************************************************/
/*
	filename   	EnemySP.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		09/10/2013
	
	brief:
	This class handles the enemy creation, movement and collision behavior.
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import GamePlay.GamePlayGlobals;
	import Engine.GameObject;
	import Engine.CollisionInfo;
	import Engine.CollisionManager;
	import flash.geom.Point;
	
	public class EnemySP extends GameObject
	{	
		/* Used to move the enemy in the right direction */
		private var  pDirection:Point;
		/* Used as speed value when moving the enemy */
		private var nSpeed:Number;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for creating/initializing the enemy's variables.
			
			Parameters:
				- nPosX_: the enemy's x position
				
				- nPosY_: the enemy's y position
				
				- nSpeed_: the enemy's speed value.
				
				- pDirection_: the enemy's direction. It is used with the speed variable
							   in order to move the enemy
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function EnemySP(nPosX_:Number, nPosY_:Number, 
								nSpeed_:Number, pDirection_:Point)
		{
			super(new Enemy(), nPosX_, nPosY_, GamePlayGlobals.GO_ENEMY);
			iCollisionType = CollisionManager.CO_DYNAMIC;
			 nSpeed = nSpeed_;
			 pDirection =  new Point(pDirection_.x, pDirection_.y);
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for moving the enemy and deciding when it is
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
			
			if(displayobject.y > 580)
			{
				bIsDead = true;
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				This function decides the enemy's behavior when collided with other 
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
				case GamePlayGlobals.GO_BULLET:
				{
					Level1.uiScore++;
					bIsDead = true;
				}
				break;
			}
		}		
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for cleanly destroying the enemy instance.
			
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