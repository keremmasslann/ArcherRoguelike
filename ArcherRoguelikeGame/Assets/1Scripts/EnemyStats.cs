using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth;
    float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }


    void Update()
    {
        
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Damage taken: " + damage + "Health left: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
