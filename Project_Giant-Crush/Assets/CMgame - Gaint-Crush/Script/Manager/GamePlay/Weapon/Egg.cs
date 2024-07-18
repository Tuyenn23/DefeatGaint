using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : DontHaveCrossHair
{
    protected override void Shooting()
    {
        BulletBase bulletClone = SimplePool.Spawn(_bullet);

        bulletClone.transform.position = _ShotPos.position;
        bulletClone.InitDataBullet(Damage);
        bulletClone.Move(_force, _ShotPos);

        OnActionReset?.Invoke();
    }
}
