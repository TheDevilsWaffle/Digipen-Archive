/***************************************************************************************/
/*
	filename   	Comments.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date    	09/22/2011 
	
	brief:
	This is a sample document that teaches students how to comment. Students have to 
	follow this commenting style exactly so that they don't get points deducted.
*/        	 
/***************************************************************************************/
package
{	
	/* Importing the MovieClip library. Explain a little why you need it */
	import flash.display.MovieClip;
	
	public class Comments
	{
		/* Public Number used for ... */
		public var bVariable:Number; 

		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for ...
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Comments()
		{
			/* 
			Initializing the variable with 0. You should always comment the code 
			inside the function. Since you are in CS176 now, you are allowed not to
			comment every line of code but if I find code that I can't understand 
			and is not commented you will get points removed.
			*/
			bVariable = 0;
			/* Greeting the user in the beginning of the game */
			trace("hello");
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is showing how to comment a function that has parameters but
				no return type.
			
			Parameters:
				- iParam1_: An integer variable responsible for a lot of awesome things
							that we can't talk about.
							
				- nParam2_: A number variable responsible for even more awesome things.
				
			Return:
				- None				     
		*/
		/*******************************************************************************/
		private function SampleFunction(iParam1_:int, nParam2_:Number):void
		{
			/* Code here */
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is showing how to comment a function that has no parameters 
				but returns an integer.
			
			Parameters:
				- None
				
			Return:
				- An integer that represents how many ...
		*/
		/*******************************************************************************/
		private function SampleFunction2():int
		{
			/* Code here */
		}
	}
}