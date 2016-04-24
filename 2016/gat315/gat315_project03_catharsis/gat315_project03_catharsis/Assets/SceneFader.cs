/*////////////////////////////////////////////////////////////////////////
//SCRIPT: SceneFader.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    #region PROPERTIES

    //references
    [SerializeField]
    private Image SceneFadeImage;

    [SerializeField]
    private float FadeInTime = 1f;
    [SerializeField]
    private float FadeOutTime = 1f;
    [SerializeField]
    private float DelayBeforeSceneLoadScaler = 1.5f;
    [SerializeField]
    private string NextScene;
    [SerializeField]
    private string PreviousScene;



    //attributes



    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //at the start of this scene, fade in the scene
        this.FadeInScene();
    }

    #endregion

    #region X_FUNCTIONS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeInScene()
    ////////////////////////////////////////////////////////////////////*/
    public void FadeInScene()
    {
        this.SceneFadeImage.CrossFadeAlpha(0f, this.FadeInTime, false);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutToNextScene()
    ////////////////////////////////////////////////////////////////////*/
    public IEnumerator FadeOutToNextScene()
    {
        //fade out the scene
        this.SceneFadeImage.CrossFadeAlpha(1f, this.FadeOutTime, false);

        //wait before loading the next scene
        yield return new WaitForSeconds(this.FadeOutTime * this.DelayBeforeSceneLoadScaler);

        //load the next scene
        SceneManager.LoadScene(this.NextScene);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutToPreviousScene()
    ////////////////////////////////////////////////////////////////////*/
    public IEnumerator FadeOutToPreviousScene()
    {
        //fade out the scene
        this.SceneFadeImage.CrossFadeAlpha(1f, this.FadeOutTime, false);

        //wait before loading the next scene
        yield return new WaitForSeconds(this.FadeOutTime * this.DelayBeforeSceneLoadScaler);

        //load the next scene
        SceneManager.LoadScene(this.PreviousScene);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_04()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_04()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_05()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_05()
    {
        //CONTENT HERE
    }

    #endregion

    #region ANIMATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_06()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_06()
    {
        //CONTENT HERE
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FUNC_07()
    ////////////////////////////////////////////////////////////////////*/
    void FUNC_07()
    {
        //CONTENT HERE
    }

    #endregion
}