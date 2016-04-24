/*////////////////////////////////////////////////////////////////////////
//SCRIPT: UIAnimations.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIAnimations : MonoBehaviour
{

    #region FADE TO

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeImage()
    ////////////////////////////////////////////////////////////////////*/
    public IEnumerator FadeImage(Image image_, Color color_, float alpha_, float time_, float delay_ = 0f, bool ignoreTimeScale_ = true)
    {
        //delay
        yield return new WaitForSeconds(delay_);
        
        //important - have to set alpha before crossfade works
        if (alpha_ > 0.0f)
        {
            image_.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
        else
        {
            image_.gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        }
        //set the new color
        image_.color = color_;

        //fade
        image_.CrossFadeAlpha(alpha_, time_, ignoreTimeScale_);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeText()
    ////////////////////////////////////////////////////////////////////*/
    public IEnumerator FadeText(Text text_, Color color_, float alpha_, float time_, float delay_ = 0f, bool ignoreTimeScale_ = true)
    {
        //delay
        yield return new WaitForSeconds(delay_);

        //important - have to set alpha before crossfade works
        if (alpha_ == 1f)
        {
            text_.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
        else
        {
            text_.gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        }
        //set the new color
        text_.color = color_;

        //fade
        text_.CrossFadeAlpha(alpha_, time_, ignoreTimeScale_);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeTo — OnComplete Version (w/ PARAMS)
    ////////////////////////////////////////////////////////////////////*/
    public void FadeTo(GameObject obj_, float alpha_, float time_, string onComplete_, string OnCompleteTarget_, string onCompleteParams_, float delay_ = 0f, string easeType_ = "easeInOutQuad", string loopType_ = "none")
    {
        iTween.FadeTo(obj_,
            iTween.Hash("name", "UIAnimations_FadeIn_OnComplete",
                "alpha", alpha_,
                "time", time_,
                "delay", delay_,
                "easetype", easeType_,
                "looptype", loopType_,
                "oncomplete", onComplete_,
                "oncompletetarget", OnCompleteTarget_,
                "oncompleteparams", onCompleteParams_));
    }

    #endregion FADE TO

    #region SCALE TO

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Scale — Regular
    ////////////////////////////////////////////////////////////////////*/
    public void ScaleTo(GameObject obj_, Vector3 scale_, float time_, string easeType_ = "easeInOutQuad", string loopType_ = "none", bool ignoreTimeScale_ = true)
    {
        iTween.ScaleTo(obj_,
            iTween.Hash("name", "UIAnimations_Scale",
                "scale", scale_,
                "time", time_,
                "easetype", easeType_,
                "ignoretimescale", ignoreTimeScale_,
                "looptype", loopType_));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: ScaleTo — OnStart Version (NO PARAMS)
    ////////////////////////////////////////////////////////////////////*/
    public void ScaleTo(GameObject obj_, Vector3 scale_, float time_, float delay_, string onStart_, string onStartTarget_, string easeType_ = "easeInOutQuad", string loopType_ = "none", bool ignoreTimeScale_ = true)
    {
        iTween.ScaleTo(obj_,
            iTween.Hash("name", "UIAnimations_ScaleTo_OnStart",
                "scale", scale_,
                "time", time_,
                "delay", delay_,
                "easetype", easeType_,
                "looptype", loopType_,
                "ignoretimescale", ignoreTimeScale_,
                "onstart", onStart_,
                "onstarttarget", onStartTarget_));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: ScaleTo — OnComplete Version (w/ PARAMS)
    ////////////////////////////////////////////////////////////////////*/
    public void ScaleTo(GameObject obj_, Vector3 scale_, float time_, float delay_, string onComplete_, string OnCompleteTarget_, string onCompleteParams_, string easeType_ = "easeInOutQuad", string loopType_ = "none", bool ignoreTimeScale_ = true)
    {
        iTween.ScaleTo(obj_,
            iTween.Hash("name", "UIAnimations_ScaleTo_OnComplete",
                "scale", scale_,
                "time", time_,
                "delay", delay_,
                "easetype", easeType_,
                "looptype", loopType_,
                "ignoretimescale", ignoreTimeScale_,
                "oncomplete", onComplete_,
                "oncompletetarget", OnCompleteTarget_,
                "oncompleteparams", onCompleteParams_));
    }

    #endregion SCALE

    #region MOVE

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: MoveFrom()
    ////////////////////////////////////////////////////////////////////*/
    public void MoveFrom(GameObject obj_, Vector3 pos_, float time_, float delay_ = 0f, string easeType_ = "easeInOutQuad", string loopType_ = "none", bool ignoreTimeScale_ = true)
    {
        iTween.MoveFrom(obj_,
            iTween.Hash("name", "UIAnimations_MoveFrom",
                "position", pos_,
                "time", time_,
                "delay", delay_,
                "easetype", easeType_,
                "islocal", true,
                "ignoretimescale", ignoreTimeScale_,
                "looptype", loopType_));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: MoveTo — Regular
    ////////////////////////////////////////////////////////////////////*/
    public void MoveTo(GameObject obj_, Vector3 pos_, float time_, float delay_ = 0f, string easeType_ = "easeInOutQuad", string loopType_ = "none", bool ignoreTimeScale_ = true)
    {
        iTween.MoveTo(obj_,
            iTween.Hash("name", "UIAnimations_MoveTo",
                "position", pos_,
                "time", time_,
                "delay", delay_,
                "easetype", easeType_,
                "islocal", true,
                "ignoretimescale", ignoreTimeScale_,
                "looptype", loopType_));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: MoveTo — OnStart Version (NO PARAMS)
    ////////////////////////////////////////////////////////////////////*/
    public void MoveTo(GameObject obj_, Vector3 pos_, float time_, string onStart_, string onStartTarget_, float delay_ = 0f, string easeType_ = "easeInOutQuad", string loopType_ = "none", bool ignoreTimeScale_ = true)
    {
        iTween.MoveTo(obj_,
            iTween.Hash("name", "UIAnimations_MoveTo_OnStart",
                "position", pos_,
                "time", time_,
                "delay", delay_,
                "easetype", easeType_,
                "looptype", loopType_,
                "ignoretimescale", ignoreTimeScale_,
                "onstart", onStart_,
                "onstarttarget", onStartTarget_));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: MoveTo — OnComplete Version (w/ PARAMS)
    ////////////////////////////////////////////////////////////////////*/
    public void MoveTo(GameObject obj_, Vector3 pos_, float time_, string onComplete_, string OnCompleteTarget_, string onCompleteParams_, float delay_ = 0f, string easeType_ = "easeInOutQuad", string loopType_ = "none", bool ignoreTimeScale_ = true)
    {
        iTween.MoveTo(obj_,
            iTween.Hash("name", "UIAnimations_MoveTo_OnComplete",
                "position", pos_,
                "time", time_,
                "delay", delay_,
                "easetype", easeType_,
                "looptype", loopType_,
                "ignoretimescale", ignoreTimeScale_,
                "oncomplete", onComplete_,
                "oncompletetarget", OnCompleteTarget_,
                "oncompleteparams", onCompleteParams_));
    }

    #endregion MOVE TO

}