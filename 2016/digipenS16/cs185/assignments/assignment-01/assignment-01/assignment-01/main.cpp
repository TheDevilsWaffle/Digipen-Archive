/*******************************************************************************
filename    main.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section     
assignment  1

Brief Description:
This program prints out the first 100 positive odd numbers.

*******************************************************************************/

#include <iostream>

//PROTOTYPE
void CalculateOddNumbers(int);

//VARIABLES
int iLimit;

/*******************************************************************************
Function: main

Description: Start of the program

Inputs: None

Outputs: int - used to tell the OS that the program ran successfully
*******************************************************************************/
int main(void)
{
	//calculates x many odd numbers based upon passed input
	CalculateOddNumbers(100);

	return 0;
}


/*******************************************************************************
Function: CalculateOddNumbers

Description: Calculates x many odd numbers based upon passed input

Inputs: int - threshold of how many odd numbers to count up to

Outputs: None
*******************************************************************************/
void CalculateOddNumbers(int threshold_)
{
	int iOddNumber = 1;

	for (int iIndex = 1; iIndex < (threshold_ + 1); ++iIndex)
	{
		//if iOddNumber is in the early teens (edge case)
		if (iIndex == 11 || iIndex == 12 || iIndex == 13)
		{
			std::cout << "The " << iIndex << "th odd number is: " << iOddNumber << std::endl;
		}
		//if iOddNumber ends in a 1
		else if (iIndex % 10 == 1)
		{
			std::cout << "The " << iIndex << "st odd number is: " << iOddNumber << std::endl;
		}
		//if iOddNumber ends in a 2
		else if (iIndex % 10 == 2)
		{
			std::cout << "The " << iIndex << "nd odd number is: " << iOddNumber << std::endl;
		}
		//if iOddNumber ends in a 3
		else if (iIndex % 10 == 3)
		{
			std::cout << "The " << iIndex << "rd odd number is: " << iOddNumber << std::endl;
		}
		//all other iOddNumbers end with a th
		else
		{
			std::cout << "The " << iIndex << "th odd number is: " << iOddNumber << std::endl;
		}

		//increment iOddNumber by 2
		iOddNumber += 2;
	}
}