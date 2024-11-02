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
    public bool isAttack { get; set; } = true;
    public bool isDead { get; set; }
    public float delayAttack { get; set; } 

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
        if (distanceEnemy <= attackRange && isAttack)
        {
            Attack(isAttack);

            return;
        }
        // -1
        if (isAttack)
        {
            OnVision(visionRange);
        }

    }
    public virtual void Attack(bool INTO_RANGES)
    {

        animator.SetTrigger("Attacking");
        // collider damages
        //effect slash
        isAttack = false;
        WaitToAttackAgain(EnemyID);
        Debug.Log("attack");
    }
    public  virtual void OnVision(float VISION_RANGES)
    {
        Debug.Log(distanceEnemy + "  distance");

        if (distanceEnemy < VISION_RANGES)
        {
            //0

            OnRange(chasingSpeed);
        }
        else
        {
            animator.SetTrigger("Idling");
        }
    }
    public virtual void OnRange(float CHASING_RANGE)
    {
        // 1
        // Chasing
        RGB.MovePosition(EnemyMovement(CHASING_RANGE));
        animator.SetTrigger("Chasing");

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
