/***************************************************************************************/
/*
	filename   	ParticleInfo.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		10/16/2011 
	
	brief:
	

*/        	 
/***************************************************************************************/
package Engine
{
	import flash.utils.getDefinitionByName;
	
	final public class ParticleInfo
	{
		public var sName:String;
		public var particleClassReference:Class;
		public var nMass:Number;
		public var nLowerLifetime:Number;
		public var nUpperLifetime:Number;
		public var nLowerStartOpacity:Number;
		public var nUpperStartOpacity:Number;
		public var nLowerEndOpacity:Number;
		public var nUpperEndOpacity:Number;
		public var nLowerStartScaleX:Number;
		public var nLowerStartScaleY:Number;
		public var nUpperStartScaleX:Number;
		public var nUpperStartScaleY:Number;
		public var nLowerEndScaleX:Number;
		public var nLowerEndScaleY:Number;
		public var nUpperEndScaleX:Number;
		public var nUpperEndScaleY:Number;
		
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