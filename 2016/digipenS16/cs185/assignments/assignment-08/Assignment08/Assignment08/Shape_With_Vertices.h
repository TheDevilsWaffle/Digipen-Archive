/*******************************************************************************
filename    Shape_With_Vertices.h
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Header file for the Shape_With_Vertices class which is used to create Rectangle
and Polygon classes. Inherits from the base class Shape
*******************************************************************************/

#pragma once
#include "Shape.h"

class Shape_With_Vertices : public Shape
{
	public:
		//methods
		Shape_With_Vertices(Point center_, 
							unsigned int number_of_vertices_);			//non-default constructor prototype
		virtual ~Shape_With_Vertices();									//default destructor prototype
		virtual void SetCenter(float x_, float y_);
		virtual void Draw() const = 0;									//marks class as abstract (no objects can be instaniated from this class)

	protected:
		//properties
		unsigned int number_of_vertices;								//used to determine how many vertices derived classes will have
		Point * pVertices;												//used to point to an array of pVertices
		unsigned int vertexIDx_;										//used to point to a particular index in pVertices
};