package GameObjects
{
	import flash.display.MovieClip;
	import flash.events.Event;

	public class Enemy extends MovieClip
	{
		private var iRotation:int;
		
		public function Enemy(_nPosX:Number, _nPosY:Number, _iRotation:int):void
		{
			x = _nPosX;
			y = _nPosY;
			iRotation = _iRotation;
			
			//add event listeners
			addEventListener(Event.ENTER_FRAME, Update);
		}
		//function to rotate things
		private function Update(_event:Event):void
		{
			rotation += iRotation;
		}
	}
}