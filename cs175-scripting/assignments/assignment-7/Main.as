/***************************************************************************************/
/*
	filename   	Main.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date    	08/28/2013
	
	brief:
	This is the starting point of the game engine. Its main objective is to initialize, 
	update and destroy the game engine components. Other responsibility is to provide
	the game loop.
*/        	 
/***************************************************************************************/
package
{
	import Engine.Game;
	import flash.display.MovieClip;
	import flash.events.Event;
	import GamePlay.MainMenu.MainMenu;
	
	final public class Main extends MovieClip
	{
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for creating the game engine components and 
				starting the game loop
			
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		final public function Main()
		{
			Game.Initialize(stage, new MainMenu());
			stage.addEventListener(Event.ENTER_FRAME, GameLoop);
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is called at the beginning of every frame and
				is responsible for updating the game by calling the Game's update 
				function. It is also responsible for destroying the game when the 
				user decides to quit.
			
			Parameters:
				- e_: An Event parameter that allows this function to be called on
				      ENTER_FRAME
			
			Return:
				- None
		*/
		/*******************************************************************************/
		final private function GameLoop(e_:Event):void
		{
			if(Game.bQuit)
			{
				Destroy();
				return;
			}
			
			Game.Update();
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for stopping the game loop and cleanly 
				exiting the game. Game.Destroy() will make sure that all components are
				cleanly destroyed.
				
			
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		final private function Destroy():void
		{
			Game.Destroy();
			stage.removeEventListener(Event.ENTER_FRAME , GameLoop);
		}
	}
}