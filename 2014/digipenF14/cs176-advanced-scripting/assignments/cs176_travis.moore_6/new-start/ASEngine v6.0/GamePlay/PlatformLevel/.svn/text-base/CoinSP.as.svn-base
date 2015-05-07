/***************************************************************************************/
/*
	filename   	CoinSP.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	The CoinSP class creates a coin.
	
*/        	 
/***************************************************************************************/
package GamePlay.Level2
{
	import Engine.GameObject;
	import Engine.CollisionInfo;
	import Engine.CollisionManager;
	import GamePlay.GamePlayGlobals;
	
	public class CoinSP extends GameObject
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
		public function CoinSP()
		{
			super(new Coin(),0,0,GamePlayGlobals.GO_COIN, CollisionManager.CO_DYNAMIC);
			Level2.iNumberOfCoins++;
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
				if(CInfo_.gameobjectCollidedWith.iType == GamePlayGlobals.GO_HERO)
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
			Level2.iNumberOfCoins--;
		}
	}

}