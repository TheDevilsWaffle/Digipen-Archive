/***************************************************************************************/
/*
	filename   	ShipSP.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu, eabichahine@digipen.edu
	date		04/18/2014 
	
	brief:
		This class handles the walls of a horizontal nature
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
	
	import Box2D.Dynamics.*;
	import Box2D.Collision.Shapes.*;
	import Box2D.Common.Math.*;
	import flash.events.Event;
	

	public class HorizontalWall extends GameObject
	{
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
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
		public function HorizontalWall(nPosX_:Number, nPosY_:Number):void
		{
			super(new wallhorizontal(), nPosX_, nPosY_, GamePlayGlobals.GO_WALL);
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
			displayobject.x = nInitialPosX;
			displayobject.y = nInitialPosY;

			//set physics to true on this object
			bPhysics = true;
			
			//set all the physics attributes for a horizontal wall
			var polygonshape:b2PolygonShape = new b2PolygonShape();
			polygonshape.SetAsBox(390/iMeterToPixelsRatio , 10/iMeterToPixelsRatio);
			
			fixturedef = new b2FixtureDef();
			fixturedef.density = 1000000.0;
			fixturedef.restitution = 1.0;
			fixturedef.friction = 1.0;
			fixturedef.shape = polygonshape;
			
			bodydef = new b2BodyDef();
			//The ground's type is b2_staticBody meaning it won't move
			bodydef.type = b2Body.b2_dynamicBody;
			bodydef.userData = "horizontalwall";
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
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				case GamePlayGlobals.GO_SHIP:
				{
					//play bump sound
					var bumpSound = new bump;
					bumpSound.play()
				}
				break;
			}
		}
	}
}