using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTutorialNPC : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialogueBox;
    // Update is called once per frame
    void Update()
    {
        QuestStarted();
        QuestFinished();
    }

    private void QuestStarted(){
        if(dialogueBox.GetComponent<DialogueBoxScript>().DialogueReadOnce()){
            GameManager.questTutorialNPCstarted = true;
        }
    }

    private void QuestFinished(){
        if(GameManager.questTutorialNPCstarted && GameManager.hasStarterArmor && GameManager.hasSword){
            GameManager.questTutorialNPCfinished = true;
        }
    }
}
