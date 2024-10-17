using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth > 0) HealthBehaviour();
    }

    //Player health functie
    private void HealthBehaviour()
	{
        if (Identity.didDamage) DamageBehaviour();

        if(playerHealth <= 0)
		{
            print("You Died!");
            playerHealth = 0;
		}
    }

    //Damage functie
    private void DamageBehaviour()
	{
        playerHealth -= Identity.static_Damage;
        Identity.didDamage = false;
    }
}
