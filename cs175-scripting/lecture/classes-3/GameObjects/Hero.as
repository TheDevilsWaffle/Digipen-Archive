package GameObjects
{
	import flash.display.MovieClip;
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;

	public class Hero extends MovieClip
	{
		public function Hero(_nPosX:Number, _nPosY:Number):void
		{
			x = _nPosX;
			y = _nPosY;
		}
		public function Movement(_keyboard:KeyboardEvent):void
		{
			//move left using A
			if(_keyboard.keyCode == Keyboard.A)
			{
				x -= 5;
			}
			//move right using D
			if(_keyboard.keyCode == Keyboard.D)
			{
				x += 5;
			}
			//move up using W
			if(_keyboard.keyCode == Keyboard.W)
			{
				y -= 5;
			}
			//move down using S
			if(_keyboard.keyCode == Keyboard.S)
			{
				y += 5;
			}
		}
	}
}