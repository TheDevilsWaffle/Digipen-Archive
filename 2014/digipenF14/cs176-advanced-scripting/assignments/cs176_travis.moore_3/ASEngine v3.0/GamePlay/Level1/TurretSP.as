/***************************************************************************************/
/*
	filename   	TurretSP.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		09/10/2013
	
	brief:
	This class handles the turret's behavior (movement, shooting, collision, etc...)
*/        	 
/***************************************************************************************/
package GamePlay.Level1
{
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	import GamePlay.MainMenu.MainMenu;
	import Engine.CollisionManager;
	import flash.ui.Keyboard;
	import flash.geom.Point;

	public class TurretSP extends GameObject
	{
		private var nRotationSpeed:Number;
		private var pOriginalDirection:Point;
		private var pCurrentDirection:Point;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor responsible for creating/initializing the turret's variables.
			
			Parameters:
				- nPosX_: turret's initial x position
				
				- nPosY_: turret's initial y position
				
				- nRotationSpeed_: turret's rotation speed, handles how fast the turret 
								   rotates
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function TurretSP(nPosX_:Number, nPosY_:Number, nRotationSpeed_:Number)
		{
			super(new Turret(),nPosX_, nPosY_);
			iCollisionType = CollisionManager.CO_DYNAMIC;
			nRotationSpeed = nRotationSpeed_;
			iType = 0;
			pOriginalDirection = new Point();
			pCurrentDirection = new Point();
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for initializing some of the turret's 
				variables to their initial values. Used mainly when the level restarts.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Initialize():void
		{
			displayobject.rotation = 0;
			pOriginalDirection.x = 0;
			pOriginalDirection.y = -1;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method updates the turret's direction variable. It uses the turret's
				current angle value in order to compute the direction. The direction is
				needed when the turret shoots a bullet in order to know in which 
				direction the bullet should go.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function UpdateCurrentDirection():void
		{	
			var nCos:Number = Math.cos(displayobject.rotation * Math.PI/180);
			var nSin:Number = Math.sin(displayobject.rotation * Math.PI/180);

			pCurrentDirection.x = (pOriginalDirection.x)*nCos - (pOriginalDirection.y)*nSin;
			pCurrentDirection.y = (pOriginalDirection.y)*nCos - (pOriginalDirection.x)*nSin;
			
			pCurrentDirection.normalize(1);
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for shooting a bullet. It is triggered when 
				the user presses space. When called, the turret's direction vector is 
				updated, a bullet is created, initialized and added to the ObjectManager.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		private function Shoot():void
		{
			UpdateCurrentDirection();
			ObjectManager.AddObject(new BulletSP(displayobject.x + pCurrentDirection.x*120,
												 displayobject.y + pCurrentDirection.y*120,
												 5, pCurrentDirection), "Bullet", 
												 ObjectManager.OM_DYNAMICOBJECT);
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for the turret's main behavior. By pressing
				Left or Right the user can rotate the turret. Pressing space shoots a
				new bullet.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Update():void
		{
			if(InputManager.IsPressed(Keyboard.LEFT) && displayobject.rotation > -90)
			{
				displayobject.rotation -= nRotationSpeed;
			}
			else if(InputManager.IsPressed(Keyboard.RIGHT) && displayobject.rotation < 90)
			{
				displayobject.rotation += nRotationSpeed;
			}
			
			if(InputManager.IsTriggered(Keyboard.SPACE))
			{
				Shoot();
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for the turret's reaction when collided with
				other objects. By colliding with one of the enemies, the player loses 
				and is taken to the MainMenu.
			
			Parameters:
				- CInfo_: Contains all the collision information.
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function CollisionReaction(CInfo_:CollisionInfo):void
		{
			switch(CInfo_.gameobjectCollidedWith.iType)
			{
				case GamePlayGlobals.GO_ENEMY:
				{
					GameStateManager.GotoState(new MainMenu());
				}
				break;
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method is responsible for destroying the turret's variables.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		override public function Destroy():void
		{
			pOriginalDirection = null;
			pCurrentDirection = null;
		}
	}
}