using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragEnemy : DragBase
{
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    protected override void OnMouseDrag()
    {
        if (GameManager.instance.WeaponController.IsHaveWeapon) return;

        _rb.isKinematic = true;
        _endPos = PrefabStorage.Instance.inputHandle.GetMousePos();
        _endPos.z = 0f;
        PrefabStorage.Instance.CurrentEnemy.transform.position = _endPos;
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
    }
}
