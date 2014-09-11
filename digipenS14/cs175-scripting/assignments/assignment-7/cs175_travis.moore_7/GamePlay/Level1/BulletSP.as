/***************************************************************************************/
/*
	filename   	BulletSP.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	This class handles the bullet movement.
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import Engine.ObjectManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import GamePlay.MainMenu.MainMenu;
	import GamePlay.GamePlayGlobals;
	import Engine.CollisionInfo;
	import flash.ui.Keyboard;
	import flash.text.TextField;
	
	import Box2D.Collision.Shapes.b2PolygonShape;
	import Box2D.Common.Math.b2Vec2;
	import Box2D.Dynamics.*;
	import Box2D.Collision.Shapes.*;
	import Box2D.Common.Math.*;
	import flash.media.Sound;
	

	public class BulletSP extends GameObject
	{
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
		public var iMeterToPixelsRatio:int = 10;
		public var nSpeed:int;
		public var nDirX:Number;
		public var nDirY:Number;
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function BulletSP(nPosX_:Number, nPosY_:Number, nSpeed_:Number, _nDirectionX:Number, _nDirectionY:Number)
		{
			super(new lazer() , nPosX_, nPosY_, GamePlayGlobals.GO_BULLET);
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
			nSpeed = nSpeed_;
			nDirX = _nDirectionX;
			nDirY = _nDirectionY;
			
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
			displayobject.x = nInitialPosX;
			displayobject.y = nInitialPosY;

			bPhysics = true;

			var circleshape:b2CircleShape = new b2CircleShape();
			circleshape.SetRadius(5/iMeterToPixelsRatio);
			
			fixturedef = new b2FixtureDef();
			fixturedef.density = 0.0;
			fixturedef.restitution = 0.2;
			fixturedef.friction = 0.5;
			fixturedef.shape = circleshape;
			
			bodydef = new b2BodyDef();
			bodydef.type = b2Body.b2_dynamicBody;
			bodydef.userData = "lazer";
			bodydef.position.Set(displayobject.x/iMeterToPixelsRatio, displayobject.y/iMeterToPixelsRatio);
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
			body.ApplyForce(new b2Vec2(nDirX * nSpeed, nDirY * nSpeed), body.GetPosition() );
			//make display object follow physics object
			displayobject.x = body.GetPosition().x * iMeterToPixelsRatio;
			displayobject.y = body.GetPosition().y * iMeterToPixelsRatio;
			displayobject.rotation = body.GetAngle() * (180/Math.PI);
		}

		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				case GamePlayGlobals.GO_ENEMY:
				{
					bIsDead = true;
				}
				break;
				case GamePlayGlobals.GO_WALL:
				{
					bIsDead = true;
				}
				break;
			}
		}
	}
}