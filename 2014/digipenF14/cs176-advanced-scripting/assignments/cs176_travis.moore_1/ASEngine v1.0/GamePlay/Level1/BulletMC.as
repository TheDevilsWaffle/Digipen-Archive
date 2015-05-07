/***************************************************************************************/
/*
	filename   	BulletMC.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore
	date		09/09/2014 
	
	brief:
	This class handles the properties and methods of the bullet.
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import Engine.ObjectManager;
	import Engine.GameObject;
	import GamePlay.GamePlayGlobals;
	import Engine.CollisionInfo;
	import GamePlay.Level1.Level1;
	import flash.text.TextField;

	public class BulletMC extends GameObject
	{		
		public var nSpeed:Number;
		public var nPlayerRotation:Number;
		public var nDirectionX:Number;
		public var nDirectionY:Number;
		public var nPosX:Number;
		public var nPosY:Number;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor used to set attributes based on passed parameters.
				
			Parameters:
				- nPosX_:Number          - Set the starting x for the object.
				- nPosY_:Number          - Set the starting y for the object.
				- nSpeed_:Number         - Set the starting speed for the object.
				- nPlayerRotation:Number - Set the starting rotation speed for the object 
				                           based on the player's rotation.
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function BulletMC(nPosX_:Number, nPosY_:Number, nSpeed_:Number, nPlayerRotation_:Number):void
		{
			super(new Bullet() , nPosX_, nPosY_, GamePlayGlobals.GO_BULLET);
			nSpeed = nSpeed_;
			nPosX = nPosX_;
			nPosY = nPosY_;
			nPlayerRotation = nPlayerRotation_;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Initialize() is used to set the initial starting values for the
				bullet's attributes and determine the path it will take.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Initialize():void
		{
			//initialize x and y variables
			displayobject.x = nPosX;
			displayobject.y = nPosY;
			
			//determine direction used in Update() pased on player's rotation and maths
			nDirectionX = Math.cos(nPlayerRotation * Math.PI/180 - Math.PI/2);
			nDirectionY = Math.sin(nPlayerRotation * Math.PI/180 - Math.PI/2);
		}
		
		/*******************************************************************************/
		/*
			Description:
				As long as the bullet is not dead, Update() moves the bullet along its
				predetermined path.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			//if the bullet is out of bounds, kill it
			if(displayobject.y <= 0)
			{
				bIsDead = true;
			}
			
			//as long as the bullet is not dead, move it along it's path
			if(!bIsDead)
			{
				displayobject.x += nDirectionX * nSpeed;
				displayobject.y += nDirectionY * nSpeed;
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
				//if the bullet hits and enemy destroy the bullet, add to Level1.iScore, and update the_score text.
				case GamePlayGlobals.GO_ENEMY:
				{
					bIsDead = true;
					
					++Level1.iScore;
					TextField((ObjectManager.GetObjectByName("the_score" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Score = " + String(Level1.iScore);	
				}
				break;
			}
		}
	}
}