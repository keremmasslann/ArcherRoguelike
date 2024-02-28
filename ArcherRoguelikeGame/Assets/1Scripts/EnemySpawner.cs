using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] GameObject enemyObject;
    [SerializeField] Canvas canvasWorld;
//    [SerializeField] Transform player;
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            SpawnEnemyStart(i+1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnEnemy();
        }

    }

    void SpawnEnemy()
    {
        int x = Random.Range(0, positions.Length);
       // GameObject en = Instantiate(enemy, positions[x].position + new Vector3(0, .75f, 0), Quaternion.identity);
        GameObject en = Instantiate(enemyObject, positions[x].position , Quaternion.identity);
        //     Collider col = en.GetComponent<Collider>();
        //     float enemyY = col.bounds.size.y/2;
        Enemy enemy = en.GetComponent<Enemy>();
        en.transform.position += new Vector3(0, enemy.GetSpawnHeight(), 0);
        enemy.SetupCanvas(canvasWorld);
        //     Debug.Log(enemyY);
        //  en.GetComponent<Enemy>().SetupHealthbar(canvas, cam);

        // en.GetComponent<EnemyAI>().SetPlayer(player);
    }

    void SpawnEnemyStart(int x)
    {
   
        // GameObject en = Instantiate(enemy, positions[x].position + new Vector3(0, .75f, 0), Quaternion.identity);
        GameObject en = Instantiate(enemyObject, new Vector3(3*x,0,3*x), Quaternion.identity);
        //     Collider col = en.GetComponent<Collider>();
        //     float enemyY = col.bounds.size.y/2;
        Enemy enemy = en.GetComponent<Enemy>();
        en.transform.position += new Vector3(0, enemy.GetSpawnHeight(), 0);
        enemy.SetupCanvas(canvasWorld);
        //     Debug.Log(enemyY);
        //  en.GetComponent<Enemy>().SetupHealthbar(canvas, cam);

        // en.GetComponent<EnemyAI>().SetPlayer(player);
    }
}
