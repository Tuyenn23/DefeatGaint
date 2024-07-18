using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAttack : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (GameManager.instance.WeaponController.IsHaveWeapon) return;

       // PrefabStorage.Instance.CurrentEnemy.TriggerRagdoll(Vector3.forward, transform.position);
    }
}
