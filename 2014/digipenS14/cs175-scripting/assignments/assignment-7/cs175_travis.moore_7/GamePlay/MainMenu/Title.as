/***************************************************************************************/
/*
	filename   	Title.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:

*/        	 
/***************************************************************************************/
package GamePlay.MainMenu
{
	import Engine.InputManager;
	import Engine.GameStateManager;
	import Engine.ObjectManager;
	import Engine.GameObject;
	import Engine.State;
	import Engine.ObjectManager;
	import GamePlay.Level1.Level1;
	import flash.ui.Keyboard;
	import flash.display.MovieClip;
	import GamePlay.Help.HelpMenu;
	import GamePlay.GamePlayGlobals;

	
	public class Title extends GameObject
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
		override public function Title(_nPosX:Number, _nPosY:Number):void
		{			
			super(new maintitle(), _nPosX, _nPosY, GamePlayGlobals.GO_HELP);
		}
	}
}
