#include <iostream>

#define ROWS 5
#define COLUMNS 10

// Functions prototypes
void Sort5by10(int Array[ROWS][COLUMNS]);
void Print5by10(int Array[ROWS][COLUMNS]);

int main(void)
{
	// declaring and initializing an 2 dimesional(5x10) array of integers
	int Array[ROWS][COLUMNS] = {
		{ 10,8,7,6,5,4,9,2,13,0 },
		{ 0,1,2,3,4,5,6,7,8,9 },
		{ 9,8,7,6,5,4,3,2,1,0 },
		{ 1,5,2,7,3,6,8,5,34,3 },
		{ 9,8,7,6,5,0,1,2,3,4 }
	};

	// Calling the functions that will sort every row in the array and print out
	// the result
	Sort5by10(Array);
	Print5by10(Array);

	//system("pause");

	// returning a value to the OS
	return 0;

}