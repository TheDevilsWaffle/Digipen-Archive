/***************************************************************************************/
/*
	filename   	StartGameMC.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		09/09/2014 
	
	brief:
	This class handles the properties and methods of the StartGameMC.
*/        	 
/***************************************************************************************/
package GamePlay.MainMenu
{
	import Engine.GameObject;
	import flash.display.MovieClip;
	import GamePlay.GamePlayGlobals
	
	public class StartGameMC extends GameObject
	{
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
		public function StartGameMC(nPosX_:Number, nPosY_:Number)
		{
			super(new StartGame(), nPosX_, nPosY_, GamePlayGlobals.GO_GAMESTART);
		}
	}
}