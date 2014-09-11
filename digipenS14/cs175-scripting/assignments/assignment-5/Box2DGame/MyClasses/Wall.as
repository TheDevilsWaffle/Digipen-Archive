package MyClasses
{
	import flash.display.MovieClip;
	import Box2D.Dynamics.b2Body;
	import flash.events.Event;

	public class Wall extends MovieClip
	{
		public var b2bodyMyBody:b2Body;
		public var bIsDead:Boolean;
		
		public function Wall()
		{
			bIsDead = false;
		}

	}

}