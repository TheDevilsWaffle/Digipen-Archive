/*////////////////////////////////////////////////////////////////////////
//SCRIPT: DialoguePanelController.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialoguePanelController : MonoBehaviour
{

    #region PROPERTIES

    //references
    MyPlatformerController s_MyPlatformerController;
    public SoundManager SpeakingSFX;
    public AudioClip[] SpeakingSFXArray;

    [Header("DIALOGUE TEXT")]
    public Text DialogueText;

    [Header("TYPE SPEED")]
    public float DelayTime = 0.05f;
    public float QuickTypeTime = 0.0025f;
    private float OriginalDelayTime;
    private bool DisplayTextComplete = false;

    [Header("DIALOGUE/STRING ARRAY")]
    public string[] TextArray;
    public float NewDialogueDelayTime = 0.75f;
    private int CurrentString = 0;

    [Header("BUTTON INSTRUCTION TEXT")]
    public Text ButtonText;
    public Image Button;
    private string SkipInstruction = "SKIP";
    private string NextInstruction = "NEXT";
    private string CloseInstruction = "CLOSE";
    private string AnswerInstruction = "ANSWER";

    [Header("PORTRAIT/CHARACTER SPEAKING")]
    public GameObject SpeakingCharacter;
    public Sprite SpeakingCharacterPortrait;
    [SerializeField]
    private Image PortraitCharacterImage;
    private Vector3 SpeakingCharacter_OriginalPos;
    public Vector3 AnimateShakeAmount = new Vector3(0.5f, 4.5f, 0.0f);
    public float AnimateShakeSpeed= 20f;

    [Header("PLAYER RESPONSE PANEL")]
    public CanvasGroup PlayerResponsePanel;
    public ResponseController ButtonX;
    public ResponseController ButtonB;

    [Header("DIALOGUE PANEL ANIMATION")]
    public Vector3 DialoguePanelAnimatePos = new Vector3(1000f, -1000f, 0f);
    public Vector3 DialoguePanelAnimateScale = new Vector3(0.01f, 0.01f, 0f);
    public float DialoguePanelAnimateTime = 1f;
    private Vector3 OriginalDialoguePanelScale;
    private Vector3 OriginalDialoguePanelPos;

    private string[] CurrentDialogueArray;
    private DialogueContainer DialogueContainer;
    private string[] ResponseSentences;
    private string[] X_EndingSentencesArray;
    private string[] B_EndingSentencesArray;

    private bool HasQuestionBeenAsked;
    public bool HasPlayerResponded;
    private GameObject CurrentNPC;

    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //get default references (if not already set)
        if(this.DialogueText == null)
            this.DialogueText = transform.Find("DialogueText").GetComponent<Text>();
        if (this.ButtonText == null)
            this.ButtonText = transform.Find("DialogueText/InstructionsGroup/ButtonText").GetComponent<Text>();

        this.s_MyPlatformerController = GameObject.Find("Player").GetComponent<MyPlatformerController>();

        //check to make sure we actually have at least one string of text
        if (this.TextArray[0] == null)
            Debug.LogWarning("No strings have been assigned to " + this.gameObject + "'s TextArray, please give it some dialogue!");

        //get correct original scale
        this.OriginalDialoguePanelScale = this.gameObject.transform.localScale;
        this.OriginalDialoguePanelPos = this.gameObject.GetComponent<RectTransform>().anchoredPosition;

        //ensure bools are correct
        this.HasQuestionBeenAsked = false;
        this.HasPlayerResponded = false;

        //in case we're left on, disable us from the start
        this.gameObject.SetActive(false);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
        this.SpeakingSFX = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();

        //ensure variables have correct default values
        this.CurrentString = 0;
        this.DisplayTextComplete = false;
        this.OriginalDelayTime = this.DelayTime;

        //once we're active, start typing out the text
        StartCoroutine(this.TypeOutDialogText());
    }

    #endregion

    #region PUBLIC METHODS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: StartDialogue()
    ////////////////////////////////////////////////////////////////////*/
    public void StartDialogue(GameObject npc_, string[] beginningSentences_, string[] responseSentences_, string[] x_endingSentences_, string[] b_endingSentences_, Sprite speakerPortrait_)
    {
        //set properties
        this.DisplayTextComplete = false;
        this.CurrentNPC = npc_;
        this.CurrentDialogueArray = beginningSentences_;
        this.CurrentString = 0;
        if (responseSentences_.Length > 0)
            this.ResponseSentences = responseSentences_;
        else
        {
            this.ResponseSentences = null;
            this.HasQuestionBeenAsked = true;
            this.HasPlayerResponded = true;

        }
        this.X_EndingSentencesArray = x_endingSentences_;
        this.B_EndingSentencesArray = b_endingSentences_;
        this.SpeakingCharacterPortrait = speakerPortrait_;
        this.PortraitCharacterImage.sprite = this.SpeakingCharacterPortrait;

        //print(this.ResponseSentences);
        //mark NPC as spoken to
        this.CurrentNPC.GetComponent<DialogueContainer>().SetHasBeenSpokenToTrue();
        
        //make sure to turn on this dialogue panel
        this.gameObject.SetActive(true);

        //start displaying text
        this.AnimateDialoguePanelIn();
        StartCoroutine(this.TypeOutDialogText());
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AdvanceText()
    ////////////////////////////////////////////////////////////////////*/
    public void AdvanceText()
    {
        //always check first to make sure this dialogue is still active
        if (this.gameObject.activeSelf && this.CurrentDialogueArray != null)
        {
            //still typing, go faster!
            if (!this.DisplayTextComplete)
            {
                print("typing faster");
                this.QuickType();
            }

            //done typing, advance to next string or close out the dialogue
            else if (!this.HasQuestionBeenAsked || this.DisplayTextComplete && this.ResponseSentences == null)
            {
                print("next string or close out");
                this.NextStringOrCloseOut(this.CurrentDialogueArray);
            }
            else if (this.HasPlayerResponded && this.DisplayTextComplete && this.CurrentString < this.CurrentDialogueArray.Length)
            {
                print("player has responded, display text complete, current string < currentDialogueArray");
                this.NextStringOrCloseOut(this.CurrentDialogueArray);
            }
            else if (this.HasPlayerResponded && this.DisplayTextComplete && this.CurrentString >= this.CurrentDialogueArray.Length)
            {
                print("releasing npc and closing out");
                print("dialogue is finally over!");
                this.ReleaseNPC();
                this.CurrentString = 0;
                StartCoroutine(this.CloseAndReset());
            }
        }
    }


    void PlaySpeakingSFX(Sprite speakingCharacter_)
    {
        switch (speakingCharacter_.name)
        {
            case "spr_dp_blueTrapezoid":
                this.SpeakingSFX.PlayPaitiently(this.SpeakingSFXArray[0], true);
                break;
            case "spr_dp_pinkTriangle":
                this.SpeakingSFX.PlayPaitiently(this.SpeakingSFXArray[1], true);
                break;
            case "spr_dp_player":
                this.SpeakingSFX.PlayPaitiently(this.SpeakingSFXArray[2], true);
                break;
            case "spr_dp_purpleTrapezoid":
                this.SpeakingSFX.PlayPaitiently(this.SpeakingSFXArray[3], true);
                break;
            default:
                Debug.LogError("NO SFX FOR THIS CHARACTER!");
                break;
        }
    }

    #endregion

    #region PRIVATE METHODS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: TypeOutDialogText()
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator TypeOutDialogText()
    {
        print("TYPING!");
        //play sfx based on portrait
        this.PlaySpeakingSFX(this.SpeakingCharacterPortrait);

        //ensure player cannot skip text until it has been completely displayed
        this.DisplayTextComplete = false;

        //pause for a brief moment before displaying text (for animation)
        yield return new WaitForSeconds(this.NewDialogueDelayTime);

        //we've started typing, change the button instructions to display "skip"
        this.UpdateButtonText(this.SkipInstruction);

        //animate the speaker by moving them up and down
        this.AnimateSpeakingCharacter(this.SpeakingCharacter);

        //cycle through individual string one character at a time
        for (int index = 0; index < (this.CurrentDialogueArray[this.CurrentString].Length + 1); ++index)
        {
            //display one character at a time
            this.DialogueText.text = this.CurrentDialogueArray[this.CurrentString].Substring(0, index);
            
            //wait before displaying next character
            yield return new WaitForSeconds(this.DelayTime);
        }

        //stop the player portrait animation
        iTween.StopByName(this.SpeakingCharacter, "AnimateSpeakingCharacter");

        //finished displaying text, allow player to proceed to next string/close
        this.DisplayTextComplete = true;

        //evaluate CurrentDialougeArray
        this.EvaluateCurrentDialougeArray();

        //reset type speed (if quick type was pressed)
        this.DelayTime = this.OriginalDelayTime;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: QuickType()
    ////////////////////////////////////////////////////////////////////*/
    public void QuickType()
    {
        //change the speed to quicker
        this.DelayTime = this.QuickTypeTime;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: NextStringorCloseOut()
    ////////////////////////////////////////////////////////////////////*/
    void NextStringOrCloseOut(string[] currentDialogueArray_)
    {
        //DEBUG
        print("CURRENT STRING = " + this.CurrentString + " and ARRAY.LENGTH = " + currentDialogueArray_.Length);
        if (this.DisplayTextComplete)
        {
            //increment to the next string
            ++this.CurrentString;

            //check to make sure we are in index range of this string array
            if (this.CurrentString < currentDialogueArray_.Length)
            {
                StartCoroutine(this.TypeOutDialogText());
            }
            else
            {
                if (this.ResponseSentences == null)
                {
                    this.ReleaseNPC();
                    this.CurrentString = 0;
                    StartCoroutine(this.CloseAndReset());
                }
                else
                {
                    this.ReleaseNPC();
                    this.CurrentString = 0;
                    StartCoroutine(this.CloseAndReset());
                }
            }
        }
    }

    IEnumerator DisplayPlayerResponses()
    {
        this.PlayerResponsePanel.GetComponent<PlayerResponseController>().EnableGamepadPlayerResponse = true;

        //print("showing player responses now");
        this.ButtonX.transform.Find("Text").GetComponent<Text>().text = this.ResponseSentences[0];
        this.ButtonB.transform.Find("Text").GetComponent<Text>().text = this.ResponseSentences[1];
        
        //set bool to true
        this.HasQuestionBeenAsked = true;
        this.PlayerResponsePanel.GetComponent<CanvasRenderer>().SetAlpha(0f);
        this.PlayerResponsePanel.gameObject.SetActive(true);
        this.PlayerResponsePanel.GetComponent<PlayerResponseController>().SetDefaultButtonToSelect();
        while (this.PlayerResponsePanel.alpha < 1f)
        {
            this.PlayerResponsePanel.alpha += Time.deltaTime / 0.5f;
            //print(this.PlayerResponsePanel.alpha);
            yield return null;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: EvaluateCurrentDialougeArray()
    ////////////////////////////////////////////////////////////////////*/
    void EvaluateCurrentDialougeArray()
    {
        int testIndex = this.CurrentString + 1;
        //we're in bounds, still more to say
        if (testIndex < this.CurrentDialogueArray.Length)
        {
            print("text index < currentDialogue array, updating button!");
            this.UpdateButtonText(this.NextInstruction);
        }

        else if (this.ResponseSentences == null)
            this.UpdateButtonText(this.CloseInstruction);

        //we're out of bounds, no more text to display
        else if (testIndex >= this.CurrentDialogueArray.Length && this.ResponseSentences != null)
        {
            print("text index is >= currentdialoguearray and we had response sentences");
            //do we have a question? (if we have responses)
            if (this.ResponseSentences != null && !this.HasQuestionBeenAsked)
            {
                //clear out CurrentDialogueArray and give it the responses
                this.CurrentDialogueArray = null;
                this.CurrentString = 0;

                //set Reponse buttons
                this.ButtonX.SetButtonResponse(this.ResponseSentences[0]);
                this.ButtonB.SetButtonResponse(this.ResponseSentences[1]);

                //show player reponses
                StartCoroutine(this.DisplayPlayerResponses());

                //update button text
                this.UpdateButtonText(this.AnswerInstruction);
            }
            else
            {
                print("just updating button text");
                this.UpdateButtonText(this.CloseInstruction);
            }
        }
    }

    void ReleaseNPC()
    {
        if(this.CurrentNPC.GetComponent<Wander>() != null)
            this.CurrentNPC.GetComponent<Wander>().ResumeWandering();
    }

    public void PlayerResponded(string response_)
    {
        switch(response_)
        {
            case "X":
                //answer with the first response
                this.CurrentDialogueArray = X_EndingSentencesArray;
                this.CurrentString = 0;
                this.CurrentNPC.GetComponent<DialogueContainer>().SetResponseTo("X");
                break;
            case "B":
                //answer with the second response
                this.CurrentDialogueArray = B_EndingSentencesArray;
                this.CurrentString = 0;
                this.CurrentNPC.GetComponent<DialogueContainer>().SetResponseTo("B");
                break;
            default:
                break;
        }
        //response collected, close out the response dialogue
        this.ClosePlayerResponses();
    }

    void ClosePlayerResponses()
    {
        this.PlayerResponsePanel.GetComponent<PlayerResponseController>().EnableGamepadPlayerResponse = false;
        this.PlayerResponsePanel.GetComponent<CanvasRenderer>().SetAlpha(0f);
        this.PlayerResponsePanel.gameObject.SetActive(false);
        StartCoroutine(this.TypeOutDialogText());
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateButtonText()
    ////////////////////////////////////////////////////////////////////*/
    void UpdateButtonText(string instructionText_)
    {
        //always check to see if the button is on
        this.Button.enabled = true;
        this.ButtonText.text = instructionText_;
        if(instructionText_ == this.AnswerInstruction)
        {
            this.Button.enabled = false;
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: CloseAndReset()
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator CloseAndReset()
    {
        //clear out the displayed text
        this.DialogueText.text = "";

        //reset the current string (in case we are spoken to again)
        this.CurrentString = 0;
        this.ResponseSentences = null;
        this.B_EndingSentencesArray = null;
        this.X_EndingSentencesArray = null;

        this.HasQuestionBeenAsked = false;
        this.HasPlayerResponded = false;

        //reset the button text
        this.UpdateButtonText(this.SkipInstruction);

        this.AnimateDialoguePanelOut();
        //wait so we can animate the dialogue out
        yield return new WaitForSeconds(this.DialoguePanelAnimateTime);

        //turn off this dialogue panel
        this.gameObject.SetActive(false);

        //reset scale and position (IMPORTANT, anchored position is KEY HERE! REMEMBER THIS!)
        this.gameObject.transform.localScale = this.OriginalDialoguePanelScale;
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = this.OriginalDialoguePanelPos;

        this.s_MyPlatformerController.PlayerIsSpeaking = false;
    }

    #endregion

    #region ANIMATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateSpeakingCharacter()
    ////////////////////////////////////////////////////////////////////*/
    void AnimateSpeakingCharacter(GameObject speakingCharacter_)
    {
        iTween.MoveTo(speakingCharacter_,
                                iTween.Hash("name", "AnimateSpeakingCharacter",
                                            "position", (speakingCharacter_.transform.position + this.AnimateShakeAmount),
                                            "speed", this.AnimateShakeSpeed,
                                            "easetype", "easeInOutElastic",
                                            "looptype", "pingpong",
                                            "ignoretimescale", false));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateDialoguePanelIn()
    ////////////////////////////////////////////////////////////////////*/
    void AnimateDialoguePanelIn()
    {
        //print("AnimateDialoguePanelIn reached!");
        iTween.MoveFrom(this.gameObject,
                                iTween.Hash("name", "AnimateDialoguePanelIn",
                                            "position", (this.gameObject.transform.position + this.DialoguePanelAnimatePos),
                                            "time", this.DialoguePanelAnimateTime,
                                            "easetype", "easeOutElastic",
                                            "looptype", "none",
                                            "ignoretimescale", false));

        iTween.ScaleFrom(this.gameObject,
                                iTween.Hash("name", "AnimateDialoguePanelIn",
                                            "scale", this.DialoguePanelAnimateScale,
                                            "time", this.DialoguePanelAnimateTime,
                                            "easetype", "easeOutElastic",
                                            "looptype", "none",
                                            "ignoretimescale", false));
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AnimateDialoguePanelOut()
    ////////////////////////////////////////////////////////////////////*/
    void AnimateDialoguePanelOut()
    {
        iTween.MoveTo(this.gameObject,
                                iTween.Hash("name", "AnimateDialoguePanelOut",
                                            "position", (this.gameObject.transform.position + this.DialoguePanelAnimatePos),
                                            "time", this.DialoguePanelAnimateTime,
                                            "easetype", "easeOutElastic",
                                            "looptype", "none",
                                            "ignoretimescale", false));

        iTween.ScaleFrom(this.gameObject,
                                iTween.Hash("name", "AnimateDialoguePanelIn",
                                            "scale", this.DialoguePanelAnimateScale,
                                            "time", this.DialoguePanelAnimateTime,
                                            "easetype", "easeOutElastic",
                                            "looptype", "none",
                                            "ignoretimescale", false));
    }

    #endregion

}