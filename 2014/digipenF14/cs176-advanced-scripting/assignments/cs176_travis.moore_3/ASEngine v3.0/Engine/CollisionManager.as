/***************************************************************************************/
/*
	filename   	CollisionManager.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		09/05/2013
	
	brief:
	This class is the collision library. It contains different methods that will help the
	user check and react to collisions.
*/        	 
/***************************************************************************************/
package Engine
{
	final public class CollisionManager
	{
		static public var CO_NO_COLLISION:int = 1;
		static public var CO_DYNAMIC:int = 2;
		static public var CO_TILEMAP:int = 4;
		
		/* A vector containing all the collision information that happened at the 
		   current frame */
		static private var vCollidedObjectsInfo:Vector.<CollisionInfo>;
		
		/**************************************************************************
		/*
			Description:
				This method initializes the CollisionManager by creating or 
				initializing the needed variables.
				
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/
		static internal function Initialize():void
		{
			vCollidedObjectsInfo = new Vector.<CollisionInfo>;
		}
		
		/**************************************************************************
		/*
			Description:
				This method checks bounding rectangle collision among all the 
				objects found in the Objects array received as parameter. 
				The method will add two CollisionInfo instances everytime it 
				finds two objects colliding.
				Every CollisionInfo instance contains information about the 
				collision that happened (ex: collided game object, collided with 
				game object ...)
				
			Parameters:
				- vObjects_: Vector of objects sent to the method in order to 
							 check collision among all the objects inside
									   
			Return:
				- None
							
		*/
		/*************************************************************************/
		static internal function CheckDynamicCollision(vObjects_:Vector.<GameObject>):void
		{
			var uiObjectsLength:uint =  vObjects_.length;
			
			/* looping through all objects to check collision between them */
			for(var i:int = 0; i < uiObjectsLength; ++i)
			{
				/* Checking if the object collides with Dynamic Objects */
				if( !vObjects_[i].IsCollisionFlagOn(CollisionManager.CO_NO_COLLISION) &&
				   vObjects_[i].IsCollisionFlagOn(CollisionManager.CO_DYNAMIC) )
				{
					/* Looping through the objects in the array that has an index
					   higher than the current object pointed to by i */
					for(var j:int = i+1; j < uiObjectsLength; ++j)
					{
						/* Check bounding rectangle collision */
						if(!vObjects_[j].IsCollisionFlagOn(CollisionManager.CO_NO_COLLISION) &&
						   vObjects_[j].IsCollisionFlagOn(CollisionManager.CO_DYNAMIC) && 
						   vObjects_[i].displayobject.hitTestObject(vObjects_[j].displayobject))
						{
							/* Add a collision info instance for the first 
							collided object */
							var CInfo:CollisionInfo = new CollisionInfo();
							CInfo.iCollisionType = CollisionManager.CO_DYNAMIC;
							CInfo.gameobject = vObjects_[i];
							CInfo.gameobjectCollidedWith = vObjects_[j];
							vCollidedObjectsInfo.push(CInfo);
							
							/* Add a collision info instance for the second 
							collided object */
							var CInfo2:CollisionInfo = new CollisionInfo();
							CInfo2.iCollisionType = CollisionManager.CO_DYNAMIC;
							CInfo2.gameobject = vObjects_[j];
							CInfo2.gameobjectCollidedWith = vObjects_[i];
							vCollidedObjectsInfo.push(CInfo2);
						}
					}
				}
			}
		}
		
		/**************************************************************************
		/*
			Description:
				This method checks collision between an object (all 8 hotspots) and
				a tile map.
				It will add a CollisionInfo instances containing all the information 
				needed.
				The collision information that will be stored are the following:
				* Collision Type
				* tile map information
				* the colliding object
				* The colliding object's row and column according to it's center
				
			Parameters:
				- vObjects_: A vector of objects sent to the method in order to 
							 check collision among all the objects inside
				
				- tilemapinfo_: A TileMapInfo instance containing all information
							   about the tile map we are checking collision with
							
		*/
		/*************************************************************************/
		static public function CheckTileMapCollision(vObjects_:Vector.<GameObject>, 
													 tilemapinfo_:TileMapInfo)
		{
			//looping through all objects to check collision between them
			for(var iIndex:int = 0; iIndex < vObjects_.length; ++iIndex)
			{
				//if this object doesn't have a no collision flag and has a tile map flag
				if(!vObjects_[iIndex].IsCollisionFlagOn(CollisionManager.CO_NO_COLLISION) &&
				   vObjects_[iIndex].IsCollisionFlagOn(CollisionManager.CO_TILEMAP))
				{
					//update the hotspots
					vObjects_[iIndex].UpdateHotSpots();
					
					//loop through the hot spots to set information about where it is and also the tile that it is currently in
					for(var iIndexHS:int = 0; iIndexHS < vObjects_[iIndex].vHotSpots.length; ++iIndexHS)
					{
						//send CInfo the x column and y row of where it is located
						var iXPos:int = vObjects_[iIndex].vHotSpots[iIndexHS].pPosition.x / tilemapinfo_.uiTileWidth;
						var iYPos:int = vObjects_[iIndex].vHotSpots[iIndexHS].pPosition.y / tilemapinfo_.uiTileHeight;
						
						//determine where this hotspot is on the tilemap
						var iTileID = tilemapinfo_.aTileMap[iYPos][iXPos];
						
						//set this hotspot's iTileID
						vObjects_[iIndex].vHotSpots[iIndexHS].iCollidedTilesID = iTileID;
					}
					//add a collision info instance for the object
					var CInfo:CollisionInfo = new CollisionInfo();
					
					//send the iCollisionType
					CInfo.iCollisionType = CollisionManager.CO_TILEMAP;
					//update uiSnapToColumn based on x
					CInfo.uiSnapToColumn = vObjects_[iIndex].displayobject.x / tilemapinfo_.uiTileWidth;
					//update uiSnapToRow based on y
					CInfo.uiSnapToRow = vObjects_[iIndex].displayobject.y / tilemapinfo_.uiTileHeight;
					//send the object itself
					CInfo.gameobject = vObjects_[iIndex];
					//send the tilemapinfo
					CInfo.tilemapinfo = tilemapinfo_;
					
					//push this info to vCollidedObjectsInfo vector
					vCollidedObjectsInfo.push(CInfo);
					
				}
			}
		}
		
		/**************************************************************************
		/*
			Description:
				This method calls the CollisionReaction function on all collided 
				dynamic object. 
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/ 
		static internal function CollidedObjectsReaction():void
		{
			var uiCollidedObjectsInfoLength:uint = vCollidedObjectsInfo.length;
			for(var i:uint = 0; i < uiCollidedObjectsInfoLength; ++i)
			{
				vCollidedObjectsInfo[i].gameobject.CollisionReaction(vCollidedObjectsInfo[i]);
				delete vCollidedObjectsInfo[i];
			}
			
			vCollidedObjectsInfo.length = 0;
		}
		
		/**************************************************************************
		/*
			Description:
				This method destroys the CollisionManager when the user decides
				to quit the application/game.
				
			Parameters:
				- None
				
			Return:
				- None
							
		*/
		/*************************************************************************/
		static internal function Destroy()
		{
			var uiCollidedObjectsInfoLength:uint = vCollidedObjectsInfo.length;
			for(var i:uint = 0; i < uiCollidedObjectsInfoLength; ++i)
			{
				delete vCollidedObjectsInfo[i];
			}
			
			vCollidedObjectsInfo.length = 0;
			vCollidedObjectsInfo = null;
		}
	}
}