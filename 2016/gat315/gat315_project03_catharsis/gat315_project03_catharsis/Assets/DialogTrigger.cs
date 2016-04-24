using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour
{
    PlayerDialog DialogSystem;
    GameObject Player;
    private bool MessageHasBeenDisplayed;

	// Use this for initialization
	void Start ()
    {
        this.DialogSystem = GameObject.FindWithTag("DialogSystem").GetComponent<PlayerDialog>();
        this.Player = GameObject.FindWithTag("Player").gameObject;
	}

    void OnTriggerEnter2D(Collider2D col2D_)
    {
        if(col2D_.gameObject == this.Player && !this.MessageHasBeenDisplayed)
        {
            this.MessageHasBeenDisplayed = true;
            this.DialogSystem.DisplayDialog();
        }
    }

	
}
