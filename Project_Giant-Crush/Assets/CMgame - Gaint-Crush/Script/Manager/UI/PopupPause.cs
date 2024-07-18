using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPause : AnimScale
{
    [Header("Setting")]
    [SerializeField] private Button btn_toogleMusic;
    [SerializeField] private List<Image> L_toogleImgMusic;

    [SerializeField] private Button btn_sfx;
    [SerializeField] private List<Image> L_toogleImgSfx;

    [SerializeField] private Button btn_backmenu;
    [SerializeField] private Button btn_continue;



    [Header("More Game")]
    [SerializeField] private Button btn_installGame1;
    [SerializeField] private Button btn_installGame2;

    private void OnEnable()
    {
        GameManager.instance.isInGame = false;

        btn_toogleMusic.onClick.AddListener(OnToogleMusic);
        btn_sfx.onClick.AddListener(OnToogleSfx);

        btn_backmenu.onClick.AddListener(OnBackMenu);
        btn_continue.onClick.AddListener(OnContinue);


        InitMusic();
        InitSfx();
    }
    private void InitMusic()
    {
        bool isOn = PlayerDataManager.GetMusic();

        if (isOn)
        {
            ResetTurnMusic();
            L_toogleImgMusic[1].gameObject.SetActive(true);
        }
        else
        {
            ResetTurnMusic();
            L_toogleImgMusic[0].gameObject.SetActive(true);
        }
    }


    private void InitSfx()
    {
        bool isOn = PlayerDataManager.GetSound();

        if (isOn)
        {
            ResetTurnSfx();
            L_toogleImgSfx[1].gameObject.SetActive(true);
        }
        else
        {
            ResetTurnSfx();
            L_toogleImgSfx[0].gameObject.SetActive(true);
        }
    }

    private void OnToogleMusic()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);

        bool isOn = PlayerDataManager.GetMusic();

        if (!isOn)
        {
            ResetTurnMusic();
            L_toogleImgMusic[1].gameObject.SetActive(true);
            PlayerDataManager.SetMusic(!isOn);
        }
        else
        {
            ResetTurnMusic();
            L_toogleImgMusic[0].gameObject.SetActive(true);
            PlayerDataManager.SetMusic(!isOn);
        }

        SoundManager.Instance.SettingMusic(PlayerDataManager.GetMusic());
    }

    private void OnToogleSfx()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);

        bool isOn = PlayerDataManager.GetSound();

        if (!isOn)
        {
            ResetTurnSfx();
            L_toogleImgSfx[1].gameObject.SetActive(true);
            PlayerDataManager.SetSound(!isOn);
        }
        else
        {
            ResetTurnSfx();
            L_toogleImgSfx[0].gameObject.SetActive(true);
            PlayerDataManager.SetSound(!isOn);
        }

        SoundManager.Instance.SettingFxSound(PlayerDataManager.GetSound());
    }

    private void OnContinue()
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);

        gameObject.SetActive(false);
    }

    private void OnBackMenu()
    {
        if(GameManager.instance.UiController.UigamePlay._isCanShowAdsInShop)
        {
            AdManager.instance.ShowInter(null, null, "ShowInter");
           // AdsHandle.instance.StopGobBackInter();
        }

        SoundManager.Instance.PlayFxSound(SoundManager.Instance.buttonclick);

        GameManager.instance.ChangeSateGame(E_GameState.Home);
        gameObject.SetActive(false);
    }


    private void ResetTurnMusic()
    {
        for (int i = 0; i < L_toogleImgMusic.Count; i++)
        {
            L_toogleImgMusic[i].gameObject.SetActive(false);
        }
    }

    private void ResetTurnSfx()
    {
        for (int i = 0; i < L_toogleImgSfx.Count; i++)
        {
            L_toogleImgSfx[i].gameObject.SetActive(false);
        }
    }

    private void RemoveAllButton()
    {
        btn_toogleMusic.onClick.RemoveListener(OnToogleMusic);
        btn_sfx.onClick.RemoveListener(OnToogleSfx);
        btn_backmenu.onClick.RemoveListener(OnBackMenu);
        btn_continue.onClick.RemoveListener(OnContinue);
    }

    private void OnDisable()
    {
        GameManager.instance.isInGame = true;
        RemoveAllButton();
    }
}
