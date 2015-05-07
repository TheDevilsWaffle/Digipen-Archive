/***************************************************************************************/
/*
	filename   	CoinSP.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		10/06/2014
	
	brief:
	The CoinSP class creates a coin.
	
*/        	 
/***************************************************************************************/
package GamePlay.Level2
{
	//import
	import Engine.GameObject;
	import Engine.CollisionInfo;
	import GamePlay.GamePlayGlobals;
	
	public class CoinSP extends GameObject
	{	
	
		/*******************************************************************************/
		/*
			Description:
				The constructor function sets the location and GamePlayGlobals type
			
			Parameters:
				- nXPos_:Number - x position
				- nYPos_:Number - y position
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function CoinSP(nXPos_:Number, nYPos_:Number):void
		{
			super(new Coin(),0,0,GamePlayGlobals.GO_COIN);
			
			//set location
			displayobject.x = nXPos_;
			displayobject.y = nYPos_;	
		}
		
		/*******************************************************************************/
		/*
			Description:
				The CollisionReaction() function takes the CInfo type and performs actions
				based on which object this object hits.
			
			Parameters:
				- CInfo_:CollisionInfo - iType of an object
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				case GamePlayGlobals.GO_HERO:
				{
					//destroy the object
					bIsDead = true;
					break;
				}
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				The Destroy() function calls the GameObject's Destroy() to remove the
				object
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Destroy():void
		{
			super.Destroy();
		}
	}

}