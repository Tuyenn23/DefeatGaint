using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBullet : BulletBase
{
    protected override void Update()
    {
        base.Update();
        Move();
    }
    public override void Move()
    {
        _rb.velocity = DirShot * 100f;
    }

    /*    private void OnCollisionEnter(Collision collision)
        {

        }*/


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Body"))
        {
            Debug.Log(other.gameObject.name);   
            PrefabStorage.Instance.CurrentEnemy.TakeDamage(E_BodyPart.Others, _damage, transform.position);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Head"))
        {
            Debug.Log(other.gameObject.name);
            PrefabStorage.Instance.CurrentEnemy.TakeDamage(E_BodyPart.Head, _damage, transform.position);
            Destroy(gameObject);
        }

        foreach (var item in PrefabStorage.Instance.CurrentEnemy.L_BodyPart)
        {
            if (item.name == other.name)
            {
                Debug.Log(item.name);
                item.gameObject.SetActive(false);
            }
        }


        ItemBase itemBase = other.GetComponent<ItemBase>();

        if (itemBase)
        {
            Piston piston = GameManager.instance.WeaponController.CurrentWeapon.GetComponent<Piston>();
            itemBase.ShowDamage(piston._CrossHair.transform.position, _damage);

            _rb.AddForceAtPosition(Vector3.forward * 100f, transform.position , ForceMode.Impulse);
        }
    }
}
