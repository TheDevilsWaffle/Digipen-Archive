/***************************************************************************************/
/*
	filename   	GameStateManager.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu, eabichahine@digipen.edu
	date    	02/18/2014 
	
	brief:
	The game is always living in a state. This class is reponsible for keeping track of 
	the current state and provide with a smooth switch from one state to another whenever
	required.
*/        	 
/***************************************************************************************/
package Engine
{
	final public class GameStateManager
	{
		//used to keep track of the current state in the Update() loop
		static private var statePrevious:State;
		static private var stateCurrent:State;				
		static private var stateNext:State;
		
		//used to keep track of which state your GSM is currently in
		static private var GSMStep:int;
		//used to keep track if the user requested a restart
		static public var bRequestRestart:Boolean;
		//used to keep track if the user requested to quit
		static public var bRequestQuit:Boolean;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor that initializes the GameStateManager's variables.
			
			Parameters:
				- stateStarting_: paramater used to indicate the next level being used.
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Initialize(stateStarting_:State):void
		{
			//give values to variables
			bRequestRestart = false;
			bRequestQuit = false;
			GSMStep = 0;
			statePrevious = null;
			//pass paramater to stateCurrent
			stateCurrent = stateStarting_;
			//set nextCurrent equal to stateCurrent
			stateNext = stateStarting_;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This function is the GameStateManager's loop, controlled by the GSMStep
				variable's current value.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Update():void
		{
			if(GSMStep == 0)
			{
				//call the currentState's Create()
				stateCurrent.Create();
				//increment GSMStep so we can go to Initialize()
				++GSMStep;
				//set stateCurrent to stateNext
				stateCurrent = stateNext;
			}
			if(GSMStep == 1)
			{
				//call the currentState's Initialize()
				stateCurrent.Initialize();
				//increment GSMStep so we can go to Update()
				++GSMStep;
			}
			if(GSMStep == 2)
			{
				if(stateCurrent == stateNext)
				{
					//call stateCurrent's update()
					stateCurrent.Update()
					//update the statePrevious
					statePrevious = stateCurrent;
				}
				else
				{
					//increment GSMStep
					++GSMStep;
				}
			}
			if(GSMStep == 3)
			{
				//perform the currentState's Uninitialize()
				stateCurrent.Uninitialize();
				
				//check if user wanted a restart
				if(bRequestRestart == true)
				{
					//set next equal to current
					stateNext = stateCurrent;
					//reset bRequestRestart back to false
					bRequestRestart = false;
					//set GSMStep == 1 so we can can Initialize()
					GSMStep = 1;
				}
				//go to next step
				else
				{
					//increment GSMStep
					++GSMStep;
				}
			}
			if(GSMStep == 4)
			{
				//perform the currentState's Destroy()
				stateCurrent.Destroy();
				
				//check if user wants to quit
				if(bRequestQuit == true)
				{
					//set bQuit to true
					Game.bQuit = true;
				}
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				This function performs the stateCurrent's Uninitiailze(), Destroy(), and
				then initializes the newly passed in level.
			
			Parameters:
				- stateNext_: paramater used to indicate the next level being used.
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function GotoState(stateNext_:State):void
		{
			//perform the currentState's Uninitialize()
			stateCurrent.Uninitialize();
			//perform the currentState's Destroy()
			stateCurrent.Destroy();
			//start the newest state
			Initialize(stateNext_);
		}
		
		/*******************************************************************************/
		/*
			Description:
				Used to indicate a player change made, specfically to restart.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function RestartState():void
		{
			//set stateNext to null
			stateNext = null;
			//set bRequestStart to true
			bRequestRestart = true;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Used to indicate a player change made, specfically to quit.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function Quit():void
		{
			//set stateNext to null
			stateNext = null;
			//set bQuitStart to true
			bRequestQuit = true;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Used to destroy all variables used by the GameStateManager.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Destroy():void
		{
			//set variables to null
			statePrevious = null;
			stateCurrent = null;				
			stateNext = null;
		}
	}
}
