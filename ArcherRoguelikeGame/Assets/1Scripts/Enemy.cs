using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy: MonoBehaviour, IDamageable //Enemy superclass
{
    [SerializeField] float maxHealth;
    float currentHealth;
    [SerializeField] HealthBar healthBar;
    Canvas worldCanvas;
    [SerializeField] Collider mainCollider;
    [Header("Damage Numbers")]
    [SerializeField] GameObject dmgNumberOjbect;
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void Die()
    {
        Destroy(healthBar.gameObject); //pool ile deactivate edilcek
        Destroy(gameObject); //pool ile deactivate edilcek
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth,maxHealth);
        //damage popup

        ShowDamageNumber(damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetupCanvas(Canvas canvas)
    {
        worldCanvas = canvas;
        healthBar.transform.SetParent(worldCanvas.transform);
    }

    public float GetSpawnHeight()
    {
        return mainCollider.bounds.size.y / 2;
    }

    public void ShowDamageNumber(float dmg)
    {
        float x = Random.Range(0, 1.5f);
        float y = Random.Range(2.5f, 3.5f);
   //     GameObject dmgNumber = Instantiate(dmgNumberOjbect, transform.position + new Vector3(x, y, 0), Quaternion.identity);
        GameObject dmgNumber = ObjectPoolManager.SpawnObject(dmgNumberOjbect, transform.position + new Vector3(x, y, 0), Quaternion.identity);
        dmgNumber.GetComponentInChildren<TMP_Text>().text = dmg.ToString("F0");
        dmgNumber.transform.SetParent(worldCanvas.transform);
    //    Destroy(dmgNumber, 1.5f);
    }
}
