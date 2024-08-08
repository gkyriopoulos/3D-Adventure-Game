using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStoneheedge : MonoBehaviour
{   
    public GameObject questStartedDialogue;
    private GameObject questFinishedTrigger;
    private string solution1 = "I LOVE GRAPHICS";
    private string solution2 = "ILOVEGRAPHICS";
    // Start is called before the first frame update
    void Start()
    {
        questFinishedTrigger = GameObject.Find("NonObjectTriggers/StonehedgeRoomQuestComplete");
        questFinishedTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        QuestStarted();
        QuestFinished();
    }

    private void QuestStarted(){
        if(GameManager.questBookFinished){
            if(questStartedDialogue.GetComponent<DialogueBoxScript>().DialogueReadOnce()){
                GameManager.questStoneheedgeStarted = true;
            }
        }
    }

    private void QuestFinished(){
        if(GameManager.questStoneheedgeStarted){
            string playerAnswer  = questStartedDialogue.GetComponent<DialogueBoxScript>().GetInputFieldText();
            if(questStartedDialogue.GetComponent<DialogueBoxScript>().DialogueFinished()){
                if(playerAnswer.Equals(solution1) || playerAnswer.Equals(solution2)){
                    GameManager.questStoneheedgeFinished = true;
                    gameObject.transform.Find("Book/BookTriggerFinalQuestStart").gameObject.SetActive(false);
                    questFinishedTrigger.SetActive(true);
                }
            }
        }
    }

}
