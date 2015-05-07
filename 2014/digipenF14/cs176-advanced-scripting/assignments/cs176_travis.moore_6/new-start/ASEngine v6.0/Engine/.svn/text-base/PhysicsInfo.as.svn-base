/***************************************************************************************/
/*
	filename   	PhysicsInfo.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		10/16/2011 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.geom.Point;

	public class PhysicsInfo
	{
		/* The object's mass */
		public var nMass:Number;
		/* The object's velocity */
		public var pVelocityDirection:Point;
		public var nVelocityMagnitude:Number;
		/* A vector that contains all the forces */
		public var vForces:Vector.<Force>;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor that initializes the PhysicsInfo's class variables.
				Make sure that pVelocityDirection is normalized.
			
			Parameters:
				- pVelocityDirection_: The object's velocity direction. Make sure
									   the velocity direction is normalized before 
									   storing it in the object.
				- nVelocityMagnitude_: represents the velocity magnitude (a.k.a speed)
				- nMass_: represents the object's mass value
							
			Return:
				- None
		*/
		/*******************************************************************************/
		public function PhysicsInfo(pVelocityDirection_:Point, nVelocityMagnitude_:Number = 0,
									nMass_:Number = 1)
		{
			nMass = nMass_;
			pVelocityDirection = pVelocityDirection_;
			pVelocityDirection.normalize(1);
			nVelocityMagnitude = nVelocityMagnitude_;
			vForces = new Vector.<Force>();
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method adds a force to the vector of forces.
				if nMagnitude_ is set to -1, then the magnitude will be the length of the
				pDirection_ vector.
			
			Parameters:
				- nTime_: represents how long the force will stay alive.
				- pDirection_: represents the direction of the force
				- nMagnitude_: represents the magnitude of the force
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function AddForce(nTime_:Number, pDirection_:Point, nMagnitude_:Number = -1):void
		{
			if(nMagnitude_ == -1)
			{
				nMagnitude_ = pDirection_.length;
			}
			pDirection_.normalize(1);
			vForces.push(new Force(pDirection_, nMagnitude_, nTime_));
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method removes all the forces from the vForces vector.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function RemoveAllForces():void
		{
			var uiForcesLength:uint = vForces.length;
			for(var f:uint=0; f < uiForcesLength; ++f)
			{
				vForces[f] = null;
			}
			vForces.length = 0;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This methods is responsible to cleanly destroy a physics info instance.
			
			Parameters:
				- none
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Destroy():void
		{
			RemoveAllForces();
			vForces = null;
		}
	}

}