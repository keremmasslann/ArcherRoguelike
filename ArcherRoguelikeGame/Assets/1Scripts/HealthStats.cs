using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class HealthStats : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth;
    protected float currentHealth;
    [SerializeField] protected HealthBar healthBar;
    protected Canvas worldCanvas;
    [Header("Damage Numbers")]
    [SerializeField] GameObject dmgNumberOjbect;
    [SerializeField] float dmgNumbersOffsetX;
    [SerializeField] float dmgNumbersOffsetY;

    public bool immuneToExplosion { get ; set; }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

 
    public abstract void Die();

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        //damage popup

        SpawnDamageNumber(damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void SetupCanvas(Canvas canvas)
    {
        worldCanvas = canvas;
    }

    public virtual void SpawnDamageNumber(float dmg)
    {     
        GameObject dmgNumber = ObjectPoolManager.SpawnObject(dmgNumberOjbect, transform.position + new Vector3(dmgNumbersOffsetX, dmgNumbersOffsetY, 0).ToIso(), Quaternion.identity);
        // dmgNumber.GetComponentInChildren<TMP_Text>().text = dmg.ToString("F0");
        DamageNumber damageNumber = dmgNumber.GetComponent<DamageNumber>();
        damageNumber.text.text = dmg.ToString("F0");
        damageNumber.SetCurrentDamage(dmg);
        dmgNumber.transform.SetParent(worldCanvas.transform);
    }
}
