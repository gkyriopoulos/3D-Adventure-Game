using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBookRiddle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject questStartedDialogue;
    private GameObject questFinishedTrigger;
    private string correctAnswer = "8";
    void Start()
    {
        gameObject.transform.Find("Book/BookTriggerCrossQuestCompleted").gameObject.SetActive(false);
        questFinishedTrigger = GameObject.Find("NonObjectTriggers/BookRoomQuestComplete");
        questFinishedTrigger.SetActive(false);
        //GameManager.questCrossFinished = true;
    }

    // Update is called once per frame
    void Update()
    {   
        QuestPrerequisites();
        QuestStarted();
        QuestFinished();
    }

    private void QuestStarted(){
        if(GameManager.questCrossFinished){
            if(questStartedDialogue.GetComponent<DialogueBoxScript>().DialogueReadOnce()){
                GameManager.questBookStarted = true;
            }
        }
    }

    private void QuestPrerequisites(){
        if(GameManager.questCrossFinished && !GameManager.questBookFinished){
            gameObject.transform.Find("Book/BookTrigger").gameObject.SetActive(false);
            gameObject.transform.Find("Book/BookTriggerCrossQuestCompleted").gameObject.SetActive(true);
        }
    }

    private void QuestFinished(){
        if(!GameManager.questBookFinished){
            string playerAnswer  = questStartedDialogue.GetComponent<DialogueBoxScript>().GetInputFieldText();
            if(GameManager.questBookStarted){
                if(questStartedDialogue.GetComponent<DialogueBoxScript>().DialogueFinished()){
                        if(playerAnswer.Equals(correctAnswer)){
                            GameManager.questBookFinished = true;
                            gameObject.transform.Find("Book/BookTriggerCrossQuestCompleted").gameObject.SetActive(false);
                            GameObject.Find("Doors/FinalDoor").SetActive(false);
                            questFinishedTrigger.SetActive(true);
                        }
                    }
            }
        }
    }
}
