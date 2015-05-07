/***************************************************************************************/
/*
	filename   	SoundChannelObject.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date		12/07/2014 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.media.SoundChannel;
	import flash.media.Sound;
	
	final public class SoundChannelObject extends SoundChannel
	{
		var sSoundChannelName:String;
		var soSound:SoundObject;
		var uiPlayPosition:uint;
		var uiLoops:uint;
		
		
		public function SoundChannelObject(soSound_:SoundObject,
										   sSoundName_:String,
										   uiPlayPosition_:uint,
										   uiLoops_:uint)
		{
			// constructor code
			soSound = soSound_;
			sSoundChannelName = sSoundName;
			uiPlayPosition = uiPlayPosition_;
			uiLoops = uiLoops;
			
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
				PlaySound()
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function PlaySound():void
		{
			//destroy code here
		}
		
		/*******************************************************************************/
		/*
			Description:
				StopSound()
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function StopSound(so_:SoundObject):void
		{
			//destroy code here
			
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