using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPoolAfterTime : MonoBehaviour
{
    [SerializeField] float timeToReturnPool;


    private void OnEnable()
    {
        StartCoroutine(ReturnToPool());
    }

    IEnumerator ReturnToPool()
    {
        float elapsedTime = 0;
        while (elapsedTime < timeToReturnPool)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

}
