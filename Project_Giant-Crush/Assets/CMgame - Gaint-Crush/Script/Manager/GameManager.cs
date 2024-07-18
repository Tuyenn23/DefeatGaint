using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Controller")]
    public DataController dataController;
    public WeaponController WeaponController;
    public UiController UiController;
    public TutController TutController;

    public bool isInGame;
    public bool isPauseGame;
    public bool isOpenShop;

    [SerializeField] private E_GameState _CurrentState = E_GameState.Home;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        ChangeSateGame(E_GameState.Home);

        AdManager.instance.ShowBanner();
    }

    private void Update()
    {
        CheckAFKAds();
    }

    public void CheckAFKAds()
    {
        if (Input.GetMouseButton(0) && AdsHandle.instance.CanShowInterAFK)
        {
            UiController.PanelAds.gameObject.SetActive(false);

        }

        if (Input.GetMouseButton(0) && !AdsHandle.instance.Detected && !isOpenShop)
        {
            AdsHandle.instance.Detected = true;
            AdsHandle.instance.ShowInterAFK();
        }
    }
    public void ChangeSateGame(E_GameState NewState)
    {
        _CurrentState = NewState;

        switch (NewState)
        {
            case E_GameState.Home:
                UiController.A_OnStateHome?.Invoke();
                WeaponController.A_ChangeHome?.Invoke();
                if (PrefabStorage.Instance.CurrentEnemy)
                {
                    PrefabStorage.Instance.CurrentEnemy.A_OnHome?.Invoke();
                }

                StopCoroutine(ChangeStateInGame());
                break;
            case E_GameState.GamePlay:
                UiController.A_OnStateGamePlay?.Invoke();
                PrefabStorage.Instance.CurrentEnemy.A_Ingame?.Invoke();

                StartCoroutine(ChangeStateInGame());
                break;
            default:
                break;
        }
    }

    IEnumerator ChangeStateInGame()
    {
        yield return null;
        isInGame = true;
    }

    public void ChangeStatePause()
    {
        StartCoroutine(IE_ChangeStateIsPause());
    }

    IEnumerator IE_ChangeStateIsPause()
    {
        yield return null;
        isPauseGame = true;
    }
}
