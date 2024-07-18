using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItems : DragBase
{
    [SerializeField] private bool _isThrew;
    [SerializeField] private int _damage;

    [SerializeField] private Vector3 _clonePosition;
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        _startPos = transform.position;
    }

    protected override void OnMouseDrag()
    {
        if (GameManager.instance.WeaponController.IsHaveWeapon) return;

        _rb.useGravity = false;

        _endPos = PrefabStorage.Instance.inputHandle.GetMousePos();
        _endPos.z = PrefabStorage.Instance.CurrentEnemy.transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, _endPos, 15f * Time.fixedDeltaTime);
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();

        if (GameManager.instance.WeaponController.IsHaveWeapon) return;

        Vector3 _Dir = (_endPos - transform.position).normalized;

        _isThrew = true;
        _rb.useGravity = true;
        _rb.velocity = _Dir * 30f;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!_isThrew) return;

        if (collision.gameObject.CompareTag("Body"))
        {
            /*PrefabStorage.Instance.CurrentEnemy.TakeDamage(E_BodyPart.Others, _damage);*/
            _isThrew = false;
        }

        if (collision.gameObject.CompareTag("Head"))
        {
           /* PrefabStorage.Instance.CurrentEnemy.TakeDamage(E_BodyPart.Head, _damage);*/
            _isThrew = false;
        }
    }
}
