using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public void ShowDamage(Vector3 Pos , int damage)
    {
        PopupHealEnemy popupHealEnemy = SimplePool.Spawn(PrefabStorage.Instance.PopupHealEnemy, Pos, Quaternion.identity);
        popupHealEnemy.transform.localScale = Vector3.one;
        if (!popupHealEnemy.transform.parent)
        {
            popupHealEnemy.transform.parent = GameManager.instance.UiController.UigamePlay.transform;
            popupHealEnemy.transform.localScale = Vector3.one;
        }
        popupHealEnemy.InitHealEnemy(damage);
    }
}
