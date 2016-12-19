/*******************************************************************************
filename    Shape.h
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Header file for the Shape class. Uses but does not inherit from Point.h
*******************************************************************************/

#pragma once
#include "Point.h"

class Shape
{
	public:
		//methods
		Shape(Point center_);				//non-default constructor prototype
		virtual ~Shape();					//default destructor prototype
		virtual void Draw() const = 0;		//mark class as abstract (cannot instantiate any objects from this class)
		virtual void SetCenter(float x_, float y_); //SetCenter prototype
		Point GetCenter() const;			//GetCenter prototype

	protected:
		//properties
		Point center;						//center point
};