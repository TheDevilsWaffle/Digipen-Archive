/***************************************************************************************/
/*
	filename   	Dot.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	This class handles the enemy's creation, movement , collision recation and destruction.
*/        	 
/***************************************************************************************/
package GamePlay.Level2
{
	//import the necessary files
	import Engine.ObjectManager;
	import Engine.GameObject;
	import GamePlay.GamePlayGlobals;
	import Engine.CollisionInfo;
	import flash.text.TextField;
	import flash.display.DisplayObject;
	import flash.display.MovieClip;
	
	public class Dot extends GameObject
	{		
		//create variables to keep track of speed, position, and dot color
		public var nSpeed:Number;
		public var nPosY:Number;
		public var nPosX:Number;
		public var iDotColor:int;
		
		/*******************************************************************************/
		/*
			Description:
				Used to set up the dot enemy based on passed variables
				
			Parameters:
				- nPosX_:Number - x position 
				- nPosY_:Number - y position
				- nSpeed_:Number - speed
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Dot(nPosX_:Number, nPosY_:Number, nSpeed_:Number)
		{
			//super the parent class, add in x and y center of the screen
			super(new dot() , nPosX_ + 320, nPosY_ + 240, GamePlayGlobals.GO_DOT);
			
			//store passed variable for x
			nPosX = nPosX_;
			//store passed variable for y
			nPosY = nPosY_;
			//sore passed variable for speed
			nSpeed = nSpeed_;
			
			//set x position divided by magnitude (radius) of circle
			nPosX /= -400;
			//set y position divided by magnitude (radius) of circle
			nPosY /= -400;
			
			//use the RandomColor() function to get a color
			RandomColor()
		}
		
		/*******************************************************************************/
		/*
			Description:
				Update loop for the dot, basically it moves the dot to the center of the
				screen and kills dots that are off the screen.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			//move the dots based on their nPosX and nSpeed
			displayobject.x += nPosX * nSpeed;
			//move the dots based on their nPosX and nSpeed
			displayobject.y += nPosY * nSpeed;
			
			//destroy the dots if they move beyond the boundries of the dots
			if( displayobject.x < -200 || displayobject.x > 800 || displayobject.y < -200 || displayobject.y > 800 )
			{
				//set the bIsDead to true
				bIsDead = true;
			}
		}
		/*******************************************************************************/
		/*
			Description:
				Picks a random color for the dot
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function RandomColor():void
		{
			//set the iDotColor based on a random number between 1 and 3
			iDotColor = (Math.floor(Math.random() * (3 - 1 + 1)) + 1);
			
			//set the movieclip to the correct color
			MovieClip(displayobject).gotoAndStop(iDotColor);
		}
		
		/*******************************************************************************/
		/*
			Description:
				Function responsible for what happens when a dot collides with the
				player (colliding with other dots is not an option).
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			//search through CInfo_
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				//if CInfo_'s gameobjectCollidedWith.iType a Player
				case GamePlayGlobals.GO_PLAYER:
				{
					//set the dot to true
					bIsDead = true;
				}
				break;
			}
		}
	}
}