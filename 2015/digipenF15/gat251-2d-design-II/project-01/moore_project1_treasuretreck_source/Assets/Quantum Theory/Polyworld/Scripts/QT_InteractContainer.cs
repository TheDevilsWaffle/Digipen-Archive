using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//script is for demo purposes only. Shouldn't be used in a production setting as all the UI work should be handled elsewhere.

public class QT_InteractContainer : MonoBehaviour {
    public GameObject ContainerTop;
    public string OpenText = "Press E to Open.";
    public string CloseText = "Press E to Close.";
    public AnimationClip OpenClip, CloseClip;

    private Vector3 centerScreen;
    public GUIText PopUpText;
    private bool isOpen = false;
    private Animator Anim;
    [HideInInspector]
    bool IsAnimating = false;
   
	// Use this for initialization
	void Start () {
      
       // int sw = Screen.width/2;
       // int sh = Screen.height/2;
        centerScreen = new Vector3(0.5f, 0.5f, 0f);
        Anim = ContainerTop.GetComponent<Animator>();
	}	

    void OnTriggerEnter(Collider col_)
    {
        Debug.Log("COLLISION IS HAPPENING");
        if(col_.gameObject.name == "Player" && !this.IsAnimating)
        {
            this.IsAnimating = true;
            this.FadeOut();
        }
            
    }

    void OnTriggerExit()
    {
        //PopUpText.gameObject.SetActive(false);       
    }

    /*/////////////////////////////////////////////////////////////////////////
    //FUNCTION: FadeOut()
    /////////////////////////////////////////////////////////////////////////*/
    void FadeOut()
    {
        Application.LoadLevel("sce-menu-win");
    }
}
