using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Punch : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private PunchController _punchController;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Action Ac_target;

    [SerializeField] private bool _isFirstTarget;
    [SerializeField] private bool _isFirstTargetItem;
    Tween T_MoveHand;

    private void OnEnable()
    {
        if (!_punchController)
        {
            _punchController = transform.parent.GetComponent<PunchController>();
        }

        Ac_target += OnTarget;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startPos = transform.localPosition;
    }

    private void OnTarget()
    {
        _isFirstTarget = true;
        transform.localPosition = _startPos;
        T_MoveHand?.Kill();
    }

    public void AttackPunch(UnityAction ActionComplete)
    {
        SoundManager.Instance.PlayFxSound(SoundManager.Instance.Punch);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 EndPos = ray.origin + ray.direction * 36f;

        T_MoveHand = transform.DOMove(EndPos, 0.5f).SetEase(Ease.Linear);

        T_MoveHand.OnComplete(() =>
        {
            T_MoveHand?.Kill();
            transform.localPosition = _startPos;

            ActionComplete?.Invoke();
        });
    }

    private void OnCollisionEnter(Collision other)
    {
        

        if (_isFirstTarget) return;

        /*BodyPart Body = other.GetComponent<BodyPart>();*/

        if (other.gameObject.CompareTag("Body"))
        {
            Debug.Log(other.gameObject.name);
            PrefabStorage.Instance.CurrentEnemy.TakeDamage(E_BodyPart.Others, _punchController.Damage, transform.position);
            Ac_target?.Invoke();


            _punchController.ResetPunch(this);
        }

        if (other.gameObject.CompareTag("Head"))
        {
            Debug.Log(other.gameObject.name);
            PrefabStorage.Instance.CurrentEnemy.TakeDamage(E_BodyPart.Head, _punchController.Damage, transform.position);
            Ac_target?.Invoke();


            _punchController.ResetPunch(this);
        }

        foreach (var item in PrefabStorage.Instance.CurrentEnemy.L_BodyPart)
        {
            if (item.name == other.gameObject.name)
            {
                Debug.Log(other.gameObject.name);
                item.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isFirstTargetItem) return;

        Debug.Log(other.name);
        int damage = UnityEngine.Random.Range(10, 21);
        PopupHealEnemy popupHealEnemy = SimplePool.Spawn(PrefabStorage.Instance.PopupHealEnemy, transform.position, Quaternion.identity);
        popupHealEnemy.transform.localScale = Vector3.one;
        if (!popupHealEnemy.transform.parent)
        {
            popupHealEnemy.transform.parent = GameManager.instance.UiController.UigamePlay.transform;
            popupHealEnemy.transform.localScale = Vector3.one;
        }
        popupHealEnemy.InitHealEnemy(damage);
        _isFirstTargetItem = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isFirstTargetItem = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        _isFirstTarget = false;
    }
    private void OnDisable()
    {
        Ac_target -= OnTarget;
    }
}
