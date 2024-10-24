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
    void Start()
    {
        countDown = delay;
    }
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
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
