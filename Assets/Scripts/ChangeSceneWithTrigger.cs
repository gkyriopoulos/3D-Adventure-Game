using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneWithTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    private GameObject Instruction;
    [SerializeField] 
    private GameObject InstructionCantEnter;
    private bool isPlayerInside = false;
    private bool requirementsMet = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScene();
        requirementsMet = GameManager.questTutorialNPCfinished;

    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {   
            if(requirementsMet)
            {
                Instruction.SetActive(true);
                isPlayerInside = true;
            }
            else
            {
                InstructionCantEnter.SetActive(true);
                isPlayerInside = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player"){   
            if(requirementsMet)
            {
                Instruction.SetActive(false);
                isPlayerInside = false;
            }
            else
            {
                InstructionCantEnter.SetActive(false);
                isPlayerInside = false;
            }
        }
    }

    private void UpdateScene(){
        if(isPlayerInside)
        {
            if(Input.GetKeyDown(KeyCode.E) && requirementsMet)
            {   
                SceneManager.LoadScene("Dungeon");
            }   
        }
    }
}
