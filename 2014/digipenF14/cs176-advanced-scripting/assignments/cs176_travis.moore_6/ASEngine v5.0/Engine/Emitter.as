/***************************************************************************************/
/*
	filename   	Emitter.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.geom.Point;
	
	internal class Emitter
	{
		public var pPosition:Point;
		public var uiEmissionRate:uint;
		public var nEmissionDelay:Number;
		private var vForceRanges:Vector.<Range>;
		
		public function Emitter(nPosX_:Number, nPosY_:Number, uiEmissionRate_:uint, nEmissionDelay_:Number)
		{
			pPosition = new Point(nPosX_, nPosY_);
			uiEmissionRate = uiEmissionRate_;
			nEmissionDelay = nEmissionDelay_;
			vForceRanges = new Vector.<Range>();
		}

		internal function SetParticlePosition(particle:Particle)
		{
		}
		
		final public function AddForceOnParticle(iLowerAngle:int, iUpperAngle:int,
					   					   nLowerMagnitude:Number, nUpperMagnitude:Number, 
					                       nLowerLifetime:Number, nUpperLifetime:Number)
		{
			vForceRanges.push(new Range(iLowerAngle, iUpperAngle, nLowerMagnitude, nUpperMagnitude, nLowerLifetime, nUpperLifetime));
		}
		
		internal function SetParticleForces(particle:Particle):void
		{
			for(var i:int = 0; i < vForceRanges.length; ++i)
			{
				var force:Force = vForceRanges[i].GetForceFromRange();
				particle.physicsinfo.AddForce(force.nTime, force.pDirection, force.nMagnitude);
			}
		}
				
		internal function Destroy()
		{
			for(var i:int = 0; i<vForceRanges.length; ++i)
			{
				vForceRanges[i] = null;
			}
			vForceRanges.length = 0;
		}
	}
}