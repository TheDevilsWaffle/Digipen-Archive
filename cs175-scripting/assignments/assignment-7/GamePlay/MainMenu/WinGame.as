/***************************************************************************************/
/*
	filename   	WinGame.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu eabichahine@digipen.edu
	date		04/18/2014
	
	brief:

*/        	 
/***************************************************************************************/
package GamePlay.MainMenu
{
	import Engine.GameObject;
	import GamePlay.GamePlayGlobals;
	import flash.display.MovieClip;
	
	public class WinGame extends GameObject
	{
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function WinGame(nPosX_:Number , nPosY_:Number)
		{
			super(new exitGame(), nPosX_, nPosY_, GamePlayGlobals.GO_EXIT_BUTTON);
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
			MovieClip(displayobject).gotoAndStop(1);
		}
	}

}