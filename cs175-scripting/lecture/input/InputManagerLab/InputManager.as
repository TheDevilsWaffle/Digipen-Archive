package 
{
	import flash.display.Stage;
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	
	//everything is static because we don't want an instance of input manager, we want THE input manager
	//final means that there is no extending from it
	final public class InputManager
	{
		//import manager needs access to stage
		static private var sStage:Stage;
		//private because we don't want the user to change the spacebar key
		//static because it's being used by a static function and there is only one spacebar key to check
		static private var bSpaceKey:Boolean;
		//keep track of it was previously pressed or not
		static private var bPrevSpaceKey:Boolean;
		//array to keep track of keycodes
		
		//constructor
		public function InputManager():void
		{
			//constructor code
		}
		
		//initialize the input manager for itself
		static public function Initialize(_sStage:Stage):void
		{
			//access the stage
			sStage = _sStage;
			
			//assign values to variables
			bSpaceKey = false;
			bPrevSpaceKey = false;
			
			//add event listeners
			sStage.addEventListener(KeyboardEvent.KEY_DOWN, KeyDown);
			sStage.addEventListener(KeyboardEvent.KEY_UP, KeyUp);
		}
		
		//update previous status of variables in InputManager
		static public function Update():void
		{
			//assign bSpaceKey to bPrevSpaceKey (to keep track of what happened last)
			bPrevSpaceKey = bSpaceKey;
		}
		
		//private because raisens?
		static private function KeyDown(_keyboardEvent:KeyboardEvent):void
		{
			if(_keyboardEvent.keyCode == Keyboard.SPACE)
			{
				bSpaceKey = true;
				//debug
				//trace("changing bSpaceKey to " + bSpaceKey);
			}
		}
		
		//private because ?
		static private function KeyUp(_keyboardEvent:KeyboardEvent):void
		{
			if(_keyboardEvent.keyCode == Keyboard.SPACE)
			{
				bSpaceKey = false;
				//debug
				//trace("changing bSpaceKey to " + bSpaceKey);
			}
		}
		
		//function is static because we only ever have one of it to update for all of its uses
		//used to check what key is pressed, receives a keycode type, returns a boolean
		static public function IsPressed(_keyCode:int):Boolean
		{
			if(_keyCode == Keyboard.SPACE)
			{
				//if bSpaceKey is true or false it gets returned
				return bSpaceKey;
			}
			
			//default if no key is being pressed
			return false;
		}
		
		static public function IsTriggered(_keyCode:int):Boolean
		{
			//if spacebar is pressed
			if(_keyCode == Keyboard.SPACE)
			{
				//if the previous button is false and the current key is true, then return IsTriggered is true!
				return (bPrevSpaceKey == false && bSpaceKey == true);
			}
			
			//nothing is triggered
			return false;
		}
		
		static public function IsReleased(_keyCode:int):Boolean
		{
			//if spacebar is pressed
			if(_keyCode == Keyboard.SPACE)
			{
				//if the previous button is false and the current key is true, then return IsTriggered is true!
				return (bPrevSpaceKey == true && bSpaceKey == false);
			}
			
			//nothing is released
			return false;
		}
		
		//used to get rid of event listners/functions/etc…
		static public function Destroy():void
		{
			//remove event listeners before removing the stage (otherwise we cannot access them)
			sStage.removeEventListener(KeyboardEvent.KEY_DOWN, KeyDown);
			sStage.removeEventListener(KeyboardEvent.KEY_UP, KeyDown);
			
			//not using sStage when Destroy() is called, set to null
			sStage = null;
			
		}
	}
}