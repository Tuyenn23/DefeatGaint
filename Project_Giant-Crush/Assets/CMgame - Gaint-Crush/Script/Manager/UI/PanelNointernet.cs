using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelNointernet : MonoBehaviour
{
    private void OnEnable()
    {
        AnimPopup();
    }

    private void AnimPopup()
    {
        transform.localScale = Vector3.one * 0f;

        transform.DOScale(1f, 0.6f).SetEase(Ease.InOutElastic).OnComplete(()=>
        {
            StartCoroutine(IE_DelayDeActive());
        });
    }

    IEnumerator IE_DelayDeActive()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }


    private void OnDisable()
    {
        StopCoroutine(IE_DelayDeActive());
    }
}
