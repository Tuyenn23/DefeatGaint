using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tut_2 : MonoBehaviour
{
    [SerializeField] private CanvasGroup PanelHeal;
    [SerializeField] private RectTransform PanelKO;
    [SerializeField] private Button btn_close;

    Tweener T_PanelKO, T_PanenHeal;

    private void OnEnable()
    {
        AnimPanelKO();
        AnimPanelHeal();

        btn_close.onClick.AddListener(Onclose);

        GameManager.instance.WeaponController.A_ChangeHome?.Invoke();
    }

    private void AnimPanelKO()
    {
        PanelKO.anchoredPosition = new Vector2(800, -100);

        T_PanelKO = PanelKO.DOAnchorPosX(0, 0.3f).OnComplete(() =>
        {
            StartCoroutine(WaitsFade());
        });
    }

    private void AnimPanelHeal()
    {
        T_PanenHeal = PanelHeal.DOFade(0.7f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void FadePanel()
    {
        PanelKO.GetComponent<CanvasGroup>().DOFade(0.7f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    IEnumerator WaitsFade()
    {
        yield return new WaitForSeconds(2f);
        FadePanel();
    }

    private void Onclose()
    {
        PrefabStorage.Instance.CurrentEnemy.transform.position = Vector3.zero;
        PrefabStorage.Instance.CurrentEnemy.TriggerGetUp();
        PlayerDataManager.setCompletedTut(true);
        GameManager.instance.ChangeSateGame(E_GameState.Home);
    }

    private void OnDisable()
    {
        btn_close.onClick.RemoveListener(Onclose);
        StopCoroutine(WaitsFade());
        T_PanelKO?.Kill();
        T_PanenHeal?.Kill();
    }
}
