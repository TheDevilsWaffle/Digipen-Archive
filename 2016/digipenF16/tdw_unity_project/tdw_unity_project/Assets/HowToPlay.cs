///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — HowToPlay.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS

#endregion

#region EVENTS
public class EVENT_HowToPlay_Complete : GameEvent
{
    public EVENT_HowToPlay_Complete() { }
}
#endregion

public class HowToPlay : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    RectTransform howToPlay_rt;

    [SerializeField]
    Text subtitle;
    [SerializeField]
    string[] subtitles;
    [SerializeField]
    GameObject[] examples;
    int numberOfExamples;
    int index = 0;
    int previousIndex = 0;
    float timer = 0f;
    float timerThreshold = 0.5f;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        timer = 0f;
        index = 0;
        previousIndex = 0;
        numberOfExamples = examples.Length;
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        if(!RoundData.hasHowToPlayBeenShown)
            howToPlay_rt.gameObject.SetActive(true);
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
        if (!RoundData.hasHowToPlayBeenShown)
        {
            timer += Time.deltaTime;
            if (GamePadInput.players[0].A == GamePadButtonState.PRESSED && timer > timerThreshold)
            {
                timer = 0f;
                previousIndex = index;
                ++index;
                DetermineNextAction();
            }
        }
        else
        {
            this.gameObject.transform.parent.gameObject.GetComponent<Countdown>().enabled = true;
            Events.instance.Raise(new EVENT_HowToPlay_Complete());
            Destroy(howToPlay_rt.gameObject);
        }
	}
    #endregion

    #region METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void DetermineNextAction()
    {
        if (index < numberOfExamples)
        {
            examples[previousIndex].SetActive(false);
            examples[index].SetActive(true);
            subtitle.text = subtitles[index];
        }
        else
        {
            RoundData.hasHowToPlayBeenShown = true;
            this.gameObject.transform.parent.gameObject.GetComponent<Countdown>().enabled = true;
            Events.instance.Raise(new EVENT_HowToPlay_Complete());
            howToPlay_rt.gameObject.SetActive(false);
            Destroy(howToPlay_rt.gameObject);
        }
    }

    #endregion
}
