/***************************************************************************************/
/*
	filename   	PlayerSP.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date		10/16/2014 
	
	brief:
	The PlayerSP class creates a hero and deals with its gameplay behavior.
	
*/        	 
/***************************************************************************************/
package GamePlay.Level3
{
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	import flash.ui.Keyboard;
	import flash.display.MovieClip;
	import GamePlay.Level3.Level3;

	final public class PlayerSP extends GameObject
	{
		/* Controls the hero's speed. Used for all four directions */
		public var nXSpeed:Number;
		public var nYSpeed:Number;
		static public var bInMotion:Boolean;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor that creates a hero instance and initializes all its
				properties.
				
			Parameters:
				- None
						 
			Return:
				- None
		*/
		/*******************************************************************************/
		final public function PlayerSP()
		{
			super(new MyPlayer(), 0, 0, GamePlayGlobals.GO_PLAYER, CollisionManager.CO_DYNAMIC | CollisionManager.CO_TILEMAP);
			nXSpeed = 0;
			nYSpeed = 0;
			UpdateHotSpots();
			bInMotion = false;
		}
		
		/*******************************************************************************/
		/*
			Description:
				The update function deals with the hero's movement.
				
			Parameters:
				- None
						 
			Return:
				- None
		*/
		/*******************************************************************************/
		final override public function Update():void
		{
			//if the player is not in motion
			if(!bInMotion)
			{
				if(InputManager.IsPressed(Keyboard.LEFT))
				{
					nXSpeed = -10;
					bInMotion = true;
					++Level3.uiMove;
				}
				if(InputManager.IsPressed(Keyboard.RIGHT))
				{
					nXSpeed = 10;
					bInMotion = true;
					++Level3.uiMove;
				}
				if(InputManager.IsPressed(Keyboard.DOWN))
				{
					nYSpeed = 10;
					bInMotion = true;
					++Level3.uiMove;
				}
				else if(InputManager.IsPressed(Keyboard.UP))
				{
					nYSpeed = -10;
					bInMotion = true;
					++Level3.uiMove;
				}
			}
			
			//move the object
			displayobject.y += nYSpeed;
			displayobject.x += nXSpeed;
		}
		
		/**************************************************************************
		/*
			Description:
				This method will check if any collision with certain tiles in the 
				tile map happened or if the hero collides with an open door in 
				order to switch to level 3.
				If collision with tile ID 1 or 2 happened we will need to snap 
				according to which side we collided (UP and DOWN will snap on the Y
				RIGHT and LEFT will snap on the X).
				
			Parameters:
				- CInfo_: The collision information. It includes the collision type, 
				         collided with object, tile map information...
						 
			Return:
				- None
		*/
		/*************************************************************************/
		final override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			//if we collide with the tilemap (which we always will)
			if(CInfo_.iCollisionType == CollisionManager.CO_TILEMAP)
			{
				//loop through our hotspots
				for(var iIndexHS:int = 0; iIndexHS < this.vHotSpots.length; ++iIndexHS)
				{
					//trace(vHotSpots[iIndexHS].iCollidedTilesID);
					//if we are hitting ground1 and ground2 (tile id = 1 and 2)
					if(vHotSpots[iIndexHS].iCollidedTilesID == 1 || vHotSpots[iIndexHS].iCollidedTilesID == 2)
					{
						//set motion back to false
						bInMotion = false;
						
						//update our speed
						nXSpeed = 0;
						nYSpeed = 0;
						
						//gameobject_:GameObject, iRow_:int, iColumn_:int, iDirection_:int
						CInfo_.tilemapinfo.SnapToTile(CInfo_.gameobject, 
											   CInfo_.uiSnapToRow, 
											   CInfo_.uiSnapToColumn, 
											   this.vHotSpots[iIndexHS].iSide);
					}
				}
			}
		}
	}
}