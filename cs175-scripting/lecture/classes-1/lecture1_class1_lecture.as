package
{
	
	public class Hero 
	{
		//PROPERTIES
		public var nPosX:Number;
		public var nPosY:Number;
		public var iHealth:int;
		
		//METHODS
		public function Initialize(_nPosX:Number, _nPosY:Number):void
		{
			nPosX = _nPosX;
			nPosY = _nPosY;
			nHealth = 100;
		}
	}
}
