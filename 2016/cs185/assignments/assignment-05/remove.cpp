/*******************************************************************************
filename    remove.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  5

Brief Description: This file contains 3 functions involved in removing an
					unwanted number in an array, swapping values in an array
					and lastly printing out an array.
*******************************************************************************/

#include <iostream>
using std::cout;
using std::endl;

/*******************************************************************************
Function: Swap()

Description: swaps the values between two integer pointers.

Inputs: int *a - first integer pointer value to swap
		int *b - second integer point value to swap

Outputs: None
*******************************************************************************/
void Swap(int *a, int *b)
{
	//create a temporary integer to hold the value of the first integer pointer
	int temp = *a;
	//swap *a value with that of *b's value
	*a = *b;
	//change value of *b to that of the temporary integer
	*b = temp;
}

/*******************************************************************************
Function: RemoveNumberInArray()

Description: removes a user specifically defined number in an array and adds
			  zeros at the end of the array in place of the old value.

Inputs: int *a     - integer pointer of an array
		int size   - size of an array (for looping)
		int number - number to remove from the array

Outputs: None
*******************************************************************************/
void RemoveNumberInArray(int *a, int size, int number)
{
	//temporarily save the position of a in temp;
	int *temp = a;

	//start by swapping the instances of the number to the back of the array
	//loop through an array using a and size
	for (int i = 0; i < size; ++i)
	{
		//dereference of a is equal to the number we do not want, start swapping
		if (*a == number)
		{
			//new integer pointer so we don't lose a's current position
			int *z = a;

			//start looping through starting at current position
			for (int j = i; j < size; ++j)
			{
				//ensure we don't go out of array bounds
				if( j != (size - 1))
				{
					//swap using z and z + 1 positions
					Swap(z, (z + 1));

					//increment z's position
					z++;
				}
			}

			//recheck a in the case that we swapped a number over
			a--;
		}

		//increment to next position of a
		a++;
	}

	//reset a back to the beginning
	a = temp;

	//loop through one more time and replace instances of the number with zeros
	for (int i = 0; i < size; ++i)
	{
		//if dereference of a is equal to the number we do not want, replace it
		if (*a == number)
		{
			*a = 0;
		}

		//increment to next position of a
		*a++;
	}
}

/*******************************************************************************
Function: PrintArray()

Description: prints out the values inside of an array.

Inputs: const int *a - points to value in an array
		int size     - the size of the array

Outputs: None
*******************************************************************************/
void PrintArray(const int *a, int size)
{
	//loop through the array using a and size
	for (int i = 0; i < size; ++i)
	{
		//print out the value of a and a space
		cout << *a << " ";

		//increment to the next position of a
		++a;
	}
}