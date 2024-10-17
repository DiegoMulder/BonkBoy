using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identity : MonoBehaviour
{
    [Header("Enemy ID")]
    public int ID;
    public static int ID_Static;

    [Header("Damage")]
    public float damageInterval = 0.5f;
    private float damageTimer;
    public int damage;
    public static int static_Damage;

    private bool triggerCheck = false;
    public static bool didDamage;

    // Start is called before the first frame update
    void Start()
    {
        triggerCheck = false;
        damageTimer = 0;
        static_Damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        ID_Static = ID;

        if (ID == 0) RoombaSawBehaviour();
    }

    //Damage behaviour
    private void RoombaSawBehaviour()
	{
		if (triggerCheck)
		{
            damageTimer += Time.deltaTime;
            if(damageTimer >= damageInterval)
			{
                didDamage = true;
                damageTimer = 0;
			}
		}
        else damageTimer = 0;
	}

    //Check als hij de speler raakt
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player")) triggerCheck = true;
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.CompareTag("Player")) triggerCheck = false;
    }
}
