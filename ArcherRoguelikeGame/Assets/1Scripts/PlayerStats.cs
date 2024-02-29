using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStats : HealthStats,IDamageable
{
  //  [SerializeField] float maxHealth;
  //  float currentHealth;
//    [SerializeField] HealthBar healthBar;
    [SerializeField] TMP_Text healthText;
    [SerializeField] Canvas canvasW;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetupCanvas(canvasW);
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }


    public override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void SetupCanvas(Canvas canvas)
    {
        worldCanvas = canvas;

    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthText.text = currentHealth + " / " + maxHealth;
    }


    public override void SpawnDamageNumber(float dmg)
    {
        base.SpawnDamageNumber(dmg);
    }

}
