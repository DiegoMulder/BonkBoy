using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth = 100;
    public float maxHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth >= maxHealth)  currentHealth = maxHealth;
        if(currentHealth > 0) HealthBehaviour();
    }

    public void HealthAmuletGrab()
    {
        maxHealth *= 1.5f;
    }

    public void IncreaseMaxHealth()
    {
        maxHealth *= 1.05f;
    }

    public void IncreaseCurrentHealth()
    {
        currentHealth = maxHealth / 100 * 25 + currentHealth;
    }

    //Player health functie
    private void HealthBehaviour()
	{
        if (Identity.didDamage) DamageBehaviour();

        if(currentHealth <= 0)
		{
            print("You Died!");
            currentHealth = 0;
		}
    }

    //Damage functie
    private void DamageBehaviour()
	{
        currentHealth -= Identity.static_Damage;
        Identity.didDamage = false;
    }
}
