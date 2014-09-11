package MyClasses
{
	public class ParentClass
	{
		private var iPrivate:int;
		
		public function ParentClass(_iNumber:int):void
		{
			trace("Parent");
		}
		
		public function SetiPrivate(_iValue:int):void
		{
			iPrivate = _iValue;
			trace(iPrivate);
		}
		public function GetiPrivate():int
		{
			return iPrivate;
		}
	}
}
