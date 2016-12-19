/*******************************************************************************
filename    Circle.cpp
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Circle class that inherits from Shape to create circles
*******************************************************************************/

#include "Circle.h"
#include<iostream>
using std::cout;
using std::endl;

/*******************************************************************************
Function: Circle

Description: Non-Default Constructor

Inputs: Point center_ — defines the center point of the shape
		unsigned int radius_ — defines the radius of the circle

Outputs: None
*******************************************************************************/
Circle::Circle(Point center_, unsigned int radius_) : Shape(center_), radius(radius_)
{
}

/*******************************************************************************
Function: ~Circle

Description: Default Destructor

Inputs: None

Outputs: None
*******************************************************************************/
Circle::~Circle()
{
}

/*******************************************************************************
Function: Draw

Description: Prints out the center point and the radius of a circle

Inputs: None

Outputs: None
*******************************************************************************/
void Circle::Draw() const
{
	cout << "Drawing a circle at x = " 
		 << center.x 
		 << " y = " 
		 << center.y 
		 << " and radius " 
		 << radius 
		 << endl 
		 << endl;
}