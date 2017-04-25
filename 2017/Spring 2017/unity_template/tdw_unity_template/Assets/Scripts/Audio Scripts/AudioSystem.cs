///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — AudioSystem.cs
//COPYRIGHT — © 2017 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#region ENUMS
//public enum EnumStatus
//{
//	
//};
#endregion

#region EVENTS
//public class EVENT_EXAMPLE : GameEvent
//{
//    public EVENT_EXAMPLE() { }
//}
#endregion

public class AudioSystem : Singleton<AudioSystem>
{
    #region FIELDS
    public Dictionary<string, AudioClip> audioLibrary;
    public List<AudioSource> liveSources;
    public GameObject audioSourcePrefab;
    public List<AudioSource> musicSources;
    public List<bool> musicSourcesAlive;
    public float maxMusicVolume = 0.4f;
    [Header("currently active audio sources")]
    public int audioClipsLoaded;
    Transform tr;
    [SerializeField]
    AudioSource Ambient;
    [SerializeField]
    AudioSource Boost;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Awake()
    {
        //refs
        tr = GetComponent<Transform>();
        //initial values
        if(audioLibrary == null)
        {
            audioLibrary = new Dictionary<string, AudioClip>();
        }
        if(audioSourcePrefab == null)
        {
            audioSourcePrefab = Resources.Load<GameObject>("Audio Source");
        }

        liveSources = new List<AudioSource>();
        musicSources = new List<AudioSource>();
        musicSourcesAlive = new List<bool>();

        //SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        ToggleBoost(false);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        //Events.instance.AddListener<>();
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
        //remove unnecessary audio
        for (int i = 0; i < liveSources.Count; ++i)
        {
            if(liveSources[i] == null)
            {
                liveSources.RemoveAt(i);
            }
            if (liveSources[i] != null && liveSources[i].isPlaying == false)
            {
                Destroy(liveSources[i].gameObject, 3.5f);
                liveSources.RemoveAt(i);
            }
        }
        //remove unnecessary music
        for (int i = 0; i < musicSources.Count; ++i)
        {
            if(musicSources[i] == null)
            {
                musicSources.RemoveAt(i);
            }
            else
            {
                if (musicSources[i].isPlaying == false)
                {
                    Destroy(musicSources[i].gameObject, 3.5f);
                    musicSourcesAlive.RemoveAt(i);
                    musicSources.RemoveAt(i);
                }
            }
        }
        //fading
        for (int i = 0; i < musicSources.Count; ++i)
        {
            CrossFadeAudioSource(musicSources[i], musicSourcesAlive[i]);
        }



    #if false
        UpdateTesting();
    #endif
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetMusicState(int _index, bool _state)
    {
        if(musicSourcesAlive.Count > _index)
        {
            musicSourcesAlive[_index] = _state;
        }
        else
        {
            Debug.LogWarning("[AudioSystem]\n\tTried to alter a music source that does not exist!\n");
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AddMusicSource(AudioSource _audioSource, bool _isPlaying = false)
    {
        musicSources.Add(_audioSource);
        musicSourcesAlive.Add(_isPlaying);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void CrossFadeAudioSource(AudioSource _audioSource, bool _fadeIn = false)
    {
        if (_fadeIn)
        {
            if (_audioSource.volume < maxMusicVolume - 0.1f)
            {
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, maxMusicVolume, 0.75f * Time.deltaTime);
            }
            else
            {
                _audioSource.volume = maxMusicVolume;
            }
        }
        else
        {
            if (_audioSource.volume > 0.05f)
            {
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, 0f, 1f * Time.deltaTime);
            }
            else
            {
                _audioSource.volume = 0;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public AudioSource MakeAudioSource(string _name = "Error", Vector3 _position = default(Vector3), Transform _parent = null)
    {
        GameObject _audioSource_go = Instantiate(audioSourcePrefab, _position, Quaternion.identity);
        AudioSource _audioSource = null;
        _audioSource_go.name = "AudioSource (" + _name + ")";
        if(_audioSource_go != null)
        {
            _audioSource = _audioSource_go.GetComponent<AudioSource>();
            liveSources.Add(_audioSource);
        }
        _audioSource_go.transform.SetParent(_parent);
        _audioSource_go.transform.position = _position;
        _audioSource.clip = FindOrLoadAudioClip(_name);
        _audioSource.PlayOneShot(_audioSource.clip);
        return _audioSource;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public AudioClip FindOrLoadAudioClip(string _name)
    {
        //if it exists, return it
        if(audioLibrary.ContainsKey(_name))
        {
            return audioLibrary[_name];
        }
        else
        {
            //load it from:
            //music and sfx are located in Resources/Audio
            AudioClip _audioClip = Resources.Load<AudioClip>("Audio/" + _name);
            if(_audioClip != null)
            {
                audioClipsLoaded++;
                audioLibrary.Add(_name, _audioClip);

                return audioLibrary[_name];
            }
        }
#if UNITY_EDITOR
        Debug.LogError("[AudioSystem]\n\tCound not find: " + _name + " in audioLibrary!");
#endif
        return null;
    }
    public void UpdateAmbientPitch(float _value)
    {
        Ambient.pitch = _value;
    }
    public void ToggleBoost(bool _on)
    {
        if(_on)
        {
            Boost.Play();
            Boost.loop = true;
        }
        else
        {
            Boost.Stop();
        }
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        //Events.instance.RemoveListener<>();
    }
    #endregion

    #region TESTING
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// UpdateTesting
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTesting()
    {
        //Keypad 0
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {

        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            
        }
        //Keypad 2
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            
        }
        //Keypad 3
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            
        }
        //Keypad 4
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            
        }
        //Keypad 5
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            
        }
        //Keypad 6
        if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            
        }
    }
    #endregion
}