package MyClasses
{
	import flash.display.MovieClip;
	import Box2D.Dynamics.b2Body;
	import flash.events.Event;

	public class Player extends MovieClip
	{
		public var b2bodyMyBody:b2Body;
		public var bIsDead:Boolean;
		
		public function Player()
		{
			bIsDead = false;
			addEventListener(Event.ENTER_FRAME , Update);
		}
		
		private function Update(e:Event)
		{
			x = b2bodyMyBody.GetPosition().x * GlobalData.iMetersToPixelsRatio;
			y = b2bodyMyBody.GetPosition().y * GlobalData.iMetersToPixelsRatio;
			rotation = b2bodyMyBody.GetAngle() * GlobalData.nRadianToDegrees;
			
			if( y <= 0 || y >= 480)
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