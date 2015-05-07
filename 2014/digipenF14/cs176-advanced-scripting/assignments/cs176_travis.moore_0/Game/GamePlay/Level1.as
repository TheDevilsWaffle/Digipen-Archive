/***************************************************************************************/
/*
	filename   	Level1.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu, eabichahine@digipen.edu
	date		02/18/2014
	
	brief:
	This is a test level to see if we can create(), initialize(), update(),
	uninitialize(), and destroy()

*/        	 
/***************************************************************************************/
package GamePlay
{
	import Engine.ObjectManager;
	import Engine.InputManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import Engine.State;
	import Engine.ObjectManager;
	import flash.ui.Keyboard;
	import flash.display.DisplayObject;
	import flash.text.TextField;
	import flash.display.MovieClip;
	
	public class Level1 extends State
	{		
	
		static public var iGameTimer:int;
		static public var iFuel:int;
		static public var iScore:int;
		/**************************************************************************
		/*
			Description:
				Empty constructor
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/
		public function Level1()
		{

		}
		
		/**************************************************************************
		/*
			Description:
				Used to trace out a create message
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/		
		override public function Create():void
		{
			trace("Level 1 - Created");
			//ADD IN UI ELEMENTS
			//create ui first, so it's on the back layer
			ObjectManager.AddObject(new ui(-1, 0), "the_ui", ObjectManager.OM_STATICOBJECT);
			//create text to represent the current score
			var tfScore:TextField = new TextField();
			tfScore.scaleX = 2;
			tfScore.scaleY = 2;
			tfScore.textColor = 0xFF9900;
			tfScore.text = "Score = 0";
			//give score text to object manager and add it to the level
			ObjectManager.AddObject(new GameObject(tfScore, 620, 8, GamePlayGlobals.GO_TEXT), "the_score_text" , ObjectManager.OM_STATICOBJECT);
			
			//create text to represent the current fuel
			var tfFuel:TextField = new TextField();
			tfFuel.scaleX = 2;
			tfFuel.scaleY = 2;
			tfFuel.textColor = 0xFF9900;
			tfFuel.text = "Fuel = 100";
			//give fuel text to object manager and add it to the level
			ObjectManager.AddObject(new GameObject(tfFuel, 30, 8, GamePlayGlobals.GO_TEXT), "the_fuel_text" , ObjectManager.OM_STATICOBJECT);
			
			//create the base, give it a starting x, y, call it "the_player", and define it as a static object
			ObjectManager.AddObject(new base(300, 00),"the_base" , ObjectManager.OM_DYNAMICOBJECT);
			
			//create the fuel, give it a random x, random y, call it "the_fuel", and define it as a dynamic object
			ObjectManager.AddObject(new fuel((20 + (740 - 20) * Math.random()),(50 + (500 - 50) * Math.random())),"the_fuel" , ObjectManager.OM_DYNAMICOBJECT);
			
			//create the star, give it a random x, random y, call it "the_fuel", and define it as a dynamic object
			ObjectManager.AddObject(new star((20 + (740 - 20) * Math.random()),(50 + (500 - 50) * Math.random())),"the_star" , ObjectManager.OM_DYNAMICOBJECT);
			
			//create the missile, give it a starting x, y, speed, a rotation speed, call it "the_missile", and define it as a dynamic object
			ObjectManager.AddObject(new missile(700, 500, 3, 3),"the_missile" , ObjectManager.OM_DYNAMICOBJECT);
			
			//create player, give it a starting x, y, velocity, max speed, rotation speed, drag factor, call it "the_player", and define it as a dynamic object
			ObjectManager.AddObject(new player(100, 100, 1, 5, 5, 0.9),"the_player" , ObjectManager.OM_DYNAMICOBJECT);
		}
		
		/**************************************************************************
		/*
			Description:
				Used to trace out an initialize message
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/	
		override public function Initialize():void
		{
			//
			trace("Level 1 - Initialized");
			trace("Press 'r' to reload.\nPress '2' to load level 2\nPress 'Space' to quit.");
			
			//INITIALIZE Level1 variables
			iScore = 0;
			iFuel = 100;
			iGameTimer = 0;
			
			//set the score equal to 0
			TextField((ObjectManager.GetObjectByName("the_score_text" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Score = 0";
			//set the fuel to default level (100)
			TextField((ObjectManager.GetObjectByName("the_fuel_text" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Fuel = 100";
		}
		
		/**************************************************************************
		/*
			Description:
				Used to keep track of key presses
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/	
		override public function Update():void
		{
			//trace("Level 1 - Updated");
			//check if user pressed keyCode "r"
			if(InputManager.IsPressed(82))
			{
				GameStateManager.RestartState();
			}
			//check if user pressed keyCode "space"
			if(InputManager.IsPressed(32))
			{
				GameStateManager.Quit();
			}
			
			//update the game timer
			++iGameTimer;
			
			
			//update the fuel text based on time elapsed from iGameTimer
			if(iGameTimer % 12 == 0)
			{
				//check for player out of fuel
				if(iFuel != 0)
				{
					//decrement the fuel
					--iFuel;
				}
				
				//update the fuel text ui
				TextField((ObjectManager.GetObjectByName("the_fuel_text" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Fuel = " + iFuel;
				//update the score text ui
				TextField((ObjectManager.GetObjectByName("the_score_text" , ObjectManager.OM_STATICOBJECT)).displayobject).text = "Score = " + iScore;	
			}
			
			
		}
		
		/**************************************************************************
		/*
			Description:
				Used to trace out an uninitialize message
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/	
		override public function Uninitialize():void
		{
			trace("Level 1 - Uninitialized");
		}
		
		/**************************************************************************
		/*
			Description:
				Used to trace out a destroyed message
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/	
		override public function Destroy():void
		{
			trace("Level 1 - Destroyed");
		}
	}
}
