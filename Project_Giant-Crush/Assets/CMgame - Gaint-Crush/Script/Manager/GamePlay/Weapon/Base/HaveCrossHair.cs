using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.EventSystems;

public class HaveCrossHair : WeaponBase, ICrossHair
{
    [SerializeField] public GameObject _CrossHair;

    [SerializeField] protected BulletBase _bullet;
    [SerializeField] protected Transform _ShotPos;


    private Vector2 bottomLeft;
    private Vector2 topRight;

    private Vector3 _startPos, _endPos;

    private bool isDragging = false;
    private Vector3 lastMousePosition;

    protected override void Start()
    {
        base.Start();
        CreateCrossHair();
        InitWidthHeghtScreen();
    }

    protected override void Update()
    {
        base.Update();
        if (!GameManager.instance.isInGame) return;

        if (Input.GetMouseButton(0) && _canShot)
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                Debug.Log("da click vao UI");
                _canShot = false;
                InitTimeShot();
                return;
            }

            _canShot = false;
            Shooting();
        }


        isCanShot();

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            MoveCrossHair();
            RotateAround();
        }
    }

    protected virtual void InitWidthHeghtScreen()
    {

        bottomLeft = new Vector2(0, 0);
        topRight = new Vector2(1, 1);
    }

    public void CreateCrossHair()
    {
        if (_CrossHair) return;

        GameObject CrossHairClone = Instantiate(PrefabStorage.Instance.ViewFinder, Vector3.zero, Quaternion.identity);
        CrossHairClone.transform.localScale = Vector3.one * 0.1f;
        CrossHairClone.transform.position = Vector3.zero;
        CrossHairClone.transform.parent = transform;

        Vector3 _crossFollowEnemy = new Vector3(CrossHairClone.transform.localPosition.x, CrossHairClone.transform.localPosition.y, 6.5f);
        CrossHairClone.transform.localPosition = _crossFollowEnemy;

        _CrossHair = CrossHairClone;
    }

    protected virtual void MoveCrossHair()
    {
        /*        Vector3 MousePos = Input.mousePosition;

                Vector3 LimitMin = PrefabStorage.Instance.inputHandle._camera.ViewportToWorldPoint(bottomLeft);
                Vector3 LimitMax = PrefabStorage.Instance.inputHandle._camera.ViewportToWorldPoint(topRight);

                float ClampX = Mathf.Clamp(MousePos.x, LimitMin.x + 0.5f, LimitMax.x - 0.5f);
                float ClampY = Mathf.Clamp(MousePos.y, LimitMin.y + 0.5f, LimitMax.y - 0.5f);


                _CrossHair.transform.position = new Vector3(ClampX, ClampY, MousePos.z);*/

    }

    protected virtual void RotateAround()
    {
        if (_CrossHair != null)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - lastMousePosition;
            mouseDelta.z = 0f;

            float rotationX = -mouseDelta.y * 0.05f;
            float rotationY = mouseDelta.x * 0.05f;

            // Get the current rotation angles
            Vector3 currentRotation = transform.eulerAngles;

            // Clamp the X rotation
            currentRotation.x = Mathf.Clamp(NormalizeAngle(currentRotation.x + rotationX), -32.5f, 0);

            // Clamp the Y rotation
            currentRotation.y = Mathf.Clamp(NormalizeAngle(currentRotation.y + rotationY), -14.5f, 12f);

            // Apply the clamped rotation
            transform.eulerAngles = currentRotation;

            lastMousePosition = currentMousePosition;
        }

        // maxY = 11 minY -13
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 180f)
        {
            angle -= 360f;
        }
        while (angle < -180f)
        {
            angle += 360f;
        }
        return angle;
    }

    protected override void Shooting()
    {
        Debug.Log("Have");
    }

    private void OnDisable()
    {
        Destroy(_CrossHair);
    }
}
