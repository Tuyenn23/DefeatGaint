using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class UiController : MonoBehaviour
{
    public UIGamePlay UigamePlay;
    public UiHome UiHome;

    [SerializeField] private E_StateUi CurrentStateUi = E_StateUi.None;

    [SerializeField] private ShopEnemy _shopEnemy;
    [SerializeField] private PopupSetting _popupSetting;
    [SerializeField] private PopupPause _popupPause;
    [SerializeField] private PopupReward _popupReward;
    [SerializeField] public PanelNointernet _popupNoInternet;

    [SerializeField] public RectTransform PanelAds;
    [SerializeField] public RectTransform panelCoin;


    public UnityAction A_OnStateGamePlay;
    public UnityAction A_OnStateHome;

    private void OnEnable()
    {
        A_OnStateGamePlay += OnStateGame;
        A_OnStateHome += OnStateHome;
    }

    public void ChangeSateUi(E_StateUi NewSate)
    {
        CurrentStateUi = NewSate;

        switch (NewSate)
        {
            case E_StateUi.None:
                CloseAllPopup();
                break;

            case E_StateUi.Setting:
                CloseAllPopup();
                OnOpenPopupSetting();
                break;
            case E_StateUi.Shop:
                CloseAllPopup();
                OnOpenPopupShop();
                break;
            case E_StateUi.Pause:
                CloseAllPopup();
                OnOpenPopupPause();
                break;
            case E_StateUi.Revive:
                Debug.Log("Revive");
                break;
            default:
                break;
        }
    }

    private void OnOpenPopupShop()
    {
        _shopEnemy.gameObject.SetActive(true);
    }

    private void OnOpenPopupSetting()
    {
        _popupSetting.gameObject.SetActive(true);
    }

    private void OnOpenPopupPause()
    {
        _popupPause.gameObject.SetActive(true);
    }

    private void CloseAllPopup()
    {
        _popupSetting.gameObject.SetActive(false);
        _shopEnemy.gameObject.SetActive(false);
        _popupSetting.gameObject.SetActive(false);
    }


    private void OnStateGame()
    {
        UiHome.gameObject.SetActive(false);
        UigamePlay.gameObject.SetActive(true);
    }

    private void OnStateHome()
    {
        UigamePlay.gameObject.SetActive(false);
        UiHome.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        A_OnStateGamePlay -= OnStateGame;
        A_OnStateHome -= OnStateHome;
    }

    public void ProcessWinLose(E_ResultLevel TypeLevel)
    {
        switch (TypeLevel)
        {
            case E_ResultLevel.Win:
                GameManager.instance.isInGame = false;
                GameManager.instance.WeaponController.A_ChangeHome?.Invoke();
                ActivePopupReward();
                break;
            case E_ResultLevel.Lose:

                break;
            case E_ResultLevel.Undecided:
                break;
            default:
                break;
        }
    }


    private void ActivePopupReward()
    {
        _popupReward.gameObject.SetActive(true);
    }
}
