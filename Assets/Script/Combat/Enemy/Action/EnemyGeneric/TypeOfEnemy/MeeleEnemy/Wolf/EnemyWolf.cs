using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWolf : MonoBehaviour,IEnemy
{
    public SystemEnemyStats stats;
    protected EnemyAction enemyAction;

    private void Awake()
    {
        enemyAction = new EnemyAction(this);
    }
    private void Start()
    {
        enemyAction.playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAction.RGB = GetComponent<Rigidbody>();
        enemyAction.animator = GetComponent<Animator>();
        // start updating 
        enemyAction.delayAttack = stats.delayAttack;
        enemyAction.attackRange = stats.attackRange;
        enemyAction.chasingSpeed = stats.chasingSpeed;
        enemyAction.visionRange= stats.visionRange;
    }
    private void Update()
    {
        enemyAction.UpdateAction();
    }
    public void WaitToAttack() 
    {
        StartCoroutine(CanAttack(enemyAction.delayAttack));
    }
    #region Resauble Method
    #endregion
    private IEnumerator CanAttack(float TIME_DELAY)
    {
        yield return new WaitForSeconds(TIME_DELAY);
        enemyAction.isAttack= true;
       
    }
}
