/***************************************************************************************/
/*
	filename   	HotSpot.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:
	This class contains the information that a hotspot has.
*/        	 
/***************************************************************************************/
package Engine
{
	import flash.geom.Point;
	
	public class HotSpot
	{
		static public var HS_NO_SIDE:int = 0;
		static public var HS_UP:int = 1;
		static public var HS_DOWN:int = 2;
		static public var HS_LEFT:int = 3;
		static public var HS_RIGHT:int = 4;
		
		/* Position of the hotspot */
		public var pPosition:Point;
		/* The TileID that the hotspot is colliding with  */
		public var iCollidedTilesID:int;
		/* The side where the hotspot is located (HS_UP , HS_DOWN , HS_LEFT , HS_RIGHT).
		   With that information we know how to snap the object */
		public var iSide:int;
		
		/**************************************************************************
		/*
			Description:
				Constructor that initializes the variables inside the hotspot class
			
			Parameters:
				- None
			
			Return:
				- None
		*/
		/*************************************************************************/
		public function HotSpot()
		{
			pPosition = new Point();
			iCollidedTilesID = -1;
			iSide = HS_NO_SIDE;
		}
	}

}