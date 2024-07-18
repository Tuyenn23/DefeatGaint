using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCanReturn : WeaponBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && _canShot)
        {
            _canShot = false;
            Shooting();
        }

        isCanShot();
    }
    protected override void Shooting()
    {
    }
}
