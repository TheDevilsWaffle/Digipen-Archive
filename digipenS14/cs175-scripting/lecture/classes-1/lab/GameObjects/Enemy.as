package GameObjects
{
	public class Enemy
	{
		//PROPERTIES
		public var nPosX:Number;
		public var nPosY:Number;
		public var sName:String;
		
		//METHODS
		public function Enemy(_nPosX:Number, _nPosY:Number):void
		{
			nPosX = _nPosX;
			nPosY = _nPosY;
			sName = "Enemy";
		}
		public function Print():void
		{
			trace(sName, nPosX, nPosY);
		}
	}
}
