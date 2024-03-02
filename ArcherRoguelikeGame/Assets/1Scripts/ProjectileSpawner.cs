using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] float spawnEveryXSeconds;
    [SerializeField] GameObject projectileObject;
    [SerializeField] Transform spawnPos;
    void Start()
    {
        InvokeRepeating("SpawnProjectile", 0,spawnEveryXSeconds);
    }

   

    void SpawnProjectile()
    {
       // GameObject projectile = Instantiate(projectileObject, spawnPos.position, Quaternion.identity);
        Vector3 direction = transform.right.ToIso();
        //   GameObject pr = Instantiate(projectile, shootingPos.position, Quaternion.LookRotation(direction));
        GameObject pr = ObjectPoolManager.SpawnObject(projectileObject, spawnPos.position, Quaternion.LookRotation(direction), ObjectPoolManager.PoolType.Gameobject);
        pr.transform.rotation = Quaternion.LookRotation(direction);
        pr.GetComponent<Projectile>().SetDamageMultiplier(1);
    }
}
