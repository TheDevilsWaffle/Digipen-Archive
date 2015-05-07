/***************************************************************************************/
/*
	filename   	SoundObject.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date		12/07/2014 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.media.Sound;

	final public class SoundObject
	{
		var sName:String;
		var soSound:Sound;
		
		public function SoundObject(soSound_:Sound,
									sName_:String)
		{
			// constructor code
			sName = sName_;
			soSound = soSound;
		}
		
		/*******************************************************************************/
		/*
			Description:
				Initialize()
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Initialize():void
		{
			//initialize code here
		}
		
		/*******************************************************************************/
		/*
			Description:
				Destroy()
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Destroy():void
		{
			//destroy code here
		}
	}
}