/***************************************************************************************/
/*
	filename   	GameObject.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	The GameObject is the base class for all the game objects and it contains properties
	that could be used for all the derived classes. Derived classes should override the
	base class methods.
*/        	 
/***************************************************************************************/

package Engine
{
	import flash.display.DisplayObject;
	
	public class GameObject
	{
		/* This the GameObject's shape. It can be anything derived from the 
		   DisplayObject class (Text, MovieClip, Sprite ...) */
		public var displayobject:DisplayObject;
		/* The game Object's Type that will help the user differentiate between 
		   different game objects. Everytime the user derives a new class from the 
		   GameObject class it is highly recommended to add a new GAMEOBJECT TYPE in the 
		   Globals file (GO_SHIP , GO_ENEMY , GO_BULLET ...) */
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
				screen (nPosX and nPosY)
				
			Parameters:
				- displayobject: can be any kind of display object or display container
				- nPosX: The object's x position on the screen
				- nPosY: The object's y position on the screen
				- iID: the object's ID initially set to GO_GAMEOBJECT (0)
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function GameObject(displayobject_:DisplayObject , nPosX:Number,
								   nPosY:Number, iType_:int)
		{
			displayobject = displayobject_;
			displayobject.x = nPosX;
			displayobject.y = nPosY;
			iType = iType_;
			bIsDead = false;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is used to reinitialize the object's variables. It is 
				mostly used when restarting a level.
			
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
				This method is called when the object collides with another object. It 
				is responsible for the reaction that the object has to do when colliding
				with certain objects. By checking the value inside the 
				iCollidedWithObjectType variable we can know with which object we 
				collided.
				
			Parameters:
				- iCollidedWithObjectType_: An integer that represents the type of the 
											object we collided with. This value will 
											help us determine what reaction the object 
											should do.
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function CollisionReaction(iCollidedWithObjectType_:int):void
		{

		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible to destroy the object by removing all its
				eventlisteners, freeing memory, removing it from the stage ... 
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Destroy():void
		{
			
		}
	}
}
