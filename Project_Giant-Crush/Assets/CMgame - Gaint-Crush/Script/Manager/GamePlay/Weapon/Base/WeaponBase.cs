using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected E_TypeWeapon _typeWeapon;

    [SerializeField] protected int damage;
    [SerializeField] protected float _DurationFire;
    [SerializeField] protected float _CurrentTimeFire;
    [SerializeField] protected bool _canShot;

    public Action OnActionReset;
    public Action OnInitWeapon;

    public int Damage { get => damage; set => damage = value; }

    protected virtual void OnEnable()
    {
        OnInitWeapon += InitWeaponAction;
        OnActionReset += InitAction;
    }
    
    protected virtual void Start()
    {

    }
    protected virtual void InitAction()
    {
        InitTimeShot();
    }

    protected virtual void InitWeaponAction()
    {
        InitWeapon();
        InitTimeShot();
    }

    protected virtual void Update()
    {
    }

    protected virtual void InitWeapon()
    {
        WEAPON weapon = GameManager.instance.dataController.DataWeapons.getWeaponByType(_typeWeapon);
        Damage = weapon.Damage;
        _DurationFire = weapon.DurationFire;

        InitCurrentWeapon();
    }

    private void InitCurrentWeapon()
    {
        GameManager.instance.WeaponController.CurrentWeapon = this;
        GameManager.instance.WeaponController.IsHaveWeapon = true;
        GameManager.instance.WeaponController.GetPosSpawnWeapon();
        transform.SetParent(GameManager.instance.WeaponController.transform);
    }

    protected virtual void InitTimeShot()
    {
        _CurrentTimeFire = _DurationFire;
    }

    protected virtual void isCanShot()
    {
        if (_CurrentTimeFire < 0)
        {
            _canShot = true;
            return;
        }

        _CurrentTimeFire -= Time.deltaTime;

        if (_CurrentTimeFire <= 0)
        {
            _canShot = true;
        }
    }

    protected abstract void Shooting();


    private void OnDisable()
    {
        OnInitWeapon += InitWeaponAction;
        OnActionReset -= InitAction;
    }
}
