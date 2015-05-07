/***************************************************************************************/
/*
	filename   	HeroSP.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		10/16/2014
	
	brief:
	The HeroSP class creates a hero and deals with its gameplay behavior.
	
*/        	 
/***************************************************************************************/
package GamePlay.Level2
{
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	import flash.ui.Keyboard;
	import flash.display.MovieClip;
	import GamePlay.Level3.Level3;

	final public class HeroSP extends GameObject
	{
		/* Controls the hero's speed. Used for all four directions */
		public var nSpeed:Number;
		
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
		final public function HeroSP()
		{
			super(new Hero(), 0, 0, GamePlayGlobals.GO_HERO, CollisionManager.CO_DYNAMIC | CollisionManager.CO_TILEMAP);
			nSpeed = 5;
			UpdateHotSpots();
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
			if(InputManager.IsPressed(Keyboard.LEFT))
			{
				displayobject.x -= nSpeed;
			}
			else if(InputManager.IsPressed(Keyboard.RIGHT))
			{
				displayobject.x += nSpeed;
			}
			else if(InputManager.IsPressed(Keyboard.DOWN))
			{
				displayobject.y += nSpeed;
			}
			else if(InputManager.IsPressed(Keyboard.UP))
			{
				displayobject.y -= nSpeed;
			}
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
			if(CInfo_.iCollisionType == CollisionManager.CO_DYNAMIC)
			{
				if(CInfo_.gameobjectCollidedWith.iType == GamePlayGlobals.GO_DOOR)
				{
					var door:GameObject = ObjectManager.GetObjectByName("Door", ObjectManager.OM_DYNAMICOBJECT);
					if((MovieClip(door.displayobject)).currentFrame == 2)
					{
						GameStateManager.GotoState(new Level3());
					}
				}
			}
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