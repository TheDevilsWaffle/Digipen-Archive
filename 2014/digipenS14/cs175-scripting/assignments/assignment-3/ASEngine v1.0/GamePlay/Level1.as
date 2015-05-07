/***************************************************************************************/
/*
	filename   	Level1.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu, eabichahine@digipen.edu
	date		02/18/2014
	
	brief:
	This is a test level to see if we can create(), initialize(), update(),
	uninitialize(), and destroy()

*/        	 
/***************************************************************************************/
package GamePlay
{
	import flash.ui.Keyboard;
	import Engine.InputManager;
	import Engine.GameStateManager;
	import Engine.State;
	
	public class Level1 extends State
	{		
		/**************************************************************************
		/*
			Description:
				Empty constructor
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/
		public function Level1()
		{
		}
		
		/**************************************************************************
		/*
			Description:
				Used to trace out a create message
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/		
		override public function Create():void
		{
			trace("Level 1 - Created");
		}
		
		/**************************************************************************
		/*
			Description:
				Used to trace out an initialize message
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/	
		override public function Initialize():void
		{
			trace("Level 1 - Initialized");
		}
		
		/**************************************************************************
		/*
			Description:
				Used to keep track of key presses
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/	
		override public function Update():void
		{
			trace("Level 1 - Updated");
			//check if user pressed keyCode "r"
			if(InputManager.IsPressed(82))
			{
				GameStateManager.RestartState();
			}
			//check if user pressed keyCode "space"
			if(InputManager.IsPressed(32))
			{
				GameStateManager.Quit();
			}
			//check if user has pressed 2
			if(InputManager.IsPressed(50))
			{
				GameStateManager.GotoState(new Level2())
			}
		}
		
		/**************************************************************************
		/*
			Description:
				Used to trace out an uninitialize message
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/	
		override public function Uninitialize():void
		{
			trace("Level 1 - Uninitialized");
		}
		
		/**************************************************************************
		/*
			Description:
				Used to trace out a destroyed message
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/	
		override public function Destroy():void
		{
			trace("Level 1 - Destroyed");
		}
	}
}
