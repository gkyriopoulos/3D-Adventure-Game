using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{   
    public GameObject instruction;
    public GameObject dialogueBox;
    private bool isPlayerInside = false;    
    private bool dialogueInitiated = false;
    public bool autoDialogue = false;
    public bool canBeReread = true;    

    // Update is called once per frame
    void Update()
    {   
        if(autoDialogue){
            AutoDialogue();
        }else{
            KeyDialogue();
        }
    }

    private void OnTriggerEnter(Collider col)
    {   
        if(isActiveAndEnabled){
            if(!dialogueBox.GetComponent<DialogueBoxScript>().DialogueFinished()){
                if(col.gameObject.tag == "Player"){   
                    isPlayerInside = true;
                    if(!autoDialogue){
                        instruction.SetActive(true);
                    }
                    dialogueBox.GetComponent<DialogueBoxScript>().SetIsPlayerInside(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {   
        if(isActiveAndEnabled){
            if(col.gameObject.tag == "Player")
            {   
                isPlayerInside = false;
                if(!autoDialogue){
                    instruction.SetActive(false);
                }
                dialogueBox.GetComponent<DialogueBoxScript>().SetIsPlayerInside(false);
                dialogueBox.SetActive(false);
                if(dialogueBox.GetComponent<DialogueBoxScript>().DialogueFinished() && canBeReread){
                    dialogueBox.GetComponent<DialogueBoxScript>().ResetDialogue();
                }
            }
        }
    }

    private void KeyDialogue(){
        if(isPlayerInside)
        {   
            if(Input.GetKeyDown(KeyCode.F))
            {   
                if(!dialogueBox.GetComponent<DialogueBoxScript>().DialogueFinished()){
                    instruction.SetActive(false);
                    dialogueBox.SetActive(true);
                    if(!dialogueInitiated){
                        dialogueBox.GetComponent<DialogueBoxScript>().InitiateDialogue();
                        dialogueInitiated = true;
                    }
                }

            }
        }
    }    

    private void AutoDialogue(){
        if(isPlayerInside){
           if(!dialogueBox.GetComponent<DialogueBoxScript>().DialogueFinished()){
                dialogueBox.SetActive(true);
                if(!dialogueInitiated){
                    dialogueBox.GetComponent<DialogueBoxScript>().InitiateDialogue();
                    dialogueInitiated = true;
                }
           }
        }
    }
}
