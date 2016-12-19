/*******************************************************************************
filename    List.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  9

Brief Description:
Implementation for the List class which creates a linked list from nodes

*******************************************************************************/

#include <iomanip>  // setw
#include <iostream> // ostream, endl
#include "List.h"

namespace CS170
{
	//set these counters to 0 upon list creation
	int List::list_count = 0;
	int List::Node::nodes_alive = 0;

	/////////////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////////
	// public methods

	/*******************************************************************************
	Function: List

	Description: default constructor

	Inputs: None

	Outputs: None
	*******************************************************************************/
	List::List()
	{
		//increment the number of lists created
		++list_count;
	}

	/*******************************************************************************
	Function: List 

	Description: copy constructor

	Inputs: const List — a list to make a copy of (doesn't change this list)

	Outputs: None
	*******************************************************************************/
	List::List(const List &list)
	{
		//reset size
		size = 0;

		//create a temporary Node pointer equal the head of the passed list
		Node *pIterator = list.head;

		//loop through the passed list using pIterator
		while (pIterator) // as long as pIterator isn't null
		{
			//DEBUG
			//std::cout << "pIterator is pointing at -> " << pIterator->data << std::endl;

			//add node using push_back giving it list's data
			push_back(pIterator->data);

			//increment pIterator to pIterator's next
			pIterator = pIterator->next;
		}

		//increment the number of lists created
		++list_count;
	}

	/*******************************************************************************
	Function: List

	Description: creates a new list from an array of ints

	Inputs: const int *array — pointer to an array of ints (values will not be
							   changed in this array)
			        int size — the size of the passed array
	Outputs: None
	*******************************************************************************/
	List::List(const int *array, int size)
	{
		//loop through array based on passed size
		for (int i = 0; i < size; ++i)
		{
			push_back(array[i]);
		}

		//increment the number of lists created
		++list_count;
	}

	/*******************************************************************************
	Function: ~List

	Description: destructor

	Inputs: None

	Outputs: None
	*******************************************************************************/
	List::~List()
	{
		//call clear
		clear();
	}

	/*******************************************************************************
	Function: push_front

	Description: creates a new Node and places it at the front of the list

	Inputs: int value_ — the number to place in Node's data

	Outputs: None
	*******************************************************************************/
	void List::push_front(int value_)
	{
		//use new_node function to create a new node, point to it with pNode
		Node *pNewNode = new_node(value_);

		//check to see if this list is NULL
		if (List::head == NULL)
		{
			//set head pointer to this pNode
			head = pNewNode;
			//set tail pointer to this pNode
			tail = pNewNode;
		}
		//this is not a new list (already has a node in it)
		else
		{
			//create a temp pointer to the head of the list
			Node * pTemp = head;

			//set pNewNode's next pointer to pTemp
			pNewNode->next = pTemp;

			//now set head pointer to pNewNode
			head = pNewNode;
		}

		//increment the number of nodes we've created
		++List::size;
	}

	/*******************************************************************************
	Function: push_back

	Description: creates a new node and places it at the back of the list

	Inputs: int value_ — the number to put in Node's data

	Outputs: None
	*******************************************************************************/
	void List::push_back(int value_)
	{
		//use new_node function to create a new node, point to it with pNode
		Node *pNewNode = new_node(value_);

		//check to see if this list is NULL
		if (List::tail == NULL)
		{
			//set head pointer to this pNode
			head = pNewNode;
			//set tail pointer to this pNode
			tail = pNewNode;
		}
		//this is not a new list (already has a node in it)
		else
		{
			//set the tail as the pNewNode
			tail = pNewNode;
			pNewNode->next = NULL;

			//create a temp pointer to the
			Node * pIterator = head;

			//traverse through the pIterator until we reach the second before the last Node
			while (pIterator)
			{
				if (pIterator->next == NULL)
				{
					//set the second before last's next to the tail's next
					pIterator->next = tail;
					break;
				}

				//increment pIterator
				pIterator = pIterator->next;
			}
		}

		//increment the number of nodes we've created
		++List::size;
	}

	/*******************************************************************************
	Function: pop_front

	Description: removes the first node in the list (pointed to by head)

	Inputs: None

	Outputs: int — the Node's data value that was deleted
	*******************************************************************************/
	int List::pop_front()
	{
		//if head exists
		if (head)
		{
			//create a temporary pointer to point to the head
			Node * pTemp = head;
			//create a temporary int to hold data
			int tempData = head->data;

			//set the head to head's next
			head = head->next;

			//set pTemp's next to null and then delete pTemp
			pTemp->next = NULL;
			delete pTemp;

			//decrement the number of nodes in this list
			--List::size;

			//return tempData
			return tempData;
		}

		//head doesn't exist
		else
			return -1;
	}

	/*******************************************************************************
	Function: pop_back

	Description: removes the last node in the list (pointed to by tail)

	Inputs: None

	Outputs: int — the Node's data value that was deleted
	*******************************************************************************/
	int List::pop_back()
	{
		//if tail exists
		if (tail)
		{
			//create a temporary pointer to point to the tail and head
			Node * pTempTail = tail;
			Node * pTempIterator = head;
			//create a temporary int to hold data
			int tempData = tail->data;

			//loop through the array
			while (pTempIterator)
			{
				//find the second before last Node
				if (pTempIterator->next->next == NULL)
				{
					//tail is now this Node and we set its next to NULL
					tail = pTempIterator;
					tail->next = NULL;
				}

				//increment pTempHead
				pTempIterator = pTempIterator->next;
			}

			//set pTempTail to null before we delete it
			pTempTail->next = NULL;
			delete pTempTail;

			//decrement the number of nodes in this list
			--List::size;

			//return tempData
			return tempData;
		}

		//tail doesn't exist
		else
			return -1;
	}

	/*******************************************************************************
	Function: remove_node_by_value

	Description: removes any nodes that have data that matches the passed value

	Inputs: int value_ — the value that we are trying to match in a Node's data

	Outputs: None
	*******************************************************************************/
	void List::remove_node_by_value(int value_)
	{
		//condition for empty list
		if (empty())
			return;
		//first search the head and tail
		
		//value is found at the head
		if (head->data == value_)
		{
			pop_front();
		}

		//value is found at the tail
		else if (tail->data == value_)
		{
			pop_back();
		}

		//create a temporary pointer to point at the head
		Node * pIterator = head;

		//traverse the rest of the linked list
		while (pIterator)
		{
			//from previous node, check the next Node's data
			if (pIterator->next != NULL)
			{
				if (pIterator->next->data == value_)
				{
					//create a temporary pointer to point for the thing we are going to delete
					Node * pToDelete = pIterator->next;

					//set pIterator's next to pTempToDelete's next
					pIterator->next = pToDelete->next;

					//set pToDelete's next to null and delete the pToDelete
					pToDelete->next = NULL;
					delete pToDelete;

					//decrement the counter of nodes in this list
					--List::size;
				}
			}

			//increment pIterator
			pIterator = pIterator->next;
		}
	}

	/*******************************************************************************
	Function: insert_node_at

	Description: inserts a Node at location_ with data equal to value_

	Inputs: int location_ — the new Node's location in the list

	Outputs: int value_   — the new Node's data value
	*******************************************************************************/
	void List::insert_node_at(int location_, int value_)
	{
		//if location is larger than our list, place node in the back and exit
		if (location_ > size - 1)
		{
			push_back(value_);
			return;
		}

		//if location is smaller than our list, place node in the front and exit
		else if (location_ <= 0)
		{
			push_front(value_);
			return;
		}

		//location is not the front or back, so it's inbetween
		else
		{
			//create temporary pointer
			Node *pIterator = head;

			//go through the loops until we reach the right location
			for (int i = 0; i < (location_ - 1); ++i)
			{
				pIterator = pIterator->next;
			}

			//pIterator is now in the right spot, create new node here
			Node *pNode = new_node(value_);
			//update the next pointer to pIterator's next
			pNode->next = pIterator->next;
			//update the previous node, update it's next to pNode
			pIterator->next = pNode;
			//increment nodes in the list
			++size;
		}
	}

	/*******************************************************************************
	Function: list_size

	Description: returns the number of nodes in a list

	Inputs: None

	Outputs: int  — the number of nodes in this list
	*******************************************************************************/
	int List::list_size() const
	{
		return size;
	}

	/*******************************************************************************
	Function: empty

	Description: checks to see if a list is empty of nodes

	Inputs: None

	Outputs: bool — true = empty, false = not empty
	*******************************************************************************/
	bool List::empty() const
	{
		//if head is not null, then the list is not empty of nodes: false
		if (head)
			return false;

		//there are not nodes, so we are empty: true
		else
			return true;
	}

	/*******************************************************************************
	Function: clear

	Description: clears all nodes from the list by using pop_front

	Inputs: None

	Outputs: None
	*******************************************************************************/
	void List::clear()
	{
		//use pop_front to get rid of all nodes
		while (head)
		{
			pop_front();
		}
	}

	/*******************************************************************************
	Function: display

	Description: goes through the list and prints each node's data

	Inputs: None

	Outputs: None
	*******************************************************************************/
	void List::display(void) const
	{
		// Start at the top
		List::Node *pnode = head;

		// Print each item
		while (pnode != 0)
		{
			std::cout << std::setw(4) << pnode->data;
			pnode = pnode->next;
		}
		std::cout << std::endl;
	}

	/*******************************************************************************
	Function: created_list_count

	Description: returns how many lists were created

	Inputs: None

	Outputs: None
	*******************************************************************************/
	int List::created_list_count()
	{
		return list_count;
	}

	/*******************************************************************************
	Function: alive_node_count

	Description: returns how many nodes are still alive

	Inputs: None

	Outputs: None
	*******************************************************************************/
	int List::alive_node_count()
	{
		return Node::nodes_alive;
	}

	/////////////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////////
	// private methods

	/*******************************************************************************
	Function: Node

	Description: non-default constructor for a Node

	Inputs: int value_ — the number to put in Node's data

	Outputs: None
	*******************************************************************************/
	List::Node::Node(int value_)
	{
		//set node's data equal to passed value_
		data = value_;
		//set node's Node pointer next to 0
		next = 0;
		//increment nodes_alive because we just made a new Node
		++nodes_alive;
	}

	/*******************************************************************************
	Function: push_back

	Description: creates a new node and places it at the back of the list

	Inputs: int value_ — the number to put in Node's data

	Outputs: None
	*******************************************************************************/
	List::Node::~Node()
	{
		//delete next pointer
		delete next;

		//decrement nodes_alive
		--nodes_alive;
		//std::cout << "nodes_alive is currently = " << nodes_alive << std::endl;
	}

	/*******************************************************************************
	Function: new_node

	Description: creates a new node returns a Node pointer to it

	Inputs: int data — the number to put in Node's data

	Outputs: Node pointer
	*******************************************************************************/
	List::Node *List::new_node(int data) const
	{
		// Make sure we have room
		Node *node = new Node(data); // create the node
		return node;
	}

} //namespace CS170
