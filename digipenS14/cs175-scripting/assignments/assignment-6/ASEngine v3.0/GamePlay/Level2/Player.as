/***************************************************************************************/
/*
	filename   	Player.as 
	author		Travis Moore, Elie Abi Chahine
	email   	travis.moore@digipen.edu, abichahine@digipen.edu
	date		04/01/2014 
	
	brief:
	This class handles the player's movement and keeps track of its health value
*/        	 
/***************************************************************************************/
package GamePlay.Level2
{
	//import necessary files
	import Engine.InputManager;
	import Engine.ObjectManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import GamePlay.MainMenu.MainMenu;
	import GamePlay.GamePlayGlobals;
	import Engine.CollisionInfo;
	import flash.ui.Keyboard;
	import flash.text.TextField;
	import flash.display.DisplayObject;
	import flash.display.MovieClip;
	

	public class Player extends GameObject
	{
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
		public var nSpeed:Number;
		public var iPlayerColor:int;
		
		/*******************************************************************************/
		/*
			Description:
				Setup the player based on passed variables
				
			Parameters:
				- nPosX_:Number - x position 
				- nPosY_:Number - y position
				- nSpeed_:Number - speed
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Player(nPosX_:Number, nPosY_:Number, nSpeed_:Number):void
		{
			//super the parent class
			super(new player, nPosX_, nPosY_, GamePlayGlobals.GO_PLAYER);
			
			//set position based on passed parameters
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
			//set speed based on passed parameters
			nSpeed = nSpeed_;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Set the Player objects attributes
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Initialize():void
		{
			//set Player's x and y position
			displayobject.x = nInitialPosX;
			displayobject.y = nInitialPosY;
			
			//use the RandomColor() function to set a color
			RandomColor();
			
			//set bIsDead to false so player is alive
			bIsDead = false;
		}
		/*******************************************************************************/
		/*
			Description:
				Picks a random color for the player
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function RandomColor():void
		{
			//set iPlayerColor to a random color between 1 and 3
			iPlayerColor = (Math.floor(Math.random() * (3 - 1 + 1)) + 1);
			
			//set the movieclip to the correct color
			MovieClip(displayobject).gotoAndStop(iPlayerColor);
		}
		/*******************************************************************************/
		/*
			Description:
				The player's update loop, listens to keypresses and plays death
				animation if the player is set to be dead
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			//if player presses up
			if(InputManager.IsPressed(Keyboard.UP))
			{
				if(displayobject.y > 0)
				{
					displayobject.y -= nSpeed;
				}
			}
			//if player presses down
			else if(InputManager.IsPressed(Keyboard.DOWN))
			{
				if(displayobject.y < 480)
				{
					displayobject.y += nSpeed;
				}
			}
			//if player presses left
			if(InputManager.IsPressed(Keyboard.LEFT))
			{
				if(displayobject.x > 0)
				{
					displayobject.x -= nSpeed;
				}
			}
			//if player presses right
			else if(InputManager.IsPressed(Keyboard.RIGHT))
			{
				if(displayobject.x < 640)
				{
					displayobject.x += nSpeed;
				}
			}
			//kill player if death animation/currentframe is beyond 15
			if(MovieClip(displayobject).currentFrame >= 15)
			{
				//set the player to dead
				bIsDead = true;
				//let the level know the game is over
				Level2.bGameOver = true;
			}
		}
		/*******************************************************************************/
		/*
			Description:
				collision reaction for the player, based on if it collides with a certain
				object type
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			//create a variable to stand for a dot enemy
			var dot = ObjectManager.GetObjectByName("Dot" , ObjectManager.OM_DYNAMICOBJECT)
			
			//if a gameobjectCollidedWith is a type
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				//if it is a dot
				case GamePlayGlobals.GO_DOT:
				{
					//check to see if the colors match
					if(MovieClip(displayobject).currentFrame == MovieClip(CInfo_.gameobjectCollidedWith.displayobject).currentFrame )
					{
						//increment score by 1
						Level2.iScore += 1;
						//update text object for score
						var gameobjectScore:GameObject = ObjectManager.GetObjectByName("ScoreText" , ObjectManager.OM_STATICOBJECT);
						//set textfield
						TextField(gameobjectScore.displayobject).text = "Score = " + String(Level2.iScore); 
					}
					//the player is not the same color as the dot
					else
					{
						//start death animation
						MovieClip(displayobject).gotoAndPlay(4);
					}
				}
				break;
			}
		}
	}
}