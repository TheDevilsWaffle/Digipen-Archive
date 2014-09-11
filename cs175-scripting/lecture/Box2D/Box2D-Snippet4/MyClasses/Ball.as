package MyClasses
{
	import flash.display.MovieClip;
	import Box2D.Dynamics.b2Body;
	import flash.events.Event;

	public class Ball extends MovieClip
	{
		public var b2bodyMyBody:b2Body;
		public var bIsDead:Boolean;
		
		public function Ball()
		{
			bIsDead = false;
			addEventListener(Event.ENTER_FRAME , Update);
		}
		
		private function Update(e:Event)
		{
			x = b2bodyMyBody.GetPosition().x * GlobalData.iMetersToPixelsRatio;
			y = b2bodyMyBody.GetPosition().y * GlobalData.iMetersToPixelsRatio;
			rotation = b2bodyMyBody.GetAngle() * GlobalData.nRadianToDegrees;
			
			if( y > 300)
			{
				bIsDead = true;
			}
		}
		
		public function Destroy()
		{
			removeEventListener(Event.ENTER_FRAME , Update);
		}

	}

}