using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, IKickable
{
    public ProjectileStats stats;
    [SerializeField] protected Rigidbody rb;

    private void OnEnable()
    {
        MoveProjectile();
    }
    private void OnDisable()
    {
        Debug.Log("disabled");
    }

    public abstract void MoveProjectile();
    public abstract void SetDamageMultiplier(float multiplier);
    public virtual void DestroyProjectile()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    public virtual void HitEffect(Collision col)
    {
        ContactPoint contact = col.contacts[0];
        Vector3 contactPoint = contact.point;
        Vector3 contactNormal = contact.normal;

        // Calculate the rotation to align with the surface normal
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contactNormal);

        // Instantiate the impact effect at the contact point with the calculated rotation
        //    GameObject impactObject = Instantiate(stats.impact, contactPoint, rotation);
        ObjectPoolManager.SpawnObject(stats.impact, contactPoint, rotation, ObjectPoolManager.PoolType.Gameobject);
    }

    public virtual void GetKicked(Vector3 dir)
    {
        Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.LookRotation(dir);
        SetDamageMultiplier(2);
   //     gameObject.layer = LayerMask.NameToLayer(stats.playerProjectileLayer); //calýsýyordu bu aslýnda
       // int newLayerIndex = stats.playerProjectileLayer; //onenableda daha performant olabilir 
    //    gameObject.layer = LayerMask.NameToLayer(newLayerName);
        //LAYERI PLAYER PROJ YAP
        //  transform.forward *= -1;
        //  rb.velocity *= -1;
    }
}
