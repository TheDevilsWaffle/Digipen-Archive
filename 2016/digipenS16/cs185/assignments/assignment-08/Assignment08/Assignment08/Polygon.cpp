/*******************************************************************************
filename    Polygon.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Polygon class that is derived from Shape_With_Vertices class. Used to define
shapes with 'X' number of vertices.
*******************************************************************************/

#include "Polygon.h"
#include<iostream>
using std::cout;
using std::endl;

/*******************************************************************************
Function: Polygon

Description: Non Default Constructor

Inputs: Point center_                     — defines the center point of the shape
		unsigned int number_of_vertices_  — defines number of vertices in the shape

Outputs: None
*******************************************************************************/
Polygon::Polygon(Point center_, 
				 unsigned int number_of_vertices_)
				 : Shape_With_Vertices(center_, 
									   number_of_vertices_)
{
	//set all vertices from points_
	for (unsigned int i = 0; i < number_of_vertices_; ++i)
	{
		SetVertex(i, 0, 0);
	}
}

/*******************************************************************************
Function: Polygon

Description: Non Default Constructor

Inputs: Point center_                     — defines the center point of the shape
		const Point * points_			  — pointer to an array of points to define
											all vertices at initialization
		unsigned int number_of_vertices_  — defines number of vertices in the shape

Outputs: None
*******************************************************************************/
Polygon::Polygon(Point center_,
				 const Point * points_,
				 unsigned int number_of_vertices_)
				 : Shape_With_Vertices(center_,
									   number_of_vertices_)
{
	//set all vertices from points_
	for (unsigned int i = 0; i < number_of_vertices_; ++i)
	{
		SetVertex(i, points_[i].x, points_[i].y);
	}
}

/*******************************************************************************
Function: Polygon

Description: Non Default Copy Constructor

Inputs: Polygon &p_  — reference to a polygon

Outputs: None
*******************************************************************************/
Polygon::Polygon(const Polygon &p_) : Shape_With_Vertices(p_.center, p_.number_of_vertices)
{
	//set all vertices from points_
	for (unsigned int i = 0; i < p_.number_of_vertices; ++i)
	{
		SetVertex(i, p_.pVertices[i].x, p_.pVertices[i].y);
	}
}

/*******************************************************************************
Function: SetVertex

Description: sets a specified vertex by passing the x and y values of a new
			 vertex point

Inputs: Polygon &p_  — reference to a polygon

Outputs: None
*******************************************************************************/
void Polygon::SetVertex(unsigned int vertexIdx_, float x_, float y_)
{
	if (vertexIdx_ >= number_of_vertices)
	{
		cout << "Bad vertex index provided" << endl;
		return;
	}
	else
	{
		pVertices[vertexIdx_].x = center.x + x_;
		pVertices[vertexIdx_].y = center.y + y_;
	}
}

/*******************************************************************************
Function: Draw

Description: "Draws" the polygon by outputing the center point and all vertices
			 to the console

Inputs: None

Outputs: None
*******************************************************************************/
void Polygon::Draw() const
{
	//print out the polygon's center x and y
	cout << "Drawing a polygon at x = "
		<< center.x
		<< " y = "
		<< center.y
		<< endl;

	//loop through pVertices and print out each vertex
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

	//one final endl
	cout << endl;
}
