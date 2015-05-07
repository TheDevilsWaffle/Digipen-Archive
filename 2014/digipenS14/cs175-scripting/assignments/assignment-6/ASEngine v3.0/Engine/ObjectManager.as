/***************************************************************************************/
/*
	filename   	ObjectManager.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu, eabichahine@digipen.edu
	date		04/01/2014 
	
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
			sStage = sStage_;
			vStaticObjects = new Vector.<GameObject>();
			vDynamicObjects = new Vector.<GameObject>();
		}

		/*******************************************************************************/
		/*
			Description: 
				This method adds a GameObject to one of the object manager's vectors 
				depending on the type passed. 
				EngineGlobals.OM_STATICOBJECT as type will add the object to the 
				vStaticObjects vector. 
				EngineGlobals.OM_DYNAMICOBJECT as type will add the object to the 
				vDynamicObjects vector. The object will also be added to the stage.
		
			Parameters:
				- gameobject_: Passing a game object to be added to the object manager
			 	- sName_: Assigning a name to the game object
				- iOMType_: specifies if it's a static object (EngineGlobals.OM_STATICOBJECT)
							or a dynamic object (EngineGlobals.OM_DYNAMICOBJECT)
						 	If any other type is specified, "Invalid Object Type!!!" 
							will be traced.
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static public function AddObject(gameobject_:GameObject , sName_:String , iOMType_:int):void
		{
			if(gameobject_)
			{
				gameobject_.displayobject.name = sName_;
				gameobject_.Initialize();
				switch(iOMType_)
				{					
					case OM_STATICOBJECT:
					{
						vStaticObjects.push(gameobject_);
						sStage.addChild(gameobject_.displayobject);
					}
					break;
					
					case OM_DYNAMICOBJECT:
					{
						vDynamicObjects.push(gameobject_);
						sStage.addChild(gameobject_.displayobject);
					}
					break;
					
					default:
					{
						trace("Invalid Object Type!!!");
					}
				}
			}
		}

		/*******************************************************************************/
		/*
			Description: 
				This method loops through a vector of objects and calls their
				Initialize function.
		
			Parameters:
				- vObjects_: Vector of objects to be initialized.
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static private function InitializeObjectsInVector(vObjects_:Vector.<GameObject>):void
		{
			var uiObjects_Length:uint = vObjects_.length;
			for(var i:uint = 0; i < uiObjects_Length; ++i)
			{
				vObjects_[i].Initialize();
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
			InitializeObjectsInVector(vStaticObjects);
			InitializeObjectsInVector(vDynamicObjects);
		}

		/*******************************************************************************/
		/*
			Description: 
				This method updates all objects by calling their Update function, checks
				collision calls the CollidedObjectsReaction on all collided objects.
		
			Parameters:
				- None 
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static internal function Update():void
		{
			var uiStaticObjectsLength:uint = vStaticObjects.length;
			for(var i:uint = 0; i < uiStaticObjectsLength; ++i)
			{
				vStaticObjects[i].Update();
			}
			
			if(vDynamicObjects.length)
			{
				var uiDynamicObjectsLength:uint = vDynamicObjects.length;
				for(i = 0; i < uiDynamicObjectsLength; ++i)
				{
					vDynamicObjects[i].Update();
				}

				CollisionManager.CheckDynamicCollision(vDynamicObjects);
				CollisionManager.CollidedObjectsReaction();
			}
		}

		/*******************************************************************************/
		/*
			Description: 
				This methods returns the right vector depending on the sent type.
		
			Parameters:
				- iOMType_: specifies which vector you want returned.
						    EngineGlobals.OM_STATICOBJECT returns the vStaticObjects 
							vector.
						    EngineGlobals.OM_DYNAMICOBJECT returns the vDynamicObjects 
							vector.
		
		    Return: 
				- Returns the appropriate vector. If the type is invalid, null is 
				  returned.
		*/
		/*******************************************************************************/
		static private function GetVectorFromType(iOMType_:int):Vector.<GameObject>
		{
			switch(iOMType_)
			{
				case OM_STATICOBJECT:
				{
					return vStaticObjects;
				}
				break;
				
				case OM_DYNAMICOBJECT:
				{
					return vDynamicObjects;
				}
				break;
				
				default:
				{
					return null;
				}
				break;
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
						    EngineGlobals.OM_STATICOBJECT searches in the vStaticObjects
							vector.
						    EngineGlobals.OM_DYNAMICOBJECT searches in the 
							vDynamicObjects vector. 
		
		    Return: 
				- The GameObject found. If the object was not found, null is returned.
		*/
		/*******************************************************************************/
		static public function GetObjectByName(sName_:String , iOMType_:int):GameObject
		{
			var vSearchVector:Vector.<GameObject> = GetVectorFromType(iOMType_);

			if(vSearchVector)
			{
				var uiSearchVectorLength:uint = vSearchVector.length;
				for(var i:uint = 0; i < uiSearchVectorLength; ++i)
				{
					if(vSearchVector[i].displayobject.name == sName_)
					{
						return vSearchVector[i];
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
				removed from. The first object that is found, having the name, 
				will be removed. If the object was correctly removed true is 
				returned. If a wrong type or the object wasn't found the method 
				returns false.
		
			Parameters:
				- sName_: the object's name you want to be removed.
				- iOMType_: specifies which vector to remove the object from.
						    EngineGlobals.OM_STATICOBJECT removes the object from
							the vStaticObjects vector
						    EngineGlobals.OM_DYNAMICOBJECT removes the object from
							the vDynamicObjects vector
		
		    Return: 
				- Returns true if the object got removed, false otherwise
		*/
		/*******************************************************************************/
		static public function RemoveObjectByName(sName_:String , iOMType_:int):Boolean
		{
			var vSearchVector:Vector.<GameObject> = GetVectorFromType(iOMType_);
			
			if(vSearchVector)
			{
				var uiSearchVectorLength:uint = vSearchVector.length;
				for(var i:uint = 0; i < uiSearchVectorLength; ++i)
				{
					if(vSearchVector[i].displayobject.name == sName_)
					{
						vSearchVector[i].bIsDead = true;
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
				removed.
		
			Parameters:
				- sName_: the object's name you want to be removed.
				- iOMType_: specifies which vector you want to remove the object from.
						    EngineGlobals.OM_STATICOBJECT removes the object from the 
						    vStaticObjects vector
						    EngineGlobals.OM_DYNAMICOBJECT removes the object from the 
						    vDynamicObjects vector
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static public function RemoveAllObjectsByName(sName_:String , iOMType_:int):void
		{
			var vSearchVector:Vector.<GameObject> = GetVectorFromType(iOMType_);
			
			if(vSearchVector)
			{
				var uiSearchVectorLength:uint = vSearchVector.length;
				for(var i:uint=0; i < uiSearchVectorLength; ++i)
				{
					if(vSearchVector[i].displayobject.name == sName_)
					{
						vSearchVector[i].bIsDead = true;
					}
				}
			}
		}

		/*******************************************************************************/
		/*
			Description: 
				This method removes all dead object from a vector of objects. Objects
				will be removed from the stage, destroyed, nulled and removed from the
				vector of objects.
		
			Parameters:
				- vObjects_: The vector of objects
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static private function RemoveDeadObjectsFromVector(vObjects_:Vector.<GameObject>):void
		{
			for(var i:uint=0; i < vObjects_.length; ++i)
			{
				if( vObjects_[i].bIsDead == true)
				{
					sStage.removeChild(vObjects_[i].displayobject);
					vObjects_[i].Destroy();
					delete vObjects_[i];
					vObjects_.splice(i,1);
					--i;
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
			RemoveDeadObjectsFromVector(vStaticObjects);
			RemoveDeadObjectsFromVector(vDynamicObjects);
		}

		/*******************************************************************************/
		/*
			Description: 
				This method removes and destroys all objects inside a vector.
				It can only be used with vectors filled with GameObjects
		
			Parameters:
				- vObjects_: The vector of objects
		
		    Return: 
				- None
		*/
		/*******************************************************************************/
		static private function DestroyVector(vObjects_:Vector.<GameObject>):void
		{
			var uiObjects_Length:uint = vObjects_.length;
			for(var i:uint = 0; i < uiObjects_Length; ++i)
			{
				sStage.removeChild(vObjects_[i].displayobject);
				vObjects_[i].Destroy();
				delete vObjects_[i];
			}
			vObjects_.length = 0;
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
			DestroyVector(vDynamicObjects);
			DestroyVector(vStaticObjects);
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
			DestroyAllData();
			
			vDynamicObjects = null;
			vStaticObjects = null;
		}
	}
}