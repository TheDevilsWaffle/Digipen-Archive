/***************************************************************************************/
/*
	filename   	HeroSP.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		10/06/2014
	
	brief:
	The HeroSP class creates a hero and deals with its gameplay behavior.
	
*/        	 
/***************************************************************************************/
package GamePlay.Level2
{
	//import
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	import GamePlay.MainMenu.MainMenu;
	import flash.ui.Keyboard;
	import flash.events.KeyboardEvent;
	import flash.display.MovieClip;

	final public class HeroSP extends GameObject
	{
		//variables to keep track of coins (9 total)
		static public var iCoinCount:int;
		//variable to keep track of door being open
		static public var bDoorOpen:Boolean;
		//variable to hook to door as a GameObject
		static public var bIsDead:Boolean;
		
		/*******************************************************************************/
		/*
			Description:
				The constructor function sets the location and GamePlayGlobals type
			
			Parameters:
				- nXPos_:Number - x position
				- nYPos_:Number - y position
				
			Return:
				- None
		*/
		/*******************************************************************************/
		final public function HeroSP(nXPos_:Number, nYPos_:Number):void
		{
			super(new Hero(), 0, 0, GamePlayGlobals.GO_HERO);
			
			//set location of object
			displayobject.x = nXPos_;
			displayobject.y = nYPos_;
			
			//initialize variables
			iCoinCount = 0;
			bDoorOpen = false;
			bIsDead = false;
		}
		
		/*******************************************************************************/
		/*
			Description:
				The Update() moves the object upon keyboard presses
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		final override public function Update():void
		{
			if(InputManager.IsPressed(Keyboard.UP))
			{
				displayobject.y -= 10;
			}
			if(InputManager.IsPressed(Keyboard.DOWN))
			{
				displayobject.y += 10;
			}
			if(InputManager.IsPressed(Keyboard.LEFT))
			{
				displayobject.x -= 10;
			}
			if(InputManager.IsPressed(Keyboard.RIGHT))
			{
				displayobject.x += 10;
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				The CollisionReaction() function takes the CInfo type and performs actions
				based on which object this object hits.
			
			Parameters:
				- CInfo_:CollisionInfo - iType of an object
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				case GamePlayGlobals.GO_COIN:
				{
					//add dem coins
					++iCoinCount;
					break;
				}

				case GamePlayGlobals.GO_DOOR:
				{
					//if door is open
					if(bDoorOpen)
					{
						//go to main menu
						GameStateManager.GotoState(new MainMenu());
					}
					break;
				}
			}
		}
		/*******************************************************************************/
		/*
			Description:
				The Destroy() function calls the GameObject's Destroy() to remove the
				object
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Destroy():void
		{
			super.Destroy();
		}
	}
}