/****************************************************************************************
FILENAME   Level.as 
AUTHOR     Travis Moore
EMAIL      travis.moore@digipen.edu
DATE       12/09/2014 
/***************************************************************************************/
package GamePlay.PlatformLevel
{
	import Engine.*;
	import flash.ui.Keyboard;
	import GamePlay.MainMenu.MainMenu;
	import GamePlay.GamePlayGlobals;
	import flash.display.MovieClip;
	import flash.utils.getDefinitionByName;
	import flash.media.Sound;
	import flash.net.URLRequest;
	import Engine.SoundManager;
	import flash.display.DisplayObject;
	import flash.geom.Rectangle;
	import flash.display.Stage;
	import flash.geom.Point;
	
	public class Level extends State
	{
		/* Dummy CoinSP variable in order for xml loading to be possible. */
		private var coin:CoinSP;
		/* Dummy HeroSP variable in order for xml loading to be possible. */
		private var hero:HeroSP;
		/* static integer that will keep track of how many coins we still have */
		static public var iNumberOfCoins:int = 0;
		private var backgroundPause:Boolean;
		static public var bIsPaused:Boolean;
		private var gCam:DisplayObject;
		private var dCam:DisplayObject;
		private var sStage:Stage;
		public var heroXPos:Number;
		public var heroYPos:Number;
		public var ActualHero:GameObject;
		
		/********************************************************************************
		FUNCTION- Load()
		/*******************************************************************************/
		override public function Load():void
		{
			//load the xmls
			XMLManager.LoadXML("GamePlay/PlatformLevel/BackgroundData.xml", "backgroundData");
			XMLManager.LoadXML("GamePlay/PlatformLevel/SpriteData.xml", "spriteData");
		}

		/********************************************************************************
		FUNCTION- Create()
		/*******************************************************************************/
		override public function Create():void
		{	
			//varaible for background
			var xml:XML = XMLManager.GetXML("backgroundData");
			//load the background, add the objects
			ObjectManager.LoadTileMap(xml, ObjectManager.OM_BACKGROUND_TILEMAP);
			//add the background
			ObjectManager.AddTileMapObjects(ObjectManager.OM_BACKGROUND_TILEMAP);
			
			//variable for sprites
			var xmlSprite:XML = XMLManager.GetXML("spriteData");
			//load the sprites
			ObjectManager.LoadTileMap(xmlSprite, ObjectManager.OM_DYNAMIC_TILEMAP);
		}
		
		/********************************************************************************
		FUNCTION- Initialize()
		/*******************************************************************************/
		override public function Initialize():void
		{
			//variables to hook to stage
			sStage = ObjectManager.sStage;
			//variable to hook to the camera
			dCam = ObjectManager.Cam;
			//add the dynamic sprites (so we can reload them on restart)
			ObjectManager.AddTileMapObjects(ObjectManager.OM_DYNAMIC_TILEMAP);
			
			//add the door
			var door:GameObject = ObjectManager.GetObjectByName("Door", 
																ObjectManager.OM_DYNAMICOBJECT);
			//set it's iType
			door.iType = GamePlayGlobals.GO_DOOR;
			(MovieClip(door.displayobject)).stop();
			
			//create the LevelBGMusic
			var soLevelBGMusic:Sound = new Sound(new URLRequest("Sounds/GameMusic.mp3"));
			
			//give it to the sound manager, set name, make sure it loops
			SoundManager.AddSoundObject(soLevelBGMusic, 
										"soLevelBGMusic", 
										true);
			//hook to the hero
			gCam = ObjectManager.GetObjectByName("Hero", ObjectManager.OM_DYNAMICOBJECT).displayobject;
		}
		
		/********************************************************************************
		FUNCTION- Update()
		/*******************************************************************************/
		override public function Update():void
		{
			
			this.heroXPos = ObjectManager.GetObjectByName("Hero", ObjectManager.OM_DYNAMICOBJECT).displayobject.x;
			this.heroYPos = ObjectManager.GetObjectByName("Hero", ObjectManager.OM_DYNAMICOBJECT).displayobject.y;
			this.ActualHero = ObjectManager.GetObjectByName("Hero", ObjectManager.OM_DYNAMICOBJECT);
			
			if(InputManager.IsTriggered(Keyboard.R))
			{
				GameStateManager.RestartState();
				SoundManager.RemoveAllSoundObjects();
			}
			
			if(InputManager.IsTriggered(Keyboard.M))
			{
				GameStateManager.GotoState(new MainMenu());
				SoundManager.RemoveAllSoundObjects();
			}
			
			//PAUSE MENU
			if(InputManager.IsTriggered(Keyboard.P))
			{
				//if not already paused
				if(!bIsPaused)
				{
					//set paused to true
					bIsPaused = true;
					
					//call the soundmanager, call it's paused, tell it we're paused.
					SoundManager.SoundObjectPause(bIsPaused);
					
					//get a new sound
					var soPauseMenuBG:Sound = new Sound(new URLRequest("Sounds/PauseMusic.mp3"));
					
					//give the new sound to the sound manager to play, give it a name, tell it to loop
					SoundManager.AddSoundObject(soPauseMenuBG, 
												"soPauseMenuBG", 
												true);
					
					//give it all the pause menu things.
					ObjectManager.AddObject(new GameObject(new ExitToMainMenu(),heroXPos,heroYPos + 90,0), 
											"ExitToMainMenu", 
											ObjectManager.OM_STATICOBJECT);
					ObjectManager.AddObject(new GameObject(new ResumeGame(),heroXPos,heroYPos - 90,0), 
											"ResumeGame", 
											ObjectManager.OM_STATICOBJECT);
					ObjectManager.AddObject(new GameObject(new RestartGame(),heroXPos,heroYPos,0), 
											"RestartGame", 
											ObjectManager.OM_STATICOBJECT);
					ObjectManager.AddObject(new PauseButtonSP(heroXPos - 90,heroYPos - 90), 
											"PauseButton", 
											ObjectManager.OM_STATICOBJECT);
				}
				//if we are already paused
				else if(bIsPaused)
				{
					//set paused to false
					bIsPaused = false;
					
					//remove the pause menu sound
					SoundManager.RemoveSoundObjectByName("BGMPause");
					
					//set resumesoundobject by giving it the state of paused
					SoundManager.ResumeSoundObject(bIsPaused);
					
					//remove the menu things
					ObjectManager.RemoveObjectByName("ExitToMainMenu", 
													 ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveObjectByName("RestartGame", 
													 ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveObjectByName("ResumeGame", 
													 ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveObjectByName("PauseButton", 
													 ObjectManager.OM_STATICOBJECT);
				}
			}
			
			if(InputManager.IsTriggered(Keyboard.Z))
			{
				gCam = ObjectManager.GetObjectByName("Door", ObjectManager.OM_DYNAMICOBJECT).displayobject;
				
			} 
			
			if(iNumberOfCoins == 0)
			{
				var door:GameObject = ObjectManager.GetObjectByName("Door", ObjectManager.OM_DYNAMICOBJECT);
				(MovieClip(door.displayobject)).gotoAndStop(2);
			}
			dCam.scrollRect = new Rectangle(gCam.x - sStage.stageWidth/2, gCam.y - sStage.stageHeight/2, sStage.stageWidth, sStage.stageHeight);
		}

		/********************************************************************************
		FUNCTION- Uninitialize()
		/*******************************************************************************/
		override public function Uninitialize():void
		{
			//get rid of dem things
			ObjectManager.RemoveAllObjectsByName("Coin", ObjectManager.OM_DYNAMICOBJECT);
			ObjectManager.RemoveAllObjectsByName("Hero", ObjectManager.OM_DYNAMICOBJECT);
			ObjectManager.RemoveAllObjectsByName("Door", ObjectManager.OM_DYNAMICOBJECT);
			SoundManager.RemoveAllSoundObjects();
			dCam.scrollRect = new Rectangle(0, 0, sStage.stageWidth, sStage.stageHeight);			
		}
	}
}