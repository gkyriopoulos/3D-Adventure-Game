using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegeneration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float healthPerTick = 1/6;

    [SerializeField]
    private float regenInterval = 1;

    private int maxHealth; 


    void Start()
    {
        maxHealth = player.gameObject.GetComponent<PlayerFunctions>().GetMaxPlayerHealth();
        StartCoroutine(RegenerateHealth());
    }

    // Update is called once per frame
    void Update()
    {   
        //print("Player Health:" + player.gameObject.GetComponent<PlayerFunctions>().GetPlayerHealth());
    }

    IEnumerator RegenerateHealth()
    {
       while(true){
        float currentHealth = player.gameObject.GetComponent<PlayerFunctions>().GetPlayerHealth(); 

        if (currentHealth < 0){
            player.gameObject.GetComponent<PlayerFunctions>().SetPlayerHealth(0);  
        }
        
        if ( currentHealth < maxHealth){
            player.gameObject.GetComponent<PlayerFunctions>().SetPlayerHealth(currentHealth + healthPerTick);
            currentHealth = player.gameObject.GetComponent<PlayerFunctions>().GetPlayerHealth(); 
            if (currentHealth > maxHealth){
                player.gameObject.GetComponent<PlayerFunctions>().SetPlayerHealth(maxHealth);
            }
        }
        yield return new WaitForSeconds(regenInterval);
       }
    }
}
