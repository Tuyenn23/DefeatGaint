using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DontHaveCrossHair : WeaponBase
{
    [SerializeField] protected BulletBase _bullet;
    [SerializeField] protected Transform _ShotPos;
    [SerializeField] protected Vector3 _force;
    [SerializeField] protected float _distance;

    [Header("Allonger")]
    protected Vector3 _startPoint, _endPoint, _dir;


    [Header("LineRenderer")]
    [SerializeField] protected LineRenderer _lineRenderer;
    [SerializeField] protected int _AmoutlinePoints = 200;
    [SerializeField] protected float _timeInterPoints = 0.01f;
    [SerializeField] protected float _clampDistance;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && _canShot)
        {
            _startPoint = PrefabStorage.Instance.inputHandle.GetMousePos();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateDrag();
            DrawTrajectory();
            _lineRenderer.enabled = true;
        }
        else
        {
            _lineRenderer.enabled = false;
            _lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButtonUp(0) && _canShot)
        {
            _canShot = false;
            Shooting();
        }

        isCanShot();
    }

    protected virtual void UpdateDrag()
    {
        _endPoint = PrefabStorage.Instance.inputHandle.GetMousePos();
        _distance = Vector3.Distance(_endPoint, _startPoint);
        _dir = (_startPoint - _endPoint).normalized;

        Vector3 Clone = new Vector3(_dir.x, _dir.y, _dir.y);

        _force = Clone * _distance * 10f;
    }

    protected override void Shooting()
    {
    }

    private void DrawTrajectory()
    {
        Vector3 _startVelocity = _force;
        Vector3 _origin = _ShotPos.position;
        _lineRenderer.positionCount = _AmoutlinePoints;

        float _time = 0;
        for (int i = 0; i < _AmoutlinePoints; i++)
        {
            var x = (_startVelocity.x * _time) + (Physics.gravity.x / 2 * _time * _time);
            var y = (_startVelocity.y * _time) + (Physics.gravity.y / 2 * _time * _time);
            var z = (_startVelocity.z * _time) + (Physics.gravity.z / 2 * _time * _time);


            Vector3 point = new Vector3(x, y, z);

            _lineRenderer.SetPosition(i, _origin + point);
            _time += _timeInterPoints;
        }

    }
}
