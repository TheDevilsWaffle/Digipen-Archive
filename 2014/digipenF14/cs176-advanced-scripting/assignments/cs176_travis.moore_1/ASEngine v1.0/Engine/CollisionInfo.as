/***************************************************************************************/
/*
	filename   	CollisionInfo.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		09/05/2013 
	
	brief:
	This class contains all the collision information that should be stored when two
	dynamic objects collide.
*/        	 
/***************************************************************************************/
package Engine
{
	final public class CollisionInfo
	{
		/* A reference to the object colliding */
		public var gameobject:GameObject;
		/* A reference to the object collided with */
		public var gameobjectCollidedWith:GameObject;
		
		/**************************************************************************
		/*
			Description:
				Constructor that initializes the collision info's variables
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*************************************************************************/
		final public function CollisionInfo()
		{
			gameobject = null;
			gameobjectCollidedWith = null;
		}
	}
}