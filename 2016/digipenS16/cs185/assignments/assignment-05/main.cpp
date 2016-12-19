/******************************************************************************
filename    main.cpp
author      Artie Fufkin
DP email    artie.fufkin@digipen.edu
course      CS185
section     
assignment  5
due date    11/26/2037

Brief Description:
	This file contains the main function and the function prototypes for the
	Lab5 exercice.

******************************************************************************/
#include <iostream> // cout

// Function prototypes
void PrintArray(const int *a, int size);
void RemoveNumberInArray(int *a, int size, int number);

int main(void)
{
	// Arrays used as test cases
	int a[10] = { 1,2,4,7,2,5,4,3,5,9 };
	int b[10] = { 1,2,0,7,2,5,0,3,5,9 };
	int c[10] = { 1,2,4,7,2,3,4,3,2,9 };
	int d[10] = { 1,2,4,7,5,5,4,3,5,9 };
	int e[10] = { 1,2,4,7,5,5,4,3,5,5 };
	int f[1] = { 1 };
	int g[1] = { 5 };
	int h[10] = { 5,5,5,5,5,4,5,5,5,5 };


	// Test 1
	PrintArray(a, 10);
	std::cout << std::endl;
	RemoveNumberInArray(a, 10, 5);
	PrintArray(a, 10);
	std::cout << std::endl << std::endl;

	// Test 2
	PrintArray(b, 10);
	std::cout << std::endl;
	RemoveNumberInArray(b, 10, 5);
	PrintArray(b, 10);
	std::cout << std::endl << std::endl;

	// Test 3
	PrintArray(c, 10);
	std::cout << std::endl;
	RemoveNumberInArray(c, 10, 5);
	PrintArray(c, 10);
	std::cout << std::endl << std::endl;

	// Test 4
	PrintArray(d, 10);
	std::cout << std::endl;
	RemoveNumberInArray(d, 10, 5);
	PrintArray(d, 10);
	std::cout << std::endl << std::endl;

	// Test 5
	PrintArray(e, 10);
	std::cout << std::endl;
	RemoveNumberInArray(e, 10, 5);
	PrintArray(e, 10);
	std::cout << std::endl << std::endl;

	// Test 6
	PrintArray(f, 1);
	std::cout << std::endl;
	RemoveNumberInArray(f, 1, 5);
	PrintArray(f, 1);
	std::cout << std::endl << std::endl;

	// Test 7
	PrintArray(g, 1);
	std::cout << std::endl;
	RemoveNumberInArray(g, 1, 5);
	PrintArray(g, 1);
	std::cout << std::endl << std::endl;

	// Test 8
	PrintArray(h, 10);
	std::cout << std::endl;
	RemoveNumberInArray(h, 10, 5);
	PrintArray(h, 10);
	std::cout << std::endl << std::endl;

	//system("pause");
	return 0; // Return to the OS
}