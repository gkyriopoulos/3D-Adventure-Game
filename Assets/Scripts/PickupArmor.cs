using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupArmor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    private GameObject instruction;

    [SerializeField] 
    private GameObject item;
    private bool isPlayerInside = false;

    private int swordAttack = 10;

    private int shieldDefence = 10;
    
    void Update()
    {
        UpdateScene();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            instruction.SetActive(true);
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            instruction.SetActive(false);
            isPlayerInside = false;
        }
    }

    private void UpdateScene(){
        if(isPlayerInside)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {   
                transform.parent.gameObject.SetActive(false);
                instruction.SetActive(false);
                
                GameObject player = GameObject.FindWithTag("Player");
                
                if(item.name == "StarterArmor_HumanMale")
                {   
                    IsArmorstandStarter(player);
                }

                if(item.name == "Sword_01_Player")  
                {   
                    IsSword(player, swordAttack);
                }
 
                if(item.name == "Shield_01_Player")
                {   
                    IsShield(player, shieldDefence);
                }

                if(item.name == "PlateArmor_HumanMale")
                {   
                    IsArmorstandPlate(player);
                }
               
            }
        }
    }

    private void IsArmorstandStarter(GameObject targetPlayer)
    {   
        targetPlayer.transform.Find("Model/Armors/StarterClothes").gameObject.SetActive(true);
        targetPlayer.transform.Find("Model/Mesh/Body/Chest").gameObject.SetActive(false);
        targetPlayer.transform.Find("Model/Mesh/Body/Arms").gameObject.SetActive(false);
        targetPlayer.transform.Find("Model/Mesh/Body/Legs").gameObject.SetActive(false);
        targetPlayer.transform.Find("Model/Mesh/Body/Feet").gameObject.SetActive(false);
        targetPlayer.transform.Find("Model/Mesh/Accessories/Underwear").gameObject.SetActive(false);
        targetPlayer.GetComponent<PlayerFunctions>().SetMaxPlayerShiled(30);
        targetPlayer.GetComponent<PlayerFunctions>().SetPlayerShield(20);
        GameManager.hasStarterArmor = true;
    }

    private void IsArmorstandPlate(GameObject targetPlayer)
    {   
        targetPlayer.transform.Find("Model/Armors/StarterClothes").gameObject.SetActive(false);
        targetPlayer.transform.Find("Model/Armors/Plate1").gameObject.SetActive(true);
        targetPlayer.transform.Find("Model/Mesh/Body/Hands").gameObject.SetActive(false);
        targetPlayer.transform.Find("Model/Armors/Plate1/PlateSet1_Boots").gameObject.SetActive(true);
        targetPlayer.transform.Find("Model/Armors/Plate1/PlateSet1_Chest").gameObject.SetActive(true);
        targetPlayer.transform.Find("Model/Armors/Plate1/PlateSet1_Gloves").gameObject.SetActive(true);
        targetPlayer.transform.Find("Model/Armors/Plate1/PlateSet1_Helmet").gameObject.SetActive(true);
        targetPlayer.transform.Find("Model/Armors/Plate1/PlateSet1_Pants").gameObject.SetActive(true);
        targetPlayer.transform.Find("Model/Armors/Plate1/PlateSet1_Shoulders").gameObject.SetActive(true);
        targetPlayer.GetComponent<PlayerFunctions>().SetMaxPlayerShiled(100);
        targetPlayer.GetComponent<PlayerFunctions>().SetPlayerShield(100);
        GameManager.hasStarterArmor = false;
        GameManager.hasPlateArmor = true;
    }

    private void IsShield(GameObject targetPlayer, int shieldDefence)
    {   
        targetPlayer.transform.Find("Model/Armature/Root_M/Spine1_M/Spine2_M/Chest_M/Scapula_L/Shoulder_L/Elbow_L/Wrist_L/jointItemL/Shield_01_Player").gameObject.SetActive(true);
        targetPlayer.GetComponent<PlayerFunctions>().SetPlayerDefence(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerDefence() + shieldDefence);
        GameManager.hasShield = true;
    }

    private void IsSword(GameObject targetPlayer, int swordAttack)
    {   
        targetPlayer.transform.Find("Model/Armature/Root_M/Spine1_M/Spine2_M/Chest_M/Scapula_R/Shoulder_R/Elbow_R/Wrist_R/jointItemR/Sword_01_Player").gameObject.SetActive(true);
        targetPlayer.GetComponent<PlayerFunctions>().SetPlayerAttack(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerAttack() + swordAttack);
        GameManager.hasSword = true;
    }
    
}

