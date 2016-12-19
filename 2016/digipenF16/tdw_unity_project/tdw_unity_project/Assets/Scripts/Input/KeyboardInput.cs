///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — KeyboardInput.cs
//COPYRIGHT — © 2016 DigiPen Institute of Technology
///////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#region ENUMS

public enum KeyboardKeyStatus
{
    INACTIVE,
    HELD,
    PRESSED, 
    RELEASED
}

#endregion

public class KeyboardInput : MonoBehaviour
{

    #region FIELDS

    [Header("ENABLE/DISABLE")]
    public bool isKeyboardInputEnabled;
    [Header("Keyboard Controls")]
    [Header("Movement/Directions")]
    public KeyCode up;
    public KeyCode right;
    public KeyCode down;
    public KeyCode left;
    [Header("Standard Buttons")]
    public KeyCode y;
    public KeyCode b;
    public KeyCode a;
    public KeyCode x;
    [Header("Shoulder Buttons")]
    public KeyCode l1;
    public KeyCode l2;
    public KeyCode r1;
    public KeyCode r2;
    [Header("Analog Stick Buttons")]
    public KeyCode l3;
    public KeyCode r3;
    [Header("Misc")]
    public KeyCode select;
    public KeyCode start;
    [HideInInspector]
    public Dictionary<KeyCode, KeyboardKeyStatus> keys;
    private List<KeyCode> keysList;
    private int keysLength;

    #endregion

    #region INITIALIZATION

    /// <summary>
    /// Awake()
    /// </summary>
    void Awake()
    {

    }

    /// <summary>
    /// Start()
    /// </summary>
    void Start()
    {
        //create keys dictionary and list (list needed to traverse dictionary and update dictionary value(cannot update value by reference))
        keys = new Dictionary<KeyCode, KeyboardKeyStatus>();
        
        //check for assigned keys and store them in keys
        AddAssignedKeys();
    }

    #endregion

    #region UPDATE

    /// <summary>
    /// Update()
    /// </summary>
    void Update()
    {
        //go through the keyboard input update loop
        if (isKeyboardInputEnabled)
            UpdateKeyboardInput();

        //DEBUG — print out the contents of keys
        /*
        foreach(KeyValuePair<KeyCode, KeyboardKeyStatus> key in keys)
        {
            print("key = " + key.Key + ", and its status is = " + key.Value);
        }
        */
    }

    #endregion

    #region METHODS

    /// <summary>
    /// Adds only the assigned buttons to the keys list
    /// </summary>
    void AddAssignedKeys()
    {
        //checking movement keys
        if (up != KeyCode.None)
            keys.Add(up, KeyboardKeyStatus.INACTIVE);
        if (right != KeyCode.None)
            keys.Add(right, KeyboardKeyStatus.INACTIVE);
        if (down != KeyCode.None)
            keys.Add(down, KeyboardKeyStatus.INACTIVE);
        if (left != KeyCode.None)
            keys.Add(left, KeyboardKeyStatus.INACTIVE);

        //checking standard button/keys
        if (y != KeyCode.None)
            keys.Add(y, KeyboardKeyStatus.INACTIVE);
        if (b != KeyCode.None)
            keys.Add(b, KeyboardKeyStatus.INACTIVE);
        if (a != KeyCode.None)
            keys.Add(a, KeyboardKeyStatus.INACTIVE);
        if (x != KeyCode.None)
            keys.Add(x, KeyboardKeyStatus.INACTIVE);

        //checking shoulder button/keys
        if (l1 != KeyCode.None)
            keys.Add(l1, KeyboardKeyStatus.INACTIVE);
        if (l2 != KeyCode.None)
            keys.Add(l2, KeyboardKeyStatus.INACTIVE);
        if (r1 != KeyCode.None)
            keys.Add(r1, KeyboardKeyStatus.INACTIVE);
        if (r2 != KeyCode.None)
            keys.Add(r2, KeyboardKeyStatus.INACTIVE);

        //checking analog button/keys
        if (l3 != KeyCode.None)
            keys.Add(l3, KeyboardKeyStatus.INACTIVE);
        if (r3 != KeyCode.None)
            keys.Add(r3, KeyboardKeyStatus.INACTIVE);

        //checking misc button/keys
        if (select != KeyCode.None)
            keys.Add(select, KeyboardKeyStatus.INACTIVE);
        if (start != KeyCode.None)
            keys.Add(start, KeyboardKeyStatus.INACTIVE);

        keysLength = keys.Count;
        keysList = new List<KeyCode>(keys.Keys);

        //DEBUG — CHECK KEYS STORED
        /*
        print("the contents of keys are: ");
        foreach(KeyCode key in keys)
        {
            print(key + ", ");
        }
        */
    }

    /// <summary>
    /// UpdateKeyboardInput() — cycles through keys and updates their status
    /// </summary>
    void UpdateKeyboardInput()
    {
        foreach (KeyCode key in keysList)
        {
            //released
            if (Input.GetKeyUp(key) && keys[key] == KeyboardKeyStatus.HELD)
            {
                keys[key] = KeyboardKeyStatus.RELEASED;

                //DEBUG — check which key is released and print its status
                //print("key released: " + key + " status = " + keys[key]);
            }

            //held
            else if (Input.GetKey(key) && keys[key] == KeyboardKeyStatus.PRESSED || keys[key] == KeyboardKeyStatus.HELD)
            {
                keys[key] = KeyboardKeyStatus.HELD;

                //DEBUG — check which key is held and print its status
                //print("key held: " + key + " status = " + keys[key]);
            }

            //pressed
            else if (Input.GetKeyDown(key) && keys[key] == KeyboardKeyStatus.INACTIVE)
            {
                keys[key] = KeyboardKeyStatus.PRESSED;

                //DEBUG — check which key is pressed and print its status
                //print("key pressed: " + key + " status = " + keys[key]);
            }

            //key is not pressed
            else
            {
                keys[key] = KeyboardKeyStatus.INACTIVE;

                //DEBUG — check which key is inactive and print its status
                //print("key inactive: " + key + " status = " + keys[key]);
            }
        }
    }

    #endregion

}
