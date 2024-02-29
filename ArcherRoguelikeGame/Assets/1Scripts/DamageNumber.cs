using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamageNumber : MonoBehaviour
{

    [SerializeField] float timeToReturnPool;
    [SerializeField] float waitTimeBeforeEffects;

    [Header("Move")]
    [SerializeField] bool activeMove = true;
    [SerializeField] float[] numbers = { 0.25f, 0.5f, 0.75f, 1f, -0.25f, -0.5f, -0.75f, -1f };
    [SerializeField] float moveSpeed;

    [Header("Scale")]
    [SerializeField] bool activeScale = true;
    [SerializeField] float targetScale;
    [SerializeField] float scaleTime;
    [SerializeField] RectTransform rect;
    Vector3 startScale;

    [Header("Color")]
    [SerializeField] bool activeColor = true;
    [SerializeField] float targetAlpha;
    [SerializeField] float colorTime;
    [SerializeField] TMP_Text text;
    Color startColor;
    [SerializeField] float fadeDelay;




    private void Awake()
    {
        startColor = text.color;
        startScale = rect.transform.localScale;
    }

    private void OnEnable()
    {
        StartCoroutine(DelayedEffects());

        /*    DOTween.Sequence()
                .AppendInterval(waitTimeBeforeEffects)
                .Append(transform.DOMove(transform.position + new Vector3(numbers[randomNumber], 1f, 0f).ToIso(), 1))
                .Append(transform.DOScale(targetScale, scaleTime))
                .Append(text.DOFade(targetAlpha, colorTime));
                 StartCoroutine(ReturnToPoolandResetEffects()); */




        /*    transform.DOMove(transform.position + new Vector3(numbers[randomNumber], 1f, 0f).ToIso(), 1); //EN SON BUYDU
            transform.DOScale(targetScale, scaleTime); //scale deðil de tmp_text font size deðiþtirsek daha iyi olabilir
            text.DOFade(targetAlpha, colorTime);  
            StartCoroutine(ReturnToPoolandResetEffects()); */
    }

    IEnumerator DelayedEffects()
    {

        yield return new WaitForSeconds(waitTimeBeforeEffects);
        if (activeMove)
        {
            int randomNumber = Random.Range(0, numbers.Length);
            transform.DOMove(transform.position + new Vector3(numbers[randomNumber], 1f, 0f).ToIso(), moveSpeed).SetSpeedBased(true);
        }

        if (activeScale)
        {
            transform.DOScale(targetScale, scaleTime); //scale deðil de tmp_text font size deðiþtirsek daha iyi olabilir
        }

        yield return new WaitForSeconds(fadeDelay); // Additional delay before fading

        if (activeColor)
        {
            text.DOFade(targetAlpha, colorTime);
        }
    

        StartCoroutine(ReturnToPoolandResetEffects());
    }
    IEnumerator ReturnToPoolandResetEffects()
    {
        float elapsedTime = 0;
        while (elapsedTime < timeToReturnPool)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.DOKill();
        text.DOKill();
        rect.localScale = startScale;
        text.color = startColor;

        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
