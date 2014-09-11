package GameObjects
{
	public class Hero
	{
		//PROPERTIES
		public var nPosX:Number;
		public var nPosY:Number;
		public var iHealth:int;
		public var sName:String;
		
		//METHODS
		public function Hero(_nPosX:Number, _nPosY:Number, _iHealth:int):void
		{
			nPosX = _nPosX;
			nPosY = _nPosY;
			iHealth = _iHealth;
			sName = "Hero";
		}
		public function Print():void
		{
			trace(sName, nPosX, nPosY, iHealth);
		}
	}
}
