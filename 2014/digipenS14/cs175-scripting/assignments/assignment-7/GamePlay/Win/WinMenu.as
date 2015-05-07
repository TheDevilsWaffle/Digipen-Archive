/***************************************************************************************/
/*
	filename   	WinMenu.as 
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
	import GamePlay.MainMenu.MainMenu;
	
	public class WinMenu extends State
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
		override public function Initialize():void
		{
			//add in the help background
			ObjectManager.AddObject(new WinSprite(320, 240), "you win background", ObjectManager.OM_STATICOBJECT);
			
			//play yay sound!
			var yaySound = new yay;
			yaySound.play();
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
			if(InputManager.IsTriggered(Keyboard.M))
			{
				//play menu select sound
				var menuSelect = new select;
				menuSelect.play();
				GameStateManager.GotoState(new MainMenu());
			}
		}
	}
}
