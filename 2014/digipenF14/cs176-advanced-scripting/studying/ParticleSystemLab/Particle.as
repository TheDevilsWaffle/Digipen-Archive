package 
{
	import flash.display.MovieClip;
	import flash.geom.Point;

	public class Particle extends MovieClip
	{
		public var pOriginalPosition:Point;
		public var pDirection:Point;
		public var nSpeed:Number;
		public var nAge:Number;
		public var nLifeTime:Number;
		public var bIsDead:Boolean;
		
		public function Particle(nPosX_:Number,
								 nPosY_:Number,
								 nScale_:Number,
								 nLifeTime_:Number)
		{
			pOriginalPosition = new Point(nPosX_, nPosY_);
			scaleX = scaleY = nScale_;
		}
		
		function GetRandomValue(nMin_:Number, nMax_:Number):Number
		{
			return Math.random() * (nMax_ - nMin_) + nMin_;
		}

		public function Initialize():void
		{
			bIsDead = false;
			nAge = 0.0;
			nLifeTime = GetRandomValue(2,3.2);
			x = pOriginalPosition.x;
			y = pOriginalPosition.y;
			pDirection = new Point();
			pDirection.x = GetRandomValue(-1, 1);
			pDirection.y = GetRandomValue(-1, 1);
			pDirection.normalize(1);
			nSpeed = GetRandomValue(5, 10)
		}
		
		public function Update():void
		{
			x += pDirection.x * nSpeed;
			y += pDirection.y * nSpeed;
			nAge += 1.0 / 24.0;
			if(nAge >= nLifeTime)
			{
				bIsDead = true;
			}
		}
		
	}
}