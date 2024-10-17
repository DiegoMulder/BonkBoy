using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identity : MonoBehaviour
{
    [Header("Enemy ID")]
    public int ID;
    public static int ID_Static;

    [Header("Weapon physics")]
    public Rigidbody rb;
    private Vector3 lastPosition;
    private Vector3 currentVelocity;
    private bool hittedPlayer = false;

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
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ID_Static = ID;
        currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        if (ID == 0) RoombaSawBehaviour();
        if (ID == 1 && !Enemy.hasDied) SkeletonEnemy();
    }

    private void RoombaSawBehaviour()
    {
        if (triggerCheck)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                didDamage = true;
                damageTimer = 0;
            }
        }
        else damageTimer = 0;
    }

    private void SkeletonEnemy()
    {
        if (currentVelocity.magnitude >= 5f && triggerCheck && !hittedPlayer)
        {
            didDamage = true;
            hittedPlayer = true;
        }

        if (!triggerCheck && hittedPlayer)
            hittedPlayer = false;
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
