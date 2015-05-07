<h1>GAM205&mdash;Project II for Designers</h1>

<h5>September 12<sup>th</sup>, 2014</h5>
<h3>Data Driven Engine</h3>

<h4>What does "Data Driven Engine mean?</h4>
<p>Use data files to determine behavior.
  <ul>
    <li>Level files</li>
    <li>Archetype files</li>
    <li>Script files</li>
    <li>Tweakables files</li>
  </ul>
You should separate game logic from everything else. Game logic does its stuff seperate from the game engine. Keeps the engine generic. The goal is to get the vast majority of how the game behaves seperate from the engine itself.
</p>

<h4>How?</h4>
<p>We ned a generic and robust way to load data. THis process is called <strong>serialization</strong>. The key is to encapsulate the variability of what data an object from the functionality of how data is loaded. Or: What the object needs (hp, position, etc&hellip;) from how the data s read (file, xml, database, etc&hellip;).</p>

<h4>Serialization Pattern</h4>
<p>Special form of the visitor pattern. Participants:
	<ul>
		<li> Object that supports the serialize method or interface which defines how it serializes itself.</li>
		<li>The serializer or steam object that encapsulates file/database saving and loading</li>
		<li>Operators that help tdefin how objects serialize</li>
	</ul>
</p>

<h4>Archetypes</h4>
<p>An <strong>archetype</strong> is a prototype or the original model (think a blueprint or recipe) for an object. The factory uses the archetype to build the oject and then run time data is modified as needed (such as its position). Example:
	<ul>
		<li>Object = Car</li>
		<li>Archetype = Gray Model 2 BMW</li>
		<li>Instance = Bill's BMW, that BMW on the corner, etc&hellip;</li>
	</ul>
</p>

<h4>Archetype Problems</h4>
<p>What data do you want t have changed per object? For example:
<ul>
	<li>Position?</li>
	<li>Scale?</li>
	<li>Rotation?</li>
</ul>
Can the archetype override everything?
</p>

<h4>Data Driven Factory</h4>
<p>THe true power of the factory is when it is data driven.</p>

<h4>Levels</h4>
<p>In a level you want to place an object multiple times, like a health pack. Use archtypes to alias out the objects so heir properties can be adjusted. The loader than overrides the position, rotation, or whatever else you need.</p>

<h4>Data Inheritance</h4>
<p>repeat information.</p>

<!--WEEKLY SECTION 2-->
<h6>Week 3&ndash;September 18<sup>th</sup>, 2014</h6>

<h3>Shared Ownership</h3>

<h4>What is Shared Ownership?</h4>
<p>Multiple objects hare the sobject and its lifetime is not bound to any object. When all objects no longer refer to the shared object it can be destroyed (but does not have to be). Some objects may not have parent/child relationships only peer relationships. Some objets may be resources shared by many objects.</p>

<h4>Reference Counting</h4>
<p>Basic problem, you have a ship in space shooting a missile. Who owns the missile? Is it the ship, space, or the missile itself? You can use reference counting and a dead flag. Every object will have a boolean alive or dead, like a 'dead_flag' and you can refer to it. This way has issues, too. Circular logic and arise from objects always containing references to each other</p>

<h4>Handles/Object IDs</h4>
<p>This method uses a reference to an object that does not effect its lifetime. Owning code must handle the reference becoming null. Used for peer to peer object referencing.</p>

<h4>Handle Benefits</h4>
<p>Ids are necessary for networking and works for game objects because the reference going null must always be handled. Referenced to game objects do not have to be tracked. Useful debugging the game work and for serialization.</p>

<h4>Destroying Objects</h4>
<p>Make your destructors private. Make your game objects friends of the factory. <strong>Delayed Destruction</strong> Use a 'destroy' function to tell the factory to put the object on its 'to-be-destroyed' list and has quite a number of benefits.</p>

<h3>Messages, Events, and Communication</h3>

<h4></h4>
<p></p>

<h4></h4>
<p></p>

<h4></h4>
<p></p>

<h4></h4>
<p></p>

<h4></h4>
<p></p>

<h4></h4>
<p></p>