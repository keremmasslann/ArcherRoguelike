using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile
{

  //  Rigidbody rb;
    float damage;
    [SerializeField] float minimumDamage;

   
   /* private void OnEnable()
    {
        MoveProjectile();
    } */

    public override void MoveProjectile()
    {
        rb.velocity = transform.forward * stats.speed;
    }

    public override void SetDamageMultiplier(float multiplier)
    {
        damage = stats.damageHit * multiplier;
        if (damage < minimumDamage)
        {
            damage = minimumDamage;
        }
    }

    public override void DestroyProjectile()
    {
        // Destroy(this.gameObject);
        base.DestroyProjectile();
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageableObject = collision.gameObject.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
           // Debug.Log("damage:" + damage);
           
        }

        HitEffect(collision);  
        DestroyProjectile();
    }

    public override void HitEffect(Collision col)
    {
        base.HitEffect(col);
    }

    public override void GetKicked(Vector3 dir)
    {
        base.GetKicked(dir);
        rb.velocity = dir * stats.speed;
    }
}
