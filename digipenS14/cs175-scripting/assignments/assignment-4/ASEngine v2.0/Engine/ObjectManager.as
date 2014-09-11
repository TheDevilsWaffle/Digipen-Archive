/***************************************************************************************/
/*
	filename   	ObjectManager.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu, eabichahine@digipen.edu
	date    	03/04/2014 
	
	brief:
	The Object Manager handles all objects found in the current state. 
	It's job is to Add/Update the objects (animation, send them for collision check ... 
	and a lot more later).
*/        	 
/***************************************************************************************/
package Engine
{
	import flash.display.Stage;
	
	final public class ObjectManager
	{
		static public const OM_STATICOBJECT:int = 1;
		static public const OM_DYNAMICOBJECT:int = 2;
		
		/* A reference to the main stage */
		static private var sStage:Stage
		/* An vector used to store all the static objects in the scene. */
		static private var vStaticObjects:Vector.<GameObject>;
		/* An vector used to store all the dynamic objects in the scene. */
		static private var vDynamicObjects:Vector.<GameObject>;
		
		//add a helper function
		//loop through both vectors and run the update functions

		/*******************************************************************************/
		/*
			Description: 
				Function that initializes the object manager's variables. It is only 
				called once when initializing all managers.
		
			Parameters:
				- sStage_: A reference to the stage 
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static internal function Initialize(sStage_:Stage)
		{
			//stage goes here, create vStaticObjects and vDynamicObject vectors
			vStaticObjects = new Vector.<GameObject>();
			vDynamicObjects = new Vector.<GameObject>();
			
			//assign the stage the passed parameter
			sStage = sStage_;
		}

		/*******************************************************************************/
		/*
			Description: 
				This method adds a GameObject to one of the object manager's vectors 
				depending on the type passed. 
				OM_STATICOBJECT as type will add the object to the vStaticObjects 
				vector. 
				OM_DYNAMICOBJECT as type will add the object to the vDynamicObjects 
				vector. The object will also be added to the stage.
		
			Parameters:
				- gameobject_: Passing a game object to be added to the object manager
			 	- sName_: Assigning a name to the game object
				- iOMType_: specifies if it's a static object (OM_STATICOBJECT)
							or a dynamic object (OM_DYNAMICOBJECT)
						 	If any other type is specified, "Invalid Object Type!!!" 
							will be traced.
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static public function AddObject(gameobject_:GameObject , sName_:String , iOMType_:int):void
		{
			//if passed object is static == 1
			if(iOMType_ == OM_STATICOBJECT)
			{
				//put the object in vDynamicObjects
				vStaticObjects.push(gameobject_);
				//add the name to the object
				vStaticObjects[vStaticObjects.length -1].displayobject.name = sName_;
				//add to the stage
				sStage.addChild(vStaticObjects[vStaticObjects.length - 1].displayobject);
			}
			//if passed object is dynamic == 0
			else if(iOMType_ == OM_DYNAMICOBJECT)
			{
				//put the object in vDynamicObjects
				vDynamicObjects.push(gameobject_);
				//add the name to the object
				vDynamicObjects[vDynamicObjects.length -1].displayobject.name = sName_;
				//add to the stage
				sStage.addChild(vDynamicObjects[vDynamicObjects.length - 1].displayobject);
			}
			else
			{
				trace("Invalid Object Type!!!");
			}
		}

		/*******************************************************************************/
		/*
			Description: 
				This methods loops through all the objects and calls their 
				Initialize function. It is mainly used when restarting a state.
		
			Parameters:
				- None
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static internal function InitializeAllObject():void
		{
			//loops through all objects in vStaticObjects
			for(var iIndex:int = 0; iIndex < vStaticObjects.length; ++iIndex)
			{
				//initialize object at index location
				vStaticObjects[iIndex].Initialize();
			}
			//reset iIndex and loops through all objects in vDynamicObjects
			for(iIndex = 0; iIndex < vDynamicObjects.length; ++iIndex)
			{
				//initialize object at index location
				vDynamicObjects[iIndex].Initialize();
			}
		}

		/*******************************************************************************/
		/*
			Description: 
				This method updates all objects by calling their Update function, and 
				checks collision amongs objects by calling the CheckCollision method. 
		
			Parameters:
				- None 
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static internal function Update():void
		{
			//loops through all objects in vStaticObjects
			for(var iIndex:int = 0; iIndex < vStaticObjects.length; ++iIndex)
			{
				//initialize object at index location
				vStaticObjects[iIndex].Update();
			}
			//reset iIndex and loops through all objects in vDynamicObjects
			for(iIndex = 0; iIndex < vDynamicObjects.length; ++iIndex)
			{
				vDynamicObjects[iIndex].Update();
			}
			CheckCollision();
		}

		/*******************************************************************************/
		/*
			Description: 
				This method checks collision among all dynamic objects. If collision 
				happens between two game objects, the collision reaction function is 
				called for both of them.
		
			Parameters:
				- None
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static private function CheckCollision():void
		{
			/* STUDENT CODE GOES HERE */
			//check collision amongst all dynamic objects
			//call both of the objects .CollisionReaction(iCollidedWithObjectType_:int) functions (in each object)
			/*example
			if collision is found
			{
				object1.CollisionReaction()
				object2.CollisionReaction()
			}*/
			//reset iIndex and loops through all objects in vDynamicObjects
			for(var iIndex:int = 0; iIndex < vDynamicObjects.length; ++iIndex)
			{
				for(var iIndex2:int = iIndex + 1; iIndex2 < vDynamicObjects.length; ++iIndex2)
				{
					if(vDynamicObjects[iIndex].displayobject.hitTestObject(vDynamicObjects[iIndex2].displayobject))
					{
						vDynamicObjects[iIndex].CollisionReaction(vDynamicObjects[iIndex2].iType);
						vDynamicObjects[iIndex2].CollisionReaction(vDynamicObjects[iIndex].iType);
					}
				}
			}
		}
		
		/*******************************************************************************/
		/*
			Description: 
				This method searches for then returns an object from it's name. 
				The user specifies the name of the object he wants and it's
				type in order to know in which vector it needs to be found in.
				The first object found with the correct name is returned. 
				If no object is found or a wrong type is sent, the function will
				return null.
		
			Parameters:
				- sName_: Object's name
				- iOMType_: specifies which vector you want to search in.
						    OM_STATICOBJECT searches in the vStaticObjects vector.
						    OM_DYNAMICOBJECT searches in the vDynamicObjects vector. 
		
		    Return: 
				- The GameObject found. If the object was not found, null is returned.
		*/
		/*******************************************************************************/
		static public function GetObjectByName(sName_:String , iOMType_:int):GameObject
		{
			//give it the object and what "list" it is found in (makes the game a little bit faster)
			//find object by name in the list it is found in.
			if(iOMType_ == OM_STATICOBJECT)
			{
				//loops through all objects in vStaticObjects
				for(var iIndex:int = 0; iIndex < vStaticObjects.length; ++iIndex)
				{
					//if sName_ is equal to an objects name in here
					if(sName_ == vStaticObjects[iIndex].displayobject.name)
					{
						//return the object
						return vStaticObjects[iIndex];
					}
				} 
			}
			if(iOMType_ == OM_DYNAMICOBJECT)
			{
				//loops through all objects in vDynamicObjects
				for(var iIndex2:int = 0; iIndex2 < vDynamicObjects.length; ++iIndex2)
				{
					//if sName_ is equal to an objects name in here
					if(sName_ == vDynamicObjects[iIndex2].displayobject.name)
					{
						//return the object
						return vDynamicObjects[iIndex2];
					}
				} 
			}
			return null;
		}

		/*******************************************************************************/
		/*
			Description: 
				This method removes the object from it's name.
				The user specifies the name of the object that he wants to remove 
				and it's type in order to know in which vector it needs to be 
				removed from. The first object that is found having the name, 
				will be removed (by setting its bIsDead to true). If the object was 
				correctly removed true is returned. If a wrong type or the object wasn't
				found the method returns false.
		
			Parameters:
				- sName_: the object's name you want to be removed.
				- iOMType_: specifies which vector to remove the object from.
						    OM_STATICOBJECT removes the object from the vStaticObjects 
							vector
						    OM_DYNAMICOBJECT removes the object from the vDynamicObjects
							vector
		
		    Return: 
				- Returns true if the object got removed, false otherwise
		*/
		/*******************************************************************************/
		static public function RemoveObjectByName(sName_:String , iOMType_:int):Boolean
		{
			//find object by name in the list it is found in.
			if(iOMType_ == OM_STATICOBJECT)
			{
				//loops through all objects in vStaticObjects
				for(var iIndex:int = 0; iIndex < vStaticObjects.length; ++iIndex)
				{
					//if sName_ is equal to an objects name in here
					if(sName_ == vStaticObjects[iIndex].displayobject.name)
					{
						//set bIsDead to true
						vStaticObjects[iIndex].bIsDead = true;
						//return the object
						return true;
					}
				} 
			}
			if(iOMType_ == OM_DYNAMICOBJECT)
			{
				//loops through all objects in vDynamicObjects
				for(var iIndex2:int = 0; iIndex2 < vDynamicObjects.length; ++iIndex2)
				{
					//if sName_ is equal to an objects name in here
					if(sName_ == vDynamicObjects[iIndex2].displayobject.name)
					{
						//set bIsDead to true
						vDynamicObjects[iIndex2].bIsDead = true;
						//return the object
						return true;
					}
				} 
			}
			
			return false;
		}

		/*******************************************************************************/
		/*
			Description: 
				This method removes all objects having a certain name.
				The user specifies the name of the objects that he wants to 
				remove and it's type in order to know in which vector it needs to
				be removed from. All objects found having that name will be 
				removed (by setting their bIsDead to true).
		
			Parameters:
				- sName_: the object's name you want to be removed.
				- iOMType_: specifies which vector you want to remove the object from.
						    OM_STATICOBJECT removes the object from the vStaticObjects 
							vector
						    OM_DYNAMICOBJECT removes the object from the vDynamicObjects
							vector
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static public function RemoveAllObjectsByName(sName_:String , iOMType_:int):void
		{
			//find object by name in the list it is found in.
			if(iOMType_ == OM_STATICOBJECT)
			{
				//loops through all objects in vStaticObjects
				for(var iIndex:int = 0; iIndex < vStaticObjects.length; ++iIndex)
				{
					//if sName_ is equal to an objects name in here
					if(sName_ == vStaticObjects[iIndex].displayobject.name)
					{
						//set bIsDead to true
						vStaticObjects[iIndex].bIsDead = true;
					}
				} 
			}
			if(iOMType_ == OM_DYNAMICOBJECT)
			{
				//loops through all objects in vDynamicObjects
				for(var iIndex2:int = 0; iIndex2 < vDynamicObjects.length; ++iIndex2)
				{
					//if sName_ is equal to an objects name in here
					if(sName_ == vDynamicObjects[iIndex2].displayobject.name)
					{
						//set bIsDead to true
						vDynamicObjects[iIndex2].bIsDead = true;
					}
				} 
			}
		}

		/*******************************************************************************/
		/*
			Description: 
				This method loops through all the object's vectors and removes all dead 
				objects. The method is called at the end of every frame. Dead objects
				will be removed from the stage, destroyed, nulled and removed from the
				vector of objects.
		
			Parameters:
				- None
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static internal function RemoveAllDeadObjects():void
		{
			//loops through all objects in vStaticObjects
			for(var iIndex:int = 0; iIndex < vStaticObjects.length; ++iIndex)
			{
				//if object is dead
				if(vStaticObjects[iIndex].bIsDead)
				{
					//remove object from the stage
					sStage.removeChild(vStaticObjects[iIndex].displayobject);
					//destroy the object
					vStaticObjects[iIndex].Destroy();
					//nullify it
					vStaticObjects[iIndex] = null;
					//splice it
					vStaticObjects.splice(iIndex, 1);
				}
			} 

			//loops through all objects in vDynamicObjects
			for(var iIndex2:int = 0; iIndex2 < vDynamicObjects.length; ++iIndex2)
			{
				//if object is dead
				if(vDynamicObjects[iIndex2].bIsDead)
				{
					//remove object from the stage
					sStage.removeChild(vDynamicObjects[iIndex2].displayobject);
					//destroy the object
					vDynamicObjects[iIndex2].Destroy();
					//nullify it
					vDynamicObjects[iIndex2] = null;
					//splice it
					vDynamicObjects.splice(iIndex2, 1);
				}
			} 
		}
		
		/*******************************************************************************/
		/*
			Description: 
				This method destroys all the objects in the object manager and empties
				the static/dynamic vectors. Objects don't have to be dead for them to be
				fully removed and destroyed. This function will be called when fully 
				destroying a state or quitting the game.
		
			Parameters:
				- None 
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static internal function DestroyAllData():void
		{
			//loops through all objects in vStaticObjects
			for(var iIndex:int = 0; iIndex < vStaticObjects.length; ++iIndex)
			{
				//set bIsDead to true
				vStaticObjects[iIndex].bIsDead = true;
			} 

			//loops through all objects in vDynamicObjects
			for(iIndex = 0; iIndex < vDynamicObjects.length; ++iIndex)
			{
				//set bIsDead to true
				vDynamicObjects[iIndex].bIsDead = true;
			}

		}

		/*******************************************************************************/
		/*
			Description: 
				This method destroys the object manager by destroying all the objects 
				and setting all the variables to null.
		
			Parameters:
				- None 
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static internal function Destroy():void
		{
			//set vStaticObjects to null
			vStaticObjects = null;
			
			//set vDynamicObjects to null
			vDynamicObjects = null;
			
			//take away the stage
			sStage = null;

		}
	}
}