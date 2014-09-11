/***************************************************************************************/
/*
	filename   	Level2.as 
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
	
	public class Level2 extends State
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
		public function Level2()
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
			trace("Level 2 - Created");
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
			trace("Level 2 - Initialized");
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
			trace("Level 2 - Updated");
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
			//check if user has pressed 1
			if(InputManager.IsPressed(49))
			{
				GameStateManager.GotoState(new Level1())
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
			trace("Level 2 - Uninitialized");
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
			trace("Level 2 - Destroyed");
		}
	}
}
