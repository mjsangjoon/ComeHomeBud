using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour
{
    public bool isLandmark;
    public bool isFamiliarScent;
    public string nextLevel;
    public DialogueTrigger trigger;
    private Outline outline;

    // Start is called before the first frame update
    void Awake()
    {
        if(this.trigger == null){
            trigger = this.GetComponent<DialogueTrigger>();
        }
        this.outline = GetComponent<Outline>();
        this.outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //called when mouse enters object. We want to highlight the object when this happens
    public void OnMouseEnter(){
        outline.enabled = true;
    }

    //called when mouse leaves object. We want to remove the highlight on the object
    public void OnMouseExit() {
        outline.enabled = false;
    }

    void OnMouseDown(){
        //some code to update landmark or scent count
        if(GameManager.gameManager){
            if(isLandmark){
                GameManager.gameManager.AddLandmarkCount();
                Debug.Log("Landmark count: " + GameManager.gameManager.getNumTimesFamiliarLandmark());
            }
            else if(isFamiliarScent){
                GameManager.gameManager.AddScentCount();
                Debug.Log("Scent count: " + GameManager.gameManager.getNumTimesFamiliarScent());
            }
        }
        DetermineNextStage();
        trigger.TriggerDialogue(nextLevel);
    }

    //uses game state to determine next level
    void DetermineNextStage(){
        if(GameManager.gameManager){
            //handle stage progression when enough landmarks/scents have been encountered
            if(nextStageThroughProgressPossible()){
                //advance to end if suburbs have been completed
                if(PlayerPrefManager.GetSuburbsComplete()){
                    this.nextLevel = "GameComplete";
                    Debug.Log(this.nextLevel);
                }
                else{
                    this.nextLevel = "CityStart";
                    Debug.Log(this.nextLevel);
                    PlayerPrefManager.SetSuburbsComplete(true);
                }
            }
            //handle rng skip stage event
            else if(GameManager.gameManager.skipToNextStagePossible){

            }
            //handle other rng events
            //else if(GameManager.gameManager){}
            //nothing happened, go on to next level as usual
            else{return;}
        }
    }

    //determines if player has seen enough landmarks or scents to advance to next stage
    bool nextStageThroughProgressPossible(){
        if(GameManager.gameManager){
            bool a = GameManager.gameManager.getNumTimesFamiliarLandmark() >= GameManager.gameManager.numTimesLandmarkNeedsToBeSeen;
            bool b = GameManager.gameManager.getNumTimesFamiliarScent() >= GameManager.gameManager.numTimesFamiliarScentNeedsToBeFound;
            return a || b;
        }
        return false;
    }
}
