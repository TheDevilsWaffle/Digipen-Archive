/***************************************************************************************/
/*
	filename   	PhysicsManager.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		10/27/2014 
	
	brief:
	The PhysicsManager is reponsible for applying forces on all objects, including global
	forces if there are any to apply.
*/        	 
/***************************************************************************************/
package Engine
{
	import flash.geom.Point;

	final public class PhysicsManager
	{
		static public const DT:Number = 1/30;
		/* A vector that contains all the global forces—applied to all objects*/
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
		static public function AddGlobalForce(nTime_:Number, 
											  pDirection_:Point, 
											  nMagnitude_:Number = -1):void
		{
			//edge case
			if(nMagnitude_ == -1)
			{
				nMagnitude_ = pDirection_.length;
			}
			
			//creates a force, set things, add to vGlobalForces
			var fNewForce = new Force(pDirection_,
									  nMagnitude_,
									  nTime_);
			
			vGlobalForces.push(fNewForce);
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
			//vector of objects, loop through vector, call IsPhysicsOn() and it will return true or false
			//true? do things - compute the sum of local forces, get the sum of global forces, add local forces + global forces to get all forces, get velocity, update the position
			//false, don't do things.
			//optimize get global forces (if there)->[loop through vector of objects->check if physics->apply local forces] -> now apply the global+local forces->direction * magnitude on x and y->normalize
			//sf.x = f1.x * magf1 + f2.x * magf2 and same goes for the y
			//a = sum of forces(x)/mass and the same goes for the y
			//vnew = a*dt+v0(previous velocity) <-- store this in an object, next frame you need to use it again.
			//newPosition = vnew * dt + p0 <-- store this in an object, next frame you need to use it again.
			var pGlobalForces:Point = new Point();
			var pLocalForces:Point = new Point();
			
			//loop through global objects
			for(var iGIndex:int = 0; iGIndex < vGlobalForces.length; ++iGIndex)
			{
				//sum of forces for x and y
				pGlobalForces.x += (vGlobalForces[iGIndex].pDirection.x * vGlobalForces[iGIndex].nMagnitude);
				pGlobalForces.y += (vGlobalForces[iGIndex].pDirection.y * vGlobalForces[iGIndex].nMagnitude);
				
				//subtract DT from duration
				vGlobalForces[iGIndex].nTime -= PhysicsManager.DT;
				
				//if duration is less than infinity
				if(vGlobalForces[iGIndex].nTime <= 0)
				{
					vGlobalForces.splice(iGIndex, 1);
					--iGIndex;
				}
			}
			
			//loop throuh objects
			for(var iOIndex:int = 0; iOIndex < vObjects_.length; ++iOIndex)
			{
				//if they have physics
				if(vObjects_[iOIndex].IsPhysicsOn())
				{
					//loop through this object's vector of forces
					for(var iFIndex:int = 0; iFIndex < vObjects_[iOIndex].physicsinfo.vForces.length; ++iFIndex)
					{
						//standin variable for something very long
						var vForces:Vector.<Force> = vObjects_[iOIndex].physicsinfo.vForces;
						
						//sum for forces for x and y
						pLocalForces.x += vForces[iFIndex].pDirection.x * vForces[iFIndex].nMagnitude;
						pLocalForces.y += vForces[iFIndex].pDirection.y * vForces[iFIndex].nMagnitude;
						
						//subtract DT from duration
						vForces[iFIndex].nTime -= PhysicsManager.DT;
						//if the duration is less than infinity
						if(vForces[iFIndex].nTime <= 0)
						{
							vForces.splice(iFIndex, 1);
							--iFIndex;
						}
					}
					
					//variables to reduce size of lines
					var object = vObjects_[iOIndex];
					var objPhysics = vObjects_[iOIndex].physicsinfo;
					
					//get total forces (local forces + global forces)
					var pTotalForces:Point = new Point(pLocalForces.x + pGlobalForces.x,
											 		   pLocalForces.y + pGlobalForces.y);
					
					//acceleration for x and y
					var pAcceleration:Point = new Point(pTotalForces.x / objPhysics.nMass,
														pTotalForces.y / objPhysics.nMass);
					
					//velocity for x and y
					var pVelocity:Point = new Point(pAcceleration.x * DT + objPhysics.pVelocityDirection.x * objPhysics.nVelocityMagnitude,
													pAcceleration.y * DT + objPhysics.pVelocityDirection.y * objPhysics.nVelocityMagnitude);
					
					//compute the new position
					object.displayobject.x = pVelocity.x * DT + object.displayobject.x;
					object.displayobject.y = pVelocity.y * DT + object.displayobject.y;
					
					//save old pVelocityDirection for next frame
					objPhysics.pVelocityDirection.x = pVelocity.x;
					objPhysics.pVelocityDirection.y = pVelocity.y; 
					//save old nVelocityMagnitude
					objPhysics.nVelocityMagnitude = pVelocity.length;
					
					//normalize
					objPhysics.pVelocityDirection.normalize(1);
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
			//loop through vGlobalForces, empty the vector 
			for(var iIndex:int = 0; iIndex < vGlobalForces.length; ++iIndex)
			{
				vGlobalForces[iIndex] = null;
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
			//Destroy!
			RemoveAllGlobalForces();
			vGlobalForces = null;
		}
	}
}