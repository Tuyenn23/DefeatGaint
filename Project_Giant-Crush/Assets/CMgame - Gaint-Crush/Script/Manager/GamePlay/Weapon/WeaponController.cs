using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponBase _currentWeapon;
    [SerializeField] private Camera _camMain;
    [SerializeField] private bool _isHaveWeapon;

    public UnityAction A_ChangeHome;

    public WeaponBase CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }
    public bool IsHaveWeapon { get => _isHaveWeapon; set => _isHaveWeapon = value; }

    private void OnEnable()
    {
        A_ChangeHome += RemoveCurrentWeapon;
    }


    public Vector3 GetPosSpawnWeapon()
    {
        Camera mainCamera = Camera.main;

        Vector3 BottomPosition = new Vector3(0.5f, 0.05f, mainCamera.nearClipPlane + 5f);
        Vector3 WorldBottomPosition = mainCamera.ViewportToWorldPoint(BottomPosition);

        return WorldBottomPosition;
    }

    public void InitNewWeapon(E_TypeWeapon _typeWeapon)
    {
        switch (_typeWeapon)
        {
            case E_TypeWeapon.None:
                RemoveCurrentWeapon();
                break;
            case E_TypeWeapon.Piston:
                RemoveCurrentWeapon();
                InitWeaponPiston();
                break;
            case E_TypeWeapon.Hand:
                RemoveCurrentWeapon();
                InitWeaponHand();
                break;
            default:
                break;
        }
    }

    private void RemoveCurrentWeapon()
    {
        if (!CurrentWeapon) return;

        _isHaveWeapon = false;
        Destroy(CurrentWeapon.gameObject);
    }

    private void InitWeaponPiston()
    {
        _isHaveWeapon = true;

        Piston _weaponPiston = Instantiate(PrefabStorage.Instance.Piston);
        _weaponPiston.transform.position = GetMidCorner();
        _weaponPiston.OnInitWeapon?.Invoke();
    }

    private void InitWeaponHand()
    {
        _isHaveWeapon = true;

        PunchController _weaponPunch = Instantiate(PrefabStorage.Instance.PunchController);
        Vector3 newPos = new Vector3(GetMidCorner().x, GetMidCorner().y + 1.5f, GetMidCorner().z);
        _weaponPunch.transform.localPosition = new Vector3(0, 0.5f, 0);
        _weaponPunch.OnInitWeapon?.Invoke();
    }

    Vector3 GetMidCorner()
    {
        Vector3 screenBottomLeft = new Vector3(Screen.width / 2, 0, Camera.main.nearClipPlane + 10f);
        return Camera.main.ScreenToWorldPoint(screenBottomLeft);
    }

    Vector3 GetBottomRightCorner()
    {
        Vector3 screenBottomRight = new Vector3(Screen.width, 0, Camera.main.nearClipPlane + 10f);
        return Camera.main.ScreenToWorldPoint(screenBottomRight);
    }


    private void OnDisable()
    {
        A_ChangeHome -= RemoveCurrentWeapon;
    }
}
