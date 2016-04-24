using UnityEngine;
using System.Collections;

public class SmallNPCScript : MonoBehaviour
{
    public GameObject FirstSmallNPC;
    public GameObject SecondSmallNPC;

    public GameObject LargeNPC;
    public GameObject EndingNode;

	// Use this for initialization
	void Start ()
    {
	
	}

    public void SmallNPCAction()
    {
        //print("SmallNPCAction() reached");
        if (this.gameObject.name == "SmallNPC")
        {
            //print("talked to " + this.gameObject);
            this.FirstSmallNPC.SetActive(false);
            this.SecondSmallNPC.SetActive(true);


            iTween.MoveTo(this.LargeNPC,
                          iTween.Hash("name", "animateLargeNPC",
                                      "position", this.EndingNode.transform.position,
                                      "time", 3f,
                                      "delay", 8f,
                                      "easetype", EaseType.easeInOutQuad.ToString(),
                                      "looptype", "none"));
        }

        if (this.gameObject.name == "SmallNPC (2)")
        {
            //print("talked to " + this.gameObject);
            //print("PLAYER RESPONDED WITH = " + this.gameObject.GetComponent<DialogueContainer>().PlayerRespondedWith);
            //player responded with X, hide the smallNPC
            if (this.gameObject.GetComponent<DialogueContainer>().PlayerRespondedWith == "X")
            {
                StartCoroutine(this.HideSmallNPC());
            }

            //
        }



    }
    IEnumerator HideSmallNPC()
    {
        //print("HideSmallNPC reached");
        yield return new WaitForSeconds(5f);

        this.gameObject.SetActive(false);
    }
}
