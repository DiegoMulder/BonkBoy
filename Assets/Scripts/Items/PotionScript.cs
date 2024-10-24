using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    PlayerHealth playerHealth;
    public bool testMaxHealth = false;
    public bool testCurrentHealth = false;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    public void UseMaxHealthPotion()
    {
        playerHealth.IncreaseMaxHealth();
    }

    public void UseCurrentHealthPotion()
    {
        playerHealth.IncreaseCurrentHealth();
    }

    private void Update()
    {
        if (testMaxHealth)
        {
            UseMaxHealthPotion();
            testMaxHealth = false;
        }

        if (testCurrentHealth)
        {
            UseCurrentHealthPotion();
            testCurrentHealth = false;
        }
    }
}
