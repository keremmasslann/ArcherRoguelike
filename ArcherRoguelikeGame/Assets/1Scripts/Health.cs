using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour,IDamageable
{
    //[SerializeField] float maxHP;
    //protected float currentHP;

   [field:SerializeField]public float maxHealth { get; set; }
    public float currentHealth { get; set; }
  //  float IDamageable.currentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
