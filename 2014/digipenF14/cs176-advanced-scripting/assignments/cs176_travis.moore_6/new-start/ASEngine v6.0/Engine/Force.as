/***************************************************************************************/
/*
	filename   	Force.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		10/16/2011 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.geom.Point;

	final public class Force
	{
		/* The force's normalized Direction (between 0 and 1) */
		public var pDirection:Point;
		/* Magnitude, how big is the force */
		public var nMagnitude:Number;
		/* Time in seconds, how long will the force be applied on the object */
		public var nTime:Number;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor that initializes the Forces variables.
			
			Parameters:
				- pDirection: 
				- nMagnitude: 
				- nTime: 
		*/
		/*******************************************************************************/
		public function Force(pDirection_:Point, nMagnitude_:Number = 0, nTime_:Number = Infinity)
		{
			pDirection = pDirection_;
			pDirection.normalize(1);
			nMagnitude = nMagnitude_;
			nTime = nTime_;
		}
	}
}