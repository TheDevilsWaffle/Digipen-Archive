/***************************************************************************************/
/*
	filename   	EnemyMC.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		09/09/2014 
	
	brief:
	This class handles the properties and methods of the ai controlled enemy.
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import Engine.ObjectManager;
	import Engine.GameObject;
	import GamePlay.GamePlayGlobals;
	import Engine.CollisionInfo;
	import flash.geom.Point;
	import flash.display.DisplayObject;

	public class EnemyMC extends GameObject
	{		
		public var nSpeed:Number;
		public var pPoint:Point;
		public var doPlayer:DisplayObject;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor used to set attributes based on passed parameters.
				
			Parameters:
				- nPosX_:Number   - Set the starting x for the object.
				- nPosY_:Number   - Set the starting y for the object.
				- nSpeed_:Number  - Set the starting speed for the object.
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function EnemyMC(nPosX_:Number, nPosY_:Number, nSpeed_:Number):void
		{
			super(new Enemy() , nPosX_, nPosY_, GamePlayGlobals.GO_ENEMY);
			nSpeed = nSpeed_;
			
			//set doPlayer to be an accessible displayobject variable for the player
			doPlayer = ObjectManager.GetObjectByName("the_turret" , ObjectManager.OM_DYNAMICOBJECT).displayobject;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Update() first checks to see if the enemy is beyond the bottom limits
				of the y axis, and if so sets bIsDead to true. Secondly, the enemy
				calculates the path towards the player using the Point class and moves
				according to its nSpeed towards the player.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			//check if the enemy is offscreen, if so change bIsDead to true
			if(displayobject.y > 550)
			{
				bIsDead = true;
			}
			//if enemy is not dead, continue moving towards the player
			if(!bIsDead)
			{
				//use doPoint object to determine path to player
				pPoint = new Point(doPlayer.x - displayobject.x, doPlayer.y - displayobject.y)
				pPoint.normalize(1);
				
				//move the enemy towards the player in the x and y axis
				displayobject.x += pPoint.x * nSpeed;
				displayobject.y += pPoint.y * nSpeed;
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				CollisionReaction() is used to determine the proper action to take for
				when this object collides with another object. The proper action to take
				depends upon the passed CInfo_ which connects to the GamePlayGlobals
				GO_ enums that we've created.
				
			Parameters:
				- CInfo_:CollisionInfo - an integer represeting the object that this
										 object has hit. This information corresponds
										 to the iType so that we can use GamePlayGlobals
			Return:
				- None
		*/
		/*******************************************************************************/
		
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				//enemy will die unless it collides with another enemy
				case GamePlay.GamePlayGlobals.GO_ENEMY:
				{
					break;
				}
				break;
				default:
				{
					bIsDead = true;
				}
				break;
			}
		}
		
	}
}