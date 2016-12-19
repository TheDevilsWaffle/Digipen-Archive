///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — ExplosiveDeath.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS
public class EVENT_FollowerDied : GameEvent
{
    public EVENT_FollowerDied()
    {
    }
}
#endregion

public class ExplosiveDeath : MonoBehaviour
{
    #region FIELDS
    public GameObject fragmentPrefab;
    [SerializeField]
    public Material fragmentMaterial;
    public bool varySizeOfFragments;
    public float minSize;
    public float maxSize;
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
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_pos"></param>
    public void Explode(Vector3 _pos)
    {
        Events.instance.Raise(new EVENT_FollowerDied());
        if (fragmentPrefab != null && fragmentMaterial != null)
        {
            GameObject _fragments = Instantiate(fragmentPrefab, _pos, Quaternion.identity) as GameObject;
            foreach(Transform _fragment in _fragments.transform)
            {
                _fragment.gameObject.GetComponent<Renderer>().material = fragmentMaterial;
                if(varySizeOfFragments)
                {
                    _fragment.localScale = GetRandomSize();
                }
            }
        }
        else
            Debug.LogError("Warning! fragmentPrefab/fragmentMaterial is not set, please set it!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Vector3 GetRandomSize()
    {
        float _randomSize = Random.Range(minSize, maxSize);
        Vector3 _newSize = new Vector3(_randomSize, _randomSize, _randomSize);
        return _newSize;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
}
