/*/////////////////////////////////////////////////////////////////////////
//SCRIPT - MenuController.cs
//AUTHOR - Travis Moore
//COPYRIGHT - © 2016 DigiPen Institute of Technology
/////////////////////////////////////////////////////////////////////////*/

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuController : MonoBehaviour
{
    //PROPERTIES
    [SerializeField]
    Canvas MenuSystemCanvas;
    [SerializeField]
    public GameObject CurrentMenu;
    GameObject PreviousMenu;
    GameObject NextMenu;

    //ANIMATION PROPERTIES
    [SerializeField]
    float TransitionTime = 0.5f;
    [SerializeField]
    string EaseType = "easeInOutQuad";
    [SerializeField]
    Vector3 MenuScaleUpSize = new Vector3(1f, 1f, 1f);
    [SerializeField]
    Vector3 MenuScaleDownSize = new Vector3(0.25f, 0.25f, 1f);

    //HIDDEN PROPERTIES
    Vector3 CenterPos = new Vector3(0f, 0f, 1f);
    Vector3 CreationPos = new Vector3(0f, -500f, 1f);

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        //assign CurrentMenu based off of Property Assignment
        if(this.CurrentMenu == null)
        {
            Debug.LogError("ERROR! Drag a menu on the screen and assign it as CurrentMenu");
        }

        if(this.gameObject.transform.Find("StartMenu/ButtonGroup/Button_Start").gameObject != null)
        {
            this.SetSelectedButton(this.gameObject.transform.Find("StartMenu/ButtonGroup/Button_Start").gameObject);
        }
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: SetSelectedButton(GameObject)
    /////////////////////////////////////////////////////////////////////////*/
    void SetSelectedButton(GameObject btn_)
    {
        //set to null (precaution)
        EventSystem.current.SetSelectedGameObject(null);

        //set first btn to passed btn
        EventSystem.current.SetSelectedGameObject(btn_);

        //ensure the button is "highlighted"
        btn_.GetComponent<Button>().Select();
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: CreateMenu(GameObject)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void CreateMenu(GameObject menuPrefab_)
    {
        //create an assign CurrentMenu
        this.CurrentMenu = GameObject.Instantiate(menuPrefab_,
                                                  this.CreationPos,
                                                  Quaternion.identity) as GameObject;

        //scale this menu smaller
        this.CurrentMenu.transform.localScale = this.MenuScaleDownSize;

        //perfrom stupid parent assignment
        this.ParentNewMenu(this.CurrentMenu);

        //animate the menu into place
        this.AnimateMenuIn(this.CurrentMenu);
        this.AnimateMenuScale(this.CurrentMenu, this.MenuScaleUpSize, "easeOutQuad");
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: ParentNewMenu(GameObject)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void ParentNewMenu(GameObject menu_)
    {
        //make this thing a child of the MenuSystemCanvas
        menu_.transform.SetParent(this.MenuSystemCanvas.transform);
        menu_.transform.localRotation = Quaternion.identity;

        //IMPORTANT: need to set these because they are not set automatically
        menu_.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        menu_.GetComponent<RectTransform>().offsetMax = new Vector2(1, 1);

        //make menu off-screen so we can animate it in
        menu_.transform.localPosition = this.CreationPos;
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateMenuIn(GameObject, Vector3)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateMenuIn(GameObject menu_)
    {
        iTween.MoveTo(menu_, iTween.Hash("name", "AnimateMenuIn",
                                         "time", this.TransitionTime,
                                         "easetype", this.EaseType,
                                         "islocal", true,
                                         "position", this.CenterPos));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DestroyPreviousMenu()
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    public void DestroyPreviousMenu()
    {
        //assign PreviousMenu from CurrentMenu, null & destroy CurrentMenu
        this.PreviousMenu = this.CurrentMenu;
        this.CurrentMenu = null;
        Destroy(this.CurrentMenu);

        //fun animations to get rid of previous menu
        this.AnimateMenuOut(this.PreviousMenu);
        this.AnimateMenuScale(this.PreviousMenu, this.MenuScaleDownSize, "easeInQuad");
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateMenuScale(GameObject, Vector3, string)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateMenuScale(GameObject menu_, Vector3 scale_, string easeType_)
    {
        iTween.ScaleTo(menu_, iTween.Hash("name", "AnimateMenuScaleUp",
                                         "time", this.TransitionTime,
                                         "easetype", easeType_,
                                         "scale", scale_));
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateMenuOut(GameObject, Vector3)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void AnimateMenuOut(GameObject menu_)
    {
        iTween.MoveTo(menu_, iTween.Hash("name", "AnimateMenuOut",
                                         "time", this.TransitionTime,
                                         "easetype", this.EaseType,
                                         "islocal", true,
                                         "position", this.CreationPos,
                                         "oncompletetarget", this.gameObject,
                                         "oncomplete", "DestroyMenu",
                                         "oncompleteparams", menu_));
    }
    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: DestroyMenu(GameObject)
    //NOTE:
    /////////////////////////////////////////////////////////////////////////*/
    void DestroyMenu(GameObject menu_)
    {
        //destroy this menu
        Destroy(menu_);
    }
}
