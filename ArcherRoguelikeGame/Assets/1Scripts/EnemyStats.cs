using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyStats: HealthStats, IDamageable, ISwappable //Enemy superclass //artýk deðil?
{
   // [SerializeField] float maxHealth;
   // float currentHealth;
 //   [SerializeField] HealthBar healthBar;
 //   Canvas worldCanvas;
    [SerializeField] Collider mainCollider;
    //  [Header("Damage Numbers")]
    // [SerializeField] GameObject dmgNumberOjbect;
    // [SerializeField] float dmgNumbersOffsetY;
    public float tpPosY;
    public float teleportPosY { get { return tpPosY; } }
    protected override void Start()
    {
        base.Start();
    }


    public override void Die()
    {
        Destroy(healthBar.gameObject); //pool ile deactivate edilcek
        Destroy(gameObject); //pool ile deactivate edilcek
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void SetupCanvas(Canvas canvas)
    {
        base.SetupCanvas(canvas);
        healthBar.transform.SetParent(worldCanvas.transform);
        healthBar.transform.SetSiblingIndex(0);
                    
    }

    public float GetSpawnHeight()
    {
        return mainCollider.bounds.size.y / 2;
    }

    public override void SpawnDamageNumber(float dmg)
    {
        base.SpawnDamageNumber(dmg);
    }
}
