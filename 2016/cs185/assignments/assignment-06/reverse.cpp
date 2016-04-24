/*******************************************************************************
filename    reverse.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  6

Brief Description:
This file turns a string into integers, integers into strings, and reverses
arrays of strings in two ways.

*******************************************************************************/

#include <string.h>

/*******************************************************************************
Function: my_power

Description: changes a passed ref value_ to the power of pow_

Inputs: int &value_ — the passed value
		int pow_ — the power to raise to

Outputs: int
*******************************************************************************/
int my_power(int &value_, int pow_)
{
	//loop through and multiply by 10 pow_ times
	for (; pow_ > 0; --pow_)
	{
		value_ *= 10;
	}
	return value_;
}

/*******************************************************************************
Function: strtoint

Description: converts a string of digits into an integer

Inputs: const char string[] — array of character strings that we'll be turning
							  into an int

Outputs: int
*******************************************************************************/
int strtoint(const char string[])
{
	//variables we'll need
	int i = 0;
	int value = 0;
	int negativeCheck = 1;
	int stringLength = strlen(string);

	//check to see if we're dealing with a negative number
	if (string[i] == '-')
	{
		//multiply final number by -1
		negativeCheck = -1;
		//adjust i
		++i;
	}
	//loop through string[]
	for (; i < stringLength; ++i)
	{
		//change string to int based on ascii value
		int temp = string[i] - '0';

		//use my_power on value based on location in string[]
		value += my_power(temp, (stringLength - i - 1));
	}
	
	//return the value based on negative check
	return value * negativeCheck;
}

/*******************************************************************************
Function: inttostr

Description: turns the passed number into a string

Inputs: int number — the number to change into a string
		char string[] — the array of characters to build from the int

Outputs: None
*******************************************************************************/
void inttostr(int number, char string[])
{
	//variables we'll need
	int i = 0;
	int stringLength = strlen(string);
	char temp;
	bool negative = false;

	//edge case for zero
	if (number == 0)
	{
		string[i] = '0';
		string[++i] = '\0';
	}

	//edge case for negative number
	if (number < 0)
	{
		number *= -1;
		negative = true;
	}

	//loop through the number
	while (number > 0)
	{
		//tranform part of number into char array (48 is ascii value for 0)
		string[i] = number % 10 + 48;
		//reduce number by 10
		number /= 10;
		//decrement stringLength
		++i;
	}

	//append a negative sign if the number was originally negative
	if (negative)
	{
		string[i] = '-';
		++i;
	}

	//null terminate the string
	string[i] = '\0';

	//find new length of string[]
	stringLength = strlen(string);

	//reverse the array
	for (i = 0; i < stringLength / 2; ++i)
	{
		temp = string[stringLength - i - 1];
		string[stringLength - i - 1] = string[i];
		string[i] = temp;
	}
}

/*******************************************************************************
Function: reverse_words1

Description: reverses the words in an array

Inputs: char output[] — array of characters that will hold the reversed content
		const char input[] - array of characters we will not change

Outputs: None
*******************************************************************************/
void reverse_words1(const char input[], char output[])
{
	//variables we'll need
	int start = 0;
	int end = strlen(input) - 1;
	int word = 0;
	int temp;

	//loop through the input[] starting from the back
	for (; end >= 0; --end)
	{
		//we've encountered a space
		if (input[end] == ' ')
		{
			temp = end + 1;
			for (; word > 0; --word)
			{
				output[start++] = input[temp++];
			}
			//insert a space
			output[start++] = ' ';
			//reset word
			word = 0;
			continue;
		}
		//we've reached the end of input[]
		if (end == 0)
		{
			temp = end;
			for (; word >= 0; --word)
			{
				output[start++] = input[temp++];
			}
		}
		//increment word
		++word;
	}
	//insert a null terminator
	output[start] = 0;
}

/*******************************************************************************
Function: reverse

Description: reverses all characters between two pointers

Inputs: char *start — first pointer (start of the word)
		char *end — second pointer (end of the word)

Outputs: None
*******************************************************************************/
void reverse(char *start, char *end)
{
	char temp;
	//meet in the middle
	while (start < end)
	{
		//swap
		temp = *start;
		*start++ = *end;
		*end-- = temp;
	}
}

/*******************************************************************************
Function: reverse_words2

Description: reverses the order of words in a string[]

Inputs: char string[] — an array of characters that we'll reverse using the same
						array.

Outputs: None
*******************************************************************************/
void reverse_words2(char string[])
{
	//get length of string
	int length = strlen(string);

	//pointers to the start and end of string[]
	char * start = &string[0];
	char * end = &string[length - 1];

	//reverse every character in array
	reverse(start, end);

	//reset start and end
	start = &string[0];
	end = &string[length - 1];

	//create a variable to capture word length
	int word = 0;

	//loop through string[]
	for (int i = 0; i < length; ++i)
	{
		//end of array, reverse the word
		if (start == end)
		{
			//use reverse helper function
			reverse(start - word, start);
		}
		//we've encounted a space, reverse the word
		if (*start == ' ')
		{
			//use reverse helper function
			reverse(start - word, start - 1);
			//reset word
			word = 0;
			//increment to next position
			start++;
			//break out so word doesn't get incremented
			continue;
		}
		//increment both word and start's position
		++word;
		start++;
	}
}
