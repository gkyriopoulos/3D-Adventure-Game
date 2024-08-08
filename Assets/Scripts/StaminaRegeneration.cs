using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaRegeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float staminaPerTick = 1/6;

    [SerializeField]
    private float regenInterval = 1;

    private int maxStamina; 

    void Start()
    {
        maxStamina = player.gameObject.GetComponent<PlayerFunctions>().GetMaxPlayerStamina();
        StartCoroutine(RegenerateStamina());
    }

    // Update is called once per frame
    void Update()
    {   
        //print("Player Stamina:" + player.gameObject.GetComponent<PlayerFunctions>().GetPlayerStamina());
    }

    IEnumerator RegenerateStamina()
    {
       while(true){
        float currentStamina = player.gameObject.GetComponent<PlayerFunctions>().GetPlayerStamina(); 

        if (currentStamina < 0){
            player.gameObject.GetComponent<PlayerFunctions>().SetPlayerStamina(0);  
        }
        
        if ( currentStamina < maxStamina){
            player.gameObject.GetComponent<PlayerFunctions>().SetPlayerStamina(currentStamina + staminaPerTick);
            currentStamina = player.gameObject.GetComponent<PlayerFunctions>().GetPlayerStamina(); 
            if (currentStamina > maxStamina){
                player.gameObject.GetComponent<PlayerFunctions>().SetPlayerStamina(maxStamina);
            }
        }
        yield return new WaitForSeconds(regenInterval);
       }
    }
}
