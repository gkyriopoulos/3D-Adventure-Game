using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCross : MonoBehaviour
{
    public GameObject dialogueBox;
    private GameObject questCompleteTrigger;
    private GameObject questStartedTrigger;
    private GameObject armorDoorTrigger; 
    private GameObject questCompleteTrigger_2;

    void Start(){
        gameObject.transform.Find("Bread_Stand/Bread_Altair/BreadTrigger").GetComponent<PlaceBread>().enabled = false;
        gameObject.transform.Find("Wineglass_Stand/Wineglass_Altair/WineglassTrigger").GetComponent<PlaceWineglass>().enabled = false;
        questStartedTrigger = GameObject.Find("NonObjectTriggers/CrossRoomQuestStart");
        questCompleteTrigger = GameObject.Find("NonObjectTriggers/CrossRoomQuestComplete");
        questCompleteTrigger_2 = GameObject.Find("NonObjectTriggers/CrossRoomQuestComplete_2");
        armorDoorTrigger = GameObject.Find("NonObjectTriggers/ArmorDoorOpened");
        questCompleteTrigger.SetActive(false);
        questCompleteTrigger_2.SetActive(false);
        questStartedTrigger.SetActive(false);
        armorDoorTrigger.SetActive(false);
    }
    void Update()
    {
        QuestStarted();
        QuestUpdatePuzzle();
        QuestFinished();
    }

    private void QuestStarted(){
        if(dialogueBox.GetComponent<DialogueBoxScript>().DialogueReadOnce()){
            GameManager.questCrossStarted = true;
        }
    }

    private void QuestFinished(){
        if(!GameManager.questCrossFinished){
            bool bread_placed = gameObject.transform.Find("Bread_Stand/Bread_Altair/Bread_Model").gameObject.activeSelf;
            bool wine_placed = gameObject.transform.Find("Wineglass_Stand/Wineglass_Altair/Wineglass_Model").gameObject.activeSelf;
            if(bread_placed && wine_placed && GameManager.questCrossStarted){
                questCompleteTrigger.SetActive(true);
                questCompleteTrigger_2.SetActive(true);
                armorDoorTrigger.SetActive(true);
                GameObject.Find("Doors/ArmorDoor").SetActive(false);
                GameManager.questCrossFinished = true;
            }
        }
    }

    private void QuestUpdatePuzzle(){
        if(GameManager.questCrossStarted){  
            if (GameManager.hasBread){
                gameObject.transform.Find("Bread_Stand/Bread_Altair/BreadTrigger").GetComponent<DialogueScript>().enabled = false;
                gameObject.transform.Find("Bread_Stand/Bread_Altair/BreadTrigger").GetComponent<PlaceBread>().enabled = true;
            }
            if (GameManager.hasWineglass){
                gameObject.transform.Find("Wineglass_Stand/Wineglass_Altair/WineglassTrigger").GetComponent<DialogueScript>().enabled = false;
                gameObject.transform.Find("Wineglass_Stand/Wineglass_Altair/WineglassTrigger").GetComponent<PlaceWineglass>().enabled = true;
            }
            if(!GameManager.questCrossFinished){
                questStartedTrigger.SetActive(true);
            }else{
                questStartedTrigger.SetActive(false);
            }
        }
    }
}
