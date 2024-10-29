using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWolf :MonoBehaviour,IMeleeEnemy
{
    public float distanceEnemy { get; set; } 
    public float damages { get;  set ; }
    public bool isAttack { get; set; } = true;
    public bool isDead { get; set; }
    public float delayAttack { get; set; } = 1.5f;

    public Rigidbody RGB { get ; set ; }
    public Transform playerPos { get; set; }
    public Animator animator { get; set; }
    public float attackRange { get; set; } = 2f;
    public float chasingSpeed { get; set; } = 3.5f;
    public float visionRange { get; set; } = 10f;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        RGB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
       
        if (distanceEnemy <= attackRange && isAttack) 
        {
            UpdateDistance();
            Attack(isAttack);
          
            return;
        }
        // -1
        if (isAttack) 
        {
           OnVision(visionRange);
        }
        
    }
    #region Main Method
    public void Attack(bool INTO_RANGES)
    {
        animator.SetTrigger("Attacking");
        // collider damages
        //effect slash
        isAttack = false;
        StartCoroutine(CanAttack(delayAttack));
    }


    // Vision
    public void OnVision(float VISION_RANGES)
    {
         UpdateDistance();
       
        if (distanceEnemy < VISION_RANGES)
        {
            //0
            OnRange(chasingSpeed);
        }else  
        {
            animator.SetTrigger("Idling");
        }
    }
    // Onrange start moving
    public void OnRange(float CHASING_RANGE)
    {
        // 1
        // Chasing
           RGB.MovePosition(EnemyMovement(CHASING_RANGE));
        animator.SetTrigger("Chasing");
        
    }
    #endregion 
    #region Resauble Method
    // 2
    private Vector3 EnemyMovement(float CHASING_SPEED)
    {
        Vector3 target = playerPos.position;
        Vector3 enemyMovement = Vector3.MoveTowards(this.transform.position, target, CHASING_SPEED * Time.deltaTime);
    
        return enemyMovement; 
    }
    //3
    private Vector2 PlayerPos() 
    {
        Vector2 playerCurrentPos = new Vector2(playerPos.position.x,playerPos.position.z);
        return playerCurrentPos;
    }
    //3
    private Vector2 EnemyPos() 
    {
        Vector2 enemyCurrentPos = new Vector2(this.transform.position.x, this.transform.position.z);
        return enemyCurrentPos;
    } // 3
    private void UpdateDistance() 
    {
        distanceEnemy = Vector2.Distance(PlayerPos(), EnemyPos());
    }
    private IEnumerator CanAttack(float TIME_DELAY) 
    {
        yield return new WaitForSeconds(TIME_DELAY);
        isAttack = true;
    }
    #endregion
}
