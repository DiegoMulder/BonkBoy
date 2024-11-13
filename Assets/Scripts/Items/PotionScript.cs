using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PotionScript : MonoBehaviour
{
    PlayerHealth playerHealth;
    private bool testMaxHealth = false;
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
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            Debug.Log("Right Trigger is pressed!");
        }

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
