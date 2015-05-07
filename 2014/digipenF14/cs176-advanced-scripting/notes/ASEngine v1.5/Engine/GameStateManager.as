/***************************************************************************************/
/*
	filename   	GameStateManager.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date    	09/02/2013 
	
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
		/* An integer that keeps track of the game state manager's current step */
		static private var iGSMStep:int;
		/* An integer that keeps track of the users request when switching, restarting or
		   quitting a state. */
		static private var iChangeStateRequest:int;
		/* A state instance that gives us access  to the previous state's public 
		   properties */
		static private var statePrevious:State;
		/* A state instance that gives us access  to the current state's public 
		   properties */
		static private var stateCurrent:State;				
		/* A state instance that gives us access  to the next state's public 
		   properties */
		static private var stateNext:State;
		/* An integer that stops the game state manager from updating when loading files.
		   This integer is incremented when a file begins loading and is decreased when 
		   done. As long as it is greater than 0 the game state manager will not update */
		static public var iLoading:int;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor that initializes the game state manager's variables. The 
				user specifies the starting state using the stateStrating_ parameter.
				
			
			Parameters:
				- stateStarting_: The user specified state that the application will 
								  start with.
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Initialize(stateStarting_:State):void
		{			
			stateCurrent = stateNext = stateStarting_;
			statePrevious = null;
			
			iGSMStep = GSMSteps.LOAD_STATE;
			iChangeStateRequest = ChangeStateRequest.NEWSTATE;
			iLoading = 0;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method updates the state that we are in. It also checks if a 
				state needs to be restarted, exited ot switched. The main role of this
				function is to make sure that whatever request comes from the user, it
				is done cleanly without leaving any memory leaks or random objects
				around.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Update():void
		{
			if(iLoading > 0)
			{
				return;
			}
			
			switch(iGSMStep)
			{
				case GSMSteps.LOAD_STATE:
				{
					stateCurrent.Load();
					iGSMStep = GSMSteps.CREATE_STATE;
				}
				break;
				
				case GSMSteps.CREATE_STATE:
				{
					stateCurrent.Create();
					iGSMStep = GSMSteps.INITIALIZE_STATE;
				}
				break;
				
				case GSMSteps.INITIALIZE_STATE:
				{
					if(stateCurrent == statePrevious)
					{
						ObjectManager.UninitializeAllObject();
						ObjectManager.InitializeAllObject();
					}
					InputManager.Reset();
					stateCurrent.Initialize();
					iGSMStep = GSMSteps.UPDATE_STATE;
				}
				break;
				
				case GSMSteps.UPDATE_STATE:
				{
					if(stateCurrent == stateNext)
					{
						ObjectManager.Update();
						stateCurrent.Update();
					}
					else
					{
						statePrevious = stateCurrent;
						iGSMStep = GSMSteps.UNINITIALIZE_STATE;
					}
				}
				break;
								
				case GSMSteps.UNINITIALIZE_STATE:
				{
					stateCurrent.Uninitialize();
					
					if (iChangeStateRequest == ChangeStateRequest.RESTART)
					{
						stateNext = stateCurrent;
						iGSMStep = GSMSteps.INITIALIZE_STATE;
						break;
					}
					iGSMStep = GSMSteps.DESTROY_STATE;
				}
				break;
					
				case GSMSteps.DESTROY_STATE:
				{
					stateCurrent.Destroy();
					ObjectManager.DestroyAllData();
					XMLManager.Clear();
					stateCurrent = null;
					
					if (iChangeStateRequest == ChangeStateRequest.QUIT)
					{
						Game.bQuit = true
					}
					else
					{
						stateCurrent = stateNext;
						iGSMStep = GSMSteps.LOAD_STATE;
					}
				}
				break;
				
				default:
				{
					trace("Game State Manager Unknown State!!!");
				}
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is used to switch to a new state. Since it is static, the 
				user can call it at any point and from any other class. A new state will
				be specified and the GameStateManager will do the rest.
			
			Parameters:
				- stateNext_: A user specified state that the application will switch to.
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function GotoState(stateNext_:State):void
		{
			stateNext = stateNext_;
			iChangeStateRequest = ChangeStateRequest.NEWSTATE;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is used to restart the state we are in.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function RestartState():void
		{
			stateNext = null;
			iChangeStateRequest = ChangeStateRequest.RESTART;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is used to quit the application at any point in time.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function Quit():void
		{
			stateNext = null;
			iChangeStateRequest = ChangeStateRequest.QUIT;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method destroys the GameStateManager and all the variables in it.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Destroy():void
		{
			statePrevious = null;
			stateCurrent = null;
			stateNext = null;
		}

	}
}

final internal class GSMSteps 
{
	static internal const LOAD_STATE:Number = -1;
	static internal const CREATE_STATE:Number = 0;
	static internal const INITIALIZE_STATE:Number = 1;
	static internal const UPDATE_STATE:int = 2;
	static internal const UNINITIALIZE_STATE:int = 3;
	static internal const DESTROY_STATE:int = 4;
}

final internal class ChangeStateRequest
{
	static internal const NEWSTATE:Number = 0;
	static internal const RESTART:int = 1;
	static internal const QUIT:int = 2;
}