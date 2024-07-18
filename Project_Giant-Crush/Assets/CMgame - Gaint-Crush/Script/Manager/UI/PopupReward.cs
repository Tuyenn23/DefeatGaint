using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupReward : AnimTopDown
{
    [SerializeField] private TMP_Text coin_txt;
    [SerializeField] private TMP_Text coinX5_txt;


    [SerializeField] private Button btn_Claim;
    [SerializeField] private Button btn_Reward;

    [SerializeField] private CanvasGroup Fade_img;
    [SerializeField] private List<Image> L_starsImg;


    private void OnEnable()
    {
        AdsHandle.instance.ResetAds();

        btn_Claim.onClick.AddListener(OnClaim);
        btn_Reward.onClick.AddListener(OnClaimReward);
        Fade_img.gameObject.SetActive(false);
        Fade_img.alpha = 0f;
        InitScaleStar();

        StartCoroutine(IE_DelayActiveStar());

        InitReward();
        InitRewardX5();

        SoundManager.Instance.PlayFxSound(SoundManager.Instance.Win);

    }

    private void InitReward()
    {
        coin_txt.text = PrefabStorage.Instance.CurrentEnemy._reWard.ToString();
    }

    private void InitRewardX5()
    {
        coinX5_txt.text = (PrefabStorage.Instance.CurrentEnemy._reWard * 5).ToString();
    }

    private void InitScaleStar()
    {
        for (int i = 0; i < L_starsImg.Count; i++)
        {
            L_starsImg[i].transform.localScale = Vector3.one * 0.2f;
            L_starsImg[i].gameObject.SetActive(false);
        }
    }

    IEnumerator IE_DelayActiveStar()
    {
        yield return new WaitForSeconds(0.75f);
        animFadeImg();

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < L_starsImg.Count;)
        {
            L_starsImg[i].gameObject.SetActive(true);
            AnimStars(L_starsImg[i]);
            yield return new WaitForSeconds(0.75f);
            i++;
        }
    }

    private void OnClaim()
    {
        AdManager.instance.ShowInter(null, null, "ShowInter");

        GiveReward();

        GameManager.instance.ChangeSateGame(E_GameState.Home);
        gameObject.SetActive(false);
    }

    private void GiveReward()
    {
        int newCoin = PlayerDataManager.GetCoin() + PrefabStorage.Instance.CurrentEnemy._reWard;

        PlayerDataManager.SetCoin(newCoin);
    }

    private void OnClaimReward()
    {
        AdManager.instance.ShowReward(delegate
        {
            GameManager.instance.UiController.panelCoin.gameObject.SetActive(true);
            GameManager.instance.ChangeSateGame(E_GameState.Home);
            gameObject.SetActive(false);
        }, delegate
        {
            GameManager.instance.UiController._popupNoInternet.gameObject.SetActive(true);
        }, "Show Reward");
    }


    private void animFadeImg()
    {
        Fade_img.gameObject.SetActive(true);
        Fade_img.DOFade(1f, 5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void AnimStars(Image Current_img)
    {
        Current_img.transform.localScale = Vector3.zero;
        Current_img.transform.DOScale(1f, 1f).OnComplete(() =>
        {
            SoundManager.Instance.PlayFxSound(SoundManager.Instance.Win);
        }).SetEase(Ease.OutElastic).SetAutoKill();
    }

    private void OnDisable()
    {
        btn_Claim.onClick.RemoveListener(OnClaim);
        PrefabStorage.Instance.CurrentEnemy.TriggerGetUp();
    }
}
