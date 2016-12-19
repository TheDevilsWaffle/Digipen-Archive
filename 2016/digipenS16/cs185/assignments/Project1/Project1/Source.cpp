#include <iostream>
#include <string>

int main(void)
{
	char str[25] = "Hello C";
	char *str2 = "Hello";
	if (strcmp(str, str2) == 0)
	{
		std::cout << "They are equal";
	}
	else
	{
		std::cout << "They are not equal";
	}
	system("pause");
	return 0;
}