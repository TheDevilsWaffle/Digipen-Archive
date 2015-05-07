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
package GamePlay.Level3
{
	import Engine.GameObject;
	import Engine.*;
	import Engine.CollisionManager;
	import GamePlay.GamePlayGlobals;
	import GamePlay.Level3.HeroSP;
	import flash.geom.Point;
	
	public class CoinSP extends GameObject
	{	
		public var nSpeed:Number;
		public var hero:GameObject;
		public var vX:Number;
		public var vY:Number;
		public var mag:Number;
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
			super(new Coin(),300,300,GamePlayGlobals.GO_COIN, CollisionManager.CO_DYNAMIC);
			nSpeed = 3;
			
			hero = ObjectManager.GetObjectByName("Hero", ObjectManager.OM_DYNAMICOBJECT);
		}
		
		final override public function Update():void
		{
			var targetPoint:Point = new Point(hero.displayobject.x - this.displayobject.x, 
											  hero.displayobject.y - this.displayobject.y);
			targetPoint.normalize(1);
			
			this.displayobject.x += targetPoint.x * nSpeed;
			this.displayobject.y += targetPoint.y * nSpeed;
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
					trace("gotcha");
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
		}
	}

}