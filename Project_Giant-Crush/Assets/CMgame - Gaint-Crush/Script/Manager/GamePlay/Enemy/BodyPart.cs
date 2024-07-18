using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] private E_BodyPart _bodyType;

    public E_BodyPart BodyType { get => _bodyType; set => _bodyType = value; }

}
