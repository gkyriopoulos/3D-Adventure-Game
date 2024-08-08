using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{   
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private GameObject player;
    private int maxHealth;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = player.gameObject.GetComponent<PlayerFunctions>().GetMaxPlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {   
        health = player.gameObject.GetComponent<PlayerFunctions>().GetPlayerHealth();
        if (healthBar.value != health)
        {
            healthBar.value = health;
        }
    }
}
