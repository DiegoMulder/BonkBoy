using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] private float baseDamage = 10f;
    [SerializeField] private float velocityDamageMultiplier = 2f;
    [SerializeField] private float minVelocityThreshold = 1f;
    [SerializeField] private float damageCooldown = 1f;

    private Rigidbody rb;

    private Dictionary<GameObject, float> enemyCooldowns = new Dictionary<GameObject, float>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            float currentTime = Time.time;

            if (enemyCooldowns.ContainsKey(enemy) && currentTime - enemyCooldowns[enemy] < damageCooldown)
            {
                return;
            }

            float velocityMagnitude = rb.velocity.magnitude;

            if (velocityMagnitude >= minVelocityThreshold)
            {
                float damage = baseDamage + (velocityMagnitude * velocityDamageMultiplier);

                Enemy enemyHealth = enemy.GetComponent<Enemy>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                    enemyCooldowns[enemy] = currentTime;

                    Debug.Log($"Dealt {damage} damage to {enemy.name}");
                }
            }
        }
    }

    public void DamageAmuletGrab()
    {
        velocityDamageMultiplier *= 1.2f;
    }

    private void Update()
    {
        List<GameObject> enemiesToRemove = new List<GameObject>();
        foreach (var entry in enemyCooldowns)
        {
            if (entry.Key == null) 
            {
                enemiesToRemove.Add(entry.Key);
            }
        }

        foreach (var enemy in enemiesToRemove)
        {
            enemyCooldowns.Remove(enemy);
        }
    }
}
