/***************************************************************************************/
/*
	filename   	State.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	This class acts as the interface to all the other states. Every single state that
	extends from this class should override all of its methods.
*/        	 
/***************************************************************************************/
package Engine
{
	public class State
	{
		/*******************************************************************************/
		/*
			Description:
				Constructor that creates a state instance 
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function State()
		{

		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is called right after the constructor. The main role is to 
				create or initialize the state's variables once in the beginning. This
				methos is not called when restarting a state.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Load():void
		{
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is called right after the constructor. The main role is to 
				create or initialize the state's variables once in the beginning. This
				methos is not called when restarting a state.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Create():void
		{
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is called to initialize the state. Usually the initialize 
				function contains all objects that have to be loaded when we reset 
				the state
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Initialize():void
		{
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method runs every frame. It updates all the objects inside a state
				and will contain all the checks that might restart/end a state. 
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Update():void
		{
		}

		/*******************************************************************************/
		/*
			Description:
				This method removes all objects created in the initialize function.
				It is called when resetting a state or before destroying it.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Uninitialize():void
		{
		}

		/*******************************************************************************/
		/*
			Description:
				This method fully destroys a state (will use the object manager's 
				functionalities to destroy the objects, will release the memory that
				the constructor allocated, etc...)
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Destroy():void
		{
		}
	}
}