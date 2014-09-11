/***************************************************************************************/
/*
	filename   	GameObject.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date    	01/28/2014 
	
	brief:
	This file contains the variables and functions that are a part of the GameObject 
	class. It extends from the MovieClip class and contains its constructor.
*/        	 
/***************************************************************************************/
package Objects
{
	//access the movieclip class for x and y functionality
	import flash.display.MovieClip;
	
	public class GameObject extends MovieClip
	{
		//static variable to keep track of the number of objects on the stage
		static public var uiNumberOfObjects:uint = 0;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for initializing the GameObject class with its 
				variables.
			
			Parameters:
				- nPosX_:Number - used to define the starting x position of the object
				- nPosY_:Number - used to define the starting y position of the object
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function GameObject(nPosX_:Number, nPosY_:Number):void
		{
			//set x to passed parameter
			x = nPosX_;
			//set y to passed parameter
			y = nPosY_;
			//update number of objects on screen
			++uiNumberOfObjects;
		}
	}
}