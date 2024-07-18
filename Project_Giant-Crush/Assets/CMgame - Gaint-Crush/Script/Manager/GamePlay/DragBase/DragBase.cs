using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragBase : MonoBehaviour
{
    [SerializeField] protected Vector3 _startPos;
    [SerializeField] protected Vector3 _endPos;
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected bool _isDrag;
    protected void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected virtual void OnMouseDown()
    {
    }
    protected abstract void OnMouseDrag();

    protected virtual void OnMouseUp()
    {
    }
}
