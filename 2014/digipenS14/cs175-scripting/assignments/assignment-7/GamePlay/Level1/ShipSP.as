/***************************************************************************************/
/*
	filename   	ShipSP.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu, eabichahine@digipen.edu
	date		04/18/2014 
	
	brief:
		This class handles the ship's movement and keeps track of its health value
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import Engine.InputManager;
	import Engine.ObjectManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import GamePlay.MainMenu.MainMenu;
	import GamePlay.GamePlayGlobals;
	import Engine.CollisionInfo;
	import flash.ui.Keyboard;
	import flash.text.TextField;
	import flash.events.Event;
	
	import Box2D.Collision.Shapes.b2PolygonShape;
	import Box2D.Common.Math.b2Vec2;
	import Box2D.Dynamics.*;
	import Box2D.Collision.Shapes.*;
	import Box2D.Common.Math.*;
	import flash.media.Sound;
	
    import GamePlay.Lose.LoseMenu;
	
	public class ShipSP extends GameObject
	{
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
		public var nSpeed:Number;
		static public var iHealth:int;
		public var iMeterToPixelsRatio:int = 10;
		public var nDragFactor:Number;
		public var nVelocity:Number;
		public var nMaxVelocity:Number;
		public var nRotationSpeed:Number;
		public var shipThruster = new thruster;
		var nDirectionX:Number;
		var nDirectionY:Number
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function ShipSP(nPosX_:Number, nPosY_:Number,  nSpeed_:Number):void
		{
			super(new ship(), nPosX_, nPosY_, GamePlayGlobals.GO_SHIP);
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
			
			iHealth = 1;
			nSpeed = nSpeed_;
			
			//set max velocity
			nMaxVelocity = 30;

			//set current velocity
			nVelocity = 0;
			//set rotation speed
			nRotationSpeed = 5;
			//set drag factor
			nDragFactor = 0.9;
		}
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Initialize():void
		{
			//turn physics on
			bPhysics = true;
			
			bIsDead = false;
			
			//set displayobject to initial position values
			displayobject.x = nInitialPosX;
			displayobject.y = nInitialPosY;

			//set health to 50
			iHealth = 50;
			
			//Box2D
			var circleshapeShip:b2CircleShape = new b2CircleShape();
			circleshapeShip.SetRadius(20/iMeterToPixelsRatio);

			fixturedef = new b2FixtureDef();
			fixturedef.density = 0.5;
			fixturedef.restitution = 1.0;
			fixturedef.friction = 0.0;
			fixturedef.shape = circleshapeShip;
			
			//fixture
			fixturedef = new b2FixtureDef();
			fixturedef.density = 0.1;
			fixturedef.restitution = 0.75;
			fixturedef.friction = 0.5;
			fixturedef.shape = circleshapeShip;
			
			//bodydef
			bodydef = new b2BodyDef();
			bodydef.type = b2Body.b2_dynamicBody;
			bodydef.userData = "Player";
			bodydef.position.Set(nInitialPosX/iMeterToPixelsRatio, nInitialPosY/iMeterToPixelsRatio);
		}
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function get iHealthValue():int
		{
			return iHealth;
		}
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function set iHealthValue(iValue_:int):void
		{
			if(iValue_ < 0 || iValue_ > 100)
			{
				iValue_ = 0;
			}
			
			iHealth = iValue_;
		}
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/

		
		override public function Update():void
		{
			if(Level1.bLevelStarted == true)
			{
				body.SetLinearDamping(0.9);
				//drag to reduce speed by nDragFactor (~1%)
				nVelocity *= nDragFactor;
				
				//set x and y direction of ship
				nDirectionX = Math.cos(body.GetAngle());
				nDirectionY = Math.sin(body.GetAngle());

				if(InputManager.IsPressed(Keyboard.UP))
				{
					if(nVelocity < nMaxVelocity)
					{
						nVelocity += nSpeed;
						//apply the forces found on both x and y for physics object
						body.ApplyForce(new b2Vec2(nDirectionX * nVelocity, nDirectionY * nVelocity), body.GetPosition() );
						
						//play thruster sound
						//shipThruster.play();
					}
				}
				if(InputManager.IsPressed(Keyboard.DOWN))
				{
					if(nVelocity > -nMaxVelocity)
					{
						nVelocity -= nSpeed;
						//apply the forces found on both x and y for physics object
						body.ApplyForce(new b2Vec2(nDirectionX * nVelocity, nDirectionY * nVelocity), body.GetPosition() );
					}
				}
				
				else if(InputManager.IsPressed(Keyboard.LEFT || Keyboard.A))
				{
					body.ApplyTorque(-nRotationSpeed);
				}
				else if(InputManager.IsPressed(Keyboard.RIGHT || Keyboard.D))
				{
					body.ApplyTorque(nRotationSpeed);
				}

				
				if(InputManager.IsTriggered(Keyboard.SPACE))
				{
					Shoot();
				}
			}
			
			//make display object follow physics object
			displayobject.x = body.GetPosition().x * iMeterToPixelsRatio;
			displayobject.y = body.GetPosition().y * iMeterToPixelsRatio;
			displayobject.rotation = body.GetAngle() * (180/Math.PI);
		}
		//SHOOTING THE LAZERS
		private function Shoot():void
		{
			var shipFire = new lazerblast;
			shipFire.play();
			ObjectManager.AddObject(new BulletSP(displayobject.x + nDirectionX * 60, displayobject.y + nDirectionY * 60, 50, nDirectionX, nDirectionY), "Bullet" , ObjectManager.OM_DYNAMICOBJECT);
		}
		
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				case GamePlayGlobals.GO_ENEMY:
				{
					iHealth -= 10;
					
					var goHealth:GameObject = ObjectManager.GetObjectByName("HealthText" , ObjectManager.OM_STATICOBJECT);
					TextField(goHealth.displayobject).text = "Health = " + String(iHealth); 
					
					if(iHealth == 0)
					{
						GameStateManager.GotoState(new LoseMenu);
					}
				}
				break;
			}
		}
	}
}