using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    //  float maxHealth { get; set; } //???
    //  float currentHealth { get; set; }
    bool immuneToExplosion { get; set; }
    void TakeDamage(float damage);

    void Die();

  
}
