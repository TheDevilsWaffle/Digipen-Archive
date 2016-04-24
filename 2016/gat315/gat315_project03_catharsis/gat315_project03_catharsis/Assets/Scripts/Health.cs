/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - Health.cs
//AUTHOR - Travis Moore
//COPYRIGHT - © 2016 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    //references
    HUDController HUD;
    [SerializeField]
    private SoundManager SFXSystem;
    [SerializeField]
    private SFXList SFXList;
    [SerializeField]
    EffectsController CameraEffects;

    //PROPERTIES
    [Header("Health Attributes")]
    [SerializeField]
    private float MaximumHP = 3;
    [SerializeField]
    public float CurrentHP;
    [Header("Damage Attributes")]
    [SerializeField]
    private bool IsDamagable = true;
    [SerializeField]
    private bool IsKillable = true;
    [SerializeField]
    private float DamageScalerValue;
    public enum DamageScalerType { LINEAR, MULTIPLIER }
    [SerializeField]
    public DamageScalerType ScalerType;
    public bool IsInvulnerable = false;

    [HideInInspector]
    public bool IsDead;
    [HideInInspector]
    public float TimeSinceDeath;

    [SerializeField]
    private float DeathTime = 3;

    float InvulnerabilityBlinkTime = 0.25f;

    private LevelController LevelSettings;

    void Awake()
    {
        //get references
        this.HUD = GameObject.Find("HUD").gameObject.GetComponent<HUDController>();
    }


    void Start()
    {
        this.SetHealthToValue(this.MaximumHP);
        this.LevelSettings = GameObject.Find("LevelSettings").gameObject.GetComponent<LevelController>();
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: IncreaseHealth(float)
    //NOTE: Calls ClampHealth() before setting CurrentHP
    /////////////////////////////////////////////////////////////////////////*/
    public void IncreaseHealth(float hp_)
    {
        this.CurrentHP += hp_;
        this.CurrentHP = Mathf.Clamp(this.CurrentHP, 0, this.MaximumHP);

        //evaluate health of object
        this.EvaluateHealth();

        //DEBUG
        //Debug.Log(this.gameObject + "HP has INCREASED to " + this.CurrentHP + " out of " + this.MaximumHP);
    }

    public float GetHealth()
    {
        return this.CurrentHP;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DecreaseHealth(float)
    //NOTE: Calls ClampHealth() before setting CurrentHP
    /////////////////////////////////////////////////////////////////////////*/
    public void DecreaseHealth(float damage_)
    {
        if (this.IsDamagable == false)
            return;

        //call ApplyDamageScaler() if applicable
        if(this.DamageScalerValue > 0f)
        {
            damage_ = this.ApplyDamageScaler(damage_);
        }
        int oldHP = (int)this.CurrentHP;
        this.CurrentHP -= damage_;
        //perform ClampHealth() to keep health within bounds
        this.CurrentHP = Mathf.Clamp(this.CurrentHP, 0, this.MaximumHP);

        //make the player invulnerable for some time
        StartCoroutine(this.Invulnerability());

        //decrease player health on the hud
        this.HUD.UpdateHealth(oldHP, (int)this.CurrentHP);

        //sfx
        this.SFXSystem.PlaySingle(this.SFXList.PlayerHurt);

        //evaluate health of object
        this.EvaluateHealth();

        //decrease color saturation
        this.CameraEffects.DecreaseColorSaturation(0.1f);


        //DEBUG
        //Debug.Log(this.gameObject + "HP has DECREASED to " + this.CurrentHP + " out of " + this.MaximumHP);
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: EvalulateHealth(float)
    //NOTE: Calls DestroyOwner() if applicable
    /////////////////////////////////////////////////////////////////////////*/
    public void EvaluateHealth()
    {
        //check to see if the player can even be damaged at all
        if (this.IsKillable && this.CurrentHP <= 0f)
        {
            this.IsDead = true;
            this.KillPlayer();
            return;
        }
        
        //DEBUG
        //Debug.Log(this.gameObject + "'S HEALTH EVALUATED TO: " + this.CurrentHP + " out of " + this.MaximumHP);
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SetHealthToValue(float)
    //NOTE: Calls ClampHealth() to ensure value is within range
    /////////////////////////////////////////////////////////////////////////*/
    public void SetHealthToValue(float hp_)
    {
        //set CurrentHP equal to new value after ClampHealth()
        this.CurrentHP = Mathf.Clamp(hp_, 0, this.MaximumHP);

        this.HUD.UpdateHealth((int)this.CurrentHP, (int)this.CurrentHP);

        //evaluate health to make sure we're not dead now
        this.EvaluateHealth();
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ApplyDamageScaler(float)
    /////////////////////////////////////////////////////////////////////////*/
    public float ApplyDamageScaler(float damage_)
    {
        //apply correct damage scaling based on ScalerType
        switch (this.ScalerType)
        {
            case DamageScalerType.LINEAR:
                damage_ = (damage_ + this.DamageScalerValue);
                break;
            case DamageScalerType.MULTIPLIER:
                damage_ = (damage_ * this.DamageScalerValue);
                break;
            default:
                Debug.LogError("APPLYDAMAGESCALER() ERROR - default case reached");
                break;
        }
        return damage_;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: StartInvulnerability()
    /////////////////////////////////////////////////////////////////////////*/
    public void StartInvulnerability()
    {
        StartCoroutine(this.Invulnerability());
    }


    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Invulnerability()
    /////////////////////////////////////////////////////////////////////////*/
    IEnumerator Invulnerability()
    {
        this.IsInvulnerable = true;

        //turn off
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        yield return new WaitForSeconds(this.InvulnerabilityBlinkTime);

        //turn on
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(this.InvulnerabilityBlinkTime);

        //turn off
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        yield return new WaitForSeconds(this.InvulnerabilityBlinkTime);

        //turn on
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(this.InvulnerabilityBlinkTime);

        //turn off
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        yield return new WaitForSeconds(this.InvulnerabilityBlinkTime);

        //turn on
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(this.InvulnerabilityBlinkTime);

        this.IsInvulnerable = false;
    }

    void KillPlayer()
    {
        Destroy(this.gameObject, 0.5f);

        //tell level settings to display the retry screen
        this.LevelSettings.RetryScreen();

    }
}
