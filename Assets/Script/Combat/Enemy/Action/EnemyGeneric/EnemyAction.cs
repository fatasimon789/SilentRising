using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAction : IMeleeEnemy
{
    #region  Enemy Initlization 
    protected GameObject theEnemy { get; private set; }

    protected TheWolf wolf { get;  set; }
    protected TheRoblin roblin { get; private set; }
   
    #endregion
    #region Enemy Action Field
    public float distanceEnemy { get; set; } 
    public float damages { get;  set ; }
    public float hp { get; set; }
    public bool isAttack { get; set; } = true;
    public bool isDead { get; set; }
    public bool isChasing { get; set; }
    public bool isIdling { get; set; }
    public float delayAttack { get; set; } 

    public Vector3 direction { get; set; }
    public Quaternion rotation { get; set; }

    public Rigidbody RGB { get ; set ; }
    public Transform playerPos { get; set; }
    public Animator animator { get; set; }
    public float attackRange { get; set; } 
    public float chasingSpeed { get; set; } 
    public float visionRange { get; set; }
    #endregion
    #region Resauble Refrence  Field
    public int EnemyID { get; set; }
    #endregion
    public EnemyAction(TheWolf ENEMY) 
    {
        wolf = ENEMY;
        theEnemy = wolf.gameObject;
        EnemyID = 1;
    } 
    public EnemyAction(TheRoblin ROBLIN) 
    {
        roblin = ROBLIN;
        theEnemy= ROBLIN.gameObject;
        EnemyID = 2;
    }
    public virtual void UpdateAction()
    {
        UpdateDistance();
        if (distanceEnemy <= attackRange && isAttack && !isDead)
        {
            EnemyAttackRotate();
            Attack(isAttack);
         
            return;
          
        }
        // 0
        if (isAttack && !isDead)
        {
            OnVision(visionRange);
            EnemyRotate();
        }
    }
    public virtual void Attack(bool INTO_RANGE)
    {
        animator.SetBool("Attacking",true);
        isAttack = false;
        WaitToAttackAgain(EnemyID);
        
    }
    // 1 VISION
    public  virtual void OnVision(float VISION_RANGES)
    {
        // VO RANGE VISION THI SE CHASING
        // NEU DANG DANH THI SE NGUNG DANH VA CHUYEN LAI CHASING
        if (distanceEnemy <= VISION_RANGES)
        {
            animator.SetBool("Attacking",false);
            OnRange(chasingSpeed);
        }
        // OUT RANGE THI DUNG LAI
        else if(distanceEnemy > VISION_RANGES )
        {
            animator.SetBool("Chasing", false);
        }

    }
    // 2 CHASING
    public virtual void OnRange(float CHASING_RANGE)
    {
        
        RGB.MovePosition(EnemyMovement(CHASING_RANGE));
        animator.SetBool("Chasing", true);
       
    }
    private void EnemyAttackRotate() 
    {
        direction = playerPos.transform.position - theEnemy.transform.position;
        theEnemy.transform.rotation = Quaternion.LookRotation(direction);
    }
    private void EnemyRotate()
    {
        var rotateSpeed = 20f;
       
        direction = playerPos.transform.position - theEnemy.transform.position;
        rotation = Quaternion.LookRotation(direction);
        theEnemy.transform.rotation = Quaternion.RotateTowards(theEnemy.transform.rotation, rotation, rotateSpeed); ;
    }

    #region Resauble Method

    // 2
    private Vector3 EnemyMovement(float CHASING_SPEED)
    {
        Vector3 target = playerPos.position;
        Vector3 enemyMovement = Vector3.MoveTowards(theEnemy.transform.position, target, CHASING_SPEED * Time.deltaTime);

        return enemyMovement;
    }
    //3
    private Vector2 PlayerPos()
    {
        Vector2 playerCurrentPos = new Vector2(playerPos.position.x, playerPos.position.z);
        return playerCurrentPos;
    }
    //3
    private Vector2 EnemyPos()
    {
        Vector2 enemyCurrentPos = new Vector2(theEnemy.transform.position.x, theEnemy.transform.position.z);
        return enemyCurrentPos;
    } // 3
    private void UpdateDistance()
    {
        distanceEnemy = Vector2.Distance(PlayerPos(), EnemyPos());
    }
  
    private void WaitToAttackAgain(int ID) 
    {
        switch(ID) 
        {
            case(1):
                wolf.WaitToAttack();
                break;
            case(2):
                roblin.WaitToAttack();
                break;
            default: 
                break;
        }
    }
    public IEnumerator CanAttack(float TIME_DELAY)
    {
        yield return new WaitForSeconds(TIME_DELAY);
        isAttack = true;
    }

    #endregion

}
