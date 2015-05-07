/***************************************************************************************/
/*
	filename   	player.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		09/04/2014 
	
	brief:
	This class handles the player's movement and keeps track of its health value
*/        	 
/***************************************************************************************/
package GamePlay
{
	import Engine.InputManager;
	import Engine.ObjectManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import flash.ui.Keyboard;
	import flash.text.TextField;
	import flash.events.KeyboardEvent;
	import GamePlay.Level1;

	public class player extends GameObject
	{
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
		public var nVelocity:Number;
		public var nCurrentSpeed:Number;
		public var nMaxSpeed:Number;
		public var nRotationSpeed:Number;
		public var nDragFactor:Number;
		public var nDirectionX:Number;
		public var nDirectionY:Number;
		public var bPickupStar:Boolean;
		
		/***************************************************************************************/
		/*
		Description:
			This function intializes the player's ship with the x pos, y pos, max velocity, 
			speed, rotation speed, and also the drag factor
		
		Parameters:
			_nXPos:Number
			_nYPos:Number
			_nMaxVelocity:Number
			_nSpeed:Number
			_nRotationSpeed:Number
			_nDragFactor:Number
				
		Return:
			void
		*/
		/***************************************************************************************/
		public function player(nPosX_:Number, nPosY_:Number, nVelocity_:Number, nMaxSpeed_:Number, nRotationSpeed_:Number, nDragFactor_:Number)
		{
			super(new myPlayer(), nPosX_, nPosY_, GamePlayGlobals.GO_PLAYER);
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
			nVelocity = nVelocity_;
			nMaxSpeed = nMaxSpeed_;
			nRotationSpeed = nRotationSpeed_;
			nDragFactor = nDragFactor_;
			nCurrentSpeed = 0;
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
		override public function Initialize():void
		{
			displayobject.x = nInitialPosX;
			displayobject.y = nInitialPosY;
			
			bPickupStar = false;
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
			if(Level1.iFuel != 0)
			{
				//as long as the player is alive
				if(!bIsDead)
				{
					if(InputManager.IsPressed(Keyboard.UP) && nVelocity < nMaxSpeed)
					{
						nCurrentSpeed += nVelocity;
					}
					if(InputManager.IsPressed(Keyboard.LEFT))
					{
						displayobject.rotation -= nRotationSpeed;
					}
					if(InputManager.IsPressed(Keyboard.RIGHT))
					{
						displayobject.rotation += nRotationSpeed;
					}
					
					//drag to reduce speed by nDragFactor (~1%)
					nCurrentSpeed *= nDragFactor;
				
					//set x and y direction of ship
					nDirectionX = Math.cos(displayobject.rotation * Math.PI / 180);
					nDirectionY = Math.sin(displayobject.rotation * Math.PI / 180);
				
					//increase per direction * velocity for x and y
					displayobject.x += nDirectionX * nCurrentSpeed;
					displayobject.y += nDirectionY * nCurrentSpeed;
				}
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
				case GamePlayGlobals.GO_MISSILE:
				{
					bIsDead = true;
					break;
				}
				case GamePlayGlobals.GO_STAR:
				{
					bPickupStar = true;
					break;
				}
				case GamePlayGlobals.GO_BASE:
				{
					if(bPickupStar == true)
					{
						Level1.iScore += 1;
						bPickupStar = false;
						ObjectManager.AddObject(new star((20 + (740 - 20) * Math.random()),(50 + (500 - 50) * Math.random())),"the_star" , ObjectManager.OM_DYNAMICOBJECT);
					}
					break;
				}
				break;
			}
		}
	}
}