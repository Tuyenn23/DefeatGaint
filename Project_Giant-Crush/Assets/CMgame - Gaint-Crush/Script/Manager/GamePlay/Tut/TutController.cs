using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class TutController : MonoBehaviour
{
    [SerializeField] private bool _isCompleteTut;
    [SerializeField] private E_TypeTut _currentTut;

    [SerializeField] private Tut_1 _tut1;
    [SerializeField] private Tut_2 _tut2;

    public bool IsCompleteTut { get => _isCompleteTut; set => _isCompleteTut = value; }

    private void OnEnable()
    {
        _isCompleteTut = PlayerDataManager.GetCompletedTut();

        if (_isCompleteTut)
        {
            DeAactiveAllTut();
            return;
        }

        InitUncompleted();
        ChangeCurrentTut(E_TypeTut.Step1);
    }

    private void InitUncompleted()
    {
        GameManager.instance.UiController.UigamePlay.Btn_Hand.gameObject.SetActive(false);
        GameManager.instance.UiController.UigamePlay.Btn_sung.gameObject.SetActive(false);
    }

    public void ChangeCurrentTut(E_TypeTut newTut)
    {
        _currentTut = newTut;

        switch (newTut)
        {
            case E_TypeTut.Step1:
                DeAactiveAllTut();
                OnTut_1();
                break;
            case E_TypeTut.Step2:
                DeAactiveAllTut();
                OnTut_2();
                break;
            default:
                break;
        }
    }

    private void OnTut_1()
    {
        _tut1.gameObject.SetActive(true);
    }

    private void OnTut_2()
    {
        GameManager.instance.UiController.UigamePlay.HealImg.gameObject.SetActive(false);
        _tut2.gameObject.SetActive(true);
    }

    private void DeAactiveAllTut()
    {
        _tut1.gameObject.SetActive(false);
        _tut2.gameObject.SetActive(false);
    }

}
