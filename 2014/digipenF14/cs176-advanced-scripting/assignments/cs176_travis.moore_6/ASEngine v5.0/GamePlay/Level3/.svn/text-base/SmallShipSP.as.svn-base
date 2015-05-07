/***************************************************************************************/
/*
	filename   	ShipSP.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	This class handles the ship's movement and keeps track of its health value
*/        	 
/***************************************************************************************/
package GamePlay.Level3
{
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	import flash.ui.Keyboard;
	import flash.geom.Point;

	public class SmallShipSP extends GameObject
	{
		private const PI_OVER_180:Number = Math.PI / 180.0;
		private var nInitialPosX:Number;
		private var nInitialPosY:Number;
		private var nSpeed:Number;
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		public function SmallShipSP(nPosX_:Number, 
							 nPosY_:Number, 
							 nSpeed_:Number,
							 iCollisionType_:int)
		{
			super(new SmallShip(), nPosX_, nPosY_, GamePlayGlobals.GO_SMALLSHIP, iCollisionType_);
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
			nSpeed = nSpeed_;
		}
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		override public function Initialize():void
		{
			displayobject.x = nInitialPosX;
			displayobject.y = nInitialPosY;
			displayobject.rotation = 0;
			EnablePhysics();
			physicsinfo.RemoveAllForces();
			physicsinfo.pVelocityDirection.x = 0;
			physicsinfo.pVelocityDirection.y = 0;
			physicsinfo.nVelocityMagnitude = 0;
		}
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		private function Shoot()
		{
			var pDirection:Point = new Point();
			pDirection.x = Math.cos(displayobject.rotation * PI_OVER_180);
			pDirection.y = Math.sin(displayobject.rotation * PI_OVER_180);
			ObjectManager.AddObject(new ShinyBulletSP(displayobject.x + pDirection.x * 50, displayobject.y + pDirection.y * 50, pDirection , 50, CollisionManager.CO_DYNAMIC), "ShinyBullet" , ObjectManager.OM_DYNAMICOBJECT);
		}
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		private function Movement()
		{
			var pDirection:Point = new Point();
			
			if(InputManager.IsPressed(Keyboard.UP))
			{
				pDirection.x = Math.cos(displayobject.rotation * PI_OVER_180);
				pDirection.y = Math.sin(displayobject.rotation * PI_OVER_180);
				physicsinfo.AddForce(PhysicsManager.DT, pDirection, nSpeed);
			}
			else if(InputManager.IsPressed(Keyboard.DOWN))
			{
				pDirection.x = Math.cos(displayobject.rotation * PI_OVER_180);
				pDirection.y = Math.sin(displayobject.rotation * PI_OVER_180);
				physicsinfo.AddForce(PhysicsManager.DT, pDirection, -nSpeed);
			}
			
			if(InputManager.IsPressed(Keyboard.LEFT))
			{
				displayobject.rotation -= 2;
			}
			else if(InputManager.IsPressed(Keyboard.RIGHT))
			{
				displayobject.rotation += 2;
			}
		}
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		private function ApplyFriction()
		{
			if(physicsinfo.nVelocityMagnitude != 0)
			{
				physicsinfo.AddForce(PhysicsManager.DT, physicsinfo.pVelocityDirection, -nSpeed/2);
			}
		}
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		override public function Update():void
		{
			Movement();
			ApplyFriction();
			
			if(InputManager.IsTriggered(Keyboard.SPACE))
			{
				Shoot();
			}
		}
		
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		override public function CollisionReaction(CInfo:CollisionInfo):void
		{
		}
		
		/**************************************************************************
		/*
			Description:
			
			Parameters:
				- None
		*/
		/*************************************************************************/
		override public function Destroy():void
		{
			super.Destroy();
		}

	}

}