/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - HUDController.cs
//AUTHOR - Travis Moore
//COPYRIGHT - © 2016 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour
{
    #region PROPERTIES

    //GameObject DamageIndicator;

    [Header("Flashlight")]
    GameObject Flashlight_Group;
    [SerializeField]
    Image CurrentFlashlightImage;
    Slider Flashlight_Slider;
    Text Flashlight_Slider_Text;

    public Sprite FlashlightOnImage;
    public Sprite FlashlightOffImage;

    [Header("Health")]
    GameObject Health_Group;
    [SerializeField]
    Image CurrentHealthImage;
    Slider Health_Slider;
    Text Health_Slider_Text;

    public Sprite HealthInactiveSprite;
    public Sprite HealthActiveSprite;

    [Header("Animation")]
    [SerializeField]
    float AnimateTime = 0.25f;
    [SerializeField]
    float DelayTime = 0.25f;
    [SerializeField]
    float FadeTime = 0.35f;

    float HUDAlpha = 0.5f;
    [SerializeField]
    float FlashTime = 0.5f;
    [SerializeField]
    Vector3 ExagerateSize = new Vector3(1.25f, 1.25f, 1f);
    [SerializeField]
    Vector3 ExagerateRotation = new Vector3(1f, 1f, 5f);
    [SerializeField]
    Vector3 OriginalSize = new Vector3(1f, 1f, 1f);

    [SerializeField]
    Color HPIncreaseColor = new Color(0f / 255f, 255f / 255f, 0f / 255f, 1f);
    [SerializeField]
    Color HPDecreaseColor = new Color(255f / 255f, 0f / 255f, 0f / 255f, 1f);


    #endregion Properties

    #region INITIALIZE

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //damage indicator (fade it out in case it is not already)
        //this.DamageIndicator = this.gameObject.transform.Find("DamageIndicator").gameObject;
        //this.DamageIndicator.GetComponent<Image>().CrossFadeAlpha(0f, 0.001f, true);

        //cooldown
        this.Flashlight_Group = this.gameObject.transform.Find("FlashlightCG").gameObject;
        this.CurrentFlashlightImage = this.Flashlight_Group.transform.Find("Icon").GetComponent<Image>();
        this.Flashlight_Slider = this.Flashlight_Group.transform.Find("Slider").GetComponent<Slider>();
        this.Flashlight_Slider_Text = this.Flashlight_Group.transform.Find("Value").GetComponent<Text>();
        
        //health
        this.Health_Group = this.gameObject.transform.Find("HealthCG").gameObject;
        this.Health_Slider = this.Health_Group.transform.Find("Slider").GetComponent<Slider>();
        this.Health_Slider_Text = this.Health_Group.transform.Find("Value").GetComponent<Text>();

        this.FadeOutAllHUDObjects();
    }

    #endregion Initialization

    #region UPDATE FLASHLIGHT / HEALTH

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateFlashlight()
    /////////////////////////////////////////////////////////////////////////*/
    public void UpdateFlashlight(int currentValue_)
    {
        //update the text component
        this.Flashlight_Slider_Text.text = currentValue_.ToString();

        //update the slider component
        this.Flashlight_Slider.value = currentValue_;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateHealth(float, float) 
    /////////////////////////////////////////////////////////////////////////*/
    public void UpdateHealth(int oldValue_, int newValue_)
    {
        //update the text component
        this.Health_Slider_Text.text = newValue_.ToString();

        //update the slider component
        this.Health_Slider.value = newValue_;

        //different flash depending on if health increased or decreased
        //increased or the same
        if (newValue_ >= oldValue_)
        {
            this.AnimateFlashHUD(this.HPIncreaseColor);
        }
        //decreased
        else
        {
            this.AnimateFlashHUD(this.HPDecreaseColor);
        }

        //animate the group
        this.AnimateHUD(this.Health_Group);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SwapSprite()
    /////////////////////////////////////////////////////////////////////////*/
    public void SwapSprite(string spriteCategory_)
    {
        switch(spriteCategory_)
        {
            case ("flashlight"):
                {
                    if(this.CurrentFlashlightImage.sprite != this.FlashlightOffImage)
                    {
                        this.CurrentFlashlightImage.sprite = this.FlashlightOffImage;
                        //animate this HUDgroup, then turn off
                        this.AnimateHUD(this.Flashlight_Group);
                    }
                    else
                    {
                        this.CurrentFlashlightImage.sprite = this.FlashlightOnImage;
                        //animate this HUDgroup, keep on
                        this.AnimateHUDKeepOn(this.Flashlight_Group);
                    }
                }
                break;

            case ("health"):
                {
                    if (this.CurrentHealthImage.sprite != this.HealthActiveSprite)
                    {
                        print("should be changing to active sprite");
                        this.CurrentHealthImage.sprite = this.HealthActiveSprite;
                        //animate this HUDgroup, then turn off
                        this.AnimateHUD(this.Health_Group);
                    }
                    else
                    {
                        print("should be changing to inactive sprite");
                        this.CurrentHealthImage.sprite = this.HealthInactiveSprite;
                        //animate this HUDgroup, keep on
                        this.AnimateHUDKeepOn(this.Health_Group);
                    }
                }
                break;

            default:
                Debug.LogError("INCORRECT CATEGORY! Please choose from 'flashlight' or 'health'.");
                break;
        }
    }

    #endregion

    #region ANIMATIONS

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateOverheated(GameObject, float)
    /////////////////////////////////////////////////////////////////////////*/
    IEnumerator AnimateOverheated(GameObject warning_, float time_)
    {
        warning_.GetComponent<Text>().CrossFadeAlpha(1f, time_, false);
        yield return new WaitForSeconds(time_);
        warning_.GetComponent<Text>().CrossFadeAlpha(0f, time_, false);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateHUD(GameObject)
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateHUD(GameObject hudGroup_)
    {
        this.FadeInHUD(hudGroup_.GetComponent<CanvasGroup>());

        //scale to larger size
        iTween.ScaleTo(hudGroup_,
                       iTween.Hash("name", "Animation_ScaleToLargerSize",
                                   "scale", this.ExagerateSize,
                                   "time", this.AnimateTime,
                                   "easetype", "easeOutBack"));
        /*
        //shake the hudgroup
        iTween.ShakeRotation(hudGroup_,
                                iTween.Hash("name", "Animation_RotationShake",
                                            "amount", this.ExagerateRotation,
                                            "time", (this.AnimateTime + this.DelayTime),
                                            "easetype", "easeInOutQuad",
                                            "oncompletetarget", this.gameObject,
                                            "oncompleteparams", hudGroup_.GetComponent<CanvasGroup>(),
                                            "oncomplete", "FadeOutHUD"));
        */

        //scale to back to regular size
        iTween.ScaleTo(hudGroup_,
                       iTween.Hash("name", "Animation_ScaleBackToOriginalSize",
                                   "scale", this.OriginalSize,
                                   "delay", this.DelayTime,
                                   "time", this.DelayTime,
                                   "easetype", "easeInBack",
                                   "oncompletetarget", this.gameObject,
                                   "oncompleteparams", hudGroup_.GetComponent<CanvasGroup>(),
                                   "oncomplete", "FadeOutHUD"));

    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateHUDKeepOn(GameObject)
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateHUDKeepOn(GameObject hudGroup_)
    {
        this.FadeInHUD(hudGroup_.GetComponent<CanvasGroup>());

        //scale to larger size
        iTween.ScaleTo(hudGroup_,
                       iTween.Hash("name", "Animation_ScaleToLargerSize",
                                   "scale", this.ExagerateSize,
                                   "time", this.AnimateTime,
                                   "easetype", "easeOutBack"));
        /*
        //shake the hudgroup
        iTween.ShakeRotation(hudGroup_,
                                iTween.Hash("name", "Animation_RotationShake",
                                            "amount", this.ExagerateRotation,
                                            "time", (this.AnimateTime + this.DelayTime),
                                            "easetype", "easeInOutQuad"));
        */
        //scale to back to regular size
        iTween.ScaleTo(hudGroup_,
                       iTween.Hash("name", "Animation_ScaleBackToOriginalSize",
                                   "scale", this.OriginalSize,
                                   "delay", this.DelayTime,
                                   "time", this.DelayTime,
                                   "easetype", "easeInBack"));

    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateFlashHUD(Color)
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateFlashHUD(Color color_)
    {
        //quickly change the color
        //this.DamageIndicator.GetComponent<Image>().color = color_;
        //this.DamageIndicator.GetComponent<Image>().CrossFadeAlpha(1f, 0.001f, false);

        //crossfade the color to completely transparent
        //this.DamageIndicator.GetComponent<Image>().CrossFadeAlpha(0f, this.FlashTime, false);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeInHUD(CanvasGroup)
    /////////////////////////////////////////////////////////////////////////*/
    void FadeInHUD(CanvasGroup hudGroup_)
    {
        while (hudGroup_.alpha < 1f)
        {
            hudGroup_.alpha += this.FadeTime * Time.deltaTime;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutHUD(CanvasGroup)
    /////////////////////////////////////////////////////////////////////////*/
    void FadeOutHUD(CanvasGroup hudGroup_)
    {
        while (hudGroup_.alpha > this.HUDAlpha)
        {
            hudGroup_.alpha -= this.FadeTime * Time.deltaTime;
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOutAllHUDObjects()
    /////////////////////////////////////////////////////////////////////////*/
    void FadeOutAllHUDObjects()
    {
        FadeOutHUD(this.Flashlight_Group.GetComponent<CanvasGroup>());
        FadeOutHUD(this.Health_Group.GetComponent<CanvasGroup>());
    }

    #endregion
    
}
