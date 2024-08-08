using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{   
    [SerializeField]
    private Slider staminaBar;
    [SerializeField]
    private GameObject player;
    private int maxStamina;
    private float stamina;
    // Start is called before the first frame update
    void Start()
    {
        maxStamina = player.gameObject.GetComponent<PlayerFunctions>().GetMaxPlayerStamina();
    }

    // Update is called once per frame
    void Update()
    {
        stamina = player.gameObject.GetComponent<PlayerFunctions>().GetPlayerStamina();
        if (staminaBar.value != stamina)
        {
            staminaBar.value = stamina;
        }
    }
}



