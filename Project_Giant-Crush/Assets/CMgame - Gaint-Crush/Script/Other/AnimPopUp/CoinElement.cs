using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class CoinElement : MonoBehaviour
{
    public AnimCoin parent;
    public Vector3 StartPos;

    Tweener T_Move;

    private void Start()
    {
        parent = transform.parent.GetComponent<AnimCoin>();

        StartPos = transform.position;
    }

    public void ResetElement()
    {
        int NewCoin = PlayerDataManager.GetCoin() + parent._coinPerElement;
        PlayerDataManager.SetCoin(NewCoin);
        EventManager.EmitEvent(EventContains.Event_Update_Coin);

        transform.position = StartPos;
        gameObject.SetActive(false);
    }

    public void Move()
    {
        T_Move = transform.DOMove(GameManager.instance.UiController.UiHome.IconPanelCoin.transform.position, 1.5f).SetEase(Ease.InOutElastic).OnComplete(() =>
        {
            ResetElement();
        });
    }

    private void OnDisable()
    {
        T_Move?.Kill();
    }
}
