/***************************************************************************************/
/*
	filename   	EmitterCircle.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.geom.Point;
	
	final public class EmitterCircle extends Emitter
	{
		internal var nInnerRadius:Number;
		internal var nOuterRadius:Number;
		internal var iInnerAngleRange:int;
		internal var iOuterAngleRange:int;
		
		public function EmitterCircle(nPosX_:Number, nPosY_:Number, uiEmissionRate_:uint, nEmissionDelay_:Number,
									  nInnerRadius_:Number, nOuterRadius_:Number, iInnerAngleRange_:int, iOuterAngleRange_:int)
		{
			super(nPosX_, nPosY_, uiEmissionRate_, nEmissionDelay_);
			nInnerRadius = nInnerRadius_;
			nOuterRadius = nOuterRadius_;
			iInnerAngleRange = iInnerAngleRange_;
			iOuterAngleRange = iOuterAngleRange_;
		}

		override internal function SetParticlePosition(particle:Particle)
		{
			var nRandomMag:Number = HelperFunctions.GetRandom(nInnerRadius, nOuterRadius);
			var nAngle:Number = HelperFunctions.GetRandom(iInnerAngleRange, iOuterAngleRange) * (Math.PI / 180.0);
			particle.displayobject.x = pPosition.x + Math.cos(nAngle) * nRandomMag;
			particle.displayobject.y = pPosition.y + Math.sin(nAngle) * nRandomMag;
		}
		
	}

}