#include <iostream>

// Prototypes
int strtoint(const char string[]);
void inttostr(int number, char string[]);
void reverse_words1(const char input[], char output[]);
void reverse_words2(char input[]);

void test1(void)
{
	std::cout << strtoint("0") << std::endl;
	std::cout << strtoint("1") << std::endl;
	std::cout << strtoint("12") << std::endl;
	std::cout << strtoint("123") << std::endl;
	std::cout << strtoint("1234") << std::endl;
	std::cout << strtoint("1234567890") << std::endl;
	std::cout << strtoint("-0") << std::endl;
	std::cout << strtoint("-12") << std::endl;
	std::cout << strtoint("-123") << std::endl;
	std::cout << strtoint("-1234") << std::endl;
	std::cout << strtoint("-1234567890") << std::endl;
}

void test2(void)
{
	char buffer[12];

	inttostr(1, buffer);
	std::cout << "|" << buffer << "|" << std::endl;

#if 1
	inttostr(12, buffer);
	std::cout << "|" << buffer << "|" << std::endl;

	inttostr(123, buffer);
	std::cout << "|" << buffer << "|" << std::endl;

	inttostr(1234, buffer);
	std::cout << "|" << buffer << "|" << std::endl;

	inttostr(1234567890, buffer);
	std::cout << "|" << buffer << "|" << std::endl;

	inttostr(0, buffer);
	std::cout << "|" << buffer << "|" << std::endl;

	inttostr(-1, buffer);
	std::cout << "|" << buffer << "|" << std::endl;

	inttostr(-12, buffer);
	std::cout << "|" << buffer << "|" << std::endl;

	inttostr(-123, buffer);
	std::cout << "|" << buffer << "|" << std::endl;

	inttostr(-1234, buffer);
	std::cout << "|" << buffer << "|" << std::endl;

	inttostr(-1234567890, buffer);
	std::cout << "|" << buffer << "|" << std::endl;
#endif
}

void test3(void)
{
	{
		char words1[] = "This";
		char rev1[sizeof(words1) + 1];
		std::cout << " Input string: |" << words1 << "|" << std::endl;
		reverse_words1(words1, rev1);
		std::cout << "Output string: |" << rev1 << "|" << std::endl;
	}

	{
		char words1[] = "This is";
		char rev1[sizeof(words1) + 1];
		std::cout << " Input string: |" << words1 << "|" << std::endl;
		reverse_words1(words1, rev1);
		std::cout << "Output string: |" << rev1 << "|" << std::endl;
	}

#if 1
	{
		char words1[] = "This is a";
		char rev1[sizeof(words1) + 1];
		std::cout << " Input string: |" << words1 << "|" << std::endl;
		reverse_words1(words1, rev1);
		std::cout << "Output string: |" << rev1 << "|" << std::endl;
	}

	{
		char words1[] = "This is a string";
		char rev1[sizeof(words1) + 1];
		std::cout << " Input string: |" << words1 << "|" << std::endl;
		reverse_words1(words1, rev1);
		std::cout << "Output string: |" << rev1 << "|" << std::endl;
	}

	{
		char words1[] = "forth brought fathers our ago years seven and score Four";
		char rev1[sizeof(words1) + 1];
		std::cout << " Input string: |" << words1 << "|" << std::endl;
		reverse_words1(words1, rev1);
		std::cout << "Output string: |" << rev1 << "|" << std::endl;
	}

	{
		char words1[] = "   This  is another   string ";
		char rev1[sizeof(words1) + 1];
		std::cout << " Input string: |" << words1 << "|" << std::endl;
		reverse_words1(words1, rev1);
		std::cout << "Output string: |" << rev1 << "|" << std::endl;
	}
#endif
}

void test4(void)
{
	{
		char words[] = "This";
		std::cout << " Input string: |" << words << "|" << std::endl;
		reverse_words2(words);
		printf("Output string: |%s|\n", words);
	}

	{
		char words[] = "This is";
		std::cout << " Input string: |" << words << "|" << std::endl;
		reverse_words2(words);
		std::cout << "Output string: |" << words << "|" << std::endl;
	}

#if 1
	{
		char words[] = "This is a";
		std::cout << " Input string: |" << words << "|" << std::endl;
		reverse_words2(words);
		std::cout << "Output string: |" << words << "|" << std::endl;
	}

	{
		char words[] = "This is a string";
		std::cout << " Input string: |" << words << "|" << std::endl;
		reverse_words2(words);
		std::cout << "Output string: |" << words << "|" << std::endl;
	}

	{
		char words[] = "forth brought fathers our ago years seven and score Four";
		std::cout << " Input string: |" << words << "|" << std::endl;
		reverse_words2(words);
		std::cout << "Output string: |" << words << "|" << std::endl;
	}

	{
		char words[] = "   This  is another   string ";
		std::cout << " Input string: |" << words << "|" << std::endl;
		reverse_words2(words);
		std::cout << "Output string: |" << words << "|" << std::endl;
	}
#endif
}

int main(void)
{
	std::cout << std::endl;
	std::wcout << "============== Test 1 ================";
	std::cout << std::endl;
	test1();

#if 1
	std::cout << std::endl;
	std::wcout << "============== Test 2 ================";
	std::cout << std::endl;
	test2();

	std::cout << std::endl;
	std::wcout << "============== Test 3 ================";
	std::cout << std::endl;
	test3();

	std::cout << std::endl;
	std::wcout << "============== Test 4 ================";
	std::cout << std::endl;
	test4();
#endif

	//system("pause");
	return 0;
}
