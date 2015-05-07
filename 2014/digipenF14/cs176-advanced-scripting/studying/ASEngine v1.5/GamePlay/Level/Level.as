/***************************************************************************************/
/*
	filename   	Level.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		09/12/2013 
	
	brief:
	This class contains is testing the XMLManager
*/        	 
/***************************************************************************************/
package GamePlay.Level
{
	import Engine.*;
	import flash.ui.Keyboard;
	
	public class Level extends State
	{
		override public function Load():void
		{
			XMLManager.LoadXML("GamePlay/Level/Level.xml", "Level");
		}
		
		override public function Create():void
		{
			var xml:XML = XMLManager.GetXML("Level");
			trace(xml);
		}
	}
}
