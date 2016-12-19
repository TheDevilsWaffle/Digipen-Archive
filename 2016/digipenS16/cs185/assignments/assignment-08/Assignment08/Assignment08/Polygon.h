/*******************************************************************************
filename    Polygon.h
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Header file for the Polygon class. Inherits from the Shape_With_Vertices class
*******************************************************************************/

#pragma once
#include "Shape_With_Vertices.h"
class Polygon : public Shape_With_Vertices
{
public:
	//methods
	Polygon(Point center_, unsigned int number_of_vertices_);						//non-default constructor prototype
	Polygon(Point center_, const Point *points_, unsigned int number_of_vertices_);	//non-default constructor prototype
	Polygon(const Polygon &p_);														//non-default copy constructor prototype
	virtual void Draw() const;														//Draw prototype
	void SetVertex(unsigned int vertexIdx_, float x_, float y_);					//SetVertex prototype
};

