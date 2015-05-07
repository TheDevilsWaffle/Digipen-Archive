/***************************************************************************************/
/*
	filename   	Particle.as 
	author		Elie Abi Chahine / Karim Fikani
	email   	eabichahine@digipen.edu / kfikani@digipen.edu
	date		24/05/2011 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.display.DisplayObject;
	import flash.geom.Point;
	
	final internal class Particle extends GameObject
	{
		private var  particleinfo:ParticleInfo
		private var nLifeTime:Number;
		private var nDeltaOpacity:Number;
		private var pDeltaScale:Point;
		public var bShouldReset:Boolean;
	
		public function Particle(displayobject_:DisplayObject, particleinfo_:ParticleInfo, iCollisionType_:int = 0, iID_:int = 14 )
		{
			super(displayobject_, 0 , 0, iID_, iCollisionType_);
			
			particleinfo = particleinfo_;
			EnablePhysics();
			pDeltaScale = new Point();
		}

		final override public function Initialize():void
		{
			bIsDead = false;
			bShouldReset = false;

			nLifeTime = HelperFunctions.GetRandom(particleinfo.nLowerLifetime, particleinfo.nUpperLifetime);
			
			var nIterations:Number = nLifeTime / PhysicsManager.DT;
			
			var nStartOpacity:Number = HelperFunctions.GetRandom(particleinfo.nLowerStartOpacity, particleinfo.nUpperStartOpacity);
			var nEndOpacity:Number = HelperFunctions.GetRandom(particleinfo.nLowerEndOpacity, particleinfo.nUpperEndOpacity);
			nDeltaOpacity = (nStartOpacity - nEndOpacity) / nIterations;
			displayobject.alpha = nStartOpacity;
			
			var pStartScale:Point = new Point();
			pStartScale.x = HelperFunctions.GetRandom(particleinfo.nLowerStartScaleX, particleinfo.nUpperStartScaleX);
			pStartScale.y = HelperFunctions.GetRandom(particleinfo.nLowerStartScaleY, particleinfo.nUpperStartScaleY);
			var pEndScale:Point = new Point();
			pEndScale.x = HelperFunctions.GetRandom(particleinfo.nLowerEndScaleX, particleinfo.nUpperEndScaleX);
			pEndScale.y = HelperFunctions.GetRandom(particleinfo.nLowerEndScaleY, particleinfo.nUpperEndScaleY);
	
			pDeltaScale.x = (pStartScale.x - pEndScale.x) / nIterations;
			pDeltaScale.y = (pStartScale.y - pEndScale.y) / nIterations;
			
			displayobject.scaleX = pStartScale.x;
			displayobject.scaleY = pStartScale.y;
		
			physicsinfo.pVelocityDirection.x = 0;
			physicsinfo.pVelocityDirection.y = 0;
			physicsinfo.nVelocityMagnitude = 0;
			physicsinfo.RemoveAllForces();
			
			physicsinfo.nMass = particleinfo.nMass;
		}
		
		final override public function Update():void
		{ 
			nLifeTime -= PhysicsManager.DT;
			
			displayobject.alpha -= nDeltaOpacity;
			displayobject.scaleX -= pDeltaScale.x;
			displayobject.scaleY -= pDeltaScale.y;
			
			if(bShouldReset == true)
			{
				bIsDead = true;
			}
			
			if(nLifeTime <= 0)
			{
				bShouldReset = true;
			}
		}
		
		final override public function Destroy():void
		{
			super.Destroy();
			particleinfo = null;
		}
	}
}