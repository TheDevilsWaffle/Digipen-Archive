/*******************************************************************************
filename    Shape_With_Vertices.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Implementation for abstract class Shape_With_Vertices that extends from Shape
*******************************************************************************/

#include "Shape_With_Vertices.h"

/*******************************************************************************
Function: Shape_With_Vertices

Description: Non Default Constructor

Inputs: Point center_					 — the center point of the shape
		unsigned int number_of_vertices_ — the number of vertices for this shape

Outputs: None
*******************************************************************************/
Shape_With_Vertices::Shape_With_Vertices(Point center_, 
										 unsigned int number_of_vertices_) 
										 : Shape(center_), 
								           number_of_vertices(number_of_vertices_)
{
	//allocate new pointer array of Points
	pVertices = new Point[number_of_vertices];
}

/*******************************************************************************
Function: ~Shape_With_Vertices

Description: Non Default Destructor (deletes dynamically created pointer array
			 pVertices

Inputs: None

Outputs: None
*******************************************************************************/
Shape_With_Vertices::~Shape_With_Vertices()
{
	delete[] pVertices;
}

/*******************************************************************************
Function: SetCenter

Description: Non Default Copy Constructor

Inputs: float x_ — x position of the center
		float y_ — y position of the center

Outputs: None
*******************************************************************************/
void Shape_With_Vertices::SetCenter(float x_, float y_)
{
	//get the difference 
	float xDifference = x_ - center.x;
	float yDifference = y_ - center.y;

	//set the new center
	center.x = x_;
	center.y = y_;

	//loop through all vertices, update the points based on differences
	for (unsigned int i = 0; i < number_of_vertices; ++i)
	{
		pVertices[i].x += xDifference;
		pVertices[i].y += yDifference;
	}
}
