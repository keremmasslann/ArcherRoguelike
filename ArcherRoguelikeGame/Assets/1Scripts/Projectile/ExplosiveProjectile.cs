using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    float damage;

    private void Awake()
    {
        SetDamageMultiplier(1);
    }
    public override void DestroyProjectile()
    {
        base.DestroyProjectile();
    }

    public override void HitEffect(Collision col)
    {
        // base.HitEffect(col);
        ObjectPoolManager.SpawnObject(stats.impact, col.contacts[0].point, Quaternion.identity);
    }

    public override void MoveProjectile()
    {
        rb.velocity = transform.forward * stats.speed;
    }

    public override void SetDamageMultiplier(float multiplier)
    {
        damage = stats.damageHit * multiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageableObject = collision.gameObject.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
            damageableObject.immuneToExplosion = true;

        }

        HitEffect(collision);
        Explosion();
        DestroyProjectile();
    }

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stats.explosionRange, ~LayerMask.GetMask("Player"));

        foreach (Collider col in colliders)
        {

            IDamageable damageable = col.GetComponent<IDamageable>();

            if (damageable != null)
            {
                if (!damageable.immuneToExplosion)
                {
                    damageable.TakeDamage(stats.damageExplosion);
                }
                else
                {
                    damageable.immuneToExplosion = false;
                }

            }
           
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.explosionRange);
    }

}
