using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile
{

    Rigidbody rb;
    float damage;
    [SerializeField] float minimumDamage;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
 
    }

    private void OnEnable()
    {
        MoveProjectile();
    }

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
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageableObject = collision.gameObject.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
           // Debug.Log("damage:" + damage);
           
        }
        ContactPoint contact = collision.contacts[0];
        Vector3 contactPoint = contact.point;
        Vector3 contactNormal = contact.normal;

        // Calculate the rotation to align with the surface normal
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contactNormal);

        // Instantiate the impact effect at the contact point with the calculated rotation
    //    GameObject impactObject = Instantiate(stats.impact, contactPoint, rotation);
        ObjectPoolManager.SpawnObject(stats.impact, contactPoint, rotation,ObjectPoolManager.PoolType.Gameobject);

      //  Destroy(impactObject, 2f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        DestroyProjectile();
    }
   
}
