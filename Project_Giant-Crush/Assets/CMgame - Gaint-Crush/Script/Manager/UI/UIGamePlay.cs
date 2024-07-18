using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private Image _healImg;

    [SerializeField] private Button btn_sung;
    [SerializeField] private Button btn_Hand;
    [SerializeField] private Button btn_pause;

    [Header("Anim Ui")]
    [SerializeField] private RectTransform ContentTop;
    [SerializeField] private RectTransform ContentBottom;
    Tweener T_MoveContentTop, T_MoveContentBottom;

    public UnityAction<int, int> A_InitHeal;

    public Button Btn_sung { get => btn_sung; set => btn_sung = value; }
    public Button Btn_Hand { get => btn_Hand; set => btn_Hand = value; }
    public Image HealImg { get => _healImg; set => _healImg = value; }


    [Header("Ads")]
    [SerializeField] private int MaxTime = 20;
    [SerializeField] private float TimeCountDown;
    [SerializeField] private int Second;

    public bool _isCanShowAdsInShop;

    private void OnEnable()
    {
        Btn_sung.onClick.AddListener(OnUsePiston);
        btn_Hand.onClick.AddListener(OnUseHand);
        btn_pause.onClick.AddListener(OnOpenPopupPause);

        A_InitHeal += InitHpEnemy;

        InitGamePlay();

        if (!GameManager.instance.TutController.IsCompleteTut)
        {
            RemoveButtonHandAndGun();
        }

        AnimContenBottom();
        AnimContentTop();

        _isCanShowAdsInShop = false;
        MaxTime = 20;

    }

    private void Update()
    {
        CountTimeOpenShop();
    }


    private void InitGamePlay()
    {
        _healImg.gameObject.SetActive(true);
        btn_sung.gameObject.SetActive(true);
        btn_Hand.gameObject.SetActive(true);
        btn_pause.gameObject.SetActive(true);
    }

    #region Count Time Open Shop
    private void CountTimeOpenShop()
    {
        if (MaxTime < 0) return;

        TimeCountDown += Time.deltaTime;

        if (TimeCountDown > 1)
        {
            TimeCountDown -= 1;
            MaxTime -= 1;

            Second = MaxTime;

            if (MaxTime <= 0)
            {
                _isCanShowAdsInShop = true;
            }
        }
    }
    #endregion

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
        _contentBottom.y -= 350f;

        ContentBottom.anchoredPosition = _contentBottom;

        T_MoveContentBottom = ContentBottom.DOAnchorPosY(150f, 0.3f).SetEase(Ease.Linear);
    }
    public void InitHpEnemy(int Heal, int maxHeal)
    {
        _healImg.fillAmount = (float)(maxHeal - Heal) / maxHeal;
    }

    public void UpdateCurrentHp(int Heal, int maxHeal)
    {
        _healImg.fillAmount = (float)(maxHeal - Heal) / maxHeal;

        //Debug.Log(_healImg.fillAmount);
    }

    private void OnUseHand()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);
        GameManager.instance.WeaponController.InitNewWeapon(E_TypeWeapon.Hand);
        RemoveButtonHandAndGun();
    }
    private void OnUsePiston()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);
        GameManager.instance.WeaponController.InitNewWeapon(E_TypeWeapon.Piston);
        RemoveButtonHandAndGun();
    }

    private void OnOpenPopupPause()
    {
        if (!GameManager.instance.isInGame) return;

        GameManager.instance.isInGame = false;
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.Open_pop);
        GameManager.instance.UiController.ChangeSateUi(E_StateUi.Pause);
    }

    private void RemoveButtonHandAndGun()
    {
        btn_sung.gameObject.SetActive(false);
        btn_Hand.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        A_InitHeal -= InitHpEnemy;
    }
}
