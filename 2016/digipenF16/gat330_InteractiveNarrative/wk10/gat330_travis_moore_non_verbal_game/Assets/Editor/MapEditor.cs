///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — MapEditor.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEditor;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor
{
    #region FIELDS

    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapGenerator _map = target as MapGenerator;

        _map.GenerateMap();
    }
    #endregion
}
