/***************************************************************************************/
/*
	filename   	YellowEnemy.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date    	01/28/2014 
	
	brief:
	This file contains the variables and functions that are a part of the YellowEnemy class.
	It extends from the Enemy class and contains its constructor, AI(), and Destroy() 
	functions.
*/        	 
/***************************************************************************************/
package Objects.Enemies
{
	//access movieclip class for x and y functionality
	import flash.display.MovieClip;
	
	//access events for use in the AI() function
	import flash.events.Event;
	
	public class YellowEnemy extends Enemy
	{
		//variable that controls the rotation speed of the object
		private var iRotationSpeed:int;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for initializing the YellowEnemy with its variables
				and functions.
			
			Parameters:
				- nPosX_:Number - used to define the starting x position of the object
				- nPosY_:Number - used to define the starting y position of the object
				- iSpeed_:int - used to define the speed of the object
				- iDamage_:int - used to define how much damage the object does
				- iRotationSpeed_:int - used to define the rotation speed of the object
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function YellowEnemy(nPosX_:Number, nPosY_:Number, iSpeed_:int, iDamage_:int, iRotationSpeed_:int)
		{
			//access and set its parent classes parameters
			super(nPosX_, nPosY_, iSpeed_, iDamage_);
			//set its rotation speed based on passed parameter
			iRotationSpeed = iRotationSpeed_;
			
			//add event listeners
			addEventListener(Event.ENTER_FRAME, AI);
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for movement of the YellowEnemy object
			
			Parameters:
				- e_:Event - used to update every frame
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function AI(e_:Event):void
		{
			//set the right to left movement of the object to its iSpeed
			x -= iSpeed;
			//rotate according to its iRotationSpeed
			rotation += iRotationSpeed;
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for destroying the YellowEnemy object
			
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