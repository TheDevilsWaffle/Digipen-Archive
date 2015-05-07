/***************************************************************************************/
/*
	filename   	WinSprite.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:

*/        	 
/***************************************************************************************/
package GamePlay.Win
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
	import GamePlay.GamePlayGlobals;

	
	public class WinSprite extends GameObject
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
		override public function WinSprite(_nPosX:Number, _nPosY:Number):void
		{			
			super(new youwin(), _nPosX, _nPosY, GamePlayGlobals.GO_WIN);
		}
	}
}
