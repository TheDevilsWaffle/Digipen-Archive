#include <iostream> /* ostream */

namespace CS170
{
	class List
	{
	public:
		// Default constructor
		List();

		// Copy contructor for constructing a list from an existing list
		List(const List &list);

		// Contruct a list from an integer array
		List(const int *array, int size);

		// Destructor
		~List();

		// adds the item to the front of the list
		void push_front(int value_);
		// adds the item to the end of the list
		void push_back(int value_);
		// removes the first item in the list
		int pop_front();
		// removes the last item in the list
		int pop_back();
		// removes the first node it finds with the user defined value
		void remove_node_by_value(int value_);
		// inserts a new node in the list. The node will be inserted 
		// at the user defined location and will have the user defined value.
		void insert_node_at(int location_, int value_);
		
		// returns the number of items in the list
		int list_size() const;
		// returns true if empty, else false
		bool empty() const;
		// clears the list from all nodes
		void clear();

		// display for printing lists 
		void display(void) const;

		// returns the number of Lists that have been created
		static int created_list_count();

		// returns the number of Nodes that are still alive
		static int alive_node_count();

	private:

		// used to build a linked list of integers
		struct Node
		{
			Node(int);              // constructor
			~Node();                // destructor
			Node *next;             // pointer to the next Node
			int data;               // the actual data in the node
			static int nodes_alive; // count of nodes still allocated
		};

		Node *head; // pointer to the head of the list
		Node *tail; // pointer to the last node
		int size;   // number of items in the list

		static int list_count;       // number of Lists created
		Node *new_node(int data_) const; // allocate node, initialize data/next
	};

} // namespace CS170
