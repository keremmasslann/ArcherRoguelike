using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBarFill;
  //  Canvas worldCanvas;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
    //    healthBarFill.fillAmount = Mathf.MoveTowards(healthBarFill.fillAmount, target, reduceSpeed * Time.deltaTime);
    }

    public void UpdateHealthBar(float currentH,float maxH)
    {
        healthBarFill.fillAmount = currentH / maxH;
    }

   
}
