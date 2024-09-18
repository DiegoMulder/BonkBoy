using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identity : MonoBehaviour
{
    public int ID;

    public float damageInterval = 0.5f;
    [SerializeField] private float damageTimer;
    public float damage;

    private bool triggerCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        triggerCheck = false;
        damageTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ID == 0) RoombaSawBehaviour();
    }

    private void RoombaSawBehaviour()
	{
		if (triggerCheck)
		{
            damageTimer += Time.deltaTime;
            if(damageTimer >= damageInterval)
			{
                Debug.Log("Damage: " + damage);
                damageTimer = 0;
			}
		}
        else damageTimer = 0;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player")) triggerCheck = true;
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.CompareTag("Player")) triggerCheck = false;
    }
}
