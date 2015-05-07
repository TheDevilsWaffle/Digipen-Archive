/***************************************************************************************/
/*
	filename   	Emitter.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		11/13/2014
	
	brief:
	The emitter is the location for the particles to appear from and applies forces
	to the particles if forces are there.
*/        	 
/***************************************************************************************/
package Engine
{
	import flash.geom.Point;
	
	internal class Emitter
	{
		public var pPosition:Point;
		public var uiEmissionRate:uint;				//pre-compute: how many particles per frame | ex: (100particles / 0.4 seconds) = (100 / (0.4 / (1/30) ) )
		public var nEmissionDelay:Number;			//
		private var vForceRanges:Vector.<Range>;	//
		
		/***************************************************************************************/
		/*
			Description:
				Constructor sets variables based upon passed parameters.
			
			Parameters:
				- nPosX_:Number 
				- nPosY_:Number 
				- uiEmissionRate_:uint 
				- nEmissionDelay_:Number
		*/
		/***************************************************************************************/
		public function Emitter(nPosX_:Number, 
								nPosY_:Number, 
								uiEmissionRate_:uint, 
								nEmissionDelay_:Number)
		{
			pPosition = new Point(nPosX_, nPosY_);
			uiEmissionRate = uiEmissionRate_;
			nEmissionDelay = nEmissionDelay_;
			vForceRanges = new Vector.<Range>();
		}

		/***************************************************************************************/
		/*
			Description:
				SetParticlePosition sets the particle position of an individual particle
			
			Parameters:
				- particle:Particle
		*/
		/***************************************************************************************/
		internal function SetParticlePosition(particle:Particle)
		{
			//stay empty because we override this in the emittercircle and the emitterrectangle classes
		}
		
		/***************************************************************************************/
		/*
			Description:
				AddForceOnParticles adds forces upon a single particle based on the passed 
				parameters.
			
			Parameters:
				- iLowerAngle:int
				- iUpperAngle:int
				- nLowerMagnitude:Number 
				- nUpperMagnitude:Number 
				- nLowerLifetime:Number 
				- nUpperLifetime:Number
		*/
		/***************************************************************************************/
		final public function AddForceOnParticle(iLowerAngle:int, 
												 iUpperAngle:int,
					   					   		 nLowerMagnitude:Number, 
												 nUpperMagnitude:Number, 
					                       		 nLowerLifetime:Number, 
												 nUpperLifetime:Number)
		{
			/* STUDENT CODE GOES HERE */
			var rNewRange:Range = new Range(iLowerAngle, 
											iUpperAngle,
					   					   	nLowerMagnitude, 
										 	nUpperMagnitude, 
					                       	nLowerLifetime, 
											nUpperLifetime);
			
			//push in vForceRanges
			this.vForceRanges.push(rNewRange);
		}
		
		/***************************************************************************************/
		/*
			Description:
				SetParticleForces sets the forces on particles
			
			Parameters:
				- particle:Particle
		*/
		/***************************************************************************************/
		internal function SetParticleForces(particle:Particle):void
		{
			/* STUDENT CODE GOES HERE */
			//loop through vForceRange
			for(var iIndex:int = 0; iIndex < this.vForceRanges.length; ++iIndex)
			{
				//get a force between range
				var fForce:Force = this.vForceRanges[iIndex].GetForceFromRange();
				
				//apply the force on the particles, they are a gameobject, so you can use applyforce()
				particle.physicsinfo.AddForce(fForce.nTime, fForce.pDirection, fForce.nMagnitude);
			}
		}
		
		/***************************************************************************************/
		/*
			Description:
				Destroy nulls out vectors and resets variables to 0
			
			Parameters:
				- None
		*/
		/***************************************************************************************/
		internal function Destroy()
		{
			/* STUDENT CODE GOES HERE */
			//wipe out vector, null the things
			for(var iIndex:int = 0; iIndex < this.vForceRanges.length; ++iIndex)
			{
				this.vForceRanges[iIndex] = null;
			}
			this.vForceRanges.length = 0;
			
			this.pPosition = null;
			this.uiEmissionRate = 0;
			this.nEmissionDelay = 0;
		}
	}
}