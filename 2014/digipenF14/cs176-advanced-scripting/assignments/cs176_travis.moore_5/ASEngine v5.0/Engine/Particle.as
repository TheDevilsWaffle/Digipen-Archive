/***************************************************************************************/
/*
	filename   	Particle.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		11/13/2014
	
	brief:
	The particle class sets all the info located in particle info on a particle and
	computes the opacity, scale, and lifetime of a particle based on the physicsmanager's
	dt.
*/        	 
/***************************************************************************************/
package Engine
{
	import flash.display.DisplayObject;
	import flash.geom.Point;
	import Engine.HelperFunctions;
	
	final internal class Particle extends GameObject
	{
		private var particleinfo:ParticleInfo
		private var nLifeTime:Number;		//never changes
		private var nDeltaOpacity:Number;		
		private var pDeltaScale:Point;
		public var bShouldReset:Boolean;
		private var nAge:Number;
		
		/***************************************************************************************/
		/*
			Description:
				Constructor that initializes the Particle variables.
			
			Parameters:
				- displayobject_:DisplayObject
				- particleinfo_:ParticleInfo
				- iCollisionType_:int 
				- iID_:int
		*/
		/***************************************************************************************/
		public function Particle(displayobject_:DisplayObject, 
								 particleinfo_:ParticleInfo, 
								 iCollisionType_:int = 0, 
								 iID_:int = 14 ):void
		{
			super(displayobject_, 0 , 0, iID_, iCollisionType_);
			/* STUDENT CODE GOES HERE */
			
			this.bIsDead = false;
			this.bShouldReset = false;
			this.particleinfo = particleinfo_;
			this.EnablePhysics();
			this.SetPhysicsProperties(this.particleinfo.nMass, new Point(0,0), 0);
		}
		
		/***************************************************************************************/
		/*
			Description:
				Initialize sets the variables of the Particle class by computing the values that
				they need to be upon creation (works with reset to create new values).
			
			Parameters:
				- None
		*/
		/***************************************************************************************/
		final override public function Initialize():void
		{
			//compute and set lifetime to help us compute delta scale and delta opacity
			this.nLifeTime = HelperFunctions.GetRandom(this.particleinfo.nLowerLifetime, this.particleinfo.nUpperLifetime);
			//use number of frames to determine how much of a change to make over time
			var iNumberOfFrames:int = nLifeTime / PhysicsManager.DT;
			
			//compute delta opacity
			var nStartOpacity:Number = HelperFunctions.GetRandom(this.particleinfo.nLowerStartOpacity, 
																 this.particleinfo.nLowerEndOpacity);
			var nEndOpacity:Number = HelperFunctions.GetRandom(this.particleinfo.nUpperStartOpacity, 
															   this.particleinfo.nUpperEndOpacity);
			//set start opacity   
			this.displayobject.alpha = nStartOpacity;
			//set nDeltaOpacity
			this.nDeltaOpacity = (nEndOpacity - nStartOpacity) / iNumberOfFrames;
			
			//compute starting scale
			var pStartScale:Point = new Point(HelperFunctions.GetRandom(this.particleinfo.nLowerStartScaleX, 
																		this.particleinfo.nLowerEndScaleX),
									 		  HelperFunctions.GetRandom(this.particleinfo.nLowerStartScaleY, 
																		this.particleinfo.nLowerEndScaleY)); 
			//compute ending scale
			var pEndScale:Point = new Point(HelperFunctions.GetRandom(this.particleinfo.nUpperStartScaleX, 
																	  this.particleinfo.nUpperEndScaleX),
									 		HelperFunctions.GetRandom(this.particleinfo.nUpperStartScaleY, 
																	  this.particleinfo.nUpperEndScaleY));
			//set start scale
			this.displayobject.scaleX = pStartScale.x;
			this.displayobject.scaleY = pStartScale.y;
			
			//set pDeltaScale
			this.pDeltaScale = new Point((pEndScale.x - pStartScale.x) / iNumberOfFrames,
										 (pEndScale.y - pStartScale.y) / iNumberOfFrames);
			//setting more variables
			this.nAge = 0;
			this.bShouldReset = false;
			this.bIsDead = false;
			this.physicsinfo.RemoveAllForces();
			this.physicsinfo.nVelocityMagnitude = 0;
			this.physicsinfo.pVelocityDirection.x = 0;
			this.physicsinfo.pVelocityDirection.y = 0;						
		}
		
		/***************************************************************************************/
		/*
			Description:
				Update takes into consideration if a particle is alive or should be reset based
				upon the lifetime of the particle, and then computes the new opacity and scale of 
				the particle.
			
			Parameters:
				- None
		*/
		/***************************************************************************************/
		final override public function Update():void
		{ 
			/* STUDENT CODE GOES HERE */
			//bIsReset should be above bIsDead, reset gets a chance before bIsDead is used.
			// scale, opacity, check the lifetime (subtract dt from lifetime)
			//if lifetime is <= 0, bIsReset should be set to true, if not reseting, then set bIsDead to true
				if(this.bShouldReset)
				{
					this.bIsDead = true;
				}
				
				//compute new values for opacity and scale
				this.displayobject.alpha -= this.nDeltaOpacity;
				this.displayobject.scaleX += this.pDeltaScale.x;
				this.displayobject.scaleY += this.pDeltaScale.y;
				//decrease its lifetime
				this.nLifeTime -= PhysicsManager.DT;
				
				if(this.nLifeTime <= 0)
				{
					this.bShouldReset = true;
				}
		}
		
		/***************************************************************************************/
		/*
			Description:
				Destroy nulls values and generally unsets things.
			
			Parameters:
				- None
		*/
		/***************************************************************************************/
		final override public function Destroy():void
		{
			//null all the things
			this.nAge = 0;
			this.nDeltaOpacity = 0;
			this.nLifeTime = 0;
			this.pDeltaScale = null;
			this.particleinfo = null;
		}
	}
}