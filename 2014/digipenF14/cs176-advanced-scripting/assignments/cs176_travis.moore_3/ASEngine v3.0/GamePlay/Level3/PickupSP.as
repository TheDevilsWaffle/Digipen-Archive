/***************************************************************************************/
/*
	filename   	PlayerSP.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date		10/16/2014 
	
	brief:
	The PickupSP class creates a thing to pick up.
	
*/        	 
/***************************************************************************************/
package GamePlay.Level3
{
	import Engine.GameObject;
	import Engine.CollisionInfo;
	import Engine.CollisionManager;
	import GamePlay.GamePlayGlobals;
	import GamePlay.Level3.Level3;
	
	public class PickupSP extends GameObject
	{	
		/*******************************************************************************/
		/*
			Description:
				Constructor that creates a coin instance and initializes all its
				properties.
				
			Parameters:
				- None
						 
			Return:
				- None
		*/
		/*******************************************************************************/
		public function PickupSP()
		{
			super(new MyPickup(),0,0,GamePlayGlobals.GO_PICKUP, CollisionManager.CO_DYNAMIC);
			Level3.iNumberOfPickups++;
		}
		
		/**************************************************************************
		/*
			Description:
				This method will check if the coin is colliding with the hero so
				that we destroy the coin.
				
			Parameters:
				- CInfo_: The collision information. It includes the collision type, 
				         collided with object, tile map information...
						 
			Return:
				- None
		*/
		/*************************************************************************/
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			if(CInfo_.iCollisionType == CollisionManager.CO_DYNAMIC)
			{
				if(CInfo_.gameobjectCollidedWith.iType == GamePlayGlobals.GO_PLAYER)
				{
					bIsDead = true;
				}
			}
		}
		
		/**************************************************************************
		/*
			Description:
				This method destroys the coin and updates the level's coins number.
				
			Parameters:
				- None
						 
			Return:
				- None
		*/
		/*************************************************************************/
		override public function Destroy():void
		{
			super.Destroy();
			Level3.iNumberOfPickups--;
		}
	}

}