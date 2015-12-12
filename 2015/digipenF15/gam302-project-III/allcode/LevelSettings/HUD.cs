/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - HUD.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour
{
    //HUD
    [Header("LOOT")]
    GameObject LootHUDGroup;
    Text LootText;

    [Header("HEALTH")]
    GameObject HealthHUDGroup;
    Text HealthText;
    Slider HealthSlider;

    [Header("VACUUM")]
    GameObject VacuumHUDGroup;
    Slider VacuumSlider;
    Text VacuumText;
    float VacuumRechargeThreshold;

    [Header("AMMO")]
    GameObject AmmoHUDGroup;
    Text AmmoText;
    Slider AmmoSlider;

    [Header("HUD FEEDBACK")]
    GameObject DamageIndicator;
    [SerializeField]
    float HUDScaleUpSize = 2f;
    Vector3 HUDOriginalScale;
    [SerializeField]
    Color DamageColor = new Color(1f, 0f, 0f, 0.3f);
    [SerializeField]
    Color HealColor = new Color(0f, 1f, 65 / 255f, 0.3f);
    [SerializeField]
    Color LootColor = new Color(1f, 1f, 0f, 0.3f);
    [SerializeField]
    Color AmmoColor = new Color(0f, 1f, 0f, 0.3f);
    [SerializeField]
    float FlashTime = 0.5f;
    GameObject MainCamera;
    bool IsAnimating;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //get the main camera (needed for camera shake)
        this.MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        //we need to connect all the parts of the HUD
        this.LootHUDGroup = GameObject.FindWithTag("HUDLoot");
        this.LootText = this.LootHUDGroup.transform.Find("LootText").GetComponent<Text>();

        this.HealthHUDGroup = GameObject.FindWithTag("HUDHealth");
        this.HealthText = this.HealthHUDGroup.transform.Find("HealthText").GetComponent<Text>();
        this.HealthSlider = this.HealthHUDGroup.transform.Find("HealthSlider").GetComponent<Slider>();

        this.VacuumHUDGroup = GameObject.FindWithTag("HUDVacuum");
        this.VacuumText = this.VacuumHUDGroup.transform.Find("VacuumText").GetComponent<Text>();
        this.VacuumSlider = this.VacuumHUDGroup.transform.Find("VacuumSlider").GetComponent<Slider>();
        this.VacuumRechargeThreshold = GameObject.FindWithTag("Player").GetComponent<Vacuum>().RechargeThreshold;

        this.AmmoHUDGroup = GameObject.FindWithTag("HUDAmmo");
        this.AmmoText = this.AmmoHUDGroup.transform.Find("AmmoText").GetComponent<Text>();
        this.AmmoSlider = this.AmmoHUDGroup.transform.Find("AmmoSlider").GetComponent<Slider>();

        this.DamageIndicator = this.gameObject.transform.Find("DamageIndicator").gameObject;

        //used to reset the size of the hud
        this.HUDOriginalScale = this.HealthHUDGroup.transform.localScale;

        this.InitializeHUD();
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: InitializeHUD()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void InitializeHUD()
    {
        GameObject player = GameObject.FindWithTag("Player");
        this.HealthText.text = player.GetComponent<Health>().CurrentHP.ToString();
        this.HealthSlider.value = player.GetComponent<Health>().CurrentHP;
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateHUDElement(Text, float, Slider)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void UpdateHUDElement(Text elementText_, float value_, Slider elementSlider_)
    {
        int value = (int)value_;
        if (elementText_ != null)
        {
            elementText_.text = value.ToString();
        }
        if (elementSlider_ != null)
        {
            elementSlider_.value = value;
        }
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateHUD(Text, float, Slider)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void UpdateHUDElementFromData(string hudElement_, int dataValue_)
    {
        switch(hudElement_)
        {
            case "loot":
                this.LootText.text = dataValue_.ToString();
                break;
            case "ammo":
                this.AmmoSlider.enabled = true;
                this.AmmoText.text = dataValue_.ToString();
                this.AmmoSlider.value = dataValue_;
                break;
        }
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: HUDEffects(string, float)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void HUDEffects(string effecttype_, float value_)
    {
        //DEBUG
        //Debug.Log("HUDEFFECTS ACTIVE!");
        switch (effecttype_)
        {
            case "damage":
                this.DamageIndicator.GetComponent<Image>().color = this.DamageColor;
                this.HUDElementScaler(this.HealthHUDGroup);
                this.CameraShake();
                this.UpdateHUDElement(this.HealthText, value_, this.HealthSlider);
                this.DamageIndicator.GetComponent<Image>().canvasRenderer.SetAlpha(1.0f);
                this.DamageIndicator.GetComponent<Image>().CrossFadeColor(new Color(1f, 1f, 1f, 0.01f), this.FlashTime, true, true);
                break;

            case "heal":
                this.DamageIndicator.GetComponent<Image>().color = this.HealColor;
                this.HUDElementScaler(this.HealthHUDGroup);
                this.UpdateHUDElement(this.HealthText, value_, this.HealthSlider);
                this.DamageIndicator.GetComponent<Image>().canvasRenderer.SetAlpha(1.0f);
                this.DamageIndicator.GetComponent<Image>().CrossFadeColor(new Color(1f, 1f, 1f, 0.01f), this.FlashTime, true, true);
                break;

            case "loot":
                this.DamageIndicator.GetComponent<Image>().color = this.LootColor;
                this.HUDElementScaler(this.LootHUDGroup);
                this.UpdateHUDElement(this.LootText, value_, null);
                this.DamageIndicator.GetComponent<Image>().canvasRenderer.SetAlpha(1.0f);
                this.DamageIndicator.GetComponent<Image>().CrossFadeColor(new Color(1f, 1f, 1f, 0.01f), this.FlashTime, true, true);
                break;

            case "ammo":
                this.DamageIndicator.GetComponent<Image>().color = this.AmmoColor;
                this.HUDElementScaler(this.AmmoHUDGroup);
                this.UpdateHUDElement(this.AmmoText, value_, this.AmmoSlider);
                this.DamageIndicator.GetComponent<Image>().canvasRenderer.SetAlpha(1.0f);
                this.DamageIndicator.GetComponent<Image>().CrossFadeColor(new Color(1f, 1f, 1f, 0.01f), this.FlashTime, true, true);
                break;

            case "vacuum":
                //DEBUG
                //Debug.Log("value_ before conversion = " + value_);

                //convert the value_ to it's reverse
                value_ = value_ / this.VacuumRechargeThreshold;
                value_ = value_ * this.VacuumSlider.maxValue;

                value_ = this.VacuumSlider.maxValue - value_;

                //make sure we never go negative
                if (value_ <= 0f)
                {
                    value_ = 0f;
                }

                //DEBUG
                //Debug.Log("value_ after conversion = " + value_);

                this.UpdateHUDElement(this.VacuumText, value_, this.VacuumSlider);
                break;

            case "vacuumRecharge":
                //DEBUG
                //Debug.Log("value_ before conversion = " + value_);

                //convert the value_
                value_ = value_ / this.VacuumRechargeThreshold;
                value_ = value_ * this.VacuumSlider.maxValue;

                //DEBUG
                //Debug.Log("value_ after conversion = " + value_);

                this.UpdateHUDElement(this.VacuumText, value_, this.VacuumSlider);
                break;

            default:
                Debug.LogError("ERROR! Choose either 'damage', 'heal', 'loot', or 'ammo'!");
                break;
        }
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: HUDElementScaler(GameObject, Vector3, float)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void HUDElementScaler(GameObject hudElement_)
    {
        //DEBUG
        //Debug.Log("SCALING HUD ELEMENT!");
        print(this.IsAnimating);
        if (!this.IsAnimating)
        {
            this.IsAnimating = true;
            iTween.ScaleFrom(hudElement_, iTween.Hash("name", "HUDElementScaleAnimation",
                                                      "scale", (new Vector3(1.5f, 1.5f, 1.5f) * this.HUDScaleUpSize),
                                                      "time", this.FlashTime,
                                                      "easetype", "easeInOutQuad",
                                                      "oncompletetarget", this.gameObject,
                                                      "oncompleteparams", hudElement_,
                                                      "oncomplete", "HUDElementReset"));
        }
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: CameraShake()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void CameraShake()
    {
        this.MainCamera.GetComponent<CameraShake>().ShakeCamera();
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: HUDElementReset(GameObject)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void HUDElementReset(GameObject hudElement_)
    {
        hudElement_.transform.localScale = this.HUDOriginalScale;
        this.IsAnimating = false;
    }
}
