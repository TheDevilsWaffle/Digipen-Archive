/*******************************************************************************
filename    Circle.h
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Header file for the Circle class. Inherits from the base class Shape
*******************************************************************************/

#pragma once
#include "Shape.h"

class Circle : public Shape
{
	public:
		//methods
		Circle(Point center_, unsigned int radius_);	//non-default constructor prototype
		~Circle();										//default destructor prototype
		virtual void Draw() const;						//overriding Draw prototype

	private:
		//properties
		unsigned int radius;							//radius of the circle
};

