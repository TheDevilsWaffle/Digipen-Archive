package MyClasses
{
	import flash.display.MovieClip;

	public class Hero extends MovieClip
	{
		public var iHealth:int;
		public static var iNumberOfPlayers:int = 0;
		
		public function Hero(nPosX_:Number, nPosY_:Number, iHealth_:int)
		{
			x = nPosX_;
			y = nPosY_;
			scaleX = 0.5;
			scaleY = 0.25;
			iHealth = iHealth_;
			++iNumberOfPlayers;
		}
	}
}