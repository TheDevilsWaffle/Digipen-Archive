/***************************************************************************************/
/*
	filename   	CountdownMC.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:

*/        	 
/***************************************************************************************/
package GamePlay
{
	import Engine.GameObject;
	import flash.display.MovieClip;
	import flash.events.Event;
	
	public class CountdownMC extends GameObject
	{
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function CountdownMC(nPosX_:Number , nPosY_:Number)
		{
			super (new countdown() , nPosX_ , nPosY_, GamePlayGlobals.GO_COUNTDOWN);
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
		}

		/*******************************************************************************/
		/*
			Description:
				
				
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
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			if(displayobject.visible && MovieClip(displayobject).currentFrame == MovieClip(displayobject).totalFrames)
			{
				displayobject.visible = false;
				MovieClip(displayobject).stop();
				Level1.bLevelStarted = true;
			}
		}
	}

}