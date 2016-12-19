/*******************************************************************************
filename    Point.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Point class is used by all classes to define center points and vertices
*******************************************************************************/

#include "Point.h"

/*******************************************************************************
Function: Point

Description: Default Constructor

Inputs: None

Outputs: None
*******************************************************************************/
Point::Point()
{
	x = 0;
	y = 0;
}

/*******************************************************************************
Function: Point

Description: Non-Default Constructor

Inputs: float x_ — the x position of a point
		float y_ — the y position of a point

Outputs: None
*******************************************************************************/
Point::Point(float x_, float y_)
{
	x = x_;
	y = y_;
}

/*******************************************************************************
Function: Shape

Description: Default Destructor

Inputs: None

Outputs: None
*******************************************************************************/
Point::~Point()
{
}