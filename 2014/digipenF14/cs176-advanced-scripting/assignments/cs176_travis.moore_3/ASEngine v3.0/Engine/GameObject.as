/***************************************************************************************/
/*
	filename   	GameObject.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		10/16/2014
	
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
	import flash.geom.Point;
	
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
		/* An integer that represents what type of collision the object will do */
		public var iCollisionType:int;
		/* Vector of 8 hot spots that surround the Game Object */
		public var vHotSpots:Vector.<HotSpot>;
		
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
								   nPosY_:Number = 0, iType_:int = 0, iCollisionType_:int = 0)
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
			iCollisionType = iCollisionType_;
			
			//create hotspots vector
			vHotSpots = new Vector.<HotSpot>(8);
			for(var i:int = 0; i < 8; ++i)
			{
				vHotSpots[i] = new HotSpot();
			}
		}
		
		/**************************************************************************
		/*
			Description:
				This method computes 8 hotspots for any game object. 
				Two hotspots for every side (shown in the image below)
				
				          UP
				      . x . x .
				      x       x
				LEFT  .       .  RIGHT
				      x       x
				      . x . x .
				         DOWN
						 
				All 8 hotspots are put in the vHotSpots vector starting with the 
				two UP hotspots and going clockwise.
				HotSpots are usually updated before the object checks collision 
				with a tilemap.
				
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/
		public function UpdateHotSpots():void
		{
			//TOP OF GAMEOBJECT
			this.vHotSpots[0].iSide = HotSpot.HS_UP;
			this.vHotSpots[0].pPosition.x = this.displayobject.x - (this.displayobject.width / 4);
			this.vHotSpots[0].pPosition.y = this.displayobject.y - (this.displayobject.height / 2);

			this.vHotSpots[1].iSide = HotSpot.HS_UP;
			this.vHotSpots[1].pPosition.x = this.displayobject.x + (this.displayobject.width / 4);
			this.vHotSpots[1].pPosition.y = this.displayobject.y - (this.displayobject.height / 2);
			
			//RIGHT OF GAMEOBJECT
			this.vHotSpots[2].iSide = HotSpot.HS_RIGHT;
			this.vHotSpots[2].pPosition.x = this.displayobject.x + (this.displayobject.width / 2);
			this.vHotSpots[2].pPosition.y = this.displayobject.y - (this.displayobject.height / 4);

			this.vHotSpots[3].iSide = HotSpot.HS_RIGHT;
			this.vHotSpots[3].pPosition.x = this.displayobject.x + (this.displayobject.width / 2);
			this.vHotSpots[3].pPosition.y = this.displayobject.y + (this.displayobject.height / 4);
			
			//BOTTOM OF GAMEOBJECT
			this.vHotSpots[4].iSide = HotSpot.HS_DOWN;
			this.vHotSpots[4].pPosition.x = this.displayobject.x + (this.displayobject.width / 4);
			this.vHotSpots[4].pPosition.y = this.displayobject.y + (this.displayobject.height / 2);
			
			this.vHotSpots[5].iSide = HotSpot.HS_DOWN;
			this.vHotSpots[5].pPosition.x = this.displayobject.x - (this.displayobject.width / 4);
			this.vHotSpots[5].pPosition.y = this.displayobject.y + (this.displayobject.height / 2);
			
			//LEFT OF GAMEOBJECT
			this.vHotSpots[6].iSide = HotSpot.HS_LEFT;
			this.vHotSpots[6].pPosition.x = this.displayobject.x - (this.displayobject.width / 2);
			this.vHotSpots[6].pPosition.y = this.displayobject.y + (this.displayobject.height / 4);

			this.vHotSpots[7].iSide = HotSpot.HS_LEFT;
			this.vHotSpots[7].pPosition.x = this.displayobject.x - (this.displayobject.width / 2);
			this.vHotSpots[7].pPosition.y = this.displayobject.y - (this.displayobject.height / 4);
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
		
		/**************************************************************************
		/*
			Description:
			This method checks if a collision flag is on for the current object.
			You can check one type at a time.
			
			Parameters:
				- iCollisionType_: The collision we are checking if it's On or Off. 
								   eg: Globals.CO_DYNAMIC
				
			Returns:
				- A Boolean that specifies if the flag is on or off. 
				  True is ON, False is OFF.
		*/
		/*************************************************************************/
		public function IsCollisionFlagOn(iCollisionType_:int):Boolean
		{
			return Boolean(iCollisionType & iCollisionType_);
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
			for(var h:int = 0; h < vHotSpots.length; ++h)
			{
				delete vHotSpots[h];
			}
			vHotSpots = null;
		}
	}
}