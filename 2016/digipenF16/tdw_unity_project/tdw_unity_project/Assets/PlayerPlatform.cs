///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — PlayerPlatform.cs
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

public class PlayerPlatform : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    Material[] materials;
    [SerializeField]
    float duration;
    [SerializeField]
    int TotalFlashes;
    [SerializeField]
    int playerNumber = 9;
    Renderer r;
    [SerializeField]
    Color playerColor;
    int flashCount = 0;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        playerNumber = 9;
        r = GetComponent<Renderer>();
        flashCount = 0;
        Events.instance.AddListener<EVENT_Player_Spawned>(FlashPlayerColor);
    }
	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
	}
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void FlashPlayerColor(EVENT_Player_Spawned _event)
    {
        //print("PLATFORM REPORTS THAT PLAYER " + _event.playerNumber + " HAS SPAWNED!");
        if (_event.playerNumber == playerNumber)
        {
            StartCoroutine(StartMaterialFlash());
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator StartMaterialFlash()
    {
        for (int i = 0; i < TotalFlashes; ++i)
        {
            if (i % 2 != 0)
            {
                materials[1].color = playerColor;
                r.material = materials[1];
            }
            else
            {
                r.material = materials[0];
            }
            yield return new WaitForSeconds(duration);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_color"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetPlayerColor(Color _color)
    {
        playerColor = _color;
        materials[1].color = playerColor;
        //print(this.gameObject.name + "'s materials[1].color = " + playerColor);
    }
    public void SetPlayerNumber(int _playerNumber)
    {
        playerNumber = _playerNumber;
        //print("player number is set to " + _playerNumber);
    }
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.KeypadEnter))
    //    {
    //        FlashPlayerColor();
    //    }
    //}

    #endregion

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        Events.instance.RemoveListener<EVENT_Player_Spawned>(FlashPlayerColor);
    }
}
