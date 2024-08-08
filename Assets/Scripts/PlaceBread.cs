using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBread : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject instruction;
    private bool isPlayerInside = false;
    public GameObject bread;
    
    private void OnTriggerEnter(Collider col)
    {   
        if(isActiveAndEnabled){
            if(col.gameObject.tag == "Player")
            {   
                instruction.SetActive(true);
                isPlayerInside = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {   
        if(isActiveAndEnabled){
            if(col.gameObject.tag == "Player")
            {
                instruction.SetActive(false);
                isPlayerInside = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlaceItem();
    }

    private void PlaceItem(){
        if (isPlayerInside)
        {
            if(GameManager.hasBread)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {   
                    bread.SetActive(true);
                    instruction.SetActive(false);
                    gameObject.SetActive(false);
                    GameManager.hasBread = false;
                }
            }
        }
    }
}
