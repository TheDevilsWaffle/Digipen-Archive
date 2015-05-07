/***************************************************************************************/
/*
	filename   	YouLose.as 
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
	
	public class YouLose extends GameObject
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
		public function YouLose(nPosX_:Number , nPosY_:Number)
		{
			super(new youlose(), nPosX_, nPosY_, GamePlayGlobals.GO_LOSE);
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