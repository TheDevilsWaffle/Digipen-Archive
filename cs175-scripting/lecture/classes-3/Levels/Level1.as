package Levels
{
	import GameObjects.*;
	import flash.display.Stage;
	import flash.events.KeyboardEvent;

	public class Level1
	{
		//create a private variable to access the Stage
		private var sStage:Stage;
		//create a private array to hold enemies
		private var aEnemies:Array;
		//create a hero variable of type Hero
		private var GoodGuy:Hero;

		//constructor: pass in Stage-(complex type, will change all instances of Stage)
		public function Level1(_sStage:Stage):void
		{
			//link sStage to passed variable
			sStage = _sStage;

			//create variable for hero, add to the stage using newly created sStage
			GoodGuy = new Hero(100,240);
			sStage.addChild(GoodGuy);
			
			//create variable for hero, add to the stage using newly created sStage
			//public var BadGuy:Enemy = new Enemy(540,240);
			//sStage.addChild(BadGuy);
			
			//initialize array, put 3 enemies inside an array
			aEnemies = new Array();
			aEnemies.push(new Enemy(540,100,5));
			aEnemies.push(new Enemy(540,260,2));
			aEnemies.push(new Enemy(540,420,7));
			//add aEnemies to the stage using a for loop
			for(var iIndex:int = 0; iIndex < aEnemies.length; ++iIndex)
			{
				sStage.addChild(aEnemies[iIndex]);
			}
			
			sStage.addEventListener(KeyboardEvent.KEY_DOWN, InputManager);
		}
		public function InputManager(_keyboard:KeyboardEvent):void
		{
			GoodGuy.Movement(_keyboard)
		}
	}
}