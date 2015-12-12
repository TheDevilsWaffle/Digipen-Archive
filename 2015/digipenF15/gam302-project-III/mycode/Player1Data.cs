/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - Player1Data.cs
//AUTHOR - Travis Moore
//COPYRIGHT - ©2015 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player1Data : MonoBehaviour
{
    [HideInInspector]
    public static Player1Data Player1DataObject;

    //PROPERTIES
    public int Loot;
    public int Ammo;
    [SerializeField]
    GameObject HUD;
    [SerializeField]
    Text LootText;
    [SerializeField]
    Text AmmoText;

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //if Player1DataObject does not exist yet, make this first instance it
        if(Player1DataObject == null)
        {
            //DontDestroyOnLoad(this.gameObject);
            Player1DataObject = this;
        }
        //otherwise Player1DataObject already exists, destroy this new one
        else if(Player1DataObject != null)
        {
            Destroy(this.gameObject);
        }

        this.LootText.text = Loot.ToString();
        this.AmmoText.text = Ammo.ToString();
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateLoot(int)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void UpdateLoot(int value_)
    {
        Loot += value_;
        this.LootText.text = Loot.ToString();
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateAmmo(int)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void UpdateAmmo(int value_)
    {
        Ammo += value_;
        this.AmmoText.text = Ammo.ToString();
    }
}
