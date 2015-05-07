/***************************************************************************************/
/*
	filename   	TileMapInfo.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		10/02/2013
	
	brief:
	The TileMapInfo class contains all the necessary information on a tile map. All the
	information were loaded from a tilemap xml.

*/        	 
/***************************************************************************************/
package Engine
{
	final public class TileMapInfo
	{
		/* An array representing the tile map (filled with tile IDs) */
		public var aTileMap:Array;
		/* An unsigned int reperesenting the number of columns in the tile map */
		public var uiColumnsCount:uint;
		/* An unsigned int reperesenting the number of rows in the tile map */
		public var uiRowsCount:uint;
		/* An unsigned int reperesenting the width of a tile */
		public var uiTileWidth:uint;
		/* An unsigned int reperesenting the height of a tile */
		public var uiTileHeight:uint;
		/* A number reperesenting the half tile width */
		public var nTileHalfWidth:Number;
		/* A number reperesenting the half tile height */
		public var nTileHalfHeight:Number;
		/* A vector of TileInfo that will be used to find the correct tile to load */
		internal var vTileInfo:Vector.<TileInfo>;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor that initializes the TileMapInfo's class variables
			
			Parameters:
				- aTileMap: An 2Dimensional array representing the tile map and containing 
							the tile IDs
							
				- uiColumnsCount: An unsigned integer representing the number of columns 
								  that the tile map has
								  
				- uiRowsCount: An unsigned integer representing the number of rows 
							   that the tile map has
							   
				- uiTileWidth: An unsigned integer representing the tile width
				
				- uiTileHeight: An unsigned integer representing the tile height
				
			Return:
				- None
		*/
		/*******************************************************************************/
		final public function TileMapInfo(aTileMap_:Array, uiColumnsCount_:uint, uiRowsCount_:uint,
									uiTileWidth_:uint, uiTileHeight_:uint)
		{
			aTileMap = aTileMap_;
			uiColumnsCount = uiColumnsCount_;
			uiRowsCount = uiRowsCount_;
			uiTileWidth = uiTileWidth_;
			uiTileHeight = uiTileHeight_;
			nTileHalfWidth = uiTileWidth / 2;
			nTileHalfHeight = uiTileHeight / 2;
			vTileInfo = new Vector.<TileInfo>();
		}
		
		/*******************************************************************************/
		/*
			Description:
				This function allows us to add a new tile info instance to our tilemap
				information instance. We will end up with one TileInfo instance for every 
				new kind of tile in the TileMap.
			
			Parameters:
				- iID_: The tile's id in the xml's TileMap node
				
				- sName_: The name that will be given to the tile's object
				
				- sClassName_: The class name that allows us to locate the actual class 
							   that will instantiate tiles of this kind.
				
				- sClassType_: The type of the class that we will be instatiating.
							   "displayobject" means that the tile we are instantiating 
							   is found in the fla's library as a MovieClip, Sprite, etc..
							   "gameobject" means that the tile we are instantiating 
							   already has a user defined class that extends from 
							   GameObject
				
			Return:
				- None
		*/
		/*******************************************************************************/
		final public function AddTileInfo(iID_:int, sName_:String, 
									sClassName_:String, sClassType_:String):void
		{
			vTileInfo.push(new TileInfo(iID_, sName_, sClassName_, sClassType_));
		}
		
		/*******************************************************************************/
		/*
			Description:
				This methods repositions the center of a game object to the center of a 
				specific tile in the tile map. We can snap in one direction at a time
				(HS_LEFT and HS_RIGHT represent snapping on the X axis while 
				HS_UP and HS_DOWN on the Y axis).
			
			Parameters:
				- gameobject_: The game object that we want to snap
				- iRow_: The Row of the tile we want to snap to
				- iColumn_: The Column of the tile we want to snap to
				- iDirection_: The direction we want to snap to.
				
			Returns:
				- None
		*/
		/*******************************************************************************/
		public function SnapToTile(gameobject_:GameObject, iRow_:int, iColumn_:int, iDirection_:int)
		{
			if(iDirection_ == HotSpot.HS_LEFT || iDirection_ == HotSpot.HS_RIGHT)
			{
				gameobject_.displayobject.x = iColumn_ * uiTileWidth + nTileHalfWidth;
			}
			
			if(iDirection_ == HotSpot.HS_UP || iDirection_ == HotSpot.HS_DOWN)
			{
				gameobject_.displayobject.y = iRow_ * uiTileHeight + nTileHalfHeight;
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				The Destroy function makes sure we are freeing all the references to 
				allocated memory.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		final public function Destroy():void
		{
			aTileMap = null;
			for(var i:int = 0; i < vTileInfo.length; ++i)
			{
				vTileInfo[i].Destroy();
			}
			vTileInfo.length = 0;
			vTileInfo = null;
		}
	}
}