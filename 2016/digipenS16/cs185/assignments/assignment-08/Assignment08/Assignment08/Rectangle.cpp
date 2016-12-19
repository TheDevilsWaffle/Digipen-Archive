/*******************************************************************************
filename    Rectangle.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Rectangle class that inherits from Shape_With_Vertices to create circles
*******************************************************************************/

#include "Rectangle.h"
#include<iostream>
using std::cout;
using std::endl;

/*******************************************************************************
Function: Rectangle

Description: Non Default Constructor

Inputs: Point center_        — defines the center point of the shape
		unsigned int width_  — defines the width of the rectangle
		unsighed int height_ — defines the height of the rectangle

Outputs: None
*******************************************************************************/
Rectangle::Rectangle(Point center_,
	                 unsigned int width_, 
	                 unsigned int height_) 
	                 : Shape_With_Vertices(center_, 4), 
	                   width(width_), 
	                   height(height_)
{
	//set all vertices right from the start
	SetVertex(0, center.x - ((float)width / 2), center.y + ((float)height / 2)); //top left
	SetVertex(1, center.x + ((float)width / 2), center.y + ((float)height / 2)); //top right
	SetVertex(2, center.x + ((float)width / 2), center.y - ((float)height / 2)); //bottom right
	SetVertex(3, center.x - ((float)width / 2), center.y - ((float)height / 2)); //bottom left
}

/*******************************************************************************
Function: Rectangle

Description: Non Default Copy Constructor

Inputs: const Rectangle &rectangle_ — reference to another rectangle

Outputs: None
*******************************************************************************/
Rectangle::Rectangle(const Rectangle &r_) : Shape_With_Vertices(r_.center, r_.number_of_vertices)
{
	width = r_.width;
	height = r_.height;

	//set all vertices right from the start
	SetVertex(0, r_.center.x - ((float)r_.width / 2), r_.center.y + ((float)r_.height / 2)); //top left
	SetVertex(1, r_.center.x + ((float)r_.width / 2), r_.center.y + ((float)r_.height / 2)); //top right
	SetVertex(2, r_.center.x + ((float)r_.width / 2), r_.center.y - ((float)r_.height / 2)); //bottom right
	SetVertex(3, r_.center.x - ((float)r_.width / 2), r_.center.y - ((float)r_.height / 2)); //bottom left
}


/*******************************************************************************
Function: SetVertex()

Description: Private helper function to set the vertices of a rectangle

Inputs: unsigned int vertexIDx__ — used to access a specific index in pVertices
		float x_ — defines the x position of the point
		float y_ — defines the y position of the point

Outputs: None
*******************************************************************************/
void Rectangle::SetVertex(unsigned int vertexIDx_, float x_, float y_)
{
	pVertices[vertexIDx_].x = x_;
	pVertices[vertexIDx_].y = y_;
}

/*******************************************************************************
Function: Draw

Description: prints out the center, width, height, and each vertices of the
			 Rectangle (does not change the Rectangle in any way)

Inputs: None

Outputs: None
*******************************************************************************/
void Rectangle::Draw() const
{
	//display the center x, y, width, and height
	cout << "Drawing a rectangle at x = "
		<< center.x
		<< " y = "
		<< center.y
		<< " with width = "
		<< width
		<< " and height = "
		<< height
		<< endl;

	//loop and print out all vertices
	for (unsigned int i = 0; i < number_of_vertices; ++i)
	{
		cout << "Vertex "
			<< i
			<< " ("
			<< pVertices[i].x
			<< ", "
			<< pVertices[i].y
			<< ")"
			<< endl;
	}

	//last little endl
	cout << endl;
}