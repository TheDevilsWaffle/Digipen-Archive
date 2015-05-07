/***************************************************************************************/
/*
	filename   	SoundManager.as 
	author		Travis Moore
	email   	travis.moore@digipen.edu
	date		12/07/2014 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.media.Sound;
	import flash.net.URLRequest;

	final public class SoundManager
	{
		//vector of soundobjects
		static public var vSounds:Vector.<SoundObject>;
		//vector of soundchannelobjects
		static public var vSoundChannels:Vector.<SoundChannelObject>;
		
		/*******************************************************************************/
		/*
			Description:
				Constructor
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		public function SoundManager():void
		{
			// constructor code
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method initializes the sound manager by creating the vector that
				will hold all sound objects.
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function Initialize():void
		{
			//initialize the vector of soundobjects
			this.vSounds = new Vector.<SoundObject>;
			//initialize the vector of soundchannelobjects
			this.vSoundChannels = new Vector.<SoundChannelObject>;
		}
		
		/*******************************************************************************/
		/*
			Description:
				LoadSound()
			
			Parameters:
				- None
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function LoadSound(sSoundPath_:String, sName:String):void
		{
			//load sound code
			var urlReq:URLRequest = new URLRequest(sSoundPath_);
			//temp sound to hold passed sound
			var soSound:Sound = new Sound;
			//load the sound
			soSound.load(urlReq);
			//create a sound
			var soSoundObject:SoundObject = new SoundObject(soSound, sName);
			//push into sound vector
			this.vSounds.push(soSoundObject);
		}
		
		/*******************************************************************************/
		/*
			Description:
				CreateSoundChannel()
			
			Parameters:
				- soSoundObject_:Sound - SoundObject to play
				- sSoundName_:String:  - SoundObject name
				- uiPlayPosition_:unit - Where to start playing the sound (default 0)
				- uiLoops_:uint	 	   - How many times to loop the sound (default 1)
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function CreateSoundChannel(soSound_:SoundObject,
										   sSoundName_:String,
										   uiPlayPosition_:uint,
										   uiLoops_:uint):void
		{
			//create a new soundchannel and give it a specific sound
			var scSoundChannel:SoundChannelObject = new SoundChannelObject(soSound_,
										   								   sSoundName_,
										   								   uiPlayPosition_,
										   								   uiLoops_);
			//push this new sound channel to the vector of soundchannels
			vSoundChannels.push(scSoundChannel);
		}
		
		/*******************************************************************************/
		/*
			Description:
				PlaySound()
			
			Parameters:
				- sSoundToPlay_:String - name of the sound we want to play
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function PlaySound(sSoundToPlay_:String):void
		{
			/*
			IMPORTANT! loop though sound objects to find the sound we want to play, then loop though soundchannels,
			if the soundchannel is not currently playing, then give it the sound to play
			*/
			//find the length of the vSoundChannels
			var uiVSCLength:uint = this.vSoundChannels.length;
			//loop through available soundchannels
			for(var iIndex:int; iIndex < uiVSCLength; ++iIndex)
			{
				//if this soundchannel has the sound we want to play
				if(this.vSoundChannels[iIndex].sSoundChannelName == sSoundToPlay_)
				{
					//play its sound
					vSoundChannels[iIndex] = vSoundChannels[iIndex].soSound.play(vSoundChannels[iIndex].uiPlayPosition,
																				 vSoundChannels[iIndex].uiLoops);
				}
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				StopSound()
			
			Parameters:
				- sSoundToStop_:String - name of the sound we want to stop
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function StopSound(sSoundToStop_:String):void
		{
			//find the length of the vSoundChannels
			var uiVSCLength:uint = this.vSoundChannels.length;
			//loop through available soundchannels
			for(var iIndex:int; iIndex < uiVSCLength; ++iIndex)
			{
				//if this soundchannel has the sound we want to stop
				if(this.vSoundChannels[iIndex].sSoundChannelName == sSoundToStop_)
				{
					//stop its sound
					vSoundChannels[iIndex].stop();
				}
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				PauseSound()
			
			Parameters:
				- sSoundToPause_:String - name of the sound we want to stop
				
			Return:
				- None
		*/
		/*******************************************************************************/
		static internal function PauseSound(sSoundToPause_:String):void
		{
			//find the length of the vSoundChannels
			var uiVSCLength:uint = this.vSoundChannels.length;
			//loop through available soundchannels
			for(var iIndex:int; iIndex < uiVSCLength; ++iIndex)
			{
				//if this soundchannel has the sound we want to stop
				if(this.vSoundChannels[iIndex].sSoundChannelName == sSoundToStop)
				{
					//update the play position of this channel
					vSoundChannels[iIndex].uiPlayPosition = vSoundChannels[iIndex].position;
					//stop its sound
					vSoundChannels[iIndex].stop();
				}
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method clears the XMLManager from all loaded xmls.
				
			Parameters:
				- None
						 
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function ClearVector(_vector:Vector):void
		{
			//get length of vSounds vector and store in variable
			var uiVectorLength:uint = _vector.length;
			
			//loop through vSounds
			for(var uiIndex:uint = 0; uiIndex < uiVectorLength; ++uiIndex)
			{
				_vector.[uiIndex].Destroy(); //call the soundobject's destroy
				_vector.[uiIndex] = null;    //null this index spot in vSounds
			}
			
			//set length of vSounds to 0
			_vector.length = 0;
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
			//loop through vSounds, call Destroy() on all SoundObjects
			this.ClearVector(this.vSounds);
			//loop through vSoundChannels, call Destroy() on all SoundChannels
			this.ClearVector(this.vSoundChannels);
			
			//null vSounds
			this.vSounds = null;
			//null vSoundChannels
			this.vSoundChannels = null;
		}
	}
}