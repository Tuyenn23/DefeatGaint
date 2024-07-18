using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FIMSpace.FProceduralAnimation;

public class EnemyController : EnemyBase
{
    public List<GameObject> L_BodyPart;

    [SerializeField] private Rigidbody centerRigid;


    public UnityAction A_OnHome;
    public UnityAction A_Ondead;

    protected override void OnEnable()
    {
        base.OnEnable();

        A_OnHome += InitActionHome;
    }


    private void InitActionHome()
    {
        for (int i = 0; i < L_BodyPart.Count; i++)
        {
            L_BodyPart[i].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        A_OnHome -= InitActionHome;
    }
}
