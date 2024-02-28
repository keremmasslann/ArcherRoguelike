using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Projectile Stats", menuName = "Projectile/Stats")]
public class ProjectileStats : ScriptableObject
{
    public float speed;
    public float damageHit; //bunu kullanmýyom tam
    public float damageExplosion;
    public float destroyTime;
    public bool isExplosive;
    public GameObject impact;
  



}
