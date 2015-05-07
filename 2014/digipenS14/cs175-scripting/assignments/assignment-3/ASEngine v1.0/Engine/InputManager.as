/***************************************************************************************/
/*
	filename   	InputManager.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date    	9/02/2013
	
	brief:
	The input manager is reponsible for handeling all the keyboard events.
*/        	 
/***************************************************************************************/
package Engine
{
	import flash.display.Stage;
	import flash.events.KeyboardEvent;
	import flash.events.Event;
	
	final public class InputManager
	{
		/* A reference to the main stage */
		static private var sStage:Stage;
		/* A vector of booleans that represents the status of all keys in the current
		game loop (current frame) */
		static private var vKeyPressed:Vector.<Boolean>;
		/* A vector of booleans that represents the status of all keys in the previous
		game loop (current frame) */
		static private var vKeyWasPressed:Vector.<Boolean>;

		/*******************************************************************************/
		/*
			Description:
				Constructor that initializes the input manager's variables
				Each array holds 256 elements which is the size of the ascii table.
				Keyboard event listeners should only be added to the stage
			
			Parameters:
				- sStage_: A reference to the stage 
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Initialize(sStage_:Stage):void
		{
			sStage = sStage_;
			
			vKeyPressed = new Vector.<Boolean>(256);
			vKeyWasPressed = new Vector.<Boolean>(256);
			
			sStage.addEventListener(KeyboardEvent.KEY_DOWN, OnKeyDown);
			sStage.addEventListener(KeyboardEvent.KEY_UP, OnKeyUp);
			Reset();
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method resets the input manager's data. It is used when restarting
				or switching to a state.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Reset():void
		{
			for(var i:int = 0; i < 256; ++i)
			{
				vKeyPressed[i] = false;
				vKeyWasPressed[i] = false;
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				Copying the current keys pressed to another array so we can keep track 
				of the keys pressed in the previous frame.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Update():void
		{
			for(var i:int = 0; i < 256; ++i)
			{
				vKeyWasPressed[i] = vKeyPressed[i];
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method destroys the Input Manager by removing all it's 
				eventListeners and setting it's variables to null.
				
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Destroy():void
		{
			sStage.removeEventListener(KeyboardEvent.KEY_DOWN, OnKeyDown);
			sStage.removeEventListener(KeyboardEvent.KEY_UP, OnKeyUp);
			
			vKeyPressed = null;
			vKeyWasPressed = null;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method sets the pressed key's flag to 1 inside the array
				
			Parameters:
				- ke_: keyboard event containing all the keyboard information including
				       which key was pressed by the user
					  
			Return:
				- None
		*/
		/*******************************************************************************/
		static private function OnKeyDown(ke_:KeyboardEvent):void
		{
			vKeyPressed[ke_.keyCode] = true;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method sets the released key's flag to 0 inside the array
				
			Parameters:
				- ke_: keyboard event containing all the keyboard information including
				       which key was released by the user
					  
			Return:
				- None
		*/
		/*******************************************************************************/
		static private function OnKeyUp(ke_:KeyboardEvent):void
		{
			vKeyPressed[ke_.keyCode] = false;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method checks if a key is pressed
			
			Parameters:
				- keyCode_: The code of the key we are checking 
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function IsPressed(keyCode_:int):Boolean
		{
			return vKeyPressed[keyCode_];
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method checks if a key is trigerred
			
			Parameters:
				- keyCode_: The code of the key we are checking 
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function IsTriggered(keyCode_:int):Boolean
		{
			return vKeyPressed[keyCode_] && !vKeyWasPressed[keyCode_];
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method checks if a key is released
				
			Parameters:
				- keyCode_: The code of the key we are checking 
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function IsReleased(keyCode_:int):Boolean
		{
			return !vKeyPressed[keyCode_] && vKeyWasPressed[keyCode_];
		}
	}
}
