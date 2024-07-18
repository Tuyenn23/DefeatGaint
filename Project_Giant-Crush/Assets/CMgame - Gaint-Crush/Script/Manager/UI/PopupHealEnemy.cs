using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupHealEnemy : MonoBehaviour
{
    [SerializeField] private TMP_Text HealEnemy_txt;

    Tweener T_Move , T_Scale;
    private void OnEnable()
    {
        AnimText();
    }

    public void InitHealEnemy(int damage)
    {
        HealEnemy_txt.text = damage.ToString();
    }

    private void AnimText()
    {
        T_Move = transform.DOMoveY(transform.position.y + 2f, 0.2f);

        T_Scale = transform.DOScale(1.1f, 0.2f);
    }

    private void OnDisable()
    {
        T_Move?.Kill();
        T_Scale?.Kill();
    }
}
