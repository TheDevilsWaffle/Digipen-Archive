/***************************************************************************************/
/*
	filename   	EngineGlobals.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date    	9/02/2013
	
	brief:
	This class is accessible from any file and contains constant values that will be used
	in the game engine.
*/        	 
/***************************************************************************************/
package Engine
{
	final public class HelperFunctions
	{
		static internal function GetRandom(nMin:Number, nMax:Number):Number
		{
			return Math.random() * (nMax - nMin) + nMin;
		}
	}
}
