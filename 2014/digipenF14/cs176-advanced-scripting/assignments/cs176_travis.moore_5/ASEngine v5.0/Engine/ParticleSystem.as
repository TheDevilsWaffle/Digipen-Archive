/***************************************************************************************/
/*
	filename   	ParticleSystem.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		11/13/2014
	
	brief:
	the particle system is responsible for determining which emitter to use and actually
	creates the particles on the screen.
*/        	 
/***************************************************************************************/
package Engine
{
	import flash.display.DisplayObjectContainer;
	import flash.geom.Point;
	import flash.display.DisplayObject;
	import flash.display.Sprite;
	
	import Engine.ObjectManager;
	
	final public class ParticleSystem extends GameObject 
	{
		public var particleinfo:ParticleInfo;
		private var uiNumberOfParticles:uint;
		private var nLifeTime:Number;				//this never changes
		private var nAge:Number;					//used in conjunction with nLifeTime
		private var vParticles:Vector.<Particle>;
		public var emitter:Emitter;					//circle or rectangle emitter?
		private var uiParticlesCount:uint;			//how many particles
		private var nEmissionDelayTimer:Number;		//compute it once in the constructor, use it all the time
	
		/***************************************************************************************/
		/*
			Description:
				Constructor takes passed parameters and sets variables with them, also 
				initializes vectors and classes and determines emitter type to used based on 
				xmlEmitterProperties_.
			
			Parameters:
				- xmlEmitterProperties_:XML
				- xmlParticleProperties_:XML
				- uiNumberOfParticles_:uint
				- nLifeTime_:Number
				- displayobjectcontainer_:DisplayObjectContainer
				- nPosX_:Number 
				- nPosY_:Number
				- iCollisionType_:int
				- iID_:int
		*/
		/***************************************************************************************/
		public function ParticleSystem(xmlEmitterProperties_:XML, 
									   xmlParticleProperties_:XML, 
									   uiNumberOfParticles_:uint, 
									   nLifeTime_:Number,
									   displayobjectcontainer_:DisplayObjectContainer = null,
									   nPosX_:Number = 0, 
									   nPosY_:Number = 0, 
									   iCollisionType_:int = 1,
									   iID_:int = 13):void
		{
			super(displayobjectcontainer_, nPosX_, nPosY_, iID_, iCollisionType_);
			/* STUDENT CODE GOES HERE */
			this.uiNumberOfParticles = uiNumberOfParticles_;
			this.nLifeTime = nLifeTime_;
			
			this.vParticles = new Vector.<Particle>;
			
			this.particleinfo = new ParticleInfo(xmlParticleProperties_);
			
			//compute nEmissionDelayTimer here
			this.nEmissionDelayTimer = (xmlEmitterProperties_.EmissionDelay / PhysicsManager.DT);
			
			//circle particle emitter created
			if(xmlEmitterProperties_.Type == "Circle")
			{
				this.emitter = new EmitterCircle(nPosX_, 
											nPosY_, 
											xmlEmitterProperties_.EmissionRate, 
											nEmissionDelayTimer, 
											xmlEmitterProperties_.InnerRadius, 
											xmlEmitterProperties_.OuterRadius, 
											xmlEmitterProperties_.InnerAngle, 
											xmlEmitterProperties_.OuterAngle);
			}
			//rectangle particle emitter created
			if(xmlEmitterProperties_.Type == "Rectangle")
			{
				this.emitter = new EmitterRectangle(nPosX_, 
											   nPosY_, 
											   xmlEmitterProperties_.EmissionRate, 
											   nEmissionDelayTimer, 
											   xmlEmitterProperties_.InnerHalfWidth, 
											   xmlEmitterProperties_.OuterHalfWidth, 
											   xmlEmitterProperties_.InnerHalfHeight, 
											   xmlEmitterProperties_.OuterHalfHeight);
			}
			
			//add forces to emitter
			for(var iIndex:int = 0; iIndex < xmlEmitterProperties_.Forces.Force.length(); ++iIndex)
			{
				this.emitter.AddForceOnParticle(xmlEmitterProperties_.Forces.Force[iIndex].LowerAngle,
												xmlEmitterProperties_.Forces.Force[iIndex].UpperAngle,
												xmlEmitterProperties_.Forces.Force[iIndex].MinMagnitude,
												xmlEmitterProperties_.Forces.Force[iIndex].MaxMagnitude,
												xmlEmitterProperties_.Forces.Force[iIndex].MinTime,
												xmlEmitterProperties_.Forces.Force[iIndex].MaxTime);
			}
		}
		
		/***************************************************************************************/
		/*
			Description:
				Initialize resets particle count
			
			Parameters:
				- None
		*/
		/***************************************************************************************/
		final override public function Initialize():void
		{
			/* STUDENT CODE GOES HERE */
			//position
			//how many particles we have (reset brings this back to 0)
			this.uiParticlesCount = 0;
		}
		
		/***************************************************************************************/
		/*
			Description:
				Generate Particles creates particles if we have not yet reached the particle
				limit.
			
			Parameters:
				- None
		*/
		/***************************************************************************************/
		final private function GenerateParticles():void
		{
			/* STUDENT CODE GOES HERE */
			//takes into consideration the delay
			if(this.uiParticlesCount < this.uiNumberOfParticles)
			{
				//create a particle
				var pParticle:Particle = new Particle(new particleinfo.particleClassReference(), this.particleinfo);
				
				//put it on the stage by giving it to the objectmanager
				ObjectManager.AddObject(pParticle, this.particleinfo.sName, ObjectManager.OM_DYNAMICOBJECT);
				
				//add to vector of particles
				this.vParticles.push(pParticle);
				
				//set position
				this.emitter.SetParticlePosition(pParticle);
				
				//set forces
				this.emitter.SetParticleForces(pParticle);
				
				//update particle count
				++this.uiParticlesCount;
			}
		}
		
		/***************************************************************************************/
		/*
			Description:
				Resets a single particle in the vParticles vector.
			
			Parameters:
				- Index:int
		*/
		/***************************************************************************************/
		final private function ResetParticle(iIndex:int)
		{
			/* STUDENT CODE GOES HERE */
			//resets one particle out of the vector (prolly calls uninitialize)
			this.vParticles[iIndex].physicsinfo.RemoveAllForces();
			this.vParticles[iIndex].Initialize();
			this.emitter.SetParticlePosition(this.vParticles[iIndex]);
			this.emitter.SetParticleForces(this.vParticles[iIndex]);
		}
		
		/***************************************************************************************/
		/*
			Description:
				Update calles GenerateParticles() and keeps track of the age and lifetime of
				particles so we can reset them.
			
			Parameters:
				- None
		*/
		/***************************************************************************************/
		final override public function Update():void
		{
			/* STUDENT CODE GOES HERE */
			this.GenerateParticles();
			++this.nAge;
			if(this.nAge >= this.nLifeTime)
			{
				this.bIsDead = true;
			}
			
			var iVParticlesLength:int = this.vParticles.length;
			for(var iIndex:int = 0; iIndex < iVParticlesLength; ++iIndex)
			{
				if(this.vParticles[iIndex].bShouldReset)
				{
					this.ResetParticle(iIndex);
				}
			}
		}
		
		/***************************************************************************************/
		/*
			Description:
				Unitialize() cleans vParticles up so we can recreate particles
			
			Parameters:
				- None
		*/
		/***************************************************************************************/
		final override public function Uninitialize():void
		{
			/* STUDENT CODE GOES HERE */
			//make sure we clean up everything that we need to recreate
			for(var iIndex:int = 0; iIndex < this.vParticles.length; ++iIndex)
			{
				delete this.vParticles[iIndex];
			}
			this.vParticles.length = 0;
		}
		
		/***************************************************************************************/
		/*
			Description:
				Destroy cleans up the Particle system.
			
			Parameters:
				- None
		*/
		/***************************************************************************************/
		final override public function Destroy():void
		{
			super.Destroy();
		}
	}

}