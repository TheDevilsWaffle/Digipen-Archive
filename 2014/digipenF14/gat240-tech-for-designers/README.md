<h1>GAT240&mdash;Technology For Designers</h1>

<h5>September 11<sup>th</sup>, 2014</h5>
<h3>Messages, Events, and Communication</h3>

<h4>Messages</h4>
<p>Messages are a way of cummunication between systems or objects that does not rely on directly linked functions. For example, Windows Message Loop. In zilch these are delegates.</p>

<h4>Scripting Languages</h4>
<p>Scripting languages relay entirely on non-compiled function calls. Some languages use late binding. C++ has virtual functions which provide simple message abilities. Why do we need them? Allows for decoupled communication accross game ystem boundaries, process boundaries, network boundaries, language boundaries, across time! This reduces dependencies. Example, starting a countdown timer calls the windows clock function in order to send time based messages.</p>

<h4>Anatomy of a Message</h4>
<p>Run Time Type Identifier, ayload Message Specific Data.</p>

<h4>Messages and Listeners</h4>
<p>
    <ul>
        <li>Listener/Observer Pattern</li>
        <p>An oject can sign up as a listener to recieve messages. When an event occurs a message is brodcasted to every listener of that event.</p>
        <li>Messages</li>
        <p>Have an address or target object. Usually only sent to one interface or object. Can be broadcast or sent to every object. Some languages use duck typing.</p>
    </ul>
</p>
<p>Very effective in UI frameworks. Examples are C# delegates, ActionScript Events.</p>

<h4>Message Queues</h4>
<p>Messages can be queued up to be processed at a later time (temporal messages!). This is critical for networking and multithread applications. Aloso useful for AI and game logic.</p>

<h4>Concrete vs. Conceptual</h4>
<p>Concrete messages are linked to particular events, used for communication. Examples are OnCollide, AnimationFinish, etc&hellip;</p>
<p>Conceptual Messages are linked to conceptual events object decided the meaning. Like little big planet, moving a box on a switch activates it. Useful for making user game editors. Examples: TakeDamage, TriggerFire, Activate, &hellip;</p>

<h4>Signals and Slots</h4>
<p>Objects have a set of signals and slots. Signals are events that the object generates: OnClick, OnCollide, OnClose, etc&hellip;</p>
<p>Slots are fuctions that can be called on objects: Fire, Open, Unlock, etc&hellip;</p>
<p>Setting up game logic is just connecting different signals to dfferent slots. When this box is destroy, unlock the door:
<pre><code>Connect(box.OnDestroy){ door.Unlock;}</code></pre></p>

<hr />

<h5>October 02<sup>nd</sup>, 2014</h5>
<h3>Vector Math</h3>

<h4>Vector Terms</h4>
<p>a <strong>point</strong> is a vector that represents a position in the world. A <strong>vector</strong> is a difference between two points. A <strong>direction/normal</strong> is a vector with a length of one. The specialized property of a normal is that it has a length of one. For example, get a direction, then go 10 units in that direction. To normalize you divie by length of the vector (do not do this for a direction of 0, cannot normalize a zero vector).</p>

<h4>Normalized Vectors</h4>
<p>May calculations depend upon using unit vectors
	<ul>
		<li>Cross Product</li>
		<li>Reflection</li>
		<li>Ray Casting</li>
		<li>Dot Product</li>
		<li>etc&hellip;</li>
	</ul>
Basically, ay calulation except distance/positions.</p>

<h4>Dot Product</h4>
<p>The dot product, or scalar product, can be used to: determine facing direction. It is positive for same direction, negative for opposite direction, zero for perpendicular, and arccosine returns the angle between the two vectors.</p>

<h4>Cross Product</h4>
<p>THe crss product a x b will give a vector c that is perpendicular to both a and b, with a direction given by the right-hand rule. In 2D there is a perpendicular right and left vector with (-y,x) and (y,-x).</p>

<h4>Trig</h4>
<p>Look at his slides, it has an amazing gif of what is happening on the graph.</p>

<h4>atan2</h4>
<p>atan2(y,x) will give a unique angle (in radians) for every direction.</p>

<h4>3D Rotations</h4>
<p>3D rotations are a bummer, they are hard to represent in a compact form. It can be represented by a direction and a up vector. They can be represented by an angle about x,y, and z.</p>

<h4>Euler Angles</h4>
<p>Euler angles have an order and can experience gimbal lock. They are hard to interpolate and sometimes lead to surprising results. Never interpolate euler because they can break easily.</p>

<h4>Quaternions</h4>
<p>Hyper complex imaginary numbers, points on a four dimensional hyper sphere. Basically imagine them as points on a sphere. They are magic boxes with no disconguities and none of the problems that euler angles do. It's just a rotation. SLERPin' vectors.</p>

<p>Multiplication is "adding rotations", it is gimbal lock free using slerp. Never set indivifual values (x,y,z,w) unless you know what you are doing. They are complex, hard to read, and not easy to limit.</p>

<h4>Transformation</h4>
<p>Translation, Rotation, and Scale of an object. In the end, this can all be combined into a transormation matrix.</p>

<h4>Relative Transforms</h4>
<p>Uses heiarchical parenting to move relative to your parent.</p>















