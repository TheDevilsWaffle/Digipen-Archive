using UnityEngine;
using System.Collections;

public class LargeNPCScript : MonoBehaviour
{
    public GameObject SecondSmallNPC;
    public GameObject ExitNode;

    public SpriteRenderer Cage;
    public BoxCollider2D[] WallArray;

    // Use this for initialization
    void Start()
    {

    }

    public void LargeNPCAction()
    {
        //print("LargeNPCAction() reached, with player responded = " + this.gameObject.GetComponent<DialogueContainer>().PlayerRespondedWith);
        //player responded with X, turn over SmallNPC
        if (this.gameObject.GetComponent<DialogueContainer>().PlayerRespondedWith == "X")
        {
                StartCoroutine(this.TurnOverSmallNPC());
        }

        //player didn't turn over the small npc
        if (this.gameObject.GetComponent<DialogueContainer>().PlayerRespondedWith == "B")
        {
            this.Cage.enabled = false;
            this.ReleasePlayerFromCage();
            this.SecondSmallNPC.SetActive(false);
            GameObject.FindWithTag("LevelSettings").gameObject.GetComponent<SceneManagementSystem>().NextScene = "sce_07";

            iTween.MoveTo(this.gameObject,
                          iTween.Hash("name", "animateLargeNPCToExit",
                                      "position", this.ExitNode.transform.position,
                                      "time", 4f,
                                      "delay", 1f,
                                      "easetype", EaseType.easeInOutQuad.ToString(),
                                      "looptype", "none"));
        }



    }
    IEnumerator TurnOverSmallNPC()
    {
        //change the level to change to
        GameObject.FindWithTag("LevelSettings").GetComponent<SceneManagementSystem>().NextScene = "sce_06";

        //print("TurnOverSmallNPC() reached");
        yield return new WaitForSeconds(5f);

        this.SecondSmallNPC.SetActive(true);
        this.SecondSmallNPC.GetComponent<CircleCollider2D>().enabled = false;
        this.SecondSmallNPC.GetComponent<SpriteRenderer>().sortingOrder = 10;
        this.SecondSmallNPC.transform.position = this.gameObject.transform.position;
        this.SecondSmallNPC.transform.SetParent(this.gameObject.transform);

        yield return new WaitForSeconds(2f);

        this.Cage.enabled = false;
        this.ReleasePlayerFromCage();
            

            iTween.MoveTo(this.gameObject,
                          iTween.Hash("name", "animateLargeNPCToExit",
                                      "position", this.ExitNode.transform.position,
                                      "time", 4f,
                                      "delay", 1f,
                                      "easetype", EaseType.easeInOutQuad.ToString(),
                                      "looptype", "none"));

    }

    void ReleasePlayerFromCage()
    {
        //print("ReleasePlayerFromCage() reached");
        foreach (BoxCollider2D wall in this.WallArray)
        {
            wall.enabled = false;
        }
    }
}
