///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — BarInfoContainerController.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class BarInfoContainerController : MonoBehaviour
{
    #region FIELDS
    Transform tr;
    [SerializeField]
    public int sizeOfBarInfoContainer;
    [SerializeField]
    GameObject gameInfoGroupPrefab;
    List<GameObject> gameInfoGroups;
    [SerializeField]
    bool gameInfoGroupsStartDisabled = true;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        tr = transform;
        gameInfoGroups = new List<GameObject> { };
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        InitializeContainer(sizeOfBarInfoContainer);
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

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public IEnumerator SetUIBarGroupInfo(UIBarInfo _uiBarInfo, int _index)
    {
        foreach (GameObject _gameInfoGroup in gameInfoGroups)
        {
            _gameInfoGroup.GetComponent<BarInfoGroup>().DisableIconAndInstructions();
        }
        yield return new WaitForEndOfFrame();

        if (gameInfoGroups[_index] != null)
        {
            gameInfoGroups[_index].GetComponent<BarInfoGroup>().SetBarInfoGroup(_uiBarInfo.gp_sprite,
                                                                                _uiBarInfo.kbm_sprite,
                                                                                _uiBarInfo.instruction);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void InitializeContainer(int _size)
    {
        for (int i = 0; i < _size; ++i)
        {
            GameObject _gameInfoGroup = Instantiate(gameInfoGroupPrefab, tr, false) as GameObject;
            gameInfoGroups.Add(_gameInfoGroup);
            if(gameInfoGroupsStartDisabled)
            {
                _gameInfoGroup.GetComponent<BarInfoGroup>().DisableIconAndInstructions();
            }
        }
        
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region OnDestroy
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
    }
    #endregion
}
