using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour, IDamage
{
    [SerializeField] protected int _damage;
    [SerializeField] protected Rigidbody _rb;

    public Vector3 DirShot;
    public int Damage { get => _damage; set => _damage = value; }

    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }

    public void InitDataBullet(int Damage)
    {
        _damage = Damage;
    }

    public abstract void Move();
    public virtual void Move(Vector3 dir , Transform shotPos)
    {

    }

    public void TakeDamage()
    {

    }
}
