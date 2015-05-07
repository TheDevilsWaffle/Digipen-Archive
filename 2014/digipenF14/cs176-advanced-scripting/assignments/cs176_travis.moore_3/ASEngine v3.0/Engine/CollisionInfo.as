/***************************************************************************************/
/*
	filename   	CollisionInfo.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		09/05/2013 
	
	brief:
	This class contains all the collision information that should be stored when two
	dynamic objects collide.
*/        	 
/***************************************************************************************/
package Engine
{
	final public class CollisionInfo
	{
		/* An integer that represents what type of collision happened 
		  (CO_DYNAMIC or CO_TILEMAP) */
		public var iCollisionType:int;
		/* A reference to the object colliding */
		public var gameobject:GameObject;
		/* A reference to the object collided with */
		public var gameobjectCollidedWith:GameObject;
		
		/*Row and Column of the tile we want to snap to */
		public var uiSnapToRow:uint;
		public var uiSnapToColumn:uint;
		/* A reference to the tilemapinfo containing all the tile map information */
		public var tilemapinfo:TileMapInfo;
		
		/**************************************************************************
		/*
			Description:
				Constructor that initializes the collision info's variables
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/
		final public function CollisionInfo()
		{
			iCollisionType = CollisionManager.CO_NO_COLLISION;
			gameobject = null;
			gameobjectCollidedWith = null;
			tilemapinfo = null;
		}

	}

}