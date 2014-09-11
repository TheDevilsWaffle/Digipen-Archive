package
{
	
	public class Hero 
	{
		//PROPERTIES
		public var nPosX:Number;
		public var nPosY:Number;
		public var iHealth:int;
		
		//METHODS
		public function Hero(_nPosX:Number, _nPosY:Number):void
		{
			nPosX = _nPosX;
			nPosY = _nPosY;
			iHealth = 100;
		}
	}
}
