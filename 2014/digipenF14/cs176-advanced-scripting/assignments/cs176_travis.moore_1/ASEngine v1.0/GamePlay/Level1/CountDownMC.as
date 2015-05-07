/***************************************************************************************/
/*
	filename   	CountDownMC.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		09/09/2014
	
	brief:
	This class handles the properties and methods of the countdown movieclip.
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import Engine.InputManager;
	import Engine.ObjectManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import GamePlay.GamePlayGlobals;
	import flash.display.MovieClip;

	public class CountDownMC extends GameObject
	{
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor used to set attributes based on passed parameters.
				
			Parameters:
				- nPosX_:Number - Set the starting x for the object.
				- nPosY_:Number - Set the starting y for the object.
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function CountDownMC(nPosX_:Number, nPosY_:Number):void
		{
			super(new Countdown(), nPosX_, nPosY_, GamePlayGlobals.GO_COUNTDOWN);
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Initialize() is used to set the initial starting values for the
				countdown's attributes and later allows the game to actually start.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Initialize():void
		{
			MovieClip(displayobject).gotoAndPlay(0);
			displayobject.x = nInitialPosX;
			displayobject.y = nInitialPosY;
			displayobject.visible = true;
			Level1.bLevelStarted = false;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Update() checks for the end of the movieclip so that countdown can be
				taken away visually and the game can start.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			//when the countdown is finished, make it invisible and start the level.
			if(displayobject.visible && MovieClip(displayobject).currentFrame == MovieClip(displayobject).totalFrames)
			{
				displayobject.visible = false;
				MovieClip(displayobject).stop();
				Level1.bLevelStarted = true;
			}
		}
	}
}