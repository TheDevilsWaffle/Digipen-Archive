///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — MapGenerator.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class MapGenerator : MonoBehaviour
{
    #region FIELDS
    public Transform tilePrefab;
    public float tileOffset;
    public Vector2 mapSize;

    [Range(0f, 1f)]
    public float outlinePercentage;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {

    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        GenerateMap();
	}
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
	
	}
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////

    public void GenerateMap()
    {
        //make a name for the generated tile map
        string _tileMapName = "GeneratedTileMap";

        //check to see if this map already exists, and if so, destroy it
        if (transform.FindChild(_tileMapName))
            DestroyImmediate(transform.FindChild(_tileMapName).gameObject);


        //create the gameobject transform that will hold all tiles
        Transform _tileMap = new GameObject(_tileMapName).transform;
        //parent it to the owner of this script
        _tileMap.parent = transform;

        //loop through rows (mapSize.x)
        for (int _x = 0; _x < mapSize.x; ++_x)
        {
            //loop through columns (mapSize.y)
            for (int _y = 0; _y < mapSize.y; ++_y)
            {
                //be sure to account for the size of the tile by utilizing tileOffset
                Vector3 tilePosition = new Vector3(((mapSize.x / 2) + tileOffset + _x), 0, ((mapSize.y / 2) + tileOffset + _y));
                //when making the new tile make sure the tile is rotated properly by 90°
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;

                //outline the tile based on outlinePercentage
                newTile.localScale = (Vector3.one * (1 - outlinePercentage));

                //set parent
                newTile.parent = _tileMap;
            }
        }
    }
    #endregion
}
