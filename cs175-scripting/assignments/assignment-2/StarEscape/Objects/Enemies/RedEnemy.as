/***************************************************************************************/
/*
	filename   	RedEnemy.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date    	01/28/2014 
	
	brief:
	This file contains the variables and functions that are a part of the RedEnemy class.
	It extends from the Enemy class and contains its constructor, AI(), and Destroy() 
	functions.
*/        	 
/***************************************************************************************/
package Objects.Enemies
{
	//import movieclip so we can access x and y properties
	import flash.display.MovieClip;
	//import events so we can listen for function AI()
	import flash.events.Event;
	
	public class RedEnemy extends Enemy
	{
		//integer that will let us know when should the object attack.
		private var iAttackX:int;
		
		//integer that will let us know which direction the object should attack
		private var iAttackYDir:int;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for initializing the RedEnemy with its variables
				and functions.
			
			Parameters:
				- nPosX_:Number - used to define the starting x position of the object
				- nPosY_:Number - used to define the starting y position of the object
				- iSpeed_:int - used to define the speed of the object
				- iDamage_:int - used to define how much damage the object does
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function RedEnemy(nPosX_:Number, nPosY_:Number, iSpeed_:int, iDamage_:int):void
		{
			//access Enemy paramters
			super(nPosX_, nPosY_, iSpeed_, iDamage_);
			
			//create iAttackXDir value
			iAttackX = 125 + (225 - 125) * Math.random();
			
			//create iAttackYDir value based on passed nPosY parameter
			if(super(nPosY_) < 200)
			{
				//move downward once iAttackX is reached
				iAttackYDir = 1;
			}
			//create iAttackYDir value based on passed nPosY parameter
			else
			{
				//move upward once iAttackX is reached
				iAttackYDir = -1
			}
			
			//add event listeners
			addEventListener(Event.ENTER_FRAME, AI);
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for movement of the RedEnemy object
			
			Parameters:
				- e_:Event - used to update every frame
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function AI(e_:Event):void
		{
			//move from right to left according to iSpeed
			x -= iSpeed;
			
			//change y-axis movement based on iAttackX and iAttackYDir == -1
			if(x <= iAttackX && iAttackYDir == 1)
			{
				//move downward according to iSpeed
				y += iSpeed;
			}
			//change y-axis movement based on iAttackX and iAttackYDir == -1
			if(x <= iAttackX && iAttackYDir == -1)
			{
				//move upward according to iSpeed
				y -= iSpeed;
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				function responsible for destroying the RedEnemy object
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function Destroy():void
		{
			//remove event listeners associated with RedEnemy
			removeEventListener(Event.ENTER_FRAME, AI)
			
			//update the uiNumberOfOjects variable
			--uiNumberOfObjects;
		}
	}
}