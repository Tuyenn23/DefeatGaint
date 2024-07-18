using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrajectory : BulletBase
{
    public override void Move()
    {
        Vector3 _dir = (PrefabStorage.Instance.inputHandle.GetMousePos() - transform.position).normalized;
        Vector3 absDir = new Vector3(Mathf.Abs(_dir.x), Mathf.Abs(_dir.y), Mathf.Abs(_dir.z));
       _rb.AddForce(absDir * 30f, ForceMode.Impulse);
    }

    public override void Move(Vector3 dir, Transform shotPos)
    {
       _rb.velocity = dir;
    }
}
