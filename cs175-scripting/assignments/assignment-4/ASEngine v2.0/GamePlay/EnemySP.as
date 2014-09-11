/***************************************************************************************/
/*
	filename   	EnemySP.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	This class handles the enemy's creation, movement , collision recation and destruction.
*/        	 
/***************************************************************************************/
package GamePlay
{
	import Engine.ObjectManager;
	import Engine.GameObject;
	import flash.text.TextField;
	
	public class EnemySP extends GameObject
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
		public function EnemySP(nPosX_:Number, 
							  nPosY_:Number,
							  nSpeed_:Number)
		{
			super(new enemy() , nPosX_, nPosY_, GamePlayGlobals.GO_ENEMY);
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
			displayobject.x -= nSpeed;
			if( displayobject.x < -10 )
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
				case GamePlayGlobals.GO_BULLET:
				{
					bIsDead = true;
					
					Level1.iScore += 10;
					var gameobjectScore:GameObject = ObjectManager.GetObjectByName("ScoreText" , ObjectManager.OM_STATICOBJECT);
					TextField(gameobjectScore.displayobject).text = "Score = " + String(Level1.iScore); 
					
				}
				break;
				
				case GamePlayGlobals.GO_SHIP:
				{
					bIsDead = true;
				}
				break;
			}
		}
	}
}