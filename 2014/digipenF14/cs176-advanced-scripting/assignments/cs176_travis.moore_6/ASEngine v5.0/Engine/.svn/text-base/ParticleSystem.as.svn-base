/***************************************************************************************/
/*
	filename   	ParticleSystem.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.display.DisplayObjectContainer;
	import flash.geom.Point;
	import flash.display.DisplayObject;
	import flash.display.Sprite;
	
	final public class ParticleSystem extends GameObject 
	{
		public var particleinfo:ParticleInfo;
		private var uiNumberOfParticles:uint;
		private var nLifeTime:Number;
		private var vParticles:Vector.<Particle>;
		public var emitter:Emitter;
		private var uiParticlesCount:uint;
		private var nEmissionDelayTimer:Number;
		
		public function ParticleSystem(xmlEmitterProperties_:XML, xmlParticleProperties_:XML, uiNumberOfParticles_:uint, nLifeTime_:Number,
									   displayobjectcontainer_:DisplayObjectContainer = null, nPosX_:Number = 0, nPosY_:Number = 0, 
									   iCollisionType_:int = 1, iID_:int = 13 )
		{
			if(displayobjectcontainer_ == null)
			{
				displayobjectcontainer_ = new Sprite();
			}
			
			super(displayobjectcontainer_, nPosX_, nPosY_, iID_, iCollisionType_);
			uiNumberOfParticles = uiNumberOfParticles_;
			nLifeTime = nLifeTime_;
			particleinfo = new ParticleInfo(xmlParticleProperties_);
			vParticles = new Vector.<Particle>(uiNumberOfParticles);
			if(xmlEmitterProperties_.Type == "Circle")
			{
				emitter = new EmitterCircle(nPosX_, nPosY_, Number(xmlEmitterProperties_.EmissionRate), Number(xmlEmitterProperties_.EmissionDelay),
											Number(xmlEmitterProperties_.InnerRadius), Number(xmlEmitterProperties_.OuterRadius), 
											int(xmlEmitterProperties_.InnerAngle), int(xmlEmitterProperties_.OuterAngle));
			}
			else if(xmlEmitterProperties_.Type == "Rectangle")
			{
				emitter = new EmitterRectangle(nPosX_, nPosY_, Number(xmlEmitterProperties_.EmissionRate), Number(xmlEmitterProperties_.EmissionDelay),
											uint(xmlEmitterProperties_.InnerHalfWidth), uint(xmlEmitterProperties_.OuterHalfWidth),
											uint(xmlEmitterProperties_.InnerHalfHeight), uint(xmlEmitterProperties_.OuterHalfHeight));
			}
			for(var f:int = 0; f < xmlEmitterProperties_.Forces.Force.length(); ++f)
			{
				emitter.AddForceOnParticle(int(xmlEmitterProperties_.Forces.Force[f].LowerAngle),int(xmlEmitterProperties_.Forces.Force[f].UpperAngle),
										   int(xmlEmitterProperties_.Forces.Force[f].MinMagnitude),int(xmlEmitterProperties_.Forces.Force[f].MaxMagnitude),
										   Number(xmlEmitterProperties_.Forces.Force[f].MinTime),Number(xmlEmitterProperties_.Forces.Force[f].MaxTime));
			}
			uiParticlesCount = 0;
			nEmissionDelayTimer = 0.0;
		}
		
		final override public function Initialize():void
		{
			GenerateParticles();
		}
		
		final private function GenerateParticles()
		{
			var uiNumberOfNewParticles:uint = 0;
			
			if(emitter.nEmissionDelay < Number.MIN_VALUE)
			{
				uiNumberOfNewParticles = vParticles.length;
			}
			else
			{
				nEmissionDelayTimer += PhysicsManager.DT;
				uiNumberOfNewParticles = nEmissionDelayTimer * emitter.uiEmissionRate / emitter.nEmissionDelay;
				if(uiNumberOfNewParticles > 0)
				{
					nEmissionDelayTimer = 0.0;
				}
			}
            
			if (uiParticlesCount + uiNumberOfNewParticles > vParticles.length)
            {
                uiNumberOfNewParticles = (uint)(vParticles.length - uiParticlesCount);
            }
			
			for(var p:int = 0; p < uiNumberOfNewParticles; ++p)
			{
				vParticles[uiParticlesCount + p] = new Particle(new particleinfo.particleClassReference() as DisplayObject, particleinfo, CollisionManager.CO_NO_COLLISION);
				ObjectManager.AddObject(vParticles[uiParticlesCount + p], particleinfo.sName, ObjectManager.OM_DYNAMICOBJECT);
				ResetParticle(uiParticlesCount + p);
			}
			
			uiParticlesCount += uiNumberOfNewParticles;
		}
		
		final private function ResetParticle(iIndex:int)
		{
			vParticles[iIndex].Initialize();
			emitter.SetParticlePosition(vParticles[iIndex]);
			emitter.SetParticleForces(vParticles[iIndex]);
		}
		
		final override public function Update():void
		{
			if(nLifeTime >= 0)
			{
				if(uiParticlesCount < vParticles.length)
				{
					GenerateParticles();
				}
				
				for(var i:int = 0; i < uiParticlesCount; ++i)
				{
					if(vParticles[i].bShouldReset == true)
					{
						ResetParticle(i);
					}
				}
				
				if(int(nLifeTime) != Infinity)
				{
					nLifeTime -= PhysicsManager.DT;
				}
			}
			else
			{
				bIsDead = true;
			}
		}
		
		final override public function Uninitialize():void
		{
			for(var p:int = 0; p < vParticles.length; ++p)
			{
				vParticles[p] = null;
			}
		}
				
		final override public function Destroy():void
		{
			super.Destroy();

			particleinfo = null;
			
			for(var p:int = 0; p < vParticles.length; ++p)
			{
				vParticles[p] = null;
			}
			vParticles.length = 0;
			vParticles = null;
			
			emitter.Destroy();
		}
	}

}