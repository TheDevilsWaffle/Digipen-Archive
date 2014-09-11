/***************************************************************************************/
/*
	filename   	Enemy.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date    	01/28/2014 
	
	brief:
	This file contains the variables and functions that are a part of the Enemy class.
	It extends from the GameObject class and contains its constructor and the
	CheckOutsideViewport() function.
*/        	 
/***************************************************************************************/
package Objects.Enemies
{
	//access movieclip class for x and y functionality
	import flash.display.MovieClip;
	//access parent class for its functions/variables
	import Objects.GameObject;
	//access events for updating on a per frame basis
	import flash.events.Event;

	public class Enemy extends GameObject
	{
		//variable to control the speed of the object
		protected var iSpeed:int;
		//variable that controls the damage an object does
		public var iDamage:int;
		//variable that controls if the object is alive or not
		public var bIsDead:Boolean;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for initializing the Enemy with its variables
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
		public function Enemy(nPosX_:Number, nPosY_:Number, iSpeed_:int, iDamage_:int):void
		{
			//access and set parent class's parameters
			super(nPosX_, nPosY_);
			
			//set speed according to passed parameter
			iSpeed = iSpeed_;
			//set damage according to passed parameter
			iDamage = iDamage_;
			//the created object is not dead (...yet)
			bIsDead = false;
			
			//add event listeners
			addEventListener(Event.EXIT_FRAME, CheckOutsideViewPort);
		}

		/*******************************************************************************/
		/*
			Description:
				function responsible for checking if an enemy is beyond the boundries of
				the stage, and if so, destroying it.
			
			Parameters:
				- e_:Event - used to update every frame
				
			Return:
				- None
		*/
		/*******************************************************************************/
		protected function CheckOutsideViewPort(e_:Event):void
		{
			//if the object is beyond the frame (x < 0)
			if(x < 0)
			{
				//set that object to dead
				bIsDead = true;
			}
		}
	}
}