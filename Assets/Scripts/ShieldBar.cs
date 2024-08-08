using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ShieldBar : MonoBehaviour
{   
    [SerializeField]
    private Slider shieldBar;
    [SerializeField]
    private GameObject player;
    private int maxShield;
    private float shield;
    // Start is called before the first frame update
    void Start()
    {
        maxShield = player.gameObject.GetComponent<PlayerFunctions>().GetMaxPlayerShield();
    }

    // Update is called once per frame
    void Update()
    {
        shield = player.gameObject.GetComponent<PlayerFunctions>().GetPlayerShield();
        if (shieldBar.value != shield)
        {
            shieldBar.value = shield;
        }
    }
}



