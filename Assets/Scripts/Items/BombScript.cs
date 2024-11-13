using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] float delay = 3f;
    [SerializeField] float countDown;
    [SerializeField] float blastRadius = 5f;
    [SerializeField] float explosionForce = 500f;
    [SerializeField] bool hasExploded = false;
    [SerializeField] float damage = 100;
    [SerializeField] GameObject particleEffect;

    private bool isCountingDown = false;

    void Start()
    {
        countDown = delay; // Initialize the countdown timer to the delay, but don't start counting down.
    }

    void Update()
    {
        if (isCountingDown)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0f && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    // Call this method to start the countdown
    public void StartCountdown()
    {
        isCountingDown = true;
    }

    // Call this method to stop the countdown
    public void StopCountdown()
    {
        isCountingDown = false;
    }

    private void Explode()
    {
        Instantiate(particleEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObj in colliders)
        {
            Rigidbody rb = nearbyObj.GetComponent<Rigidbody>();
            Enemy enemy = nearbyObj.GetComponent<Enemy>();

            if (rb != null && enemy != null)
            {
                Debug.Log("Enemy hit: " + enemy.gameObject.name);
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
                enemy.TakeDamage(damage);
            }
            else if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }
        }

        Destroy(gameObject);
    }
}
