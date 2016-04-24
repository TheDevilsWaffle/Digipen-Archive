/*******************************************************************************
filename    Point.h
author      Travis Moore
DP email    travis.moore@digipen.edu
course      CS185
section
assignment  8

Brief Description:
Header file for the Point Class
*******************************************************************************/

#pragma once

class Point
{
	public:
		//methods
		Point();					//default constructor prototype
		Point(float x_, float y_);	//non-default constructor prototype
		~Point();					//default destructor prototype
		
		//properties
		float x;					//point x
		float y;					//point y
};

