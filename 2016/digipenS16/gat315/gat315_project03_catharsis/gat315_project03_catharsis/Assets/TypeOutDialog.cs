/*////////////////////////////////////////////////////////////////////////
//SCRIPT: TypeOutDialog.cs
//AUTHOR: Travis Moore
//COPYRIGHT: © 2016 DigiPen Institute of Technology, All Rights Reserved
////////////////////////////////////////////////////////////////////////*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypeOutDialog : MonoBehaviour
{
    #region PROPERTIES

    //dialogue
    [Header("DIALOGUE TEXT")]
    public Text DialogueText;

    [Header("CHARACTER TYPE SPEED")]
    public float DelayTime = 0.075f;
    public float QuickTypeTime = 0.0025f;
    private float OriginalDelayTime;

    [Header("DIALOGUE/STRING ARRAY")]
    public string[] TextArray;
    public float NewDialogueDelayTime = 0.5f;
    private int CurrentString = 0;

    //button
    [Header("BUTTON INSTRUCTION TEXT")]
    public Text ButtonText;
    private string SkipInstruction = "SKIP";
    private string NextInstruction = "NEXT";
    private string CloseInsturction = "CLOSE";

    //next string delay input
    private bool DisplayTextComplete = false;

    #endregion

    #region INITIALIZATION

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Awake()
    ////////////////////////////////////////////////////////////////////*/
    void Awake()
    {
        //get references
        this.DialogueText = transform.Find("DialogText").GetComponent<Text>();

        //in case we're left on, disable us from the start
        this.gameObject.SetActive(false);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: Start()
    ////////////////////////////////////////////////////////////////////*/
    void Start()
    {
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
    //FUNCTION: DisplayDialogue()
    ////////////////////////////////////////////////////////////////////*/
    public void DisplayDialogue()
    {
        if (!this.gameObject.activeSelf)
        {
            //make sure to turn on this dialogue panel
            this.gameObject.SetActive(true);

            //start displaying some dialogue, baby!
            StartCoroutine(this.TypeOutDialogText());
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: AdvanceText()
    ////////////////////////////////////////////////////////////////////*/
    public void AdvanceText()
    {
        //always check first to make sure this dialogue is still active
        if (this.gameObject.activeSelf)
        {
            //still typing, go faster!
            if (!this.DisplayTextComplete)
                this.QuickType();

            //done typing, advance to next string or close out the dialogue
            else
                this.NextStringOrCloseOut();
        }
    }

    #endregion

    #region PRIVATE METHODS

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: TypeOutDialogText()
    ////////////////////////////////////////////////////////////////////*/
    IEnumerator TypeOutDialogText()
    {
        //ensure player cannot skip text until it has been completely displayed
        this.DisplayTextComplete = false;

        //pause for a brief moment before displaying text (for animation)
        yield return new WaitForSeconds(this.NewDialogueDelayTime);

        //we've started typing, change the button instructions to display "skip"
        this.UpdateButtonText(this.SkipInstruction);

        //cycle through individual string one character at a time
        for (int index = 0; index < (this.TextArray[this.CurrentString].Length + 1); ++index)
        {
            //display one character at a time
            this.DialogueText.text = this.TextArray[this.CurrentString].Substring(0, index);

            //wait before displaying next character
            yield return new WaitForSeconds(this.DelayTime);
        }

        //finished displaying text, allow player to proceed to next string/close
        this.DisplayTextComplete = true;

        //evaluate TextArray at this point to display the correct button instructions
        this.EvaluateTextArray();

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
    void NextStringOrCloseOut()
    {
        if (this.DisplayTextComplete)
        {
            //increment to the next string
            ++this.CurrentString;

            //check to make sure we are in index range of this string array
            if (this.CurrentString < this.TextArray.Length)
            {
                //type out next string
                StartCoroutine(this.TypeOutDialogText());
            }

            //we've run out of strings in the array
            else
            {
                this.CloseAndReset();
            }
        }
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: EvaluateTextArray()
    ////////////////////////////////////////////////////////////////////*/
    void EvaluateTextArray()
    {
        int testIndex = this.CurrentString + 1;
        //we're in bounds, still more to say
        if (testIndex < this.TextArray.Length)
            this.UpdateButtonText(this.NextInstruction);

        //we're out of bounds, no more text to display
        if(testIndex >= this.TextArray.Length)
            this.UpdateButtonText(this.CloseInsturction);
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: UpdateButtonText()
    ////////////////////////////////////////////////////////////////////*/
    void UpdateButtonText(string instructionText_)
    {
        this.ButtonText.text = instructionText_;
    }

    /*////////////////////////////////////////////////////////////////////
    //FUNCTION: CloseAndReset()
    ////////////////////////////////////////////////////////////////////*/
    void CloseAndReset()
    {
        //clear out the displayed text
        this.DialogueText.text = "";

        //reset the current string (in case we are spoken to again)
        this.CurrentString = 0;

        //turn off this dialogue panel
        this.gameObject.SetActive(false);
    }

    #endregion
}