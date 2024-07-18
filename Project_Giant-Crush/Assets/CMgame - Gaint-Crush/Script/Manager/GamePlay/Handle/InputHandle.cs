using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandle : MonoBehaviour
{
    [SerializeField] public Camera _camera;

    public Vector3 GetMousePos()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = Camera.main.nearClipPlane + 0.3f;
        return MousePos;
    }
}
