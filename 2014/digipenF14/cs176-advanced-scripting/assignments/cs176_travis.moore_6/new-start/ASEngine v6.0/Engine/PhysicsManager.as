/***************************************************************************************/
/*
	filename   	PhysicsManager.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		10/16/2011 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.geom.Point;

	final public class PhysicsManager
	{
		static public const DT:Number = 1/30;
		/* A vector that contains all the global forces */
		static private var vGlobalForces:Vector.<Force>;
		
		/*******************************************************************************/
		/*
			Description:
				This method initializes the physics manager by creating the vector that
				will hold all global forces.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Initialize():void
		{
			vGlobalForces = new Vector.<Force>();
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method adds a global force to the vector of global forces.
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
		static public function AddGlobalForce(nTime_:Number, pDirection_:Point, nMagnitude_:Number = -1):void
		{
			if(nMagnitude_ == -1)
			{
				nMagnitude_ = pDirection_.length;
			}
			pDirection_.normalize(1);
			vGlobalForces.push(new Force(pDirection_, nMagnitude_, nTime_));
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method will go through every physics object and computes it's new
				position according to all the forces applied on that object.
			
			Parameters:
				- vObjects_: A vector containing all the dynamic game objects
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function ApplyPhysicsOnObjects(vObjects_:Vector.<GameObject>):void
		{
			if(vObjects_)
			{
				var pSumOfGlobalForces:Point = new Point(0,0);
				
				var uiGlobalForcesLength:uint = vGlobalForces.length;
				for(var f:uint = 0; f < uiGlobalForcesLength; ++f )
				{
					
					pSumOfGlobalForces.x += vGlobalForces[f].pDirection.x * vGlobalForces[f].nMagnitude;
					pSumOfGlobalForces.y += vGlobalForces[f].pDirection.y * vGlobalForces[f].nMagnitude;
					
					if(int(vGlobalForces[f].nTime) != Infinity)
					{
						vGlobalForces[f].nTime -= DT;
						if(vGlobalForces[f].nTime <= 0 )
						{
							vGlobalForces.splice(f,1);
							--f;
						}
					}
				}
				
				var pSumOfForces:Point = new Point(0,0);
				var pAcceleration:Point = new Point(0,0);
				
				var uivObjectsLength:uint = vObjects_.length;
				for(var i:uint = 0; i < uivObjectsLength; ++i)
				{
					if(vObjects_[i].IsPhysicsOn())
					{
						pSumOfForces.x = 0;
						pSumOfForces.y = 0;
						
						pAcceleration.x = 0;
						pAcceleration.y = 0;
						
						for(f = 0; f < vObjects_[i].physicsinfo.vForces.length; ++f )
						{
							
							pSumOfForces.x += vObjects_[i].physicsinfo.vForces[f].pDirection.x * vObjects_[i].physicsinfo.vForces[f].nMagnitude;
							pSumOfForces.y += vObjects_[i].physicsinfo.vForces[f].pDirection.y * vObjects_[i].physicsinfo.vForces[f].nMagnitude;
							
							if(int(vObjects_[i].physicsinfo.vForces[f].nTime) != Infinity)
							{
								
								vObjects_[i].physicsinfo.vForces[f].nTime -= DT;
								if(vObjects_[i].physicsinfo.vForces[f].nTime <= 0 )
								{
									vObjects_[i].physicsinfo.vForces.splice(f,1);
									--f;
								}
							}
						}						

						pSumOfForces.x += pSumOfGlobalForces.x;
						pSumOfForces.y += pSumOfGlobalForces.y;
						
						pAcceleration.x = pSumOfForces.x / vObjects_[i].physicsinfo.nMass;
						pAcceleration.y = pSumOfForces.y / vObjects_[i].physicsinfo.nMass;
						
						vObjects_[i].physicsinfo.pVelocityDirection.x = pAcceleration.x * DT + vObjects_[i].physicsinfo.pVelocityDirection.x * vObjects_[i].physicsinfo.nVelocityMagnitude;
						vObjects_[i].physicsinfo.pVelocityDirection.y = pAcceleration.y * DT + vObjects_[i].physicsinfo.pVelocityDirection.y * vObjects_[i].physicsinfo.nVelocityMagnitude;
						
						vObjects_[i].displayobject.x = vObjects_[i].physicsinfo.pVelocityDirection.x * DT + vObjects_[i].displayobject.x;
						vObjects_[i].displayobject.y = vObjects_[i].physicsinfo.pVelocityDirection.y * DT + vObjects_[i].displayobject.y;
						
						vObjects_[i].physicsinfo.nVelocityMagnitude = vObjects_[i].physicsinfo.pVelocityDirection.length;
						vObjects_[i].physicsinfo.pVelocityDirection.normalize(1);
					}
				}
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method removes all the global forces from the vGlobalForces vector.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function RemoveAllGlobalForces():void
		{
			var uiGlobalForcesLength:uint = vGlobalForces.length;
			for(var f:int=0; f < uiGlobalForcesLength; ++f)
			{
				vGlobalForces[f] = null;
			}
			vGlobalForces.length = 0;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method destroys all the memory allocated by the Physics Manager.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Destroy():void
		{
			RemoveAllGlobalForces();
			vGlobalForces = null;
		}
		
	}
}