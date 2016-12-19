/*////////////////////////////////////////////////////////////////////////
//SCRIPT: MazeDirection.cs
//AUTHOR: Travis Moore
////////////////////////////////////////////////////////////////////////*/

using UnityEngine;

public enum MazeDirection
{
    North,
    East,
    South,
    West
}

//used to get a random direction from MazeDirection enum
public static class MazeDirections
{
    //update this with new directions if needed
    public const int Count = 4;

    //get a random direction
    public static MazeDirection RandomValue
    {
        get
        {
            //return a random direction within the range of enum values
            return (MazeDirection)Random.Range(0, Count);
        }
    }

    //used to convert a direction into an integer vector
    //used only by this script, hence static
    private static IntVector2[] vectors =
    {
        new IntVector2(0, 1),   //north
        new IntVector2(1, 0),   //east
        new IntVector2(0, -1),  //south
        new IntVector2(-1, 0)   //west
    };

    //given a direction it will return the correct conversion from enum to IntVector
    //"this" allows us to use it as an extension method instead of having to call "MazeDirections.ToIntVector2" every time
    public static IntVector2 ToIntVector2(this MazeDirection direction_)
    {
        return vectors[(int)direction_];
    }
}
