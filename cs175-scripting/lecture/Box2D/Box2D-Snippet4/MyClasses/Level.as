package MyClasses
{
	import Box2D.Dynamics.b2World;
	import Box2D.Common.Math.b2Vec2;
	import Box2D.Collision.Shapes.*;
	import Box2D.Dynamics.*;
	import flash.display.Stage;
	import flash.events.Event;
	
	public class Level
	{
		private var sMyStage:Stage;
		private var wMyWorld:b2World;
		private var aObjects:Array;
		
		
		public function Level( s:Stage , b2vec2Gravity:b2Vec2 , bDoSleep:Boolean)
		{
			sMyStage = s;
			wMyWorld = new b2World(b2vec2Gravity, bDoSleep);
			aObjects = new Array();
			
			InitializeLevelObjects();
			
			sMyStage.addEventListener(Event.ENTER_FRAME, Update);
		}
		
		
		private function InitializeLevelObjects()
		{
			aObjects.push(new Ball());
			sMyStage.addChild(aObjects[aObjects.length-1]);
			
			/* Creating the shape*/
			var circleshapeBall:b2CircleShape = new b2CircleShape();
			circleshapeBall.SetRadius(40/GlobalData.iMetersToPixelsRatio);
			
			/* Creating a fixture definition. */
			var fixturedefBall:b2FixtureDef = new b2FixtureDef();
			fixturedefBall.density = 0;
			fixturedefBall.restitution = 0.7;
			fixturedefBall.friction = 0.2;
			fixturedefBall.shape = circleshapeBall;
			
			
			/* Creating the Body Definition. */
			var bodydefBall:b2BodyDef = new b2BodyDef();
			bodydefBall.type = b2Body.b2_dynamicBody;
			bodydefBall.userData = "ball";
			bodydefBall.position.Set(275/GlobalData.iMetersToPixelsRatio, 100/GlobalData.iMetersToPixelsRatio);
			
			/* Finally we create the body and add it to the world. */
			aObjects[aObjects.length-1].b2bodyMyBody = wMyWorld.CreateBody(bodydefBall);
			aObjects[aObjects.length-1].b2bodyMyBody.CreateFixture(fixturedefBall);
			
		}
		
		function Update(e:Event)
		{
			wMyWorld.Step(1.0/24.0 , 10 , 10);
			wMyWorld.ClearForces();
			DestroyDeadPhysicsObjects();
		}
		
		function DestroyDeadPhysicsObjects()
		{
			for(var i:int = 0; i<aObjects.length; ++i)
			{
				if(aObjects[i].bIsDead == true)
				{
					wMyWorld.DestroyBody(aObjects[i].b2bodyMyBody);
					aObjects[i].Destroy();
					aObjects[i] = null;
					sMyStage.removeChild(aObjects[i]);
					aObjects.splice(i,1);
					--i;
				}
			}
		}

	}

}