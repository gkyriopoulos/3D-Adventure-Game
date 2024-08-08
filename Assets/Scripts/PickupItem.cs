using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour

{
    // Start is called before the first frame update
    private int healthHeal = 40;
    private int staminaHeal = 40;
    private int shieldHeal = 30;
    
    [SerializeField] 
    private GameObject instruction;

    [SerializeField] 
    private GameObject item;
    private bool isPlayerInside = false;
    void Start()
    {
    }

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
                
                if(item.name == "Bottle_01")
                {   
                    IsHealthPot(player, healthHeal);
                }
                else if(item.name == "Bottle_02")
                {
                    IsStaminaPot(player, staminaHeal);
                }
                else if(item.name == "Bottle_03")
                {
                    IsShieldPot(player, shieldHeal);
                }else if((item.name == "Bread_Model")){
                    IsBread();
                }else if ((item.name == "Wineglass_Model")){

                    IsWineglass();
                }

            }
        }
    }

    private void IsHealthPot(GameObject targetPlayer, float healAmmount)    
    {
        if(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerHealth() + healAmmount <= targetPlayer.GetComponent<PlayerFunctions>().GetMaxPlayerHealth())
        {   
            targetPlayer.GetComponent<PlayerFunctions>().SetPlayerHealth(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerHealth() + healAmmount);
        }
        else{
            healAmmount = targetPlayer.GetComponent<PlayerFunctions>().GetMaxPlayerHealth() - targetPlayer.GetComponent<PlayerFunctions>().GetPlayerHealth();
            targetPlayer.GetComponent<PlayerFunctions>().SetPlayerHealth(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerHealth() + healAmmount);
        }
    }

    private void IsStaminaPot(GameObject targetPlayer, float healAmmount){
        if(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerStamina() + healAmmount <= targetPlayer.GetComponent<PlayerFunctions>().GetMaxPlayerStamina())
        {   
            targetPlayer.GetComponent<PlayerFunctions>().SetPlayerStamina(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerStamina() + healAmmount);
        }
        else{
            healAmmount = targetPlayer.GetComponent<PlayerFunctions>().GetMaxPlayerStamina() - targetPlayer.GetComponent<PlayerFunctions>().GetPlayerStamina();
            targetPlayer.GetComponent<PlayerFunctions>().SetPlayerStamina(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerStamina() + healAmmount);
        }
    }

    private void IsShieldPot(GameObject targetPlayer, float healAmmount){
        if(GameManager.hasPlateArmor || GameManager.hasStarterArmor){
            if(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerShield() + healAmmount <= targetPlayer.GetComponent<PlayerFunctions>().GetMaxPlayerShield()){   
                targetPlayer.GetComponent<PlayerFunctions>().SetPlayerShield(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerShield() + healAmmount);
            }
            else{
                healAmmount = targetPlayer.GetComponent<PlayerFunctions>().GetMaxPlayerShield() - targetPlayer.GetComponent<PlayerFunctions>().GetPlayerShield();
                targetPlayer.GetComponent<PlayerFunctions>().SetPlayerShield(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerShield() + healAmmount);
            }
        }else{
            targetPlayer.GetComponent<PlayerFunctions>().SetPlayerHealth(targetPlayer.GetComponent<PlayerFunctions>().GetPlayerHealth() - 10);
        }
    }

    private void IsWineglass(){
        GameManager.hasWineglass = true;
    }

    private void IsBread(){
        GameManager.hasBread = true;
    }
}
