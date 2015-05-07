/***************************************************************************************/
/*
	filename   	Level2.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		10/06/2014
	
	brief:
	This class contains level 2's gameplay code. Level 2 is a tile based level. First 
	we will load both background and dynamic object tilemaps then we add all the tiles to
	the object manager. Finally, pressing "R" restarts the level, and "M" takes us to the
	main menu.
	
*/        	 
/***************************************************************************************/
package GamePlay.Level2
{
	import Engine.*;
	import GamePlay.Level2.*;
	import GamePlay.Level1.*;
	import flash.display.MovieClip;
	import flash.ui.Keyboard;
	import GamePlay.MainMenu.MainMenu;
	import GamePlay.GamePlayGlobals;
	import flash.display.MovieClip;
	import flash.utils.getDefinitionByName;
	
	public class Level2 extends State
	{
		/*******************************************************************************/
		/*
			Description:
				The load function will load all needed tile map xmls. 
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Load():void
		{
			XMLManager.LoadXML("Gameplay/Level2/BackgroundData.xml", "xmlBackgroundData");
			XMLManager.LoadXML("Gameplay/Level2/SpriteData.xml", "xmlSpriteData");
		}

		/*******************************************************************************/
		/*
			Description:
				The create function is called after all the xmls are loaded. Its job is
				to load the tile map information from each xml. Only the background xml
				is added at this point since it will not be added again when we restart
				the level.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Create():void
		{
			//create variables to hold xml data
			var xmlBackgroundTileData = XMLManager.GetXML("xmlBackgroundData");
			var xmlDyanmicTileData = XMLManager.GetXML("xmlSpriteData");
			
			//load the xmls
			ObjectManager.LoadTileMap(xmlBackgroundTileData, ObjectManager.OM_BACKGROUND_TILEMAP);
			ObjectManager.LoadTileMap(xmlDyanmicTileData, ObjectManager.OM_DYNAMIC_TILEMAP);
			
			//add the background tilemap
			ObjectManager.AddTileMapObjects(ObjectManager.OM_BACKGROUND_TILEMAP);
			
		}
		
		/*******************************************************************************/
		/*
			Description:
				The Initialize function will add the dynamic tile map objects.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Initialize():void
		{
			//dummy hack fix
			var dummy:CoinSP = new CoinSP(0,0);
			var dummy1:HeroSP = new HeroSP(0,0);
			
			//add the dynamic tilemap
			ObjectManager.AddTileMapObjects(ObjectManager.OM_DYNAMIC_TILEMAP);
			
			//connect to doorset door's iType
			var door = ObjectManager.GetObjectByName("Door", ObjectManager.OM_DYNAMICOBJECT);
			//set door's iType
			door.iType = GamePlayGlobals.GO_DOOR;
		}
		
		/*******************************************************************************/
		/*
			Description:
				The update function handles the user's choice. The user can restart the 
				level by pressing "R", go to level 1 by pressing "N", or go back to the 
				main menu by pressing "M". If all coins are picked up, door is opened.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			//restart
			if(InputManager.IsTriggered(Keyboard.R))
			{
				//call ObjectManager.RemoveAllObjectsByName on hero, door, and coin
				ObjectManager.RemoveAllObjectsByName("Hero", ObjectManager.OM_DYNAMICOBJECT);
				ObjectManager.RemoveAllObjectsByName("Door", ObjectManager.OM_DYNAMICOBJECT);
				ObjectManager.RemoveAllObjectsByName("Coin", ObjectManager.OM_DYNAMICOBJECT);
				GameStateManager.RestartState();
			}
			
			//go go level 1
			if(InputManager.IsTriggered(Keyboard.N))
			{
				GameStateManager.GotoState(new Level1());
			}
			
			//go to main menu
			if(InputManager.IsTriggered(Keyboard.M))
			{
				GameStateManager.GotoState(new MainMenu());
			}
			
			//check to see if coins are all gone
			if(HeroSP.iCoinCount == 9)
			{
				//set bDoorOpen true;
				HeroSP.bDoorOpen = true;
				//connect to door object's displayobject
				var doDoor = ObjectManager.GetObjectByName("Door", ObjectManager.OM_DYNAMICOBJECT).displayobject;
				//set mcDoor as a MovieClip of goDoor
				var mcDoor:MovieClip = doDoor as MovieClip;
				//animate door opening
				mcDoor.gotoAndStop(2);
			}
		}

		/*******************************************************************************/
		/*
			Description:
				The uninitialize function is responsible to clean up objects that were 
				created in the initialize function. 
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Uninitialize():void
		{
		}
	}
}