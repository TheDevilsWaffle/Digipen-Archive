/***************************************************************************************/
/*
	filename   	Level1.as 
	author		Elie Abi Chahine
	email   	eabichahine@digipen.edu
	date		24/05/2011 
	
	brief:

*/        	 
/***************************************************************************************/
package GamePlay.Level4
{
	import Engine.*;
	import GamePlay.GamePlayGlobals;
	
	import flash.ui.Keyboard;
	import flash.display.DisplayObject;
	import flash.display.MovieClip;
	import flash.geom.Point;
	import GamePlay.MainMenu.MainMenu;
	import GamePlay.Level2.HeroSP;

	public class Level4 extends State
	{
		var particlesystemCurrent_:ParticleSystem;
		
		override public function Create():void
		{
			XMLManager.LoadXML("GamePlay/Level4/EmitterProperties_FilledCircle.xml", "Emitter_FilledCircle_XML");
			XMLManager.LoadXML("GamePlay/Level4/EmitterProperties_Circle.xml", "Emitter_Circle_XML");
			XMLManager.LoadXML("GamePlay/Level4/EmitterProperties_PizzaSlice.xml", "Emitter_PizzaSlice_XML");
			XMLManager.LoadXML("GamePlay/Level4/EmitterProperties_Doughnut.xml", "Emitter_Doughnut_XML");
			
			XMLManager.LoadXML("GamePlay/Level4/EmitterProperties_FilledRectangle.xml", "Emitter_FilledRectangle_XML");
			XMLManager.LoadXML("GamePlay/Level4/EmitterProperties_Rectangle.xml", "Emitter_Rectangle_XML");
			
			XMLManager.LoadXML("GamePlay/Level4/EmitterProperties_Fountain.xml", "Emitter_Fountain_XML");
			XMLManager.LoadXML("GamePlay/Level4/EmitterProperties_Explosion.xml", "Emitter_Explosion_XML");
			
			XMLManager.LoadXML("GamePlay/Level4/ParticleProperties_Water.xml", "ParticleWater_XML");
			XMLManager.LoadXML("GamePlay/Level4/ParticleProperties_RainbowStar.xml", "ParticleRainbow_XML");
		}
		
		override public function Initialize():void
		{
		}

		override public function Update():void
		{
			if(InputManager.IsTriggered(Keyboard.M))
			{
				GameStateManager.GotoState(new MainMenu());
			}
			
			if(InputManager.IsTriggered(Keyboard.R))
			{
				GameStateManager.RestartState();
			}
			
			if(InputManager.IsTriggered(Keyboard.W))
			{
				PhysicsManager.AddGlobalForce(Infinity, new Point(-1,0), 25);
			}
			
			if(InputManager.IsTriggered(Keyboard.Q))
			{
				PhysicsManager.RemoveAllGlobalForces();
			}
			
			//Filled Circle
			if(InputManager.IsTriggered(Keyboard.NUMBER_1))
			{
				if(particlesystemCurrent_ != null)
				{
					ObjectManager.RemoveObjectByName(particlesystemCurrent_.displayobject.name, ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveAllObjectsByName(particlesystemCurrent_.particleinfo.sName,ObjectManager.OM_DYNAMICOBJECT);
					particlesystemCurrent_ = null;
				}
				
				particlesystemCurrent_ = new ParticleSystem(XMLManager.GetXML("Emitter_FilledCircle_XML"), XMLManager.GetXML("ParticleRainbow_XML"), 1000, Infinity, null, 375, 275);
				ObjectManager.AddObject(particlesystemCurrent_, "ParticleSystem", ObjectManager.OM_STATICOBJECT);
			}
			else if(InputManager.IsTriggered(Keyboard.NUMBER_2)) //Filled Circle
			{
				if(particlesystemCurrent_ != null)
				{
					ObjectManager.RemoveObjectByName(particlesystemCurrent_.displayobject.name,ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveAllObjectsByName(particlesystemCurrent_.particleinfo.sName,ObjectManager.OM_DYNAMICOBJECT);
					particlesystemCurrent_ = null;
				}
				
				particlesystemCurrent_ = new ParticleSystem(XMLManager.GetXML("Emitter_Circle_XML"), XMLManager.GetXML("ParticleRainbow_XML"), 2000, Infinity, null, 375, 275);
				ObjectManager.AddObject(particlesystemCurrent_, "ParticleSystem", ObjectManager.OM_STATICOBJECT);
			}
			else if(InputManager.IsTriggered(Keyboard.NUMBER_3)) //Pizza SLice
			{
				if(particlesystemCurrent_ != null)
				{
					ObjectManager.RemoveObjectByName(particlesystemCurrent_.displayobject.name, ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveAllObjectsByName(particlesystemCurrent_.particleinfo.sName, ObjectManager.OM_DYNAMICOBJECT);
					particlesystemCurrent_ = null;
				}
				
				particlesystemCurrent_ = new ParticleSystem(XMLManager.GetXML("Emitter_PizzaSlice_XML"), XMLManager.GetXML("ParticleRainbow_XML"), 1000, Infinity, null, 375, 275);
				ObjectManager.AddObject(particlesystemCurrent_, "ParticleSystem", ObjectManager.OM_STATICOBJECT);
			}
			else if(InputManager.IsTriggered(Keyboard.NUMBER_4)) //Doghnut
			{
				if(particlesystemCurrent_ != null)
				{
					ObjectManager.RemoveObjectByName(particlesystemCurrent_.displayobject.name, ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveAllObjectsByName(particlesystemCurrent_.particleinfo.sName, ObjectManager.OM_DYNAMICOBJECT);
					particlesystemCurrent_ = null;
				}
				
				particlesystemCurrent_ = new ParticleSystem(XMLManager.GetXML("Emitter_Doughnut_XML"), XMLManager.GetXML("ParticleRainbow_XML"), 1000, Infinity, null, 375, 275);
				ObjectManager.AddObject(particlesystemCurrent_, "ParticleSystem", ObjectManager.OM_STATICOBJECT);
			}
			else if(InputManager.IsTriggered(Keyboard.NUMBER_5)) // Filled Rectangle
			{
				if(particlesystemCurrent_ != null)
				{
					ObjectManager.RemoveObjectByName(particlesystemCurrent_.displayobject.name, ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveAllObjectsByName(particlesystemCurrent_.particleinfo.sName, ObjectManager.OM_DYNAMICOBJECT);
					particlesystemCurrent_ = null;
				}
				
				particlesystemCurrent_ = new ParticleSystem(XMLManager.GetXML("Emitter_FilledRectangle_XML"), XMLManager.GetXML("ParticleRainbow_XML"), 1000, Infinity, null, 375, 275);
				ObjectManager.AddObject(particlesystemCurrent_, "ParticleSystem", ObjectManager.OM_STATICOBJECT);
			}
			else if(InputManager.IsTriggered(Keyboard.NUMBER_6)) // Rectangle
			{
				if(particlesystemCurrent_ != null)
				{
					ObjectManager.RemoveObjectByName(particlesystemCurrent_.displayobject.name, ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveAllObjectsByName(particlesystemCurrent_.particleinfo.sName, ObjectManager.OM_DYNAMICOBJECT);
					particlesystemCurrent_ = null;
				}

				particlesystemCurrent_ = new ParticleSystem(XMLManager.GetXML("Emitter_Rectangle_XML"), XMLManager.GetXML("ParticleRainbow_XML"), 1000, Infinity, null, 375, 275);
				ObjectManager.AddObject(particlesystemCurrent_, "ParticleSystem", ObjectManager.OM_STATICOBJECT);
			}
			else if(InputManager.IsTriggered(Keyboard.NUMBER_7)) // Fountain
			{
				if(particlesystemCurrent_ != null)
				{
					ObjectManager.RemoveObjectByName(particlesystemCurrent_.displayobject.name, ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveAllObjectsByName(particlesystemCurrent_.particleinfo.sName, ObjectManager.OM_DYNAMICOBJECT);
					particlesystemCurrent_ = null;
				}

				particlesystemCurrent_ = new ParticleSystem(XMLManager.GetXML("Emitter_Fountain_XML"), XMLManager.GetXML("ParticleWater_XML"), 2000, Infinity, null, 375, 275);
				ObjectManager.AddObject(particlesystemCurrent_, "ParticleSystem", ObjectManager.OM_STATICOBJECT);
			}
			else if(InputManager.IsTriggered(Keyboard.NUMBER_8)) // Explosion
			{
				if(particlesystemCurrent_ != null)
				{
					ObjectManager.RemoveObjectByName(particlesystemCurrent_.displayobject.name, ObjectManager.OM_STATICOBJECT);
					ObjectManager.RemoveAllObjectsByName(particlesystemCurrent_.particleinfo.sName, ObjectManager.OM_DYNAMICOBJECT);
					particlesystemCurrent_ = null;
				}

				particlesystemCurrent_ = new ParticleSystem(XMLManager.GetXML("Emitter_Explosion_XML"), XMLManager.GetXML("ParticleRainbow_XML"), 500, 0.4, null, 375, 275);
				particlesystemCurrent_.particleinfo.nLowerLifetime = 0.2;
				particlesystemCurrent_.particleinfo.nUpperLifetime = 0.8;
				ObjectManager.AddObject(particlesystemCurrent_, "ParticleSystem", ObjectManager.OM_STATICOBJECT);
			}
			
			if(particlesystemCurrent_ != null && particlesystemCurrent_.bIsDead == true)
			{
				particlesystemCurrent_ = null;
			}
			
			if(InputManager.IsTriggered(Keyboard.SPACE))
			{
				if(particlesystemCurrent_ != null)
				{
					particlesystemCurrent_.particleinfo.nMass += 0.1;
				}
			}
		}
		
		override public function Uninitialize():void
		{
			if(particlesystemCurrent_ != null)
			{
				ObjectManager.RemoveAllObjectsByName(particlesystemCurrent_.particleinfo.sName, ObjectManager.OM_DYNAMICOBJECT);
				ObjectManager.RemoveAllObjectsByName(particlesystemCurrent_.displayobject.name, ObjectManager.OM_STATICOBJECT);
				particlesystemCurrent_ = null;
			}
		}
		
		override public function Destroy():void
		{
		}
	}
}
