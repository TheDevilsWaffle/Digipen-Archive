/***************************************************************************************/
/*
	filename   	EmitterRectangle.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		11/13/2014
	
	brief:
	The emitter rectangle sets the position of the particles based on a rectangle shape. 

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
		
		/***************************************************************************************/
		/*
			Description:
				Constructor sets values on variables based upon passed parameters.
			
			Parameters:
				- nPosX_:Number 
				- nPosY_:Number
				- uiEmissionRate_:uint 
				- nEmissionDelay_:Number
				- uiInnerHalfWidth_:uint 
				- uiOuterHalfWidth_:uint 
				- uiInnerHalfHeight_:uint 
				- uiOuterHalfHeight_:uint
		*/
		/***************************************************************************************/
		public function EmitterRectangle(nPosX_:Number, 
										 nPosY_:Number, 
										 uiEmissionRate_:uint, 
										 nEmissionDelay_:Number,
										 uiInnerHalfWidth_:uint, 
										 uiOuterHalfWidth_:uint, 
										 uiInnerHalfHeight_:uint, 
										 uiOuterHalfHeight_:uint):void
		{
			super(nPosX_, nPosY_, uiEmissionRate_, nEmissionDelay_);
			//set them values based on parameters passed
			uiInnerHalfWidth = uiInnerHalfWidth_;
			uiOuterHalfWidth = uiOuterHalfWidth_;
			uiInnerHalfHeight = uiInnerHalfHeight_;
			uiOuterHalfHeight = uiOuterHalfHeight_;
		}

		/***************************************************************************************/
		/*
			Description:
				SetParticlePosition takes a particle and positions it based upon the shape that
				is defined by the emitterrectangle
			
			Parameters:
				- particle:Particle
		*/
		/***************************************************************************************/
		override internal function SetParticlePosition(particle:Particle)
		{
			/* STUDENT CODE GOES HERE */
			//same thing as the other, but between the width and the height
			var iFirstRandom:int = (Math.random() * 100) % 2;
			var iSecondRandom:int = (Math.random() * 100) % 2;
			
			if(iFirstRandom == 0)
			{
				//set positive x
				particle.displayobject.x = this.pPosition.x + HelperFunctions.GetRandom(-this.uiOuterHalfWidth, this.uiOuterHalfWidth);
				
				//y's turn
				if(iSecondRandom == 0)
				{
					//set positive y
					particle.displayobject.y = this.pPosition.y + HelperFunctions.GetRandom(this.uiInnerHalfHeight, this.uiOuterHalfHeight);
				}
				else
				{
					//set negative y
					particle.displayobject.y = this.pPosition.y + HelperFunctions.GetRandom(-this.uiOuterHalfHeight, -this.uiInnerHalfHeight);
				}
			}
			if(iFirstRandom == 1)
			{
				//set positive y
				particle.displayobject.y = this.pPosition.y + HelperFunctions.GetRandom(-this.uiOuterHalfHeight, this.uiOuterHalfHeight);
				//y's turn
				if(iSecondRandom == 0)
				{
					//set positive x
					particle.displayobject.x = this.pPosition.x + HelperFunctions.GetRandom(this.uiInnerHalfWidth, this.uiOuterHalfWidth);
				}
				else
				{
					//set negative x
					particle.displayobject.x = this.pPosition.x + HelperFunctions.GetRandom(-this.uiOuterHalfWidth, -this.uiInnerHalfWidth);
				}
			}
		}
	}
}