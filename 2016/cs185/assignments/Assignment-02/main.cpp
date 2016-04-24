/******************************************************************************
filename    main.c
author      Artie Fufkin
DP email    artie.fufkin@digipen.edu
course      CS185
section
assignment  7
due date    11/26/2037

Brief Description:
    This file contains the main function for the e_constant assignment.

******************************************************************************/
#include <iostream> /* cout */

/* Calculates the value of e */
double compute_e(unsigned int iterations);
/* Calculates a number's factorial */
unsigned int compute_factorial(unsigned int number);


int main(void)
{
	unsigned int iterations = 1; // loop counter 

	// Print out table header 
	std::cout << "Approximations for e" << std::endl << std::endl;
	std::cout << "Iteration       e Value" << std::endl;
	std::cout << "--------------------------------" << std::endl;

	// Print out value of e for each set of numbers
	for (; iterations <= 15; ++iterations)
	{
		// Calculate the value of e for each iteration
		double e_value = compute_e(iterations);
		// Print the results of the calculations
		std::cout.width(10);
		std::cout << iterations;
		std::cout.setf(std::ios_base::showpoint);
		std::cout.width(20);
		std::cout.precision(13);
		std::cout << e_value;
		std::cout << std::endl;
	}

	return 0; // Return to the OS
}