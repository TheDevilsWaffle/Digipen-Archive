/****************************************************************************************
FILENAME   SoundManager.as 
AUTHOR     Travis Moore
EMAIL      travis.moore@digipen.edu
DATE       12/09/2014 
/***************************************************************************************/
package Engine 
{
	//IMPORT
	import flash.media.Sound;
	import flash.media.SoundChannel;
	import Engine.SoundObject;
	
	//CLASS SOUNDMANAGER
	final public class SoundManager 
	{
		//VARIABLES
		static private var vSoundObjects:Vector.<SoundObject>;  //contains all the sound objects
		
		/********************************************************************************
		FUNCTION- Constructor()
		/*******************************************************************************/
		public function SoundManager():void
		{
			//nothing to see here, move along
		}
		
		/********************************************************************************
		FUNCTION- Initialize()
		/*******************************************************************************/
		static internal function Initialize():void
		{
			//create the vector of sound objects
			vSoundObjects = new Vector.<SoundObject>();
		}
		
		/********************************************************************************
		FUNCTION- AddSoundObject()
		/*******************************************************************************/
		static public function AddSoundObject(soSound_:Sound, 
											  sName_:String, 
											  bIsLooping_:Boolean):void
		{
			//make sure we are sending a sound, just in case
			if(soSound_)
			{
				//create a new sound object, pass it all dem things
				var soSoundObject:SoundObject = new SoundObject(soSound_, sName_, bIsLooping_);
				//call this sound object's initialize function
				soSoundObject.Initialize();
				//push this sound object into vSoundObjects
				vSoundObjects.push(soSoundObject);
			}
		}
		
		/********************************************************************************
		FUNCTION- SoundObjectPause()
		/*******************************************************************************/
		static public function SoundObjectPause(bIsPaused_:Boolean):void
		{
			//get the current length of vSoundObjects (so we do not evaulate over and over again)
			var iVSoundObjectsLength:int = vSoundObjects.length;
			
			//loop
			for(var iIndex:int; iIndex < iVSoundObjectsLength; ++iIndex)
			{
				//if we're set to be paused
				if(bIsPaused_)
				{
					//call this sound object's pause function
					vSoundObjects[iIndex].SoundPause();
				}
			}
		}
		
		/********************************************************************************
		FUNCTION- SoundObjectResume()
		/*******************************************************************************/
		static public function ResumeSoundObject(bIsPaused_:Boolean):void
		{
			//get the current length of vSoundObjects (so we do not evaulate over and over again)
			var iVSoundObjectsLength:int = vSoundObjects.length;
			
			//loop
			for(var iIndex:int; iIndex < iVSoundObjectsLength; ++iIndex)
			{	
				//if we are not supposed to be paused
				if(!bIsPaused_)
				{
					//call this sound object's resome function
					vSoundObjects[iIndex].SoundResume();
				}
			}
		}
		
		/********************************************************************************
		FUNCTION- RemoveSoundObjectByName()
		/*******************************************************************************/
		static public function RemoveSoundObjectByName(sName_:String):void
		{
			//get the current length of vSoundObjects (so we do not evaulate over and over again)
			var iVSoundObjectsLength:int = vSoundObjects.length;
			
			//loop
			for(var iIndex:int; iIndex < iVSoundObjectsLength; ++iIndex)
			{	
				//if we got a name match
				if(vSoundObjects[iIndex].sName == sName_)
				{
					//do the remove from vector things
					vSoundObjects[iIndex].Uninitialize();
					vSoundObjects[iIndex] = null;
					vSoundObjects.splice(iIndex, 1);
				}
			}
		}
		
		/********************************************************************************
		FUNCTION- RemoveAllSoundObjects()
		/*******************************************************************************/
		static public function RemoveAllSoundObjects():void
		{
			//get the current length of vSoundObjects (so we do not evaulate over and over again)
			var iVSoundObjectsLength:int = vSoundObjects.length;
			
			//loop
			for(var iIndex:int; iIndex < iVSoundObjectsLength; ++iIndex)
			{	
				//do the remove from vector things 
				vSoundObjects[iIndex].Uninitialize();
				vSoundObjects[iIndex] = null;
				vSoundObjects.splice(iIndex, 1);
			}
		}
		
		/********************************************************************************
		FUNCTION- Destroy()
		/*******************************************************************************/
		static internal function Destroy():void
		{
			//do the remove all things from vector stuff
			RemoveAllSoundObjects();
			//now we null out the vector
			vSoundObjects = null
		}
	}
}