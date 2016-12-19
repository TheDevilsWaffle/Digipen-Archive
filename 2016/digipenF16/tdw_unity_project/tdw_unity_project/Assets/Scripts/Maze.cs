/*////////////////////////////////////////////////////////////////////////
//SCRIPT: Maze.cs
//AUTHOR: Travis Moore
////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic; //used to get access to the list data type

public class Maze : MonoBehaviour
{

    public IntVector2 size;

    public float generationStepDelay = 0.01f;

    public MazeCell cellPrefab;

    private MazeCell[,] cells;

    /*////////////////////////////////////////////////////////////////////////
    //GetCell(IntVector2)
    //retrieve coordinates of a given cell
    ////////////////////////////////////////////////////////////////////////*/
    public MazeCell GetCell(IntVector2 coordinates_)
    {
        return cells[coordinates_.x, coordinates_.z];
	}

    /*////////////////////////////////////////////////////////////////////////
    //Generate()
    //generates the spaceship cells
    ////////////////////////////////////////////////////////////////////////*/
    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];

        //create a temporary list to store cell coordinates
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        print(activeCells.Count);
        while(activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }

    /*////////////////////////////////////////////////////////////////////////
    //DoFirstGenerationStep(List<MazeCell>)
    //add cell to the list, create cell using CreateCell and RandomCoordinates
    ////////////////////////////////////////////////////////////////////////*/
    private void DoFirstGenerationStep(List<MazeCell> activeCells_)
    {
        activeCells_.Add(CreateCell(RandomCoordinates));
    }

    /*////////////////////////////////////////////////////////////////////////
    //CreateCell(IntVector2)
    //
    ////////////////////////////////////////////////////////////////////////*/
    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f,
                                                      0f, 
                                                      coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    /*////////////////////////////////////////////////////////////////////////
    //DoNextGenerationStep(List<MazeCell>)
    //
    ////////////////////////////////////////////////////////////////////////*/
    private void DoNextGenerationStep(List<MazeCell> activeCells_)
    {
        int currentIndex = activeCells_.Count - 1;
        MazeCell currentCell = activeCells_[currentIndex];
        MazeDirection direction = MazeDirections.RandomValue;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();

        //if we don't have a cell here, make a new cell, otherwise, remove and start over
        if (ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
        {
            activeCells_.Add(CreateCell(coordinates));
            print("new cell created at" + coordinates.x + coordinates.z);
        }
        else {
            activeCells_.RemoveAt(currentIndex);
            print("DUPLICATE, cell NOT created at " + coordinates.x + coordinates.z);
        }
    }

    /*////////////////////////////////////////////////////////////////////////
    //RandomCoordinates()
    //
    ////////////////////////////////////////////////////////////////////////*/
    public IntVector2 RandomCoordinates
    {
        get
        { return new IntVector2(Random.Range(0, size.x), 
                                Random.Range(0, size.z));
        }
    }

    /*////////////////////////////////////////////////////////////////////////
    //ContainsCoordinates(IntVector2)
    //
    ////////////////////////////////////////////////////////////////////////*/
    public bool ContainsCoordinates(IntVector2 coordinate_)
    {
        return coordinate_.x >= 0 
               && coordinate_.x < size.x 
               && coordinate_.z >= 0 
               && coordinate_.z < size.z;
    }
}
