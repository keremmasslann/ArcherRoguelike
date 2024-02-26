using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public ProjectileStats stats;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void MoveProjectile();
    public abstract void SetDamageMultiplier(float multiplier);
    public abstract void DestroyProjectile();



}
