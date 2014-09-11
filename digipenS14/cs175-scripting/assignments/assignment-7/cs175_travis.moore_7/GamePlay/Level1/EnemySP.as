/***************************************************************************************/
/*
	filename   	EnemySP.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	This class handles the enemy's creation, movement , collision recation and destruction.
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import Engine.ObjectManager;
	import Engine.GameObject;
	import GamePlay.GamePlayGlobals;
	import Engine.CollisionInfo;
	import flash.text.TextField;
	import flash.display.MovieClip;
	
	import Box2D.Collision.Shapes.b2PolygonShape;
	import Box2D.Common.Math.b2Vec2;
	import Box2D.Dynamics.*;
	import Box2D.Collision.Shapes.*;
	import Box2D.Common.Math.*;
	
	public class EnemySP extends GameObject
	{		
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
		public var nSpeed:Number;
		public var iMeterToPixelsRatio:int = 10;
		
		/*******************************************************************************/
		/*
			Description:
				
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function EnemySP(nPosX_:Number, nPosY_:Number, nSpeed_:Number)
		{
			super(new enemy() , nPosX_, nPosY_, GamePlayGlobals.GO_ENEMY);
			nSpeed = nSpeed_;
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
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
			MovieClip(displayobject).gotoAndStop(1);
			
			//turn physics on
			bPhysics = true;
			
			bIsDead = false;
			
			//set displayobject to initial position values
			displayobject.x = nInitialPosX;
			displayobject.y = nInitialPosY;

			//Box2D
			var circleshapeAsteroid:b2CircleShape = new b2CircleShape();
			circleshapeAsteroid.SetRadius(20/iMeterToPixelsRatio);

			fixturedef = new b2FixtureDef();
			fixturedef.density = 0.5;
			fixturedef.restitution = 1.0;
			fixturedef.friction = 0.0;
			fixturedef.shape = circleshapeAsteroid;
			
			//bodydef
			bodydef = new b2BodyDef();
			bodydef.type = b2Body.b2_dynamicBody;
			bodydef.userData = "Asteroid";
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
		override public function Update():void
		{
			body.ApplyForce(new b2Vec2(-nSpeed, 0), body.GetPosition());
			//make display object follow physics object
			displayobject.x = body.GetPosition().x * iMeterToPixelsRatio;
			displayobject.y = body.GetPosition().y * iMeterToPixelsRatio;
			displayobject.rotation = body.GetAngle() * (180/Math.PI);
			
			if(MovieClip(displayobject).currentFrame == 4)
			{
				bIsDead = true;
			}
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
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			var asteroidExplosion = new explosion;
			var yaySound = new yay;
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				case GamePlayGlobals.GO_BULLET:
				{
					MovieClip(displayobject).gotoAndPlay(2);
					asteroidExplosion = new explosion;
					asteroidExplosion.play();
				}
				break;
				case GamePlayGlobals.GO_SHIP:
				{
					MovieClip(displayobject).gotoAndPlay(2);
					//play explosion sound
					asteroidExplosion = new explosion;
					asteroidExplosion.play();
					//play ship hit sound
					var shipDamaged = new shiphit;
				}
				break;
				case GamePlayGlobals.GO_WALL:
				{
					MovieClip(displayobject).gotoAndPlay(2);
					//play explosion sound
					asteroidExplosion = new explosion;
					asteroidExplosion.play();
				}
				break;
			}
		}
	}
}