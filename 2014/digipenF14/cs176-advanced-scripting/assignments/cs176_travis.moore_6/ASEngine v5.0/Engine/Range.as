/***************************************************************************************/
/*
	filename   	Range.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.geom.Point;
	import Engine.Force;
	
	final internal class Range
	{
		internal var iLowerAngle_:int;
		internal var iUpperAngle_:int;
		internal var nLowerMagnitude_:Number;
		internal var nUpperMagnitude_:Number;
		internal var nLowerLifetime_:Number;
		internal var nUpperLifetime_:Number;
		
		public function Range(iLowerAngle:int, iUpperAngle:int,
					   nLowerMagnitude:Number, nUpperMagnitude:Number, 
					   nLowerLifetime:Number, nUpperLifetime:Number)
		{
			iLowerAngle_ = iLowerAngle;
			iUpperAngle_ = iUpperAngle;
			nLowerMagnitude_ = nLowerMagnitude;
			nUpperMagnitude_ = nUpperMagnitude;
			nLowerLifetime_ = nLowerLifetime;
			nUpperLifetime_ = nUpperLifetime;
		}
		
		final internal function GetForceFromRange():Force
		{
			var nAngle:Number = HelperFunctions.GetRandom(iLowerAngle_, iUpperAngle_) * (Math.PI / 180.0);
			var pDirection:Point = new Point();
			pDirection.x = Math.cos(nAngle);
			pDirection.y = Math.sin(nAngle);
			if(nLowerLifetime_ == -1 && nUpperLifetime_ == -1)
			{
				return new Force(pDirection, HelperFunctions.GetRandom(nLowerMagnitude_, nUpperMagnitude_), Infinity);
			}
			else
			{
				return new Force(pDirection, HelperFunctions.GetRandom(nLowerMagnitude_, nUpperMagnitude_), HelperFunctions.GetRandom(nLowerLifetime_, nUpperLifetime_));
			}
		}
	}
}