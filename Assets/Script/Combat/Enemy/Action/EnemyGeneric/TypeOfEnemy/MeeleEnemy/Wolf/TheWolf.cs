using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWolf : MonoBehaviour
{
    public SystemEnemyStats stats;
    protected EnemyWolf enemyWolf;

    [field:SerializeField] public Vector3 colliderPos { get; set; }
    [field:SerializeField] public Vector3 colliderHalfExtend { get; set; }

    private void Awake()
    {
        enemyWolf = new EnemyWolf(this);
       
    }
    void Start()
    {
        enemyWolf.RGB = GetComponent<Rigidbody>();
        enemyWolf.animator = GetComponent<Animator>();
       
        // start updating 
        enemyWolf.delayAttack = stats.delayAttack;
        enemyWolf.attackRange = stats.attackRange;
        enemyWolf.chasingSpeed = stats.chasingSpeed;
        enemyWolf.visionRange = stats.visionRange;
    }

    // Update is called once per frame
    void Update()
    {
        enemyWolf.playerPos = GameObject.FindGameObjectWithTag("Player").transform;
     
        enemyWolf.UpdateAction();
    }
    public void AttackCollider()
    {
        if (enemyWolf.ColiderDamages1() != null)
        {
            Player.instance.statsSystem.basicTakeDamages(stats.attack);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(colliderPos, colliderHalfExtend);
    }
    public void WaitToAttack() 
    {
        StartCoroutine(enemyWolf.CanAttack(enemyWolf.delayAttack));
    }
    public void EventColiderAttack() 
    {
        enemyWolf.ColiderDamages1();
    }
}
