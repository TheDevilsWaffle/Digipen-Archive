/***************************************************************************************/
/*
	filename   	EmitterCircle.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		11/13/2014 
	
	brief:
	the emitter circle sets the position of particles in the shape of a circle
*/        	 
/***************************************************************************************/
package Engine
{
	import flash.geom.Point;
	import flash.display.Stage;
	import Math;
	
	final public class EmitterCircle extends Emitter
	{
		internal var nInnerRadius:Number;
		internal var nOuterRadius:Number;
		internal var iInnerAngleRange:int;
		internal var iOuterAngleRange:int;
		
		/***************************************************************************************/
		/*
			Description:
				Constructor that sets the parameters of the EmitterCircle class
			
			Parameters:
				- nPosX_:Number
				- nPosY_:Number 
				- uiEmissionRate_:uint
				- nEmissionDelay_:Number
				- nInnerRadius_:Number
				- nOuterRadius_:Number 
				- iInnerAngleRange_:int 
				- iOuterAngleRange_:int
		*/
		/***************************************************************************************/
		public function EmitterCircle(nPosX_:Number, 
									  nPosY_:Number, 
									  uiEmissionRate_:uint, 
									  nEmissionDelay_:Number,
									  nInnerRadius_:Number, 
									  nOuterRadius_:Number, 
									  iInnerAngleRange_:int, 
									  iOuterAngleRange_:int)
		{
			super(nPosX_, nPosY_, uiEmissionRate_, nEmissionDelay_);
			nInnerRadius = nInnerRadius_;
			nOuterRadius = nOuterRadius_;
			iInnerAngleRange = iInnerAngleRange_;
			iOuterAngleRange = iOuterAngleRange_;
		}
		
		/***************************************************************************************/
		/*
			Description:
				SetParticlePosition takes a particle and sets where it will be on the screen.
			
			Parameters:
				- particle:Particle - the actual particle being used
		*/
		/***************************************************************************************/
		override internal function SetParticlePosition(particle:Particle)
		{
			/* STUDENT CODE GOES HERE */
			//when we create or reset, where should the particle restart at? between inner and outer radius
			/*
			place particles inside a circle

			step 1—random angle on the circle (0 to 360 degrees
			step 2—get unit vector from the circle
			step 3—get random value between inner radius and outer radius
			step 4-place particle at the center and move it using vector + magnitude
			
			p.x = center.x + (v.x * mag)
			p.y = center.y + (v.y * mag)
			*/
			
			//get random angle between inner and outer angles
			var nRandomAngle:Number = HelperFunctions.GetRandom(this.iInnerAngleRange, 
																this.iOuterAngleRange);
			//degrees to radians
			nRandomAngle *= Math.PI/180;
			
			//no need to normalize this because cos/sin give a value between 0 and 1
			var pUnitCircle:Point = new Point(Math.cos(nRandomAngle), 
											  Math.sin(nRandomAngle));
			
			//get a random distance between inner and outer radius
			var nRandomDistance:Number = HelperFunctions.GetRandom(this.nInnerRadius, 
																   this.nOuterRadius);
			
			//particle systems's x and y for center
			particle.displayobject.x = this.pPosition.x + (pUnitCircle.x * nRandomDistance);
			particle.displayobject.y = this.pPosition.y + (pUnitCircle.y * nRandomDistance);
		}
		
	}

}