using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpdatingPositionAbility 
{
    
    [SerializeField] public Transform colliderPosQ ;
    [SerializeField] public Vector3 localColliderHalfExtendQ ;
  
    [SerializeField] public Transform colliderPosE ;
    [SerializeField] public Vector3 localColliderHalfExtendE;
    [SerializeField] public Transform colliderPosR ;
    [SerializeField] public Vector3 localColliderHalfExtendR;
    [SerializeField] public Transform colliderPosAttack;
    [SerializeField] public Vector3 localColliderHalfExtendAttack;
}
