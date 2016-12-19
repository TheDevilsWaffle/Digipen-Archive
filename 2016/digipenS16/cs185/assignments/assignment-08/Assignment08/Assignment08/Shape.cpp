/*******************************************************************************
filename    Shape.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Base class for Shapes_With_Vertices, Circle, Rectangle, and Polygon
*******************************************************************************/

#include "Shape.h"

/*******************************************************************************
Function: Shape

Description: Non-Default Constructor

Inputs: Point — Used to define the center point of the shape

Outputs: None
*******************************************************************************/
Shape::Shape(Point center_)
{
	center = center_;
}

/*******************************************************************************
Function: ~Shape

Description: Destructor

Inputs: None

Outputs: None
*******************************************************************************/
Shape::~Shape()
{
}

/*******************************************************************************
Function: SetCenter

Description: Sets the center point for the Shape based on passed parameters

Inputs: float x_ — the x position for the center point
		float y_ — the y position for the center point

Outputs: None
*******************************************************************************/
void Shape::SetCenter(float x_, float y_)
{
	center.x = x_;
	center.y = y_;
}

/*******************************************************************************
Function: GetCenter()

Description: Returns the center point

Inputs: None

Outputs: None
*******************************************************************************/
Point Shape::GetCenter() const
{
	return center;
}