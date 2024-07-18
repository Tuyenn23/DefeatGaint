using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVirtual : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camVirtual;
    [SerializeField] private Vector3 _EnemyPos;

    private void Update()
    {
        FollowCamera();
    }

    private void FollowCamera()
    {
        _EnemyPos = new Vector3(PrefabStorage.Instance.CurrentEnemy.transform.position.x, PrefabStorage.Instance.CurrentEnemy.transform.position.y, 0f);
        transform.position = Vector3.Lerp(transform.position, _EnemyPos, 2f * Time.deltaTime);
    }
}   
