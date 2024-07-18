using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopEnemy : MonoBehaviour
{
    [SerializeField] private Camera _camRenderTexture;


    [SerializeField] private E_TypeEnemy _CurrentTypeEnemy;
    [SerializeField] private int _CurrentEnemy;
    [SerializeField] private int _currentEnemyCoin;
    [SerializeField] private List<GameObject> L_EnemiesObject;

    [SerializeField] private List<Button> L_btnPurchase;

    [SerializeField] private Button btn_Back;
    [SerializeField] private Button btn_Next;
    [SerializeField] private Button btn_Previous;
    [SerializeField] private Button btn_PurchaseAndFree;
    [SerializeField] private Button btn_Equip;

    [SerializeField] private TMP_Text NameEnemy_txt;
    [SerializeField] private TMP_Text Quantity_CoinPurchase;
    [SerializeField] private TMP_Text Coin_txt;

    [SerializeField] private Image img_Owned;
    [SerializeField] private Image img_PurchaseFree;

    [SerializeField] private Sprite IconFree;
    [SerializeField] private Sprite IconCoin;


    [Header("Anim Content")]
    [SerializeField] private RectTransform ContentTop;
    [SerializeField] private RectTransform ContentBottom;
    [SerializeField] private RectTransform ContentLeft;
    [SerializeField] private RectTransform ContentRight;

    [Header("Ads")]
    int MaxTime;
    float TimeCountDown;
    [SerializeField] int Second;

    bool _isCanShowAdsInShop;

    Tweener T_MoveContentTop, T_MoveContentBottom, T_MoveContentLeft, T_ContentRight;

    private void OnEnable()
    {
        btn_Back.onClick.AddListener(OnCloseShop);
        btn_Next.onClick.AddListener(OnNext);
        btn_Previous.onClick.AddListener(OnPrevious);

        btn_PurchaseAndFree.onClick.AddListener(OnClaimWithCoinAndFree);
        btn_Equip.onClick.AddListener(OnEquipSkin);

        AnimContentTop();
        AnimContenBottom();
        AnimContentLeft();
        AnimContentRight();

        _CurrentTypeEnemy = E_TypeEnemy.Zoombie;
        InitEnemyInShop();

        UpdateCoin();

        _isCanShowAdsInShop = false;
        MaxTime = 20;
    }


    private void Update()
    {
        CountTimeOpenShop();
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
    private void UpdateCoin()
    {
        Coin_txt.text = Helper.FormatCurrency(PlayerDataManager.GetCoin());
    }


    private void InitEnemyInShop()
    {
        _CurrentEnemy = (int)_CurrentTypeEnemy;

        ActiveCurrentEnemy(_CurrentEnemy);
        LoadCurrentEnemy(_CurrentEnemy);
    }

    private void LoadCurrentEnemy(int Current)
    {
        for (int i = 0; i < L_EnemiesObject.Count; i++)
        {
            L_EnemiesObject[i].gameObject.SetActive(false);
        }

        L_EnemiesObject[Current].gameObject.SetActive(true);
        _camRenderTexture.gameObject.SetActive(true);
    }

    private void OnNext()
    {
        Debug.Log("onnext");
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);
        _CurrentEnemy++;

        if (_CurrentEnemy >= PrefabStorage.Instance.DataShopEnemy.L_Enemies.Count)
        {
            _CurrentEnemy = 0;
        }

        ActiveCurrentEnemy(_CurrentEnemy);
    }

    private void OnPrevious()
    {
        Debug.Log("On Previous");

        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);
        _CurrentEnemy--;

        if (_CurrentEnemy < 0)
        {
            _CurrentEnemy = PrefabStorage.Instance.DataShopEnemy.L_Enemies.Count - 1;
        }

        ActiveCurrentEnemy(_CurrentEnemy);
    }

    private void OnCloseShop()
    {
        if (_isCanShowAdsInShop)
        {
            AdManager.instance.ShowInter(null, null, "ShowInter");
            AdsHandle.instance.StopGobBackInter();
        }

        SoundManager.Instance.PlayFxSound(SoundManager.Instance.Close_pop);
        GameManager.instance.UiController.UiHome.A_InitHome?.Invoke();
        gameObject.SetActive(false);
    }

    private void ActiveCurrentEnemy(int Current)
    {
        SHOPENEMY shopEnemy = PrefabStorage.Instance.DataShopEnemy.getCurrentEnemyShop((E_TypeEnemy)Current);
        _CurrentTypeEnemy = (E_TypeEnemy)Current;
        LoadCurrentEnemy(Current);

        if (shopEnemy != null)
        {
            NameEnemy_txt.text = shopEnemy.Name;

            //icon

            if (PlayerDataManager.getCurrentSkin() == _CurrentTypeEnemy)
            {
                ActiveImageTick();

                return;
            }

            for (int i = 0; i < PlayerDataManager.GetListSkinOnwed().Count; i++)
            {
                if (PlayerDataManager.GetListSkinOnwed()[i] == (E_TypeEnemy)Current)
                {
                    ActiveBtnEquip();

                    break;
                }
                else
                {
                    if (PlayerDataManager.GetCoin() < shopEnemy.QuantityCoin)
                    {
                        Quantity_CoinPurchase.text = "Free";
                        img_PurchaseFree.sprite = IconFree;
                        _currentEnemyCoin = shopEnemy.QuantityCoin;
                        ActiveBtnBuyWithCoinAndFree();
                    }
                    else
                    {
                        Quantity_CoinPurchase.text = Helper.FormatCurrency(shopEnemy.QuantityCoin);
                        img_PurchaseFree.sprite = IconCoin;
                        _currentEnemyCoin = shopEnemy.QuantityCoin;
                        ActiveBtnBuyWithCoinAndFree();
                    }
                }
            }

        }
    }


    private void DeActiveAllBtnPurchase()
    {
        for (int i = 0; i < L_btnPurchase.Count; i++)
        {
            L_btnPurchase[i].gameObject.SetActive(false);
        }

        img_Owned.gameObject.SetActive(false);
    }

    private void ActiveBtnBuyWithCoinAndFree()
    {
        DeActiveAllBtnPurchase();

        L_btnPurchase[0].gameObject.SetActive(true);

    }

    private void ActiveBtnEquip()
    {
        DeActiveAllBtnPurchase();

        L_btnPurchase[1].gameObject.SetActive(true);
    }

    private void ActiveImageTick()
    {
        DeActiveAllBtnPurchase();

        img_Owned.gameObject.SetActive(true);
    }

    private void OnClaimWithCoinAndFree()
    {
        if (PlayerDataManager.GetCoin() < _currentEnemyCoin)
        {

            AdManager.instance.ShowReward(delegate
            {
                PlayerDataManager.AddSkin((E_TypeEnemy)_CurrentEnemy);
                ActiveBtnEquip();
            }, delegate
            {
                GameManager.instance.UiController._popupNoInternet.gameObject.SetActive(true);
            }, "Show reward");
        }
        else
        {
            PlayerDataManager.AddSkin((E_TypeEnemy)_CurrentEnemy);

            int newCoin = PlayerDataManager.GetCoin() - _currentEnemyCoin;
            PlayerDataManager.SetCoin(newCoin);

            UpdateCoin();
            ActiveBtnEquip();
        }
    }

    private void OnEquipSkin()
    {
        PlayerDataManager.SetCurrentSKinUsing((E_TypeEnemy)_CurrentEnemy);
        PrefabStorage.Instance.LoadEnemies(_CurrentEnemy);
        ActiveImageTick();
    }

    private void AnimContentTop()
    {
        Vector3 _contentTop = ContentTop.anchoredPosition;
        _contentTop.y += 450f;

        ContentTop.anchoredPosition = _contentTop;

        T_MoveContentTop = ContentTop.DOAnchorPosY(-25f, 0.3f).SetEase(Ease.Linear);
    }


    private void AnimContenBottom()
    {
        Vector3 _contentBottom = ContentBottom.anchoredPosition;
        _contentBottom.y -= 500f;

        ContentBottom.anchoredPosition = _contentBottom;

        T_MoveContentBottom = ContentBottom.DOAnchorPosY(400f, 0.3f).SetEase(Ease.Linear);
    }


    private void AnimContentRight()
    {
        Vector3 _contentRight = ContentRight.anchoredPosition;
        _contentRight.x += 350f;

        ContentRight.anchoredPosition = _contentRight;

        T_ContentRight = ContentRight.DOAnchorPosX(-100f, 0.3f).SetEase(Ease.Linear);
    }

    private void AnimContentLeft()
    {
        Vector3 _ContentLeft = ContentLeft.anchoredPosition;
        _ContentLeft.x -= 350f;

        ContentLeft.anchoredPosition = _ContentLeft;

        T_MoveContentLeft = ContentLeft.DOAnchorPosX(200f, 0.3f).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        _camRenderTexture.gameObject.SetActive(false);
        for (int i = 0; i < L_EnemiesObject.Count; i++)
        {
            L_EnemiesObject[i].gameObject.SetActive(false);
        }


        btn_Back.onClick.RemoveListener(OnCloseShop);
        btn_Next.onClick.RemoveListener(OnNext);
        btn_Previous.onClick.RemoveListener(OnPrevious);
    }
}
