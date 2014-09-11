/***************************************************************************************/
/*
	filename   	Hero.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date    	01/28/2014 
	
	brief:
	This file contains the variables and functions that are a part of the Hero class.
	It extends from the GameObject class and contains its constructor, Movement(), 
	GotHit(), and iHealthValue() functions.
*/        	 
/***************************************************************************************/
package Objects
{
	//access movieclip class for x and y functionality
	import flash.display.MovieClip;
	//access MouseEvent for tracking the mouse for movement
	import flash.events.MouseEvent;
	//import events for updating every frame
	import flash.events.Event;
	//access parent class for its variables and functions
	import Objects.GameObject;

	public class Hero extends GameObject
	{
		//variable to represent player health
		private var iHealth:int;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for initializing the Hero class with its 
				variables and functions.
			
			Parameters:
				- nPosX_:Number - used to define the starting x position of the object
				- nPosY_:Number - used to define the starting y position of the object
				- iHealth_:int - used to define health of the object
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Hero(nPosX_:Number, nPosY_:Number, iHealth_:int):void
		{
			//access and set the parent class's parameters
			super(nPosX_, nPosY_);
			//set the health to the passed parameter
			iHealth = iHealth_;
			
			//add event listeners
			addEventListener(MouseEvent.MOUSE_MOVE, Movement);
		}
		/*******************************************************************************/
		/*
			Description:
				function responsible for updating hero movement based on mouse movement.
			
			Parameters:
				- me_:MouseEvent - track mouse events (in this case, movement)
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Movement(me_:MouseEvent):void
		{
			//update y based on mouseY movement
			y = me_.localY;
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for damaging the player based on enemy damage
			
			Parameters:
				- iDamage_:int - used to define how much damage the object does
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function GotHit(iDamage_:int):void
		{
			//update iHealth based on damage taken
			iHealth = iHealth - iDamage_;
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for getting the iHealth value when called
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function get iHealthValue():int
		{
			//return the iHealth value
			return iHealth;
		}
	}
}