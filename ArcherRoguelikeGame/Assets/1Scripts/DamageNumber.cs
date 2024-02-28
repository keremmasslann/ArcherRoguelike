using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    float[] numbers = { 0.25f, 0.5f, 0.75f, 1f, -0.25f, -0.5f, -0.75f, -1f };
    [SerializeField] float timeToReturnPool;

    [Header("Scale")]
    [SerializeField] float targetScale;
    [SerializeField] float scaleTime;
    [SerializeField] RectTransform rect;
    Vector3 startScale;

    [Header("Color")]
    [SerializeField] float targetAlpha;
    [SerializeField] float colorTime;
    [SerializeField] TMP_Text text;
    Color startColor;



    private void Awake()
    {
        startColor = text.color;
        startScale = rect.transform.localScale;
    }

    private void OnEnable()
    {
        
        int randomNumber = Random.Range(0, numbers.Length);
        transform.DOMove(transform.position + new Vector3(numbers[randomNumber], 1f, 0f).ToIso(), 1); //30fps test et ya da chatggpt sor
        transform.DOScale(targetScale, scaleTime); //scale deðil de tmp_text font size deðiþtirsek daha iyi olabilir
        text.DOFade(targetAlpha, colorTime);  
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
