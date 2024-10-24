using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;
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

    public void IncreaseMaxHealth()
    {
        maxHealth += 20;
    }

    public void IncreaseCurrentHealth()
    {
        currentHealth += 50;
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
