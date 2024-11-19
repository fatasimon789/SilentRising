using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRoblin : MonoBehaviour,IEnemy
{
    protected EnemyRoblin enemyRoblin;
    [field: SerializeField] public SystemEnemyStats statsRobilin { get; set; }
    public SystemEnemyStats stats { get ; set ; }
    public EnemyHP enemyHP { get ; set ; }
    public Transform colliderPos { get ; set ; }
    public Vector3 localColliderHalfExtend { get ; set ; }
    public LayerMask layerMask { get ; set; }

    private void Awake()
    {
        enemyRoblin = new EnemyRoblin(this);
    }
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    public void WaitToAttack()
    {
        StartCoroutine(enemyRoblin.CanAttack(enemyRoblin.delayAttack));
    }
}
