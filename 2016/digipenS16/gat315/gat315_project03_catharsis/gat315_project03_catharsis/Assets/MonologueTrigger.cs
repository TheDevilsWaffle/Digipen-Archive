/*////////////////////////////////////////////////////////////////////////
//SCRIPT: MonologueTrigger.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;

public class MonologueTrigger : MonoBehaviour
{
    #region PROPERTIES

    //references
    [SerializeField]
    private GameObject Player;
    private DialogueContainer s_DialogueContainer;
    public bool IsCageTrigger;
    public SpriteRenderer X_and_Cage;
    public Sprite CageSprite;
    public bool IsEndOfGameTrigger;
    public BoxCollider2D[] WallArray;
    public AudioClip CageSFX;


    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //get self references
        this.s_DialogueContainer = GetComponent<DialogueContainer>();  
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get player reference if not set
        if(this.Player == null)
            this.Player = GameObject.FindWithTag("Player").gameObject;
    }

    #endregion

    #region ONTRIGGERENTER2D

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: OnTriggerEnter2D()
    ////////////////////////////////////////////////////////////////////*/
    void OnTriggerEnter2D(Collider2D collider2D_)
    {
        //if the player enters this trigger zone
        if (collider2D_.gameObject == this.Player)
        {
            this.s_DialogueContainer.SendDialogue();
            if (this.IsCageTrigger)
            {
                this.X_and_Cage.sprite = CageSprite;
                GameObject.FindWithTag("SFX").gameObject.GetComponent<SoundManager>().PlayPaitiently(this.CageSFX);
                this.X_and_Cage.sortingOrder = 10;

                foreach(BoxCollider2D wall in this.WallArray)
                {
                    wall.isTrigger = false;
                }

                StartCoroutine(this.DelayBeforeNextLevel());
            }

            //end of game trigger
            if(this.IsEndOfGameTrigger)
            {
                StartCoroutine(this.DelayBeforeEndGame());
            }

        }
    }

    IEnumerator DelayBeforeNextLevel()
    {
        yield return new WaitForSeconds(4.25f);
        StartCoroutine(GameObject.FindWithTag("LevelSettings").GetComponent<SceneManagementSystem>().FadeOutToNextScene());
    }

    IEnumerator DelayBeforeEndGame()
    {
        yield return new WaitForSeconds(25f);
        StartCoroutine(GameObject.FindWithTag("LevelSettings").GetComponent<SceneManagementSystem>().FadeOutToNextScene());
    }

    #endregion

}