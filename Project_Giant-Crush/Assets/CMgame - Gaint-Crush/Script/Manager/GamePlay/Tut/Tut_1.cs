using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tut_1 : MonoBehaviour
{
    [SerializeField] private RectTransform HandTut;

    [SerializeField] private Button btn_Gun;
    [SerializeField] private Button btn_Hand;

    Tweener T_HandTut, T_ClickGun;

    private void OnEnable()
    {
        InitTut_1();
    }

    private void InitTut_1()
    {
        btn_Gun.onClick.AddListener(OnclickGun);
        btn_Hand.interactable = false;
        AnimHandTut();
    }

    private void AnimHandTut()
    {
        T_HandTut = HandTut.transform.DOScale(1.1f, 0.4f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnclickGun()
    {
        T_ClickGun = btn_Gun.transform.DOScale(0.8f, 0.2f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            btn_Gun.transform.localScale = Vector3.one;
            GameManager.instance.WeaponController.InitNewWeapon(E_TypeWeapon.Piston);
            gameObject.SetActive(false); 
        });
    }

    private void OnDisable()
    {
        T_ClickGun?.Kill();
        T_HandTut?.Kill();
    }

}
