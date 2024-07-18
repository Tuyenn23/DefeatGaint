using DG.Tweening;
using PolygonArsenal;
using UnityEngine;

public class Piston : HaveCrossHair
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();


    }

    protected override void Shooting()
    {
        if (!GameManager.instance.isInGame) return;

        SoundManager.Instance.PlayFxSound(SoundManager.Instance.Gun);

        BulletBase bullet = Instantiate(_bullet , _ShotPos.transform.position,Quaternion.identity);
        bullet.InitDataBullet(Damage);

        Vector3 newPos = new Vector3(_CrossHair.transform.position.x, _CrossHair.transform.position.y, PrefabStorage.Instance.CurrentEnemy.transform.position.z + 1f);
        bullet.DirShot = (newPos - _ShotPos.position).normalized;


        InitTimeShot();
    }
}
