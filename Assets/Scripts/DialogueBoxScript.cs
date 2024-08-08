using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBoxScript : MonoBehaviour
{   
    public TextMeshProUGUI textObject;
    public string[] lines;
    public float textSpeed;
    public int repeatLastLines;
    private bool isPlayerInside = false;
    private int index;
    private bool dialogueReadOnce = false;
    public GameObject inputField;
    public int inputFieldIndex;
    
    // Start is called before the first frame update
    void Start()
    {   

    }

    // Update is called once per frame
    void Update(){

        if(isPlayerInside){
            if(Input.GetKeyDown(KeyCode.F)){
            //Display next line or auto complete dialogue
                if (textObject.text == lines[index]){
                    NextLine();
                }else{
                    StopAllCoroutines();
                    textObject.text = lines[index];
                }
            }
        }

        UnlockMouseWhenInputfield();
    }

    private void StartDialgue(){
        index = 0;
        StartCoroutine(WriteLine());
    }

    private IEnumerator WriteLine(){
        if(isPlayerInside){
            //Write each character of the line
            foreach(char letter in lines[index].ToCharArray()){
                textObject.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }

        }
    }

    private void NextLine(){
        if(isPlayerInside){
            //Display next line
            if(index < lines.Length - 1){
                index++;
                textObject.text = "";
                StartCoroutine(WriteLine());
                if(inputField != null){
                    ActivateInputField();
                }
            }
            else{
            //At the end of the dialogue, disable the dialogue box and repeat last sentances;
                index -= repeatLastLines;
                textObject.text = lines[index];
                gameObject.SetActive(false);
                if(inputField != null){
                    inputField.SetActive(false);
                    GameManager.inputFieldActive = false;
                }
                dialogueReadOnce = true;
            }
        }
    }

    public void SetIsPlayerInside(bool value){
        isPlayerInside = value;
    }

    public void InitiateDialogue()
    {
        //Start the dialogue;
        textObject.text = "";
        StartDialgue();
    }

    public bool DialogueFinished(){
        if (index == lines.Length - 1){
            return true;
        }
        return false;
    }

    public bool DialogueReadOnce(){
        return dialogueReadOnce;
    }

    public void ResetDialogue(){
        if(repeatLastLines > 0){
            index -= repeatLastLines;
            textObject.text = lines[index];
        }
        else{
            index = 0;
        }
    }

    public void ActivateInputField(){
        if(inputField != null){
            if(dialogueReadOnce){
                if(inputFieldIndex == index){
                    inputField.SetActive(true);
                    GameManager.inputFieldActive = true;
                }else{
                    inputField.SetActive(false);
                    GameManager.inputFieldActive = false;
                }
            }
        }
    }

    public string GetInputFieldText(){
        if(inputField != null){
            return inputField.GetComponent<TMP_InputField>().text;
        }
        return null;
    }

    private void UnlockMouseWhenInputfield(){
        if(inputField != null){
            if(inputField.activeSelf){
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }else{
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

}
