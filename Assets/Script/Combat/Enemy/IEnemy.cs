using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  IEnemy 
{
    public SystemEnemyStats stats { get; set; }
    public EnemyHP enemyHP { get; set; }

    [SerializeField] public Transform colliderPos { get; set; }
    [SerializeField] public Vector3 localColliderHalfExtend { get; set; }
    [SerializeField] public LayerMask layerMask { get; set; }
}
