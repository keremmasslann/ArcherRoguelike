using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth;
    float currentHealth;
    [SerializeField] HealthBar healthBar;
    Canvas worldCanvas;
    [SerializeField] Collider mainCollider;
    void Start()
    {
        currentHealth = maxHealth;
    }


    void Update()
    {
        
    }

    public void Die()
    {
        Destroy(healthBar.gameObject); //pool ile deactivate edilcek
        Destroy(gameObject); //pool ile deactivate edilcek
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth,maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetupCanvas(Canvas canvas)
    {
        worldCanvas = canvas;
        healthBar.transform.SetParent(worldCanvas.transform);
    }

    public float GetSpawnHeight()
    {
        return mainCollider.bounds.size.y / 2;
    }
}
