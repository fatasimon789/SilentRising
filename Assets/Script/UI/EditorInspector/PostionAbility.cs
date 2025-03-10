using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityPostion : PropertyAttribute
{
    // hien 1 trong 2 button
    [SerializeField] public string name;
    [SerializeField] public Transform centerPostion;
    [SerializeField] public Vector3 rangeExtendBoxCollider;
    [SerializeField] public float rangeRadiusCircleCollider;
    [SerializeField] public bool isBoxPostion;
    [SerializeField] public bool isCirclePostion;
}
