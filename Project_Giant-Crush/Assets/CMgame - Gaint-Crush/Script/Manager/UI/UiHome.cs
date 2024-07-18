using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TigerForge;
using UnityEngine.Events;

public class UiHome : MonoBehaviour
{
    [SerializeField] private Button btn_setting;
    [SerializeField] private Button btn_shopEnemy;
    [SerializeField] private Button btn_tapToPlay;

    [SerializeField] private TMP_Text coin_txt;
    [SerializeField] private TMP_Text tapToPlay_txt;

    [Header("Anim Content")]
    [SerializeField] private RectTransform ContentTop;
    [SerializeField] private RectTransform ContentBottom;
    [SerializeField] private RectTransform ContentLeft;
    [SerializeField] private RectTransform ContentRight;

    [SerializeField] public Image IconPanelCoin;



    Tweener TweenScaleTextTapToPlay, T_MoveContentTop, T_MoveContentBottom, T_MoveContentLeft, T_MoveContentRight;

    public UnityAction A_InitHome;

    private void OnEnable()
    {
        btn_setting.onClick.AddListener(OnSetting);
        btn_shopEnemy.onClick.AddListener(OnShopEnemy);
        btn_tapToPlay.onClick.AddListener(OnPlayGame);

        EventManager.StartListening(EventContains.Event_Update_Coin, UpdateCoin);

        AnimContentTop();
        AnimContenBottom();
        AnimContentRight();
        AnimContentLeft();

        UpdateCoin();

        A_InitHome += InitHome;
    }

    private void InitHome()
    {
        UpdateCoin();

        AnimContentTop();
        AnimContenBottom();
        AnimContentRight();
        AnimContentLeft();
    }



    private void UpdateCoin()
    {
        coin_txt.text = Helper.FormatCurrency(PlayerDataManager.GetCoin());

    }

    private void AnimbtnTapToPlay()
    {
        TweenScaleTextTapToPlay?.Kill();
        tapToPlay_txt.transform.localScale = Vector3.one;

        TweenScaleTextTapToPlay = tapToPlay_txt.transform.DOScale(1.05f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void AnimContentTop()
    {
        Vector3 _contentTop = ContentTop.anchoredPosition;
        _contentTop.y += 450f;

        ContentTop.anchoredPosition = _contentTop;

        T_MoveContentTop = ContentTop.DOAnchorPosY(0f, 0.3f).SetEase(Ease.Linear);
    }


    private void AnimContenBottom()
    {
        Vector3 _contentBottom = ContentBottom.anchoredPosition;
        _contentBottom.y -= 1000f;

        ContentBottom.anchoredPosition = _contentBottom;

        T_MoveContentBottom = ContentBottom.DOAnchorPosY(-650f, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            AnimbtnTapToPlay();
        });
    }


    private void AnimContentRight()
    {
        Vector3 _contentRight = ContentRight.anchoredPosition;
        _contentRight.x += 350f;

        ContentRight.anchoredPosition = _contentRight;

        T_MoveContentRight = ContentRight.DOAnchorPosX(-35f, 0.3f).SetEase(Ease.Linear);
    }

    private void AnimContentLeft()
    {
        Vector3 _ContentLeft = ContentLeft.anchoredPosition;
        _ContentLeft.x -= 350f;

        ContentLeft.anchoredPosition = _ContentLeft;

        T_MoveContentLeft = ContentLeft.DOAnchorPosX(35f, 0.3f).SetEase(Ease.Linear);
    }


    private void OnShopEnemy()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.Open_pop);
        GameManager.instance.UiController.ChangeSateUi(E_StateUi.Shop);
    }

    private void OnSetting()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);
        GameManager.instance.UiController.ChangeSateUi(E_StateUi.Setting);
    }

    private void OnPlayGame()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);
        GameManager.instance.ChangeSateGame(E_GameState.GamePlay);
    }

    private void ResetTween()
    {
        tapToPlay_txt.transform.localScale = Vector3.one;

        TweenScaleTextTapToPlay?.Kill();
        T_MoveContentBottom?.Kill();
        T_MoveContentTop?.Kill();
        T_MoveContentLeft?.Kill();
        T_MoveContentRight?.Kill();
    }

    private void OnDisable()
    {
        A_InitHome -= InitHome;

        ResetTween();

        EventManager.StopListening(EventContains.Event_Update_Coin, UpdateCoin);
    }
}
