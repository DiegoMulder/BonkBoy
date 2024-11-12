using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAmulet : MonoBehaviour
{
    PlayerSword playerSword;
    public bool testDamage = false;
    void Start()
    {
        playerSword = GameObject.FindGameObjectWithTag("Sword").GetComponent<PlayerSword>();
    }
    public void DamageAmuletEquipped()
    {
        playerSword.DamageAmuletGrab();
        Destroy(gameObject);
    }
    void Update()
    {
        if (testDamage)
        {
            DamageAmuletEquipped();
            testDamage = false;
        }
    }
}
