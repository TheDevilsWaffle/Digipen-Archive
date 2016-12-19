using UnityEngine;
using System.Collections;



public class AudioSystem : MonoBehaviour
{

    private static AudioSystem instance = null;
    public static AudioSystem Instance
    {
        get { return instance; }
    }

    // Use this for initialization
    void Awake ()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
