/*******************************************************************************
filename    e.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section     
assignment  2

Brief Description:
e.cpp calculated the value of e using a passed value from main.cpp and by
calculating factorials.

*******************************************************************************/
#include <iostream>

//PROTOTYPE
/* Calculates the value of e */
double compute_e(unsigned int iterations);
/* Calculates a number's factorial */
unsigned int compute_factorial(unsigned int number);

/*******************************************************************************
Function: compute_e

Description: computed the value of e

Inputs: unsigned int

Outputs: double
*******************************************************************************/
double compute_e(unsigned int iterations)
{
	//variable to store answer
	double answer = 0.0;

	//loop through e calculation based on iterations passed
	for (unsigned int i = 0; i < iterations; ++i)
	{
		//update answer per loop
		answer += 1.0/ compute_factorial(i);
	}

	return answer;
}

/*******************************************************************************
Function: compute_factorial

Description: calculates a number's factorial

Inputs: unsigned int

Outputs: unsigned int
*******************************************************************************/
unsigned int compute_factorial(unsigned int number)
{
	//variable to hold answer
	unsigned int answer = 0;

	//edge case for factorial
	if (number <= 1)
	{
		return 1;
	}

	//multiply answer based on calling function again 
	//but with the next number in the sequence
	answer = number * compute_factorial(number - 1);
	
	return answer;
}

