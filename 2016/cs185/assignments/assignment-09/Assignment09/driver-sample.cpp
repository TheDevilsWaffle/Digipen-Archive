#include "List.h"
#include <iostream>

void TestPushFront(void)
{
	std::cout << "TestPushFront..." << std::endl;

	int size = 5;
	CS170::List list;
	for (int i = 1; i <= size; i++)
	{
		list.push_front(i);
	}

	list.display();
	std::cout << "Items in the list: " << list.list_size() << std::endl;
	std::cout << std::endl;
}

void TestPushBack(void)
{
	std::cout << "TestPushBack..." << std::endl;

	int size = 5;
	CS170::List list;
	for (int i = 1; i <= size; i++)
	{
		list.push_back(i);
	}

	list.display();
	std::cout << "Items in the list: " << list.list_size() << std::endl;
	std::cout << std::endl;
}

void TestPushFrontBack(void)
{
	std::cout << "TestPushFrontBack..." << std::endl;

	int size = 10;
	CS170::List list;
	for (int i = 1; i <= size; i++)
	{
		list.push_front(i);
		list.push_back(i * 5);
	}

	list.display();
	std::cout << "Items in the list: " << list.list_size() << std::endl;
	std::cout << std::endl;
}

void TestPopFront(void)
{
	std::cout << "TestPopFront..." << std::endl;

	int size = 5;
	CS170::List list;
	for (int i = 1; i <= size; i++)
	{
		list.push_front(i);
	}

	list.display();
	while (!list.empty())
	{
		int item = list.pop_front();
		std::cout << "First item was: " << item << std::endl;
		std::cout << "New list:";
		list.display();
		std::cout << "Items in the list: " << list.list_size() << std::endl;
	}
	std::cout << std::endl;
}

void TestPopBack(void)
{
	std::cout << "TestPopBack..." << std::endl;

	int size = 5;
	CS170::List list;
	for (int i = 1; i <= size; i++)
	{
		list.push_front(i);
	}

	list.display();
	while (!list.empty())
	{
		int item = list.pop_back();
		std::cout << "Last item was: " << item << std::endl;
		std::cout << "New list:";
		list.display();
		std::cout << "Items in the list: " << list.list_size() << std::endl;
	}
	std::cout << std::endl;
}

void TestArray1(void)
{
	std::cout << "TestArray1..." << std::endl;

	int array[] = { 4, 7, 12, 5, 9, 23, 7, 11, 15, 2 };
	int size = static_cast<int>(sizeof(array) / sizeof(*array));

	// Construct from array
	CS170::List list(array, size);

	list.display();
	std::cout << "Items in the list: " << list.list_size() << std::endl;
	std::cout << std::endl;
}

void TestArray2(void)
{
	std::cout << "TestArray2..." << std::endl;

	const int array[] = { 4, 7, 12, 5, 9, 23, 7, 11, 15, 2 };
	int size = static_cast<int>(sizeof(array) / sizeof(*array));

	// Construct from array
	CS170::List list(array, size);

	list.display();
	std::cout << "Items in the list: " << list.list_size() << std::endl;
	std::cout << std::endl;
}

void TestCopyConstructor1(void)
{
	std::cout << "TestCopyConstructor1..." << std::endl;
	int size = 10;
	CS170::List list1;
	for (int i = 1; i <= size; i++)
	{
		list1.push_front(i * 3);
	}

	std::cout << "List 1: ";
	list1.display();

	CS170::List list2(list1);
	std::cout << "List 2: ";
	list2.display();

	std::cout << std::endl;
}

void TestCopyConstructor2(void)
{
	std::cout << "TestCopyConstructor2..." << std::endl;
	int size = 10;
	CS170::List list1;
	for (int i = 1; i <= size; i++)
		list1.push_front(i * 3);

	std::cout << "List 1: ";
	list1.display();

	const CS170::List list2(list1);
	std::cout << "List 2: ";
	list2.display();

	if (list2.empty())
	{
		std::cout << "List 2 is empty\n";
	}
	else
	{
		std::cout << "List 2 is not empty\n";
	}

	std::cout << "Items in List2: ";
	std::cout << list2.list_size();
	std::cout << std::endl;

	std::cout << std::endl;
}

void TestRemoveNode(void)
{
	std::cout << "TestRemoveNode..." << std::endl;

	int size = 5;
	CS170::List list1;
	for (int i = 1; i <= size; i++)
	{
		list1.push_front(i);
	}

	std::cout << "Original List:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.remove_node_by_value(3);
	std::cout << "After removing value 3:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.remove_node_by_value(1);
	std::cout << "After removing value 1:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.remove_node_by_value(5);
	std::cout << "After removing value 5:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.remove_node_by_value(10);
	std::cout << "After removing value 10:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.remove_node_by_value(2);
	std::cout << "After removing value 2:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.remove_node_by_value(4);
	std::cout << "After removing value 4:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.remove_node_by_value(5);
	std::cout << "After remove attempt in empty list:" << std::endl;
	std::cout << "List: ";
	list1.display();

	std::cout << "Items in the list: " << list1.list_size() << std::endl;
	std::cout << std::endl;
}

void TestInsertNodeAt(void)
{
	std::cout << "TestInsertNodeAt..." << std::endl;

	int size = 5;
	CS170::List list1;

	list1.insert_node_at(0, 4);
	std::cout << "After inserting value 4 at location 0:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.insert_node_at(0, 3);
	std::cout << "After inserting value 3 at location 0:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.insert_node_at(10, 5);
	std::cout << "After inserting value 5 at location 10:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.insert_node_at(3, 6);
	std::cout << "After inserting value 3 at location 6:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.insert_node_at(-5, 1);
	std::cout << "After inserting value 1 at location -5:" << std::endl;
	std::cout << "List: ";
	list1.display();

	list1.insert_node_at(1, 2);
	std::cout << "After inserting value 2 at location 1:" << std::endl;
	std::cout << "List: ";
	list1.display();

	std::cout << "Items in the list: " << list1.list_size() << std::endl;
	std::cout << std::endl;
}

int main(void)
{
#if 1
	try {
		TestCopyConstructor1();
	}
	catch (...) {
		std::cout << "***TestCopyConstructor1 revealed something bad in the List class" << std::endl;
	}

	std::cout << "============================================\n";
	std::cout << "Total number of Lists created: ";
	std::cout << CS170::List::created_list_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;

	std::cout << "============================================\n";
	std::cout << "Total number of nodes alive: ";
	std::cout << CS170::List::alive_node_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;
#endif

#if 0
	try {
		TestCopyConstructor2();
	}
	catch (...) {
		std::cout << "***TestCopyConstructor2 revealed something bad in the List class" << std::endl;
	}

	std::cout << "============================================\n";
	std::cout << "Total number of Lists created: ";
	std::cout << CS170::List::created_list_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;

	std::cout << "============================================\n";
	std::cout << "Total number of nodes alive: ";
	std::cout << CS170::List::alive_node_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;
#endif

#if 0
	try {
		TestArray1();
	}
	catch (...) {
		std::cout << "***TestArray1 revealed something bad in the List class" << std::endl;
	}

	std::cout << "============================================\n";
	std::cout << "Total number of Lists created: ";
	std::cout << CS170::List::created_list_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;

	std::cout << "============================================\n";
	std::cout << "Total number of nodes alive: ";
	std::cout << CS170::List::alive_node_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;
#endif

#if 0
	try {
		TestArray2();
	}
	catch (...) {
		std::cout << "***TestArray2 revealed something bad in the List class" << std::endl;
	}

	std::cout << "============================================\n";
	std::cout << "Total number of Lists created: ";
	std::cout << CS170::List::created_list_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;

	std::cout << "============================================\n";
	std::cout << "Total number of nodes alive: ";
	std::cout << CS170::List::alive_node_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;
#endif

#if 0
	try {
		TestPushFront();
	}
	catch (...) {
		std::cout << "***TestPushFront revealed something bad in the List class" << std::endl;
	}

	std::cout << "============================================\n";
	std::cout << "Total number of Lists created: ";
	std::cout << CS170::List::created_list_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;

	std::cout << "============================================\n";
	std::cout << "Total number of nodes alive: ";
	std::cout << CS170::List::alive_node_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;
#endif

#if 0
	try {
		TestPushBack();
	}
	catch (...) {
		std::cout << "***TestPushBack revealed something bad in the List class" << std::endl;
	}

	std::cout << "============================================\n";
	std::cout << "Total number of Lists created: ";
	std::cout << CS170::List::created_list_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;

	std::cout << "============================================\n";
	std::cout << "Total number of nodes alive: ";
	std::cout << CS170::List::alive_node_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;
#endif

#if 0
	try {
		TestPushFrontBack();
	}
	catch (...) {
		std::cout << "***TestPushFrontBack revealed something bad in the List class" << std::endl;
	}

	std::cout << "============================================\n";
	std::cout << "Total number of Lists created: ";
	std::cout << CS170::List::created_list_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;

	std::cout << "============================================\n";
	std::cout << "Total number of nodes alive: ";
	std::cout << CS170::List::alive_node_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;
#endif

#if 0
	try {
		TestPopFront();
	}
	catch (...) {
		std::cout << "***TestPopFront revealed something bad in the List class" << std::endl;
	}

	std::cout << "============================================\n";
	std::cout << "Total number of Lists created: ";
	std::cout << CS170::List::created_list_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;

	std::cout << "============================================\n";
	std::cout << "Total number of nodes alive: ";
	std::cout << CS170::List::alive_node_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;
#endif

#if 0
	try {
		TestRemoveNode();
	}
	catch (...) {
		std::cout << "***TestRemoveNode revealed something bad in the List class" << std::endl;
	}

	std::cout << "============================================\n";
	std::cout << "Total number of Lists created: ";
	std::cout << CS170::List::created_list_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;

	std::cout << "============================================\n";
	std::cout << "Total number of nodes alive: ";
	std::cout << CS170::List::alive_node_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;
#endif

#if 0
	try {
		TestInsertNodeAt();
	}
	catch (...) {
		std::cout << "***TestInsertNodeAt revealed something bad in the List class" << std::endl;
	}

	std::cout << "============================================\n";
	std::cout << "Total number of Lists created: ";
	std::cout << CS170::List::created_list_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;

	std::cout << "============================================\n";
	std::cout << "Total number of nodes alive: ";
	std::cout << CS170::List::alive_node_count();
	std::cout << std::endl;
	std::cout << "============================================\n";
	std::cout << std::endl;
#endif

	return 0;
}
