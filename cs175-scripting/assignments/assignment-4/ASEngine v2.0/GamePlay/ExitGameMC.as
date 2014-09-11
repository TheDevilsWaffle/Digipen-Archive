/***************************************************************************************/
/*
	filename   	ExitGameMC.as 
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
	
	public class ExitGameMC extends GameObject
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
		public function ExitGameMC(nPosX_:Number , nPosY_:Number)
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