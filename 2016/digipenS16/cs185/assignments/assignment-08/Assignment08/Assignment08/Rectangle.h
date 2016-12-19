/*******************************************************************************
filename    Rectangle.h
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Header file for the Rectangle class. Inherits from the base class
Shape_With_Vertices.
*******************************************************************************/

#pragma once
#include "Shape_With_Vertices.h"

class Rectangle : public Shape_With_Vertices
{
public:
	//methods
	Rectangle(Point center_, 
		      unsigned int width_, 
		      unsigned int height_);								//non-default constructor
	Rectangle(const Rectangle &r_);									//non-default copy constructor

	virtual void Draw() const;										//overriding Draw prototype

protected:
	//properties
	unsigned int width;												//the width of the rectangle
	unsigned int height;											//the height of the rectangle

private:
	//helper method
	void SetVertex(unsigned int vertexIDx_, float x_, float y_);	//SetVertex prototype - used to set the vertices of the rectangle

};