/***************************************************************************************/
/*
	filename   	TileInfo.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		10/02/2013 
	
	brief:
	The TileInfo class contains all the necessary information on an individual tile used
	in a tile map.

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.utils.getDefinitionByName;
	
	final internal class TileInfo
	{
		/* An integer that represents the tile's id in the xml's TileMap node */
		public var iID:int;
		/* A string that represents the name that will be given to the tile's object */
		public var sName:String;
		/* A class from which we will instantiate many tiles */
		public var classTileObject:Class;
		/* A string that represents what type the classTileObject class is.
		   It can be one of two options: "displayobject" or "gameobject"*/
		public var sClassType:String;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor that initializes the TileMapInfo's class variables
			
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
		final public function TileInfo(iID_:int, sName_:String, 
								 sClassName_:String, sClassType_:String)
		{
			iID = iID_;
			sName = sName_;
			classTileObject = getDefinitionByName(sClassName_) as Class;
			sClassType = sClassType_;
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
			sName = null;
			classTileObject = null;
			sClassType = null;
		}
	}
}