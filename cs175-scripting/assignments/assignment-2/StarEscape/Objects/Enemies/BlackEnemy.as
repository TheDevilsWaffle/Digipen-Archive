/***************************************************************************************/
/*
	filename   	BlackEnemy.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date    	01/28/2014 
	
	brief:
	This file contains the variables and functions that are a part of the BlackEnemy class.
	It extends from the Enemy class and contains its constructor, AI(), and Destroy() 
	functions.
*/        	 
/***************************************************************************************/
package Objects.Enemies
{
	//access movieclip class for x and y functionality
	import flash.display.MovieClip;

	//access events for use in updating every frame in AI() function
	import flash.events.Event;
	
	public class BlackEnemy extends Enemy
	{
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for initializing the BlackEnemy with its variables
				and functions.
			
			Parameters:
				- nPosX_:Number - used to define the starting x position of the object
				- nPosY_:Number - used to define the starting y position of the object
				- iSpeed_:int - used to define the speed of the object
				- iDamage_:int - used to define how much damage the object does
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function BlackEnemy(nPosX_:Number, nPosY_:Number, iSpeed_:int, iDamage_:int)
		{
			//access and set parent class's parameters
			super(nPosX_, nPosY_, iSpeed_, iDamage_);
			
			//add event listeners
			addEventListener(Event.ENTER_FRAME, AI);
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for movement of the BlackEnemy object
			
			Parameters:
				- e_:Event - used to update every frame
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function AI(e_:Event):void
		{
			//move right to left based on iSpeed
			x -= iSpeed;
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for destroying the BlackEnemy object
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Destroy():void
		{
			//remove event listeners
			removeEventListener(Event.ENTER_FRAME, AI)
			
			//update the uiNumberOfOjects variable
			--uiNumberOfObjects;
		}
	}
}