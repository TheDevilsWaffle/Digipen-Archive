/***************************************************************************************/
/*
	filename   	GameObject.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		08/29/2013
	
	brief:
	The GameObject is the base class for all the game objects and it contains properties
	that could be used for all the derived classes. Derived classes should override the
	base class methods.
*/        	 
/***************************************************************************************/
package Engine
{
	import flash.display.DisplayObject;
	import flash.display.Shape;
	
	public class GameObject
	{
		/* This the GameObject's shape. It can be anything derived from the DisplayObject
		   class (Text, MovieClip, Sprite ...) */
		public var displayobject:DisplayObject;
		/* The game Object's type that will help the user differentiate between different 
		   game objects. Everytime the user derives a new class from the GameObject class
		   it is highly recommended to add a new GAMEOBJECT ID in the Globals file 
		   (GO_GAMEOBJECT, GO_DYNAMICGAMEOBJECT, GO_SHIP ...) */
		public var iType:int;
		/* Boolean that represents if the object is dead or not. When true, the 
		   ObjectManager will totally destroy the object at the end of the frame */
		public var bIsDead:Boolean;
		
		/*******************************************************************************/
		/*
			Description:
				This method is the Constructor. It is responsible for initializing
				all the GameObject's variables. The user specifies the displayobject
				he wants (sprite, movieclip, text ...) and where he wants it on the 
				screen (nPosX and nPosY). If null is sent for the displayobject, the 
				constructor will initialize it as a Shape instance.				
			
			Parameters:
				- displayobject_: can be any kind of display object or display container
				- nPosX_: The object's x position on the screen
				- nPosY_: The object's y position on the screen
				- iType__: the object's Type initially set to GO_GAMEOBJECT (0)
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function GameObject(displayobject_:DisplayObject, nPosX_:Number = 0,
								   nPosY_:Number = 0, iType_:int = 0)
		{
			if(displayobject_ == null)
			{
				displayobject = new Shape();
			}
			else
			{
				displayobject = displayobject_;
			}
			displayobject.x = nPosX_;
			displayobject.y = nPosY_;
			iType = iType_;
			bIsDead = false;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is used to initialize/re-initialize the object's variables. 
				It is used when adding a new object or restarting a level.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Initialize():void
		{
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible to update the object(movement, animation...).
				This function will most probably be called every frame as long as the
				object is still alive.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Update():void
		{
		}
		
		/*******************************************************************************/
		/*
			Description:
			This method specifies the behavior that the object will have when colliding 
			with a specific object type.
			
			Parameters:
				- CInfo_: The collision information. It includes the collision type, 
				          collided with object, tile map information...
						 
			Return:
				- None
		*/
		/*******************************************************************************/
		public function CollisionReaction(CInfo_:CollisionInfo):void
		{
		
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is used to uninitialize the object's variables when we 
				restart/extit a level or quit the game. 
				All data and events created in the initialize function should be dealt
				with in this function.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Uninitialize():void
		{
		
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible to destroy the object by removing all its
				eventlisteners, freeing the memory allocated in the constructor, etc...
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Destroy():void
		{
			displayobject = null;
		}
	}
}