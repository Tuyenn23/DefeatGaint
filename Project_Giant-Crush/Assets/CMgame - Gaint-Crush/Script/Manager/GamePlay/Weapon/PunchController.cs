using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PunchController : WeaponCanReturn
{
    [SerializeField] private List<Punch> L_Hands;
    [SerializeField] private int CurrentHandShot;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private bool _isClickUI;

    Tween T_MoveHand;

    protected override void Start()
    {
        base.Start();
    }

    protected override void InitAction()
    {
        base.InitAction();
    }
    protected override void Shooting()
    {
        if(EventSystem.current.IsPointerOverGameObject(0))
        {
            _isClickUI = true;

            StartCoroutine(IE_DelayChangeIsClick());
        }
        if (GameManager.instance.isInGame && !_isClickUI)
        {
            Move(L_Hands[CurrentHandShot]);
        }
    }


    public void Move(Punch CurrentHand)
    {
        if (_isAttacking) return;

        _isAttacking = true;
        CurrentHand.AttackPunch(() =>
        {
            ResetPunch(CurrentHand);
        });
    }

    private void MoveReturn(Punch CurrentHand)
    {

        CurrentHandShot++;

        if (CurrentHandShot > 1)
            CurrentHandShot = 0;
    }

    public void ResetPunch(Punch CurrentHand)
    {
        MoveReturn(CurrentHand);
        _isAttacking = false;
        OnActionReset?.Invoke();
    }

    private void ClampPunch()
    {

    }

    IEnumerator IE_DelayChangeIsClick()
    {
        yield return null;
        _isClickUI = false;
        yield return null;

        StopCoroutine(IE_DelayChangeIsClick());
    }
}
