/***************************************************************************************/
/*
	filename   	ParticleInfo.as 
	author		Elie Abi Chahine, Travis Moore
	email   	eabichahine@digipen.edu, travis.moore@digipen.edu
	date		11/13/2014
	
	brief:
	the ParitcleInfo class holds the xml file info for particles
*/        	 
/***************************************************************************************/
package Engine
{
	import flash.utils.getDefinitionByName;
	
	final public class ParticleInfo
	{
		public var sName:String;					//name of the particle
		public var particleClassReference:Class;	//use this to create classes from the xml (getdefinitionbyname)
		public var nMass:Number;					//all stuff below from xml
		public var nLowerLifetime:Number;			//lifetime range values
		public var nUpperLifetime:Number;
		public var nLowerStartOpacity:Number;		//start opacity range values
		public var nUpperStartOpacity:Number;
		public var nLowerEndOpacity:Number;			//end opacity range values
		public var nUpperEndOpacity:Number;
		public var nLowerStartScaleX:Number;		//start scale x and y range values
		public var nLowerStartScaleY:Number;
		public var nUpperStartScaleX:Number;		
		public var nUpperStartScaleY:Number;
		public var nLowerEndScaleX:Number;			//end scale x and y range values
		public var nLowerEndScaleY:Number;
		public var nUpperEndScaleX:Number;
		public var nUpperEndScaleY:Number;
		
		/***************************************************************************************/
		/*
			Description:
				Constructor that initializes the ParticleInfo variables.
			
			Parameters:
				- xmlParticleProperties_:XML - the xml file that contains all the particle info
		*/
		/***************************************************************************************/
		public function ParticleInfo(xmlParticleProperties_:XML)
		{
			if(xmlParticleProperties_ != null)
			{
				sName = xmlParticleProperties_.Name;
				particleClassReference = getDefinitionByName(xmlParticleProperties_.ShapeClass) as Class;
				nMass = Number(xmlParticleProperties_.Mass);
				nLowerLifetime = Number(xmlParticleProperties_.Lifetime.Lower);
				nUpperLifetime = Number(xmlParticleProperties_.Lifetime.Upper);
				nLowerStartOpacity = Number(xmlParticleProperties_.Opacity.Start.Lower);
				nUpperStartOpacity = Number(xmlParticleProperties_.Opacity.Start.Upper);
				nLowerEndOpacity = Number(xmlParticleProperties_.Opacity.End.Lower);
				nUpperEndOpacity = Number(xmlParticleProperties_.Opacity.End.Upper);
				nLowerStartScaleX = Number(xmlParticleProperties_.Scale.Start.LowerX);
				nLowerStartScaleY = Number(xmlParticleProperties_.Scale.Start.LowerY);
				nUpperStartScaleX = Number(xmlParticleProperties_.Scale.Start.UpperX);
				nUpperStartScaleY = Number(xmlParticleProperties_.Scale.Start.UpperY);
				nLowerEndScaleX = Number(xmlParticleProperties_.Scale.End.LowerX);
				nLowerEndScaleY = Number(xmlParticleProperties_.Scale.End.LowerY);
				nUpperEndScaleX = Number(xmlParticleProperties_.Scale.End.UpperX);
				nUpperEndScaleY = Number(xmlParticleProperties_.Scale.End.UpperY);
			}
		}
	}
}