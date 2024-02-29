using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBarFill;
    [SerializeField] float reduceSpeed;
    [SerializeField] Image fillDelay;
    [SerializeField] float fillDelaySpeed;
    [SerializeField] float fillDelayTime;
    //    float target;
    //  Canvas worldCanvas;
   
    public void UpdateHealthBar(float currentH, float maxH)
    {
        //  healthBarFill.fillAmount = currentH / maxH;
        //    target = currentH / maxH;
        //     StartCoroutine(HealthBarSmooth(currentH, maxH));
        healthBarFill.DOFillAmount(currentH / maxH, reduceSpeed).SetSpeedBased(true);
        if(fillDelay != null)
        {
            DOTween.Sequence()
            .AppendInterval(fillDelayTime) // Delay of 1 second
            .Append(fillDelay.DOFillAmount(currentH / maxH, fillDelaySpeed).SetSpeedBased(true));
        }




    }

    /*  IEnumerator HealthBarSmooth(float ch, float mh)
       {
           healthBarFill.fillAmount = Mathf.MoveTowards(healthBarFill.fillAmount, ch/mh, reduceSpeed * Time.deltaTime);
       } */
}
