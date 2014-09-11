/***************************************************************************************/
/*
	filename   	MainBg.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:

*/        	 
/***************************************************************************************/
package GamePlay.MainMenu
{
	import Engine.GameObject;
	import GamePlay.GamePlayGlobals;
	import flash.display.MovieClip;
	
	public class MainBg extends GameObject
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
		public function MainBg(nPosX_:Number , nPosY_:Number)
		{
			super(new bgmain(), nPosX_, nPosY_, GamePlayGlobals.GO_HELP);
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