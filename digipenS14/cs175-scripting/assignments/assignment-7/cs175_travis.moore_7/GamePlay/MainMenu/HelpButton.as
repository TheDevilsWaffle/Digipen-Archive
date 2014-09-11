/***************************************************************************************/
/*
	filename   	HelpButton.as 
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
	
	public class HelpButton extends GameObject
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
		public function HelpButton(nPosX_:Number , nPosY_:Number)
		{
			super(new helpclickie(), nPosX_, nPosY_, GamePlayGlobals.GO_HELP);
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