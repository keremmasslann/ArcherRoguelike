using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public ProjectileStats stats;


    public abstract void MoveProjectile();
    public abstract void SetDamageMultiplier(float multiplier);
    public abstract void DestroyProjectile();
   //public abstract void HitEffect(Collision collision);



}
