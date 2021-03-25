using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{

    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    private string nextLevel;

    // Start is called before the first frame update
    private void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, string nextLevel){
        //set animator to new scene's dialogue box animator if null
        if(animator == null){
            animator = GameObject.Find("DialogueBox").GetComponent<Animator>();
        }

        GameObject.Find("DialogueBox").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("DialogueBox").GetComponent<Button>().onClick.AddListener(DisplayNextSentence);

        animator.SetBool("IsOpen", true);
        sentences.Clear();
        this.nextLevel = nextLevel;
        Debug.Log("Next level is " + nextLevel);

        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log("Displaying sentence: " + sentence);
        //stop coroutines so that there it stops animating previous sentences
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Coroutine to display the dialogue 1 letter at a time
    IEnumerator TypeSentence(string sentence){
        //set text to new scene's dialogue box text if null
        if(dialogueText == null){
            dialogueText = GameObject.Find("Text").GetComponent<Text>();
        }
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue(){
        animator.SetBool("IsOpen", false);
        if(GameManager.gameManager){
            GameManager.gameManager.LevelComplete(nextLevel);
        }
    }
}