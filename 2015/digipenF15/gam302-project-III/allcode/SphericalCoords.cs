/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - SphericalCoords.cs
//AUTHOR - Fernando Lima
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;

public class SphericalCoords {

    //radius, azimuth angle (in radians), polar angle (in radians) 
    public float r;
    public float az;
    public float po; 

    SphericalCoords()
    {
        r = 0;
        az = 0;
        po = 0; 
    }

    SphericalCoords(Vector3 rhs)
    {
        r = rhs.x;
        az = rhs.y;
        po = rhs.z; 
    }

    SphericalCoords(float x, float y, float z)
    {
        r = x;
        az = y;
        po = z; 
    }

	public Vector3 toCartesian()
    {
        Vector3 cartCoords;

        float cosAz = Mathf.Cos(az);
        float sinAz = Mathf.Sin(az);
        float cosPo = Mathf.Cos(po);
        float sinPo = Mathf.Sin(po);

        cartCoords.x = r * cosAz * sinPo;
        cartCoords.y = r * sinAz * sinPo;
        cartCoords.z = r * cosPo;
        
        return cartCoords; 
    }   
    //all of our angles are measured in radians
    public void toSpherical(Vector3 cartCoords)
    {
        float xSq = cartCoords.x * cartCoords.x;
        float ySq = cartCoords.y * cartCoords.y;
        float zSq = cartCoords.z * cartCoords.z; 

        r = Mathf.Sqrt(xSq + ySq + zSq);
        az = Mathf.Atan(cartCoords.y / cartCoords.x);
        po = Mathf.Acos(cartCoords.z / r); 
    }
}
