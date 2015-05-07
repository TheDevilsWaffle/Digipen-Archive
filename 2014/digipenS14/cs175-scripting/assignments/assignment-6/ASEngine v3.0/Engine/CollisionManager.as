/***************************************************************************************/
/*
	filename   	CollisionManager.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu, eabichahine@digipen.edu
	date		04/01/2014 
	
	brief:
	This class is the collision library. It contains different methods that will help the
	object manager and the user check and react to collisions.
*/        	 
/***************************************************************************************/
package Engine
{
	
	final internal class CollisionManager
	{
		//vector used to store objects that have collided
		static private var vCollidedObjectsInfo:Vector.<CollisionInfo>;
		
		/**************************************************************************
		/*
			Description:
				Constructor that initializes the vCollidedObjectsInfo vector
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/
		static internal function Initialize():void
		{
			//initialize the collision info vector
			vCollidedObjectsInfo = new Vector.<CollisionInfo>;
		}
		/**************************************************************************
		/*
			Description:
				Function that checks the contents of the passed vObjects_ vector
				to see if objects in it have collided. Once collided objects are
				found it stores them in an instance of the CollisionInfo class.
			
			Parameters:
				- vObjects_:Vector - vector that has dynamic objects in it
				
			Return:
				- None
		*/
		/*************************************************************************/
		static internal function CheckDynamicCollision(vObjects_:Vector.<GameObject>):void
		{
			//search through vDynamicObjects in first loop
			for(var iIndex1:int = 0; iIndex1 < vObjects_.length; ++iIndex1)
			{
				//search through vDynamicObjects in second loop at next spot in vector
				for(var iIndex2:int = iIndex1 + 1; iIndex2 < vObjects_.length; ++iIndex2)
				{
					//if iIndex1 hits an object that is iIndex2
					if(vObjects_[iIndex1].displayobject.hitTestObject(vObjects_[iIndex2].displayobject))
					{
						//create class instances
						var cInfo1:CollisionInfo = new CollisionInfo();
						var cInfo2:CollisionInfo = new CollisionInfo();
						
						//assign cInfo1's variables with the objects in vDynamicObjects
						cInfo1.gameobject = vObjects_[iIndex1];
					    cInfo1.gameobjectCollidedWith = vObjects_[iIndex2];
					   
					    //push cInfo1 into vCollidedObjectsInfo
					    vCollidedObjectsInfo.push(cInfo1);
					   
					    //assign cInfo2's variables with the swapped objects in vDynamicObjects
					    cInfo2.gameobject = vObjects_[iIndex2];
					    cInfo2.gameobjectCollidedWith = vObjects_[iIndex1];
					   
					    //push cInfo2 into vCollidedObjectsInfo
					    vCollidedObjectsInfo.push(cInfo2);
					}
				}
			}
		}
		/**************************************************************************
		/*
			Description:
				Function that loops through the vCollidedObjectsInfo vector to
				call each of these object's CollisionReaction() and passing the
				object it collided with.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/
		static internal function CollidedObjectsReaction():void
		{
			//loop through the collision infos and call the gameobject's collision reaction, give it the object it collided
			//with.
			//search through vCollidedObjectsInfo
			for(var iIndex:int = 0; iIndex < vCollidedObjectsInfo.length; ++iIndex)
			{
				//call the gameobject's CollisionReaction() and give it gameObjectCollidedWith
				vCollidedObjectsInfo[iIndex].gameobject.CollisionReaction(vCollidedObjectsInfo[iIndex]);

			}
			//clean out the vector by splicing stuff out
			vCollidedObjectsInfo.splice(0, vCollidedObjectsInfo.length);
		}
		/**************************************************************************
		/*
			Description:
				Function that destroys the vCollidedObjectsInfo vector because it
				is no longer needed.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/
		static internal function Destroy()
		{
			//set vCollidedObjectsInfo vector to null
			vCollidedObjectsInfo = null;
		}
	}
}