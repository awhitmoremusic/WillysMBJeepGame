using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
    This script is adapted from an online example of a dialogue manager. The script runs the dialogue box in game.
    Dialogue Manager takes a Dialogue Class Object and uses it to display messages in a popup box

*/
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI speech;

    private Queue<string> sentances;

    public Animator animator;

    public Button continueButton;
    public Button previousButton;

    private TextMeshProUGUI _continueText;
    private TextMeshProUGUI _previousText;

    public float DialogueSpeed = 0.25f;

    private int MissionProgressD;

    
    void Start()
    {
        //gets required objects in scene
        sentances = new Queue<string>();
        _continueText = continueButton.GetComponentInChildren<TextMeshProUGUI>();
        _previousText = previousButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    //Triggers Dialogue when mission progression changes
    private void Update()
    {
        //each time mission progress rises the dialogue will trigger
        if(MissionProgressD != MissionProgress.MissionProgression)
        {
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();

            MissionProgressD = MissionProgress.MissionProgression;
        }
    }

    //Runs when the dialogue is expected to start within the game
    public void StartDialogue (Dialogue di)
    {

        //sets initial conditions and animation of the box
        animator.SetBool("isOpen", true);
        sentances.Clear();
        title.text = di.name;


        //get latest message first, for a game with progressing hints we get the latest message first with an option to go back to a previous one
        di.sentances.Reverse();
        foreach (string sentance in di.sentances)
        {
            sentances.Enqueue(sentance);  //add each sentace to a Queue collection
        }


        DisplayNextSentence();
    }


    //Display the next sentance or ends the dialogue
    private void DisplayNextSentence()
    {
        if(sentances.Count == 0) { EndDialogue(); return; } //breaks out of the loop

        string s1 = sentances.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(s1)); //displays s1 character by character using co-routine
    }


    //ends dialogue simply by animating the dialogue offscreen (object is never destoryed so it can be called again)
    public void EndDialogue()
    {
        print("End of Con");
        animator.SetBool("isOpen", false);
    }

    


    //This coroutine allows the sentance to be typed out letter for letter rather than all at once immitating a typewriter for the game
    IEnumerator TypeSentence (string sentence)
    {
        //running typerwriter audio
        AudioSource AudioType = GameObject.Find("Typewriter").GetComponent<AudioSource>();
        AudioType.Play();

        //Temporarily hide text values which could skip message
        _continueText.text = "";
        _previousText.text = "";
        continueButton.interactable = false;
        
        yield return new WaitForSeconds(0.25f);


        speech.text = "";
        //convert to character array to add 1 letter at a time
        //yeild return acts as a delay in this situation as each loop of the foreach runs
        foreach (char letter in sentence.ToCharArray())
        {
            speech.text += letter;
            yield return new WaitForSeconds(DialogueSpeed/10);
        }

        //sets up button text to progress
        continueButton.interactable = true;
        _continueText.text = "Continue >>";
        _previousText.text = "<< Previous";


        //stop playing audio
        AudioType.Stop();

    }
}
