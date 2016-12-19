///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — LivesSystem.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS

#endregion

public class LivesSystem : MonoBehaviour
{
    #region FIELDS
    public GameObject skullContainer;
    public GameObject skull;
    public GameObject lifeMeter;
    public GameObject[] jewels;
    public Text livesText;
    public string eliminated = "ELIMINATED!";
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
	}
    #endregion

    #region METHODS
    public void InitializeHUDProperties(Color _color)
    {
        foreach (GameObject jewel in jewels)
            jewel.GetComponent<Image>().color = _color;
    }
    public void EliminatePlayer()
    {
        LeanTween.cancel(skull);
        skull.SetActive(false);
        livesText.text = eliminated;
    }
    public void DestroyLife(int _livesRemaining)
    {
        if (_livesRemaining > 0)
        {
            jewels[_livesRemaining].SetActive(false);
        }
        else if(_livesRemaining == 0)
        {
            jewels[_livesRemaining].SetActive(false);
            LeanTween.alpha(skull.GetComponent<RectTransform>(), 0.1f, 0.25f).setEase(LeanTweenType.easeInOutBack).setLoopPingPong();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
