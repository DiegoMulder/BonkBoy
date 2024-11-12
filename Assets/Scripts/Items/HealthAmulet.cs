using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAmulet : MonoBehaviour
{
    PlayerHealth playerHealth;
    public bool testMaxHealth = false;
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    public void HealthAmuletEquipped()
    {
        playerHealth.HealthAmuletGrab();
        Destroy(gameObject);
    }
    void Update()
    {
        if (testMaxHealth)
        {
            HealthAmuletEquipped();
            testMaxHealth = false;
        }
    }

}
