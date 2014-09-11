/* Example 1: Display Message */
trace("****Example 1: Display Message - Start****");
trace("Welcome to CS116");
trace("I want to get an 'A' in this class");
trace("Welcome to CS116\nI want to get an 'A' in this class");
trace("****Example 1: Display Message - End****\n\n");


/* Example 2: Comments */
trace("****Example 2: Comments - Start****");
/* using the trace function */
trace("Welcome to CS116"); //first line
trace("I want to get an 'A' in this class"); // second line
/* about commenting in code */
trace("Just get in the habit of commmenting using \"/* comment */\" and not \"//\", okay? Thanks.");
/*
Displaying two lines with one trace call
The output should be the same as the two lines displayed above
*/
trace("Welcome to CS116\nI want to get an 'A' in this class");
trace("****Example 2: Comments - End****\n\n");


/* Example 3: Multiple Instructions */
trace("****Example 3: Multiple Instructions - Start****");
/* a function that displays the class schedule */
function DisplayClassSchedule()
{
	trace("We have only one section");
	trace("It is 2 times per week");
	trace("Monday and Wednesday");
}
/* a function that displays a welcome phrase */
function DisplayWelcome()
{
	trace ("Welcome to CS116");
}
/* calling the DisplayWelcome function */
DisplayWelcome();
/*calling the DisplayClassSchedule function*/
DisplayClassSchedule();
trace("****Example 3: Multiple Instructions - End****\n\n");


/* Example 4: Functions */
trace("****Example 4: Functions - Start****");
/*a function that adds two numbers and returns the result*/
function Add(n1_:Number, n2_:Number):Number
{
	return n1_ + n2_;
}
/*calling the Add function and displaying the result*/
trace("The result is: " + Add(11,11));
trace("****Example 4: Functions - End****\n\n");


/* Example 5: Variables */
trace("****Example 5: Variables - Start****");
/*a function that adds two numbers and returns the result*/
function AddExample5(n1_:Number, n2_:Number):Number
{
	return n1_ + n2_;
}
/*create two variables used as imput for the Add function*/
var nInput1:Number = 5;
var nInput2:Number = 6;

/*call the add function and display the result*/
trace("The result of adding nInput1 and nInput2 is: " + AddExample5(nInput1, nInput2));
trace("****Example 5: Variables - End ****\n\n");


/* Example 6: Arithmetic expression */
trace("****Example 6: Arithmetic - Start****");
/*a function that comptes the distance between two points*/
function GetDistance(x1_:Number, y1_:Number, x2_:Number, y2_:Number):Number
{
	var nDeltaX:Number = x2_ - x1_;
	var nDeltaY:Number = y2_ - y1_;
	trace("nDeltaX is = " + nDeltaX);
	trace("nDeltaY is = " + nDeltaY);
	return Math.sqrt( nDeltaX * nDeltaX + nDeltaY * nDeltaY );
}

/*create four variables representing two points in 2D space*/
var nPoint1_X:Number = 2, nPoint1_Y:Number = 2, nPoint2_X:Number = 4, nPoint2_Y:Number = 4;

/*call GetDistance function and display the result*/
trace("The result of the GetDistance function is: " + GetDistance(nPoint1_X, nPoint1_Y, nPoint2_X, nPoint2_Y) );
trace("****Example 6: Arithmetic - End****\n\n");