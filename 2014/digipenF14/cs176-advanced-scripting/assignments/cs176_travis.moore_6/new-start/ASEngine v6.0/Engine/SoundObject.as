/****************************************************************************************
FILENAME   SoundObject.as 
AUTHOR     Travis Moore
EMAIL      travis.moore@digipen.edu
DATE       12/09/2014 
/***************************************************************************************/
package Engine
{
	//IMPORT
	import flash.events.Event;
	import flash.media.Sound;
	import flash.media.SoundChannel;
	
	//CLASS SOUNDOBJECT
	public class SoundObject
	{
		//SOUND OBJECT VARIABLES
		var soSound:Sound;
		var sName:String;					//name of the sound
		var bIsLooping:Boolean;				//used to tell if the sound is looping or not
		//SOUND CHANNEL VARIABLEs
		var scSoundChannel:SoundChannel;	//the sound channel used
		var iPausePosition:int;				//time position of a sound used for pause/unpausing
		
		/********************************************************************************
		FUNCTION- Constructor()
		/*******************************************************************************/
		public function SoundObject(soSound_:Sound, 
									sName_:String, 
									bIsLooping_:Boolean):void
		{
			//set variables based on passed parameters 
			this.soSound = soSound_;
			this.sName = sName_;
			this.bIsLooping = bIsLooping_;
		}
		
		/********************************************************************************
		FUNCTION- Initialize()
		/*******************************************************************************/
		public function Initialize():void
		{
			//give the scSoundChannel a sound to play
			this.scSoundChannel = this.soSound.play();
			//add event listener to know when sound is over
			this.scSoundChannel.addEventListener(Event.SOUND_COMPLETE, SoundComplete);
		}
		
		/********************************************************************************
		FUNCTION- SoundPause()
		/*******************************************************************************/
		public function SoundPause():void
		{
			//set pause position to where the soundchannel's current position is
			this.iPausePosition = this.scSoundChannel.position;
			//stop playing the sound
			scSoundChannel.stop();
		}
		
		/********************************************************************************
		FUNCTION- SoundResume()
		/*******************************************************************************/
		public function SoundResume():void
		{
			//set the soundchannel to play the original sound from the pause position
			this.scSoundChannel = this.soSound.play(this.iPausePosition);
		}
		
		/********************************************************************************
		FUNCTION- SoundComplete()
		/*******************************************************************************/
		public function SoundComplete():void
		{
			//check to see if the sound is supposed to play again
			if(bIsLooping)
			{
				//play the sound again
				this.scSoundChannel = this.soSound.play();
			}
			//stop playing the sound
			else
			{
				//stop dat sound
				this.soSound.close();
			}
		}
		
		/********************************************************************************
		FUNCTION- Unitialize()
		/*******************************************************************************/
		public function Uninitialize():void
		{
			//stop playing sounds
			this.scSoundChannel.stop();
			//get rid of this by calling Destroy()
			this.Destroy();
		}
		
		/********************************************************************************
		FUNCTION- Destroy()
		/*******************************************************************************/
		public function Destroy():void
		{
			//null all dem things
			this.soSound = null;
			this.scSoundChannel = null;
		}
	}
}