package MyClasses
{
	import Box2D.Dynamics.*;
	import Box2D.Collision.*;
	import Box2D.Collision.Shapes.*;
	import Box2D.Dynamics.Joints.*;
	import Box2D.Dynamics.Contacts.*;
	import Box2D.Common.*;
	import Box2D.Common.Math.*;
	
	import flash.display.Stage;
	import flash.events.Event;
	import flash.display.Sprite;
	import flash.text.TextField;

	public class Level
	{
		private var sMyStage:Stage;
		private var wMyWorld:b2World;
		private var aObjects:Array;
		
		private var textLeft:TextField;
		private var textRight:TextField;
		
		private var iLeftCounter:int;
		private var iRightCounter:int;
		
		static public var iMeterToPixelsRatio:int = 30;

		public function Level( sStage_:Stage )
		{
			sMyStage = sStage_;
			wMyWorld = new b2World(new b2Vec2(0.0, 0.0), true);
			aObjects = new Array();
			
			/* Debug Data */
			var dbgDraw:b2DebugDraw = new b2DebugDraw();
			var spriteDebug:Sprite = new Sprite();
			sMyStage.addChild(spriteDebug);
			dbgDraw.SetSprite(spriteDebug);
			dbgDraw.SetDrawScale(iMeterToPixelsRatio);
			dbgDraw.SetFillAlpha(0.3);
			dbgDraw.SetLineThickness(1.0);
			dbgDraw.SetFlags(b2DebugDraw.e_shapeBit | b2DebugDraw.e_jointBit);
			wMyWorld.SetDebugDraw(dbgDraw);

			InitializeLevelObjects();
			sMyStage.addEventListener(Event.ENTER_FRAME, Update);
		}


		private function InitializeLevelObjects()
		{
			var polygonshapeGround:b2PolygonShape = new b2PolygonShape();
			polygonshapeGround.SetAsBox(275/iMeterToPixelsRatio , 5/iMeterToPixelsRatio);
			
			var fixturedefGround:b2FixtureDef = new b2FixtureDef();
			fixturedefGround.density = 0.0;
			fixturedefGround.restitution = 1.0;
			fixturedefGround.friction = 0.0;
			fixturedefGround.shape = polygonshapeGround;
			
			var bodydefGround:b2BodyDef = new b2BodyDef();
			/* The ground's type is b2_staticBody meaning it won't move */
			bodydefGround.type = b2Body.b2_staticBody;
			bodydefGround.userData = "Ground";
			bodydefGround.position.Set(275/iMeterToPixelsRatio, 390/iMeterToPixelsRatio);
			
			/* Ground: Bottom */
			var bodyGroundBottom:b2Body;
			bodyGroundBottom = wMyWorld.CreateBody(bodydefGround);
			bodyGroundBottom.CreateFixture(fixturedefGround);
			
			/* Ground: Top */
			var bodyGroundTop:b2Body;
			bodyGroundTop = wMyWorld.CreateBody(bodydefGround);
			bodyGroundTop.CreateFixture(fixturedefGround);
			bodyGroundTop.SetPosition(new b2Vec2(275/iMeterToPixelsRatio, 10/iMeterToPixelsRatio));
			bodyGroundTop.SetUserData("Ceiling");
			
			/* Wall: Left */
			var bodyWallLeft:b2Body;
			bodyWallLeft = wMyWorld.CreateBody(bodydefGround);
			bodyWallLeft.CreateFixture(fixturedefGround);
			bodyWallLeft.SetPositionAndAngle(new b2Vec2(10/iMeterToPixelsRatio, 130/iMeterToPixelsRatio), Math.PI/2.0);
			bodyWallLeft.SetUserData("Left Wall");
			
			/* Wall: Right */
			var bodyWallRight:b2Body;
			bodyWallRight = wMyWorld.CreateBody(bodydefGround);
			bodyWallRight.CreateFixture(fixturedefGround);
			bodyWallRight.SetPositionAndAngle(new b2Vec2(540/iMeterToPixelsRatio, 130/iMeterToPixelsRatio), Math.PI/2.0);
			bodyWallRight.SetUserData("Right Wall");
			
			/* Pillars */
			var circleshapePillar:b2CircleShape = new b2CircleShape();
			circleshapePillar.SetRadius(20/iMeterToPixelsRatio);
			
			var fixturedefPillar:b2FixtureDef = new b2FixtureDef();
			fixturedefPillar.density = 0.0;
			fixturedefPillar.restitution = 0.2;
			fixturedefPillar.friction = 0.5;
			fixturedefPillar.shape = circleshapePillar;

			var bodydefPillar:b2BodyDef = new b2BodyDef();
			bodydefPillar.type = b2Body.b2_staticBody;
			bodydefPillar.userData = "Left Pillar";
			bodydefPillar.position.Set(150.0/iMeterToPixelsRatio, 150.0/iMeterToPixelsRatio);

			/* Left Pillar Body */
			var bodyLeftPillar:b2Body;
			bodyLeftPillar = wMyWorld.CreateBody(bodydefPillar);
			bodyLeftPillar.CreateFixture(fixturedefPillar);
			
			/* Right Pillar Body */
			var bodyRightPillar:b2Body;
			bodyRightPillar = wMyWorld.CreateBody(bodydefPillar);
			bodyRightPillar.CreateFixture(fixturedefPillar);
			bodyRightPillar.SetPosition(new b2Vec2(400.0/iMeterToPixelsRatio, 250.0/iMeterToPixelsRatio));
			bodyRightPillar.SetUserData("Right Pillar");
			
			/* Ball */
			var circleshapeBall:b2CircleShape = new b2CircleShape();
			circleshapeBall.SetRadius(30/iMeterToPixelsRatio);

			var fixturedefBall:b2FixtureDef = new b2FixtureDef();
			fixturedefBall.density = 0.5;
			fixturedefBall.restitution = 1.0;
			fixturedefBall.friction = 0.0;
			fixturedefBall.shape = circleshapeBall;

			var bodydefBall:b2BodyDef = new b2BodyDef();
			bodydefBall.type = b2Body.b2_dynamicBody;
			bodydefBall.userData = "Ball";
			bodydefBall.position.Set(300/iMeterToPixelsRatio, 100/iMeterToPixelsRatio);

			var bodyBall:b2Body;
			bodyBall = wMyWorld.CreateBody(bodydefBall);
			bodyBall.CreateFixture(fixturedefBall);
			
			/* Apply a 1 time force to make the ball move */
			bodyBall.ApplyForce(new b2Vec2(20.0, 300.0), bodyBall.GetPosition());
			
			/* Text: Left Counter */
			textLeft = new TextField();
			textLeft.x = 20.0;
			textLeft.y = 20.0;
			textLeft.scaleX = 2.0;
			textLeft.scaleY = 2.0;
			textLeft.width = 200.0;
			sMyStage.addChild(textLeft);
			
			/* Text: Right Counter */
			textRight = new TextField();
			textRight.x = 430.0;
			textRight.y = 20.0;
			textRight.scaleX = 2.0;
			textRight.scaleY = 2.0;
			textRight.width = 200.0;
			sMyStage.addChild(textRight);
			
			iLeftCounter = 0;
			iRightCounter = 0;
			
			UpdateTexts();
			
			/* Add our own custom event listener to the world */
			//SetContactListener is the wMyWorld's way of adding an event listener that listens for collision
			//SetContactListener takes a function MyCollisionListner, with its own parameter OnBeginContact
			//MyCollisionListener
			wMyWorld.SetContactListener(new MyCollisionListener(OnBeginContact));
		}

		private function Update(e_:Event)
		{
			wMyWorld.Step(1.0/30.0 , 20 , 20);
			wMyWorld.ClearForces();
			wMyWorld.DrawDebugData();
			DestroyDeadPhysicsObjects();
		}

		private function DestroyDeadPhysicsObjects()
		{
			for (var i:int = 0; i<aObjects.length; ++i)
			{
				if (aObjects[i].bIsDead == true)
				{
					aObjects[i].Destroy();
					sMyStage.removeChild(aObjects[i]);
					wMyWorld.DestroyBody(aObjects[i].b2bodyMyBody);
					aObjects.splice(i,1);
					--i;
				}
			}
		}
		
		private function UpdateTexts()
		{
			textLeft.text = "Left: " + iLeftCounter;
			textRight.text = "Right: " + iRightCounter;
		}
		
		/* This function is called by the collision listener */
		private function OnBeginContact(ContactName1_:String, ContactName2_:String)
		{
			trace(ContactName1_, ContactName2_);
			
			if("Ball" == ContactName1_)
			{
				if("Left Pillar" == ContactName2_)
				{
					++iLeftCounter;
				}
				else if("Right Pillar" == ContactName2_)
				{
					++iRightCounter;
				}
			}
			else if("Ball" == ContactName2_)
			{
				if("Left Pillar" == ContactName1_)
				{
					++iLeftCounter;
				}
				else if("Right Pillar" == ContactName1_)
				{
					++iRightCounter;
				}
			}
			
			UpdateTexts();
		}
	}
}

import Box2D.Dynamics.*;
import Box2D.Collision.*;
import Box2D.Collision.Shapes.*;
import Box2D.Dynamics.Joints.*;
import Box2D.Dynamics.Contacts.*;
import Box2D.Common.*;
import Box2D.Common.Math.*;

/* 
	Class internal to this file. It is used by the physics world as the collision listener.
	In other words, everytime 2 bodies collides in the physics world, the functions of this class 
	will be called .
*/
class MyCollisionListener extends b2ContactListener
{
	private var sContactName1:String;
	private var sContactName2:String;
	private var fOnBeginContact:Function;
	
	/*
		This is the constructor, it takes a reference to a function
		which will be called later on in "BeginContact"
	*/
	public function MyCollisionListener(fOnBeginContact_:Function)
	{
		sContactName1 = new String();
		sContactName2 = new String();
		fOnBeginContact = fOnBeginContact_;
	}
	
	/*
		This function override the default BeginContact of the contact listener.
		It allows us to write our own code, which in this case is our own collision
		reaction function. Remember that "mOnBeginContact" is a reference to the 
		collision reaction function in the "Level" class.
	*/
	public override function BeginContact(contact_:b2Contact):void
	{
		/*
			Getting the names of the 2 bodies which collided
		*/
		sContactName1 = contact_.GetFixtureA().GetBody().GetUserData();
		sContactName2 = contact_.GetFixtureB().GetBody().GetUserData();
		
		/*
			Calling the collision reaction function, which was passed as a parameter
			in the constructor.
		*/
		fOnBeginContact(sContactName1, sContactName2);
	}
}