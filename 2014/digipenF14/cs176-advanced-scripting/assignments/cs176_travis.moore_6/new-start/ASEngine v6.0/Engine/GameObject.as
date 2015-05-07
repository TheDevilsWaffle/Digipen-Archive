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
		/* A boolean that is set to true if the object is a physics object and 
		   is set to false if it isn't*/
		private var bPhysicsOn:Boolean;
		/* Physics info contains all the object's physics properties and 
		   local forces that will be applied on it */
		public var physicsinfo:PhysicsInfo;
		
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
			
			vHotSpots = new Vector.<HotSpot>(8);
			for(var i:int = 0; i < 8; ++i)
			{
				vHotSpots[i] = new HotSpot();
			}
			bPhysicsOn = false;
			physicsinfo = null;
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
			/* Delete in the student's version */
			var nHalfWidth:Number = (displayobject.width / 2);
			var nHalfHeight:Number = (displayobject.height / 2);
			var nQuarterWidth:Number = (displayobject.width / 4);
			var nQuarterHeight:Number = (displayobject.height / 4);			
			
			vHotSpots[0].pPosition.x = displayobject.x - nQuarterWidth;
			vHotSpots[0].pPosition.y = displayobject.y - nHalfHeight;
			vHotSpots[0].iSide = HotSpot.HS_UP;
			
			vHotSpots[1].pPosition.x =  displayobject.x + nQuarterWidth;
			vHotSpots[1].pPosition.y =  displayobject.y - nHalfHeight;
			vHotSpots[1].iSide = HotSpot.HS_UP;
			
			vHotSpots[2].pPosition.x = displayobject.x + nHalfWidth;
			vHotSpots[2].pPosition.y = displayobject.y - nQuarterHeight;
			vHotSpots[2].iSide = HotSpot.HS_RIGHT;
			
			vHotSpots[3].pPosition.x = displayobject.x + nHalfWidth;
			vHotSpots[3].pPosition.y = displayobject.y + nQuarterHeight;
			vHotSpots[3].iSide = HotSpot.HS_RIGHT;
			
			vHotSpots[4].pPosition.x = displayobject.x + nQuarterWidth;
			vHotSpots[4].pPosition.y = displayobject.y + nHalfHeight;
			vHotSpots[4].iSide = HotSpot.HS_DOWN;
			
			vHotSpots[5].pPosition.x = displayobject.x - nQuarterWidth;
			vHotSpots[5].pPosition.y = displayobject.y + nHalfHeight;
			vHotSpots[5].iSide = HotSpot.HS_DOWN;
			
			vHotSpots[6].pPosition.x = displayobject.x - nHalfWidth;
			vHotSpots[6].pPosition.y = displayobject.y + nQuarterHeight;
			vHotSpots[6].iSide = HotSpot.HS_LEFT;
			
			vHotSpots[7].pPosition.x = displayobject.x - nHalfWidth;
			vHotSpots[7].pPosition.y =  displayobject.y - nQuarterHeight;
			vHotSpots[7].iSide = HotSpot.HS_LEFT;
		}
		
		/**************************************************************************
		/*
			Description:
			This method enables Physics for a game object by setting the 
			bPhysicsOn boolean to true. If you turn on physics for the first time,
			the object's physicsinfo instance will be created with default values.
			
			Parameters:
				- None
			
			Return:
				- None 
		*/
		/*************************************************************************/
		final public function EnablePhysics():void
		{
			bPhysicsOn = true;
			if(physicsinfo == null)
			{
				physicsinfo = new PhysicsInfo(new Point());
			}
		}
		
		/**************************************************************************
		/*
			Description:
			This method disables Physics for a game object by setting the 
			bPhysicsOn boolean to false. physicsinfo still contains the same data.
			
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*************************************************************************/
		final public function DisablePhysics():void
		{
			bPhysicsOn = false;
		}
		
		/**************************************************************************
		/*
			Description:
			This method returns the physics' status. True means that the physics 
			is on, false means the physics is off.
			
			Parameters:
				- None
			
			Return:
				- True if physics is on, false if it isn't
		*/
		/*************************************************************************/
		final public function IsPhysicsOn():Boolean
		{
			return bPhysicsOn;
		}
		
		/**************************************************************************
		/*
			Description:
			This method sets the object's physics properties. It's job is to fill
			the object's physicsinfo instance with all the required information. 
			If physicsinfo is null, this method will create it then fill it with 
			all the information.
			
			Parameters:
				- nMass_: represents the object's mass value
				- pVelocityDirection_: The object's velocity direction. Make sure
									   the velocity direction is normalized before 
									   storing it in the object.
				- nVelocityMagnitude_: represents the velocity magnitude (a.k.a speed)
				
			Return:
				- None
		*/
		/*************************************************************************/
		final public function SetPhysicsProperties(nMass_:Number, pVelocityDirection_:Point,
											 	   nVelocityMagnitude_:Number):void
		{
			if(physicsinfo == null)
			{
				physicsinfo = new PhysicsInfo(pVelocityDirection_, nVelocityMagnitude_, nMass_);
			}
			else
			{
				physicsinfo.nMass = nMass_;
				physicsinfo.pVelocityDirection = pVelocityDirection_;
				physicsinfo.pVelocityDirection.normalize(1);
				physicsinfo.nVelocityMagnitude = nVelocityMagnitude_;
			}
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
			
			if(physicsinfo != null)
			{
				physicsinfo.Destroy();
				physicsinfo = null;
			}
		}
	}
}