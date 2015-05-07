/***************************************************************************************/
/*
	filename   	Level2.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		10/02/2013 
	
	brief:
	This class contains level 3's gameplay code. 
	
*/        	 
/***************************************************************************************/
package GamePlay.Level3
{
	import Engine.*;
	import flash.ui.Keyboard;
	import GamePlay.MainMenu.MainMenu;
	import GamePlay.Level3.Level3;
	import GamePlay.GamePlayGlobals;
	import flash.display.MovieClip;
	import flash.utils.getDefinitionByName;
	import flash.ui.Keyboard;
	import flash.text.TextField;
	import GamePlay.Level3.PlayerSP;

	public class Level3 extends State
	{
		/* Dummy CoinSP variable in order for xml loading to be possible. */
		private var pickup:PickupSP;
		/* Dummy HeroSP variable in order for xml loading to be possible. */
		private var player:PlayerSP;
		/* static integer that will keep track of how many coins we still have */
		static public var iNumberOfPickups:int = 0;
		//variables to keep track of moves
		static public var uiMove:uint;
		private var textfieldMove:TextField;
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
			XMLManager.LoadXML("GamePlay/Level3/BackgroundData.xml", "backgroundData");
			XMLManager.LoadXML("GamePlay/Level3/SpriteData.xml", "spriteData");
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
			var xml:XML = XMLManager.GetXML("backgroundData");
			var xmlSprite:XML = XMLManager.GetXML("spriteData");

			ObjectManager.LoadTileMap(xml, ObjectManager.OM_BACKGROUND_TILEMAP);
			ObjectManager.AddTileMapObjects(ObjectManager.OM_BACKGROUND_TILEMAP);
			
			ObjectManager.LoadTileMap(xmlSprite, ObjectManager.OM_DYNAMIC_TILEMAP);
			
			//add textfield and properties
			textfieldMove = new TextField();
			textfieldMove.text = "Moves = 0";
			textfieldMove.scaleX = 2.5;
			textfieldMove.scaleY = 2.5;
			textfieldMove.mouseEnabled = false;
			textfieldMove.textColor = 0XFFFFFF;
			
			//give it to objectmanager
			ObjectManager.AddObject(new GameObject(textfieldMove,40,-2), "MoveText", ObjectManager.OM_STATICOBJECT);
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
			ObjectManager.AddTileMapObjects(ObjectManager.OM_DYNAMIC_TILEMAP);
			//set initial value of uiMove
			uiMove = 0;
		}
		
		/*******************************************************************************/
		/*
			Description:
				The update function handles the user's choice. The user can restart the 
				level by pressing "R" or go back to the main menu by pressing "M". If
				all coins are picked up, the level is over and the game goes back to the
				main menu.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			if(InputManager.IsTriggered(Keyboard.R))
			{
				GameStateManager.RestartState();
			}
			
			if(InputManager.IsTriggered(Keyboard.M))
			{
				GameStateManager.GotoState(new MainMenu());
			}
			
			textfieldMove.text = "Moves = " + uiMove;
			
			//game over - win
			if(iNumberOfPickups == 0)
			{
				PlayerSP.bInMotion = true;
				textfieldMove.text = "Moves = " + uiMove + " Win!";
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
			ObjectManager.RemoveAllObjectsByName("MoveText", ObjectManager.OM_DYNAMICOBJECT);
			ObjectManager.RemoveAllObjectsByName("Player", ObjectManager.OM_DYNAMICOBJECT);
			ObjectManager.RemoveAllObjectsByName("Pickup", ObjectManager.OM_DYNAMICOBJECT);
		}
	}
}