/***************************************************************************************/
/*
	filename   	EmitterRectangle.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.geom.Point;
	
	final public class EmitterRectangle extends Emitter
	{
		internal var uiInnerHalfWidth:uint;
		internal var uiOuterHalfWidth:uint;
		internal var uiInnerHalfHeight:uint;
		internal var uiOuterHalfHeight:uint;
		
		public function EmitterRectangle(nPosX_:Number, nPosY_:Number, uiEmissionRate_:uint, nEmissionDelay_:Number,
										 uiInnerHalfWidth_:uint, uiOuterHalfWidth_:uint, uiInnerHalfHeight_:uint, uiOuterHalfHeight_:uint)
		{
			super(nPosX_, nPosY_, uiEmissionRate_, nEmissionDelay_);
			uiInnerHalfWidth = uiInnerHalfWidth_;
			uiOuterHalfWidth = uiOuterHalfWidth_;
			uiInnerHalfHeight = uiInnerHalfHeight_;
			uiOuterHalfHeight = uiOuterHalfHeight_;
		}

		override internal function SetParticlePosition(particle:Particle)
		{
			var nXOrY:Number = HelperFunctions.GetRandom(0,200);
			var nPositiveOrNegative:Number = HelperFunctions.GetRandom(0,200);
			
			// X is free but Y limited
			if(nXOrY > 0 && nXOrY <= 100)
			{
				particle.displayobject.x = pPosition.x + HelperFunctions.GetRandom(-uiOuterHalfWidth, uiOuterHalfWidth);
				if( nPositiveOrNegative > 0 && nPositiveOrNegative <= 100)
				{
					particle.displayobject.y = pPosition.y + HelperFunctions.GetRandom(uiInnerHalfHeight, uiOuterHalfHeight);
				}
				else
				{
					particle.displayobject.y = pPosition.y + HelperFunctions.GetRandom(-uiInnerHalfHeight, -uiOuterHalfHeight);
				}
			}
			else // Y is free but X Limited
			{
				particle.displayobject.y = pPosition.y + HelperFunctions.GetRandom(-uiOuterHalfHeight, uiOuterHalfHeight);
				if( nPositiveOrNegative > 0 && nPositiveOrNegative <= 100)
				{
					particle.displayobject.x = pPosition.x + HelperFunctions.GetRandom(uiInnerHalfWidth, uiOuterHalfWidth);
				}
				else
				{
					particle.displayobject.x = pPosition.x + HelperFunctions.GetRandom(-uiInnerHalfWidth, -uiOuterHalfWidth);
				}
			}
		}
	}

}