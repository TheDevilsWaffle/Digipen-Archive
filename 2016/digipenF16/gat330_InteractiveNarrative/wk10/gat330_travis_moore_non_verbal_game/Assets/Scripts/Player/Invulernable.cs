///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Invulernable.cs
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

public class Invulernable : MonoBehaviour
{
    #region FIELDS
    public bool isPermanentlyInvulnerable;
    public Material invulnerableMaterial;
    public int numberOfInvulnerableFlashes;
    public float flashDuration;
    bool isInvulnerable;
    public bool IsInvulnerable
    {
        get { return isInvulnerable; }
    }
    Material playerMaterial;
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
        if (isPermanentlyInvulnerable)
            isInvulnerable = true;
        playerMaterial = GetComponent<Renderer>().material;
        StartCoroutine(FlashInvulnerable());
	}
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void TemporaryInvulnerability()
    {
        if(!isPermanentlyInvulnerable)
            StartCoroutine(FlashInvulnerable());
    }

    IEnumerator FlashInvulnerable()
    {
        Renderer _playerRenderer = this.gameObject.GetComponent<Renderer>();
        for (int i = 1; i <= numberOfInvulnerableFlashes; ++i)
        {
            if (i % 2 == 0)
                _playerRenderer.material = playerMaterial;
            else
                _playerRenderer.material = invulnerableMaterial;

            yield return new WaitForSeconds(flashDuration);
        }
        //return to normal
        _playerRenderer.material = playerMaterial;
        isInvulnerable = false;
    }
    #endregion
}
