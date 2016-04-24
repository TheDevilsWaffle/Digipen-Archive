/******************************************************************************
filename    array.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  3

Brief Description:
This file contains 5 functions which manipulate arrays.

******************************************************************************/

/*******************************************************************************
Function: reverse_array

Description: takes an array and reverses the order of its elements

Inputs: int array[], int size

Outputs: none
*******************************************************************************/
void reverse_array(int a[], int size)
{
	//temp variable and iterators
	int temp, i, j;

	//i loops through the front and j loops through the back (only do half of the array)
	for (i = 0, j = (size - 1); i < (size / 2); ++i, --j)
	{
		//temporary store origina a[i], assign a[j] to a[i], assign temp to a[j] 
		temp = a[i];
		a[i] = a[j];
		a[j] = temp;
	}
}

/*******************************************************************************
Function: add_arrays

Description: adds the values of two arrays

Inputs: int a[], int b[], int c[] (new array to return), int size

Outputs: none
*******************************************************************************/
void add_arrays(const int a[], const int b[], int c[], int size)
{
	//iterators
	int i;

	//loop through all arrays with i
	for (i = 0; i < size; ++i)
	{
		//c = a + b
		c[i] = a[i] + b[i];
	}
}

/*******************************************************************************
Function: scaler_multiply

Description: multiplies the values in an array

Inputs: int a[], int size, int multiplier

Outputs: none
*******************************************************************************/
void scalar_multiply(int a[], int size, int multiplier)
{
	//iterator
	int i;

	//loop through array with i
	for (i = 0; i < size; ++i)
	{
		//a *= multiplier
		a[i] *= multiplier;
	}
}

/*******************************************************************************
Function: dot_product

Description: multiplies elements in an array and adds them all together

Inputs: int a[], int b[], int size

Outputs: int
*******************************************************************************/
int dot_product(const int a[], const int b[], int size)
{
	//iterator and variable
	int i;
	int dotProduct = 0;

	//loop through both arrays with i
	for (i = 0; i < size; ++i)
	{
		//dotProduct += (a * b);
		dotProduct += (a[i] * b[i]);
	}

    return dotProduct;
}

/*******************************************************************************
Function: cross_product

Description: cross product of two arrays stored in a third array

Inputs: int a[], int b[], int c[]

Outputs: none
*******************************************************************************/
void cross_product(const int a[], const int b[], int c[])
{
	//no need to loop, just get values
	c[0] = ((a[1] * b[2]) - (a[2] * b[1]));
	c[1] = -((b[2] * a[0]) - (b[0] * a[2]));
	c[2] = ((a[0] * b[1]) - (a[1] * b[0]));
}