using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Projectile Stats", menuName = "Projectile/Stats")]
public class ProjectileStats : ScriptableObject
{
    public float speed;
    public float damageHit; //bunu kullanmýyom tam
    public float destroyTime;
    public GameObject impact;

    [Header("Explosion")]
    public bool isExplosive;
    public float damageExplosion;
    public float explosionRange;
 




}
