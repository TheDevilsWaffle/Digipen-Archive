/***************************************************************************************/
/*
	filename   	SelectionButtonSP.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		09/10/2013
	
	brief:
	This class handles the selection button's position and keeps track of the user's 
	menu choice.
*/        	 
/***************************************************************************************/
package GamePlay.MainMenu
{
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	import GamePlay.Level1.Level1;
	import flash.ui.Keyboard;

	public class SelectionButtonSP extends GameObject
	{
		/* Integer used to keep track of the user's menu choice */
		private var iSelection:int;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for creating/initializing the button's variables.
			
			Parameters:
				- nPosX_: the selection button's x position
				
				- nPosY_: the selection button's y position
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function SelectionButtonSP(nPosX_:Number, nPosY_:Number)
		{
			super(new SelectionButton(), nPosX_, nPosY_);
			iSelection = SELECTED.STARTGAME;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for the object's behavior. Pressing up or 
				down updates the button's position and selection state. Pressing 
				space will select the user's choice and will of course take us to the 
				right state.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			if(InputManager.IsPressed(Keyboard.UP))
			{
				displayobject.y = 200;
				iSelection = SELECTED.STARTGAME;
			}
			else if(InputManager.IsPressed(Keyboard.DOWN))
			{
				displayobject.y = 350;
				iSelection = SELECTED.EXITGAME;
			}
			
			if(InputManager.IsPressed(Keyboard.SPACE))
			{
				if(iSelection == SELECTED.STARTGAME)
				{
					GameStateManager.GotoState(new Level1());
				}
				else
				{
					GameStateManager.Quit();
				}
			}
		}
	}
}

internal class SELECTED
{
	public static const STARTGAME:Number = 0;
	public static const EXITGAME:Number = 1;
}