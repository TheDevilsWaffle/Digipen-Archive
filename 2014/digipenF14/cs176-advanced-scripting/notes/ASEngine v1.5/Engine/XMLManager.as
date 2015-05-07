/***************************************************************************************/
/*
	filename   	XMLManager.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		10/02/2013
	
	brief:
	This class handles loading XML files.
*/        	 
/***************************************************************************************/
package Engine
{
	final public class XMLManager
	{
		/* A vector that hold all the loaded xmls and their information */
		static private var vXMLs:Vector.<XMLInfo>;
		
		/*******************************************************************************/
		/*
			Description:
				This method initializes all the XMLManager's variables.
				
			Parameters:
				- None
						 
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function Initialize():void
		{
			vXMLs = new Vector.<XMLInfo>();
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method loads an xml from a user specified location. Every loaded xml
				will be given a name in order to locate it later..
				
			Parameters:
				- sFile_: The xml name with its path in order to load it from the 
						  system / server.
				
				- sName_: A user defined name used later for searching, acquiring or 
						  unloading the appropriate xml.
				
				- bAsynchronous_: True if we want to load Asynchronously (meaning without
								  stopping the engine). False in order to load
								  Synchronously (everything stops until the file is 
								  loaded).
						 
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function LoadXML(sFile_:String, sName_:String, 
									   bAsynchronous_:Boolean = false):void
		{
			vXMLs.push(new XMLInfo(sFile_, sName_, bAsynchronous_));
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method searches for an xml by its name and returns it to the user.
				if the xml is not found or is still loading null is returned.
				
			Parameters:
				- sName_: The user defined name in order to search for the xml
						 
			Return:
				- Returns the xml data if found and is done loading, otherwise null.
		*/
		/*******************************************************************************/
		static public function GetXML(sName_:String):XML
		{
			var vXMLsLength:uint = vXMLs.length;
			for(var i:uint = 0; i < vXMLsLength; ++i)
			{
				if(vXMLs[i].sName == sName_ && vXMLs[i].xml != null)
				{
					return vXMLs[i].xml;
				}
			}
			return null;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method checks if the manager is still loading any files.
				
			Parameters:
				- None
						 
			Return:
				- True if file are still loading, false if all are fully loaded.
		*/
		/*******************************************************************************/
		static public function filesStillLoading():Boolean
		{
			var vXMLsLength:uint = vXMLs.length;
			for(var i:uint = 0; i < vXMLsLength; ++i)
			{
				if(vXMLs[i].xml == null)
				{
					return true;
				}
			}
			return false;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method unloads an xml by its name.
				
			Parameters:
				- sName_: The user defined name in order to unload the xml
						 
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function unloadXMLByName(sName_:String):void
		{
			var vXMLsLength:uint = vXMLs.length;
			for(var i:uint = 0; i < vXMLsLength; ++i)
			{
				if(vXMLs[i].sName == sName_)
				{
					vXMLs[i].Destroy();
					vXMLs[i] = null;
					vXMLs.splice(i, 1);
					return;
				}
			}
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method unloads all xmls that has a certain name.
				
			Parameters:
				- sName_: The user defined name in order to unload all xml with that name
						 
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function unloadAllXMLsByName(sName_:String):void
		{
			for(var i:int = 0; i < vXMLs.length; ++i)
			{
				if(vXMLs[i].sName == sName_)
				{
					vXMLs[i].Destroy();
					vXMLs[i] = null;
					vXMLs.splice(i, 1);
					--i;
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
		static public function Clear():void
		{
			var vXMLsLength:uint = vXMLs.length;
			for(var i:uint = 0; i < vXMLsLength; ++i)
			{
				vXMLs[i].Destroy();
				vXMLs[i] = null;
			}
			vXMLs.length = 0;
		}
		
		/*******************************************************************************/
		/*
			Description:
				This method destroys the XMLManager by clearing all the xmls and nulling
				all properties.
				
			Parameters:
				- None
						 
			Return:
				- None
		*/
		/*******************************************************************************/
		static public function Destroy():void
		{
			Clear();
			vXMLs = null;
		}
	}
}

import flash.events.Event;
import flash.net.URLLoader;
import flash.net.URLRequest;
import Engine.GameStateManager;
	
final internal class XMLInfo 
{
	/* Contains the loaded xml */
	public var xml:XML;
	/* Name used to find the xml in the XMLManager */
	public var sName:String;
	/* The loader is used to load the xml file */
	private var myLoader:URLLoader;
	/* A boolean that specifies if we are loading Asynchronously or Synchronously */
	private var bAsynchronous:Boolean;
	
	/*******************************************************************************/
	/*
		Description:
			Constructor that initializes all properties and starts loading the xml.
				
		Parameters:
			- sFile_: The xml name with its path in order to load it from the 
						  system / server.
				
			- sName_: A user defined name used later for searching, acquiring or 
					  unloading the appropriate xml.
			
			- bAsynchronous_: True if we want to load Asynchronously (meaning without
							  stopping the engine). False in order to load
							  Synchronously (everything stops until the file is 
							  loaded).
						 
		Return:
			- None
	*/
	/*******************************************************************************/
	public function XMLInfo(sFile_:String, sName_:String, bAsynchronous_:Boolean)
	{
		xml = null;
		myLoader = new URLLoader();
		myLoader.load(new URLRequest(sFile_));
		myLoader.addEventListener(Event.OPEN, xmlLoadingStarted);
		sName = sName_;
		bAsynchronous = bAsynchronous_;
	}
	
	/*******************************************************************************/
	/*
		Description:
			This method is called when the xml is opened for loading. If the loading
			is synchronous, the iLoading variable is incremented. Finally, an event
			listener will be added to wait for the loading to be complete.
			
		Parameters:
			- e_: event data information.
					 
		Return:
			- None
	*/
	/*******************************************************************************/
	private function xmlLoadingStarted(e_:Event):void 
	{
		if(bAsynchronous == false)
		{
			GameStateManager.iLoading++;
		}
		myLoader.removeEventListener(Event.OPEN, xmlLoadingStarted);
		myLoader.addEventListener(Event.COMPLETE, xmlLoadingComplete);
	}
	
	/*******************************************************************************/
	/*
		Description:
			This method is called when the xml is fully loaded. If the xml was 
			loading synchronously, the game state manager's iLoding variable is
			decremented to let the game state manager know that the file is done 
			loading. Finally, the event listeners are removed since we are done
			loading.
			
		Parameters:
			- e_: event data information. Will be our access to the xml loaded data.
					 
		Return:
			- None
	*/
	/*******************************************************************************/
	private function xmlLoadingComplete(e_:Event):void 
	{
		if(bAsynchronous == false)
		{
			GameStateManager.iLoading--;
		}
		
		xml = new XML(e_.target.data);
		myLoader.removeEventListener(Event.COMPLETE, xmlLoadingComplete);
		myLoader = null;
	}
	
	/*******************************************************************************/
	/*
		Description:
			This method destroys an XMLInfo instance by nulling all the properties
			and removing all remaining EventListeners.
			
		Parameters:
			- None
					 
		Return:
			- None
	*/
	/*******************************************************************************/
	public function Destroy():void
	{
		xml = null;
		sName = null;
		if(myLoader != null)
		{
			myLoader.removeEventListener(Event.OPEN, xmlLoadingStarted);
			myLoader.removeEventListener(Event.COMPLETE, xmlLoadingComplete);
			myLoader = null;
		}
	}
}