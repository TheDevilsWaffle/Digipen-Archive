/*******************************************************************************
filename    sorting.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  4

Brief Description:
This program calculates the distance to the moon using only a
compass and a straight-edge.

*******************************************************************************/
#include <iostream>

#define ROWS 5
#define COLUMNS 10

void Sort5by10(int Array[ROWS][COLUMNS]);
void Print5by10(int Array[ROWS][COLUMNS]);
void SelectionSort(int Array[], unsigned int Size);

/*******************************************************************************
Function: Sort5by10

Description: sorts a two dimensional array into ascending order

Inputs: int Array[ROWS][COLUMNS]

Outputs: None
*******************************************************************************/
void Sort5by10(int Array[ROWS][COLUMNS])
{
	//loop through the array's rows and sort the columns
	for (int i = 0; i < ROWS; ++i)
	{
		SelectionSort(Array[i], COLUMNS);
	}
}

/*******************************************************************************
Function: Print5by10

Description: prints out the contents of a two dimensional array

Inputs: int Array[ROWS][COLUMNS]

Outputs: None
*******************************************************************************/
void Print5by10(int Array[ROWS][COLUMNS])
{
	//loop through rows
	for (int i = 0; i < ROWS; ++i)
	{
		//loop through columns
		for (int j = 0; j < COLUMNS; ++j)
		{
			//print out the value found at Array[i][j] followed by a space
			std::cout << Array[i][j] << + " ";
			
			//if the end of an array, add an endl
			if (j == (COLUMNS - 1))
			{
				std::cout << std::endl;
			}
		}
	}
}

/*******************************************************************************
Function: SelectionSort

Description: Sorts through an array and arranges contents in ascending order

Inputs: int Array[], unsigned int Size

Outputs: None
*******************************************************************************/
void SelectionSort(int Array[], unsigned int Size)
{
	//variable to store data temporarily
	int temp;

	//loop through array
	for (unsigned int i = 0; i < Size; ++i)
	{
		//ensure j doesn't exceed size of the array
		for (unsigned int j = i + 1; j < Size; ++j)
		{
			//evaluate i > j
			if (Array[i] > Array[j])
			{
				//swap values so smaller is first
				temp = Array[i];
				Array[i] = Array[j];
				Array[j] = temp;
			}
		}
	}
}