using DG.Tweening;
using FIMSpace.FProceduralAnimation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] protected E_TypeEnemy _typeEnemy;
    [SerializeField] protected int _hp;
    [SerializeField] protected int _Maxhp;
    [SerializeField] public int _reWard;

    [Header("Ragdoll")]
    public Animator Animator;
    public RagdollAnimator ragdoll;
    public RagdollProcessor.EGetUpType CanGetUp = RagdollProcessor.EGetUpType.None;
    public LayerMask snapToGroundLayer = 1 << 0;
    public bool GetUpVersion2 = true;


    public UnityAction A_Ingame;

    protected virtual void OnEnable()
    {
        transform.DORotate(new Vector3(0, 180, 0), 0.01f,RotateMode.Fast);
        A_Ingame += InitActionIngame;
        InitDataEnemy();
    }

    private void InitActionIngame()
    {
        InitDataEnemy();
    }
    protected void InitDataEnemy()
    {
        ENEMY enemy = GameManager.instance.dataController.dataEnemies.getEnemyByType(_typeEnemy);

        _hp = enemy.Hp;
        _Maxhp = enemy.Hp;
        _reWard = enemy.Reward;
        GameManager.instance.UiController.UigamePlay.A_InitHeal?.Invoke(_hp, _Maxhp);
    }

    public void TakeDamage(E_BodyPart _type, int damage, Vector3 Pos)
    {
        if (isDead()) return;

        if (_type == E_BodyPart.Head)
        {
            damage = Random.Range(10, 21);
            PopupHealEnemy popupHealEnemy = SimplePool.Spawn(PrefabStorage.Instance.PopupHealEnemy, Pos, Quaternion.identity);
            popupHealEnemy.InitHealEnemy(damage);   
        }
        else
        {
            damage = Random.Range(10, 21);
            Pos.z = -5f;
            PopupHealEnemy popupHealEnemy = SimplePool.Spawn(PrefabStorage.Instance.PopupHealEnemy, Pos, Quaternion.identity);
            popupHealEnemy.transform.localScale = Vector3.one;
            if (!popupHealEnemy.transform.parent)
            {
                popupHealEnemy.transform.parent = GameManager.instance.UiController.UigamePlay.transform;
                popupHealEnemy.transform.localScale = Vector3.one;
            }
            popupHealEnemy.InitHealEnemy(damage);
        }

        _hp -= damage;


        GameManager.instance.UiController.UigamePlay.UpdateCurrentHp(_hp, _Maxhp);
        Animator.SetTrigger("isAttacking");

        if (isDead())
        {
            GameManager.instance.isInGame = false;
            _hp = 0;
            GameManager.instance.UiController.UigamePlay.UpdateCurrentHp(_hp, _Maxhp);
            EnemyDeath();

            StartCoroutine(IE_DelayEndGame());
        }
    }

    IEnumerator IE_DelayEndGame()
    {
        yield return new WaitForSeconds(3f);

        if (!GameManager.instance.TutController.IsCompleteTut)
        {
            GameManager.instance.TutController.ChangeCurrentTut(E_TypeTut.Step2);
        }
        else
        {
            GameManager.instance.UiController.ProcessWinLose(E_ResultLevel.Win);
        }
    }

    private void EnemyDeath()
    {
        ragdoll.StopAllCoroutines();
        ragdoll.Parameters.SafetyResetAfterCouroutinesStop();
        ragdoll.User_SetAllKinematic(false);
        ragdoll.User_EnableFreeRagdoll();
        ragdoll.User_SwitchAnimator(null, false, 0.15f);
        CanGetUp = ragdoll.Parameters.User_CanGetUp(null, false);
    }


    public void TriggerGetUp()
    {

        if (GetUpVersion2)
        {
            ragdoll.transform.rotation = ragdoll.Parameters.User_GetMappedRotationHipsToHead(Vector3.up);
            ragdoll.User_SwitchAnimator(null, true);
            ragdoll.User_GetUpStackV2(0f, 0.8f, 0.7f);
            ragdoll.User_ForceRagdollToAnimatorFor(0.5f, 0.5f); // (if using blend on collision) Force non-ragdoll for 0.5 sec and restore transition in 0.5 sec
            TryPlayGetupAnimation();
            PrefabStorage.Instance.CurrentEnemy.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            PrefabStorage.Instance.CurrentEnemy.transform.position = Vector3.zero;

        }
        else
        {
            ragdoll.StopAllCoroutines();
            ragdoll.Parameters.SafetyResetAfterCouroutinesStop();
            ragdoll.User_SwitchAnimator(null, true);
            ragdoll.User_ForceRagdollToAnimatorFor(0.75f, 0.2f);
            ragdoll.Parameters.FreeFallRagdoll = false;
            ragdoll.User_FadeMuscles(0.85f, 1f, 0.05f);
            ragdoll.User_FadeRagdolledBlend(0f, 1.25f);
            ragdoll.User_RepositionRoot(null, null, CanGetUp, snapToGroundLayer);

            TryPlayGetupAnimation();
            PrefabStorage.Instance.CurrentEnemy.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            PrefabStorage.Instance.CurrentEnemy.transform.position = Vector3.zero;
        }
    }

    void TryPlayGetupAnimation()
    {
        Animator anim = GetComponentInChildren<Animator>();
        if (anim)
        {
            string animationClip = "GetUpFace";

            if (CanGetUp == RagdollProcessor.EGetUpType.FromBack)
                animationClip = "GetUpBack";

            anim.Play(animationClip, 0, 0f);

            UnityEngine.Debug.Log("[Ragdoll Animator] There you can trigger playing get-up animation! Package is not including any get up animation.");
        }
    }

    protected bool isDead()
    {
        if (_hp <= 0) return true;

        return false;
    }
}
