/****************************************************************************************/
/*
	filename   	Level.as
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date    	03/13/2014 
	
	brief:
	Level.as creates, initializes, and destroys objects and functions needed in the game.
*/        	 
/****************************************************************************************/
package MyClasses
{
	//import the needed Box2D files
	import Box2D.Dynamics.b2World;
	import Box2D.Common.Math.b2Vec2;
	import Box2D.Collision.Shapes.*;
	import Box2D.Dynamics.*;

	//import the needed flash files
	import flash.display.Sprite;
	import flash.display.Stage;
	import flash.events.Event;
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	
	public class Level
	{
		//create a variable to represent the stage
		public var sMyStage:Stage;
		//create a variable to access the physics world
		public var wMyWorld:b2World;
		//create an array to store objects in
		public var aObjects:Array;
		//thePlayer
		public var thePlayer:Player;
		//the timer
		public var uiTimer:uint;
		
		/*******************************************************************************/
		/*
			Description: 
				Level()'s main purpose is to create a ball at the top of the
				stage at either the left or right depending on a random variable.
		
			Parameters:
				- _sStage:Stage - the passed stage
				- _b2vec2Gravity:b2Vec2 - the gravity parameters for x and y
				- _b2DoSleep:Boolean - the true/false for if objects can sleep
		
			Return: 
				- None
		*/
		/*******************************************************************************/
		public function Level( _sStage:Stage , _b2vec2Gravity:b2Vec2 , _bDoSleep:Boolean):void
		{
			//set sMyStage equal to passed stage parameter
			sMyStage = _sStage;
			//set wMyWorld equal to passed world parameters
			wMyWorld = new b2World(_b2vec2Gravity, _bDoSleep);
			//initialize the aObjects array
			aObjects = new Array();
			
			//add event listener for Update function
			sMyStage.addEventListener(Event.ENTER_FRAME, Update);
			
			//start the level by calling InitializeLevelObjects
			InitializeLevelObjects();
		}
		
		/*******************************************************************************/
		/*
			Description: 
				InitializeLevelObjects()'s main purpose is to create and initialize
				objects used in the game, as well as add event listeners.
		
			Parameters:
				- None
		
			Return: 
				- None
		*/
		/*******************************************************************************/
		public function InitializeLevelObjects():void
		{
			//initialize uiTimer
			uiTimer = 0;
			
			/*//////////////////////////////////////////////////////////////////////////////////////////////////
			//create the player
			//////////////////////////////////////////////////////////////////////////////////////////////////*/
			var shapePlayer:b2PolygonShape = new b2PolygonShape();
			shapePlayer.SetAsBox(10/GlobalData.iMetersToPixelsRatio, 10/GlobalData.iMetersToPixelsRatio);
			
			var fixturePlayer:b2FixtureDef = new b2FixtureDef();
			fixturePlayer.density = 0.0;
			fixturePlayer.restitution = 0.2;
			fixturePlayer.friction = 0.5;
			fixturePlayer.shape = shapePlayer;
			
			var bodyDefPlayer:b2BodyDef = new b2BodyDef();
			bodyDefPlayer.type = b2Body.b2_staticBody;
			bodyDefPlayer.userData = "player";
			bodyDefPlayer.position.Set(300/GlobalData.iMetersToPixelsRatio, 220/GlobalData.iMetersToPixelsRatio);
			
			var bodyPlayer:b2Body;
			thePlayer = new Player();
			
			//create the body and add it to the world
			thePlayer.b2bodyMyBody = wMyWorld.CreateBody(bodyDefPlayer);
			thePlayer.b2bodyMyBody.CreateFixture(fixturePlayer);
			//rotate the player so it is a diamond shape
			thePlayer.b2bodyMyBody.SetAngle(Math.PI / 4);
			
			//add newly created ball in aObjects to the stage
			sMyStage.addChild(thePlayer);

			/*//////////////////////////////////////////////////////////////////////////////////////////////////
			//create the left wall
			//////////////////////////////////////////////////////////////////////////////////////////////////*/
			var polygonshapeLeftWall:b2PolygonShape = new b2PolygonShape();
			polygonshapeLeftWall.SetAsBox(5/GlobalData.iMetersToPixelsRatio , 480/GlobalData.iMetersToPixelsRatio);
			
			var fixturedefLeftWall:b2FixtureDef = new b2FixtureDef();
			fixturedefLeftWall.density = 0.0;
			fixturedefLeftWall.restitution = 0.2;
			fixturedefLeftWall.friction = 0.5;
			fixturedefLeftWall.shape = polygonshapeLeftWall;
			
			var bodydefLeftWall:b2BodyDef = new b2BodyDef();
			bodydefLeftWall.type = b2Body.b2_staticBody;
			bodydefLeftWall.userData = "left wall";
			bodydefLeftWall.position.Set(5/GlobalData.iMetersToPixelsRatio, 200/GlobalData.iMetersToPixelsRatio);
			
			var bodyLeftWall:b2Body;
			bodyLeftWall = wMyWorld.CreateBody(bodydefLeftWall);
			bodyLeftWall.CreateFixture(fixturedefLeftWall);
			
			/*//////////////////////////////////////////////////////////////////////////////////////////////////
			//create the right wall
			//////////////////////////////////////////////////////////////////////////////////////////////////*/
			var polygonshapeRightWall:b2PolygonShape = new b2PolygonShape();
			polygonshapeRightWall.SetAsBox(5/GlobalData.iMetersToPixelsRatio , 480/GlobalData.iMetersToPixelsRatio);
			
			var fixturedefRightWall:b2FixtureDef = new b2FixtureDef();
			fixturedefRightWall.density = 0.0;
			fixturedefRightWall.restitution = 0.2;
			fixturedefRightWall.friction = 0.5;
			fixturedefRightWall.shape = polygonshapeRightWall;
			
			var bodydefRightWall:b2BodyDef = new b2BodyDef();
			bodydefRightWall.type = b2Body.b2_staticBody;
			bodydefRightWall.userData = "right wall";
			bodydefRightWall.position.Set(635/GlobalData.iMetersToPixelsRatio, 200/GlobalData.iMetersToPixelsRatio);
			
			var bodyRightWall:b2Body;
			bodyRightWall = wMyWorld.CreateBody(bodydefRightWall);
			bodyRightWall.CreateFixture(fixturedefRightWall);
			
			//debug info
			var dbgDraw:b2DebugDraw = new b2DebugDraw();
			var m_sprite:Sprite = new Sprite();
			sMyStage.addChild(m_sprite);
			dbgDraw.SetSprite(m_sprite);
			dbgDraw.SetDrawScale(GlobalData.iMetersToPixelsRatio);
			dbgDraw.SetFillAlpha(0.3);
			dbgDraw.SetLineThickness(1.0);
			dbgDraw.SetFlags(b2DebugDraw.e_shapeBit | b2DebugDraw.e_jointBit);
			wMyWorld.SetDebugDraw(dbgDraw);
			
			//add event listenters to the stage
			sMyStage.addEventListener(Event.ENTER_FRAME, Update);
			sMyStage.addEventListener(KeyboardEvent.KEY_DOWN, KeyInput);
		}

		/*******************************************************************************/
		/*
			Description: 
				Update()'s main purpose is to create and initialize
				objects used in the game, as well as add event listeners.
		
			Parameters:
				- _event:Event - used to keep track of every logic update
		
			Return: 
				- None
		*/
		/*******************************************************************************/
		public function Update(_event:Event):void
		{
			//setting frame rate and the amount of times to check for accuracy
			wMyWorld.Step(1.0/24.0 , 10 , 10);
			
			//generate balls randomly
			GenerateBallsTop();
			GenerateBallsBot();
			
			//clear forces for accurate calcuations
			wMyWorld.ClearForces();
			
			//destroy dead physics objects
			DestroyDeadPhysicsObjects();
			
			//draw the debug info/shapes
			wMyWorld.DrawDebugData();
			
			//increment ui timer
			++uiTimer;
		}
		
		/*******************************************************************************/
		/*
			Description: 
				GenerateBallsTop()'s main purpose is to create a ball at the top of the
				stage at either the left or right depending on a random variable.
		
			Parameters:
				- None
		
			Return: 
				- None
		*/
		/*******************************************************************************/
		public function GenerateBallsTop():void
		{
			//if 2 seconds have passed (48 frames)
			if(uiTimer % 48 == 0)
			{
				//random variable (either 1 or 2) to decide where ball should spawn from the left or right.
				var iRandomizer:int = (Math.floor(Math.random() * (2 - 1 + 1)) + 1);
					
				//push the ball into aObjects
				aObjects.push(new Ball());
				//add newly created ball in aObjects to the stage
				sMyStage.addChild(aObjects[aObjects.length - 1]);
				
				var circleshapeBall:b2CircleShape = new b2CircleShape();
				circleshapeBall.SetRadius(10/GlobalData.iMetersToPixelsRatio);
				
				//create the ball fixture (physics properties)
				var fixturedefBall:b2FixtureDef = new b2FixtureDef();
				fixturedefBall.density = 0;
				fixturedefBall.restitution = 0.7;
				fixturedefBall.friction = 0.2;
				fixturedefBall.shape = circleshapeBall;
				
				//create the body def
				var bodydefBall:b2BodyDef = new b2BodyDef();
				bodydefBall.type = b2Body.b2_dynamicBody;
				bodydefBall.userData = "ball";
				bodydefBall.position.Set(600/GlobalData.iMetersToPixelsRatio, 440/GlobalData.iMetersToPixelsRatio);
				if(iRandomizer == 1)
				{
					bodydefBall.position.Set(40/GlobalData.iMetersToPixelsRatio, 440/GlobalData.iMetersToPixelsRatio);
				}
				
				//create the body and add it to the world
				aObjects[aObjects.length - 1].b2bodyMyBody = wMyWorld.CreateBody(bodydefBall);
				aObjects[aObjects.length - 1].b2bodyMyBody.CreateFixture(fixturedefBall);
				aObjects[aObjects.length - 1].b2bodyMyBody.ApplyForce(new b2Vec2(-100.0, 15.0), aObjects[aObjects.length - 1].b2bodyMyBody.GetPosition());
			}
		}
		/*******************************************************************************/
		/*
			Description: 
				GenerateBallsTop()'s main purpose is to create a ball at the bottom of 
				the stage at either the left or right depending on a random variable.
		
			Parameters:
				- None
		
			Return: 
				- None
		*/
		/*******************************************************************************/
		public function GenerateBallsBot():void
		{
			//if 2 seconds have passed (48 frames)
			if(uiTimer % 48 == 0)
			{
				//random variable (either 1 or 2) to decide where ball should spawn from the left or right.
				var iRandomizer:int = (Math.floor(Math.random() * (2 - 1 + 1)) + 1);
					
				//push the ball into aObjects
				aObjects.push(new Ball());
				//add newly created ball in aObjects to the stage
				sMyStage.addChild(aObjects[aObjects.length - 1]);
				
				var circleshapeBall:b2CircleShape = new b2CircleShape();
				circleshapeBall.SetRadius(10/GlobalData.iMetersToPixelsRatio);
				
				//create the ball fixture (physics properties)
				var fixturedefBall:b2FixtureDef = new b2FixtureDef();
				fixturedefBall.density = 0;
				fixturedefBall.restitution = 0.7;
				fixturedefBall.friction = 0.2;
				fixturedefBall.shape = circleshapeBall;
				
				//create the body def
				var bodydefBall:b2BodyDef = new b2BodyDef();
				bodydefBall.type = b2Body.b2_dynamicBody;
				bodydefBall.userData = "ball";
				bodydefBall.position.Set(600/GlobalData.iMetersToPixelsRatio, 40/GlobalData.iMetersToPixelsRatio);
				if(iRandomizer == 1)
				{
					bodydefBall.position.Set(40/GlobalData.iMetersToPixelsRatio, 40/GlobalData.iMetersToPixelsRatio);
				}
				
				//create the body and add it to the world
				aObjects[aObjects.length - 1].b2bodyMyBody = wMyWorld.CreateBody(bodydefBall);
				aObjects[aObjects.length - 1].b2bodyMyBody.CreateFixture(fixturedefBall);
				aObjects[aObjects.length - 1].b2bodyMyBody.ApplyForce(new b2Vec2(-100.0, 15.0), aObjects[aObjects.length - 1].b2bodyMyBody.GetPosition());
			}
		}
		
		/*******************************************************************************/
		/*
			Description: 
				DestroyDeadPhysicsObjects()'s main purpose is destroy objects if their
				bIsDead is set to true.
		
			Parameters:
				- None
		
			Return: 
				- None
		*/
		/*******************************************************************************/
		public function DestroyDeadPhysicsObjects():void
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
		
		/*******************************************************************************/
		/*
			Description: 
				KeyInput is used to move the player object based on passed keyboard keys
		
			Parameters:
				- _ke:KeyboardEvent - pass in keyboard keys
		
			Return: 
				- None
		*/
		/*******************************************************************************/
		public function KeyInput(_ke:KeyboardEvent):void
		{
			//when player pressed keyboard key "left"
			if(_ke.keyCode == Keyboard.LEFT)
			{
				//set variable pos to bodyPlayer.GetPosition()
				var pos:b2Vec2 = thePlayer.b2bodyMyBody.GetPosition();
				//allow movement if player is within bounds
				if(pos.x > 25 / GlobalData.iMetersToPixelsRatio)
				{
					pos.x -= 1;
					thePlayer.b2bodyMyBody.SetPosition(pos);
					
					//trace("player is at = " + (pos.x / iMeterPixelRatio) + ", " + (pos.y / iMeterPixelRatio));
				}
			}
			//when player pressed keyboard key "right"
			if(_ke.keyCode == Keyboard.RIGHT)
			{
				//set variable pos to bodyPlayer.GetPosition()
				pos = thePlayer.b2bodyMyBody.GetPosition();
				//allow movement if player is within bounds
				if(pos.x < 610 / GlobalData.iMetersToPixelsRatio)
				{
					pos.x += 1;
					thePlayer.b2bodyMyBody.SetPosition(pos);
					
					//trace("player is at = " + (pos.x / iMeterPixelRatio) + ", " + (pos.y / iMeterPixelRatio));
				}
			}
			//when player pressed keyboard key "up"
			if(_ke.keyCode == Keyboard.UP)
			{
				//set variable pos to bodyPlayer.GetPosition()
				pos = thePlayer.b2bodyMyBody.GetPosition();
				//allow movement if player is within bounds
				if(pos.y > 20 / GlobalData.iMetersToPixelsRatio)
				{
					pos.y -= 1;
					thePlayer.b2bodyMyBody.SetPosition(pos);
					
					//trace("player is at = " + (pos.x / iMeterPixelRatio) + ", " + (pos.y / iMeterPixelRatio));
				}
			}
			//when player pressed keyboard key "down"
			if(_ke.keyCode == Keyboard.DOWN)
			{
				//set variable pos to bodyPlayer.GetPosition()
				pos = thePlayer.b2bodyMyBody.GetPosition();
				//allow movement if player is within bounds
				if(pos.y < 450 / GlobalData.iMetersToPixelsRatio)
				{
					pos.y += 1;
					thePlayer.b2bodyMyBody.SetPosition(pos);
					
					//trace("player is at = " + (pos.x / iMeterPixelRatio) + ", " + (pos.y / iMeterPixelRatio));
				}
			}	
		}
	}
}