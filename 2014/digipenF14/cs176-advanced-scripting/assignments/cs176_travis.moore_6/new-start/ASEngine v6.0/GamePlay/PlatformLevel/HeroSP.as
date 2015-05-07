/****************************************************************************************
FILENAME   HeroSP.as 
AUTHOR     Travis Moore
EMAIL      travis.moore@digipen.edu
DATE       12/09/2014 
/***************************************************************************************/
package GamePlay.PlatformLevel
{
	//IMPORT
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	import flash.ui.Keyboard;
	import flash.display.MovieClip;
	import Engine.PhysicsManager;
	import Engine.PhysicsInfo;
	import flash.geom.Point;

	//CLASS HEROSP
	final public class HeroSP extends GameObject
	{
		
		//VARIABLES
		public var nSpeed:Number;				//speed of the hero
		public var nMovingMagnitude:Number; 	//magnitude
		public var nJumpingMagnitude:Number;	//jumping magnitude
		
		/********************************************************************************
		FUNCTION- Constructor()
		/*******************************************************************************/
		final public function HeroSP()
		{
			super(new Hero(), 0, 0, GamePlayGlobals.GO_HERO, CollisionManager.CO_DYNAMIC | CollisionManager.CO_TILEMAP);
			
			//set hero speed
			this.nSpeed = 5;
			//set magntiude for moving
			this.nMovingMagnitude = 50;
			//set jumping magnitude
			this.nJumpingMagnitude = 35000;
			
			//enable the physics on the hero
			this.EnablePhysics();
		}
		
		/********************************************************************************
		FUNCTION- Update()
		/*******************************************************************************/
		final override public function Update():void
		{
			//variables for gravity and jumping
			var pGravity:Point = new Point(0,1);
			var pJump:Point = new Point(0,-1);
			
			//reset the magnitude at the start of every frame
			this.physicsinfo.nVelocityMagnitude = 0;
			
			//add a force of gravity to keep the player down
			this.physicsinfo.AddForce(PhysicsManager.DT, pGravity, 10000);
			
			//move left
			if(InputManager.IsPressed(Keyboard.LEFT))
			{
				this.physicsinfo.pVelocityDirection.x = -10;
				this.physicsinfo.nVelocityMagnitude = this.nMovingMagnitude;
			}
			//move left
			else if(InputManager.IsPressed(Keyboard.RIGHT))
			{
				this.physicsinfo.pVelocityDirection.x = 10;
				this.physicsinfo.nVelocityMagnitude = this.nMovingMagnitude;
			}
			//jump
			if(InputManager.IsTriggered(Keyboard.UP))
			{
				this.physicsinfo.AddForce(PhysicsManager.DT + 0.1, pJump, this.nJumpingMagnitude);
			}
		}
		
		/**************************************************************************
		/*
			Description:
				This method will check if any collision with certain tiles in the 
				tile map happened or if the hero collides with an open door in 
				order to switch to level 3.
				If collision with tile ID 1 or 2 happened we will need to snap 
				according to which side we collided (UP and DOWN will snap on the Y
				RIGHT and LEFT will snap on the X).
				
			Parameters:
				- CInfo_: The collision information. It includes the collision type, 
				         collided with object, tile map information...
						 
			Return:
				- None
		*/
		/*************************************************************************/
		final override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			
			if( CInfo_.iCollisionType == CollisionManager.CO_DYNAMIC )
			{
				if(CInfo_.gameobjectCollidedWith.iType == GamePlayGlobals.GO_DOOR)
				{
					var door:GameObject = ObjectManager.GetObjectByName("Door", ObjectManager.OM_DYNAMICOBJECT);
					if((MovieClip(door.displayobject)).currentFrame == 2)
					{
						//GameStateManager.GotoState(new Level3());
					}
				}
			}
			else if( CInfo_.iCollisionType == CollisionManager.CO_TILEMAP )
			{
				
				var bSnapOnY:Boolean = false;
				var bSnapOnX:Boolean = false;
				var HotspotsSize:int = CInfo_.gameobject.vHotSpots.length;
				for(var i:int = 0; i < HotspotsSize; ++i)
				{
					
					if(CInfo_.gameobject.vHotSpots[i].iCollidedTilesID == 1 || 
					   CInfo_.gameobject.vHotSpots[i].iCollidedTilesID == 2)
					{
						if(CInfo_.gameobject.vHotSpots[i].iSide == HotSpot.HS_UP ||
						   CInfo_.gameobject.vHotSpots[i].iSide == HotSpot.HS_DOWN)
						{
							bSnapOnY = true;
						}
						else if(CInfo_.gameobject.vHotSpots[i].iSide == HotSpot.HS_RIGHT ||
								CInfo_.gameobject.vHotSpots[i].iSide == HotSpot.HS_LEFT)
						{
							bSnapOnX = true;
						}
					}
					
				}
				
				if(bSnapOnY)
				{
					CInfo_.tilemapinfo.SnapToTile(this, CInfo_.uiSnapToRow, CInfo_.uiSnapToColumn, HotSpot.HS_UP);
				}
				
				if(bSnapOnX)
				{
					CInfo_.tilemapinfo.SnapToTile(this, CInfo_.uiSnapToRow, CInfo_.uiSnapToColumn , HotSpot.HS_LEFT);
				}
			}
		}
	}
}