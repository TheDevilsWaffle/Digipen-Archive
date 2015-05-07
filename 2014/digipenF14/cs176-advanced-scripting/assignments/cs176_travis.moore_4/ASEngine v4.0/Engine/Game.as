/***************************************************************************************/
/*
	filename   	Game.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date    	08/28/2013 
	
	brief:
	The game class is reponsible for providing functions that initializes, updates and 
	destroys the game engine compoenents.
*/        	 
/***************************************************************************************/
package Engine
{	
	import flash.display.Stage;

	final public class Game
	{
		/* 
		Static Public Boolean accessible anywhere in the engine.  when set to true, it 
		triggers the engine's destruction.
		*/
		static public var bQuit:Boolean = false; 
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for initializing the game engine components.
			
			Parameters:
				- sStage_: A refernce of the stage to be sent to all components that 
				           need an instance of the stage(example: InputManager, 
						   ObjectManager, etc...)
						   
				- startingState_: This is a reference of the state that the user wants 
				                  to initially run( example: MainMenu, Level1, etc...)
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function Initialize(sStage_:Stage, startingState_:State):void
		{
			InputManager.Initialize(sStage_);
			PhysicsManager.Initialize();
			ObjectManager.Initialize(sStage_);
			CollisionManager.Initialize();
			GameStateManager.Initialize(startingState_);
			XMLManager.Initialize();
		}
		
		/**************************************************************************
		/*
			Description:
				This method is responsible for updating all engine components. 
				It is called every frame by the game loop.
			
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*************************************************************************/
		static public function Update():void
		{
			GameStateManager.Update();
			
			/* Code below has to stay at the end */
			InputManager.Update();
			ObjectManager.RemoveAllDeadObjects();
		}

		
		/*******************************************************************************/
		/*
			Description:
				This method is destroys all the game engine components when called.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function Destroy():void
		{
			InputManager.Destroy();
			GameStateManager.Destroy();
			ObjectManager.Destroy();
			CollisionManager.Destroy();
			XMLManager.Destroy();
			PhysicsManager.Destroy();
		}
	}
}