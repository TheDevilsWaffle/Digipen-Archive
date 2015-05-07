/***************************************************************************************/
/*
	filename   	TurretMC.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		09/09/2014
	
	brief:
	This class handles the properties and methods of the player controlled turret.
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import Engine.InputManager;
	import Engine.ObjectManager;
	import Engine.GameStateManager;
	import Engine.GameObject;
	import GamePlay.GamePlayGlobals;
	import GamePlay.MainMenu.MainMenu;
	import Engine.CollisionInfo;
	import flash.ui.Keyboard;
	import flash.text.TextField;

	public class TurretMC extends GameObject
	{
		public var nInitialPosX:Number;
		public var nInitialPosY:Number;
		public var nRotationSpeed:Number;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor used to set attributes based on passed parameters.
				
			Parameters:
				- nPosX_:Number         - Set the starting x for the object.
				- nPosY_:Number         - Set the starting y for the object.
				- nSpeed_:Number        - Set the starting speed for the object.
				- nRotationSpeed:Number - Set the starting rotation speed for the object.
			
			Return:
				- None
		*/
		/*******************************************************************************/
		public function TurretMC(nPosX_:Number, nPosY_:Number, nRotationSpeed_:Number):void
		{
			super(new Turret(), nPosX_, nPosY_, GamePlayGlobals.GO_TURRET);
			nInitialPosX = nPosX_;
			nInitialPosY = nPosY_;
			nRotationSpeed = nRotationSpeed_;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Initialize() is used to set the initial starting values for the
				player's attributes.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Initialize():void
		{
			//store initial starting x and y values
			displayobject.x = nInitialPosX;
			displayobject.y = nInitialPosY;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Shoot() is used to create a bullet that shoots out from the player
				based on the current rotation of the player object.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		private function Shoot():void
		{
			//determine x and y origin point for bullet
			var nXBulletOrigin:Number = displayobject.x + Math.cos((displayobject.rotation - 90) * Math.PI/180) * 130;
			var nYBulletOrigin:Number = displayobject.y + Math.sin((displayobject.rotation - 90) * Math.PI/180) * 130;
			
			//create bullet, pass the player's x and y, set a speed, and pass the player's current rotation.
			ObjectManager.AddObject(new BulletMC(nXBulletOrigin, nYBulletOrigin, 10, displayobject.rotation), "Bullet" , ObjectManager.OM_DYNAMICOBJECT);
		}
		
		/*******************************************************************************/
		/*
			Description:
				Update() runs only when Level1.bLevelStarted == true. Used to apply
				actions to player input based upon which key is pressed.
				
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			//only start when countdown sets Level1.bLevelStarted to true
			if(Level1.bLevelStarted == true)
			{
				//Left key
				if(InputManager.IsPressed(Keyboard.LEFT) && displayobject.rotation > -90)
				{
					displayobject.rotation -= nRotationSpeed;
				}
				//Right key
				if(InputManager.IsPressed(Keyboard.RIGHT) && displayobject.rotation < 90)
				{
					displayobject.rotation += nRotationSpeed;
				}
				
				//Spacebar calls Shoot()
				if(InputManager.IsTriggered(Keyboard.SPACE))
				{
					Shoot();
				}
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				CollisionReaction() is used to determine the proper action to take for
				when this object collides with another object. The proper action to take
				depends upon the passed CInfo_ which connects to the GamePlayGlobals
				GO_ enums that we've created.
				
			Parameters:
				- CInfo_:CollisionInfo - an integer represeting the object that this
										 object has hit. This information corresponds
										 to the iType so that we can use GamePlayGlobals
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				//return to MainMenu if the player loses the game
				case GamePlayGlobals.GO_ENEMY:
				{
					GameStateManager.GotoState(new MainMenu());
				}
				break;
			}
		}
	}
}