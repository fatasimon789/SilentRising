using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TheWolf : MonoBehaviour
{
    public SystemEnemyStats stats;
    protected EnemyWolf enemyWolf;

    [field:SerializeField] public Transform colliderPos { get; set; }
    [field:SerializeField] public Vector3 localColliderHalfExtend { get; set; }
    [field:SerializeField] public LayerMask layerMask { get; set; }

   // public Vector3 colliderPos { get; set; }
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
        enemyWolf.hp = stats.hp;

    }

    // Update is called once per frame
    void Update()
    {
        enemyWolf.playerPos = GameObject.FindGameObjectWithTag("Player").transform;
     
        enemyWolf.UpdateAction();
        VirtualBox.DisplayBox(colliderPos.position, localColliderHalfExtend, Quaternion.identity);
    }
    public void AttackCollider()
    {
        foreach (Collider col in enemyWolf.ColiderDamages1())
        {
            Vector3 closetPoint = col.ClosestPoint(this.transform.position); // diem collider gan nhat
            
            Vector3 posDifferent = (closetPoint - this.transform.position); //  chi ra huong khi va cham lan dau
                                                                            //  va den trung tam collider
            Vector3 overlapDirection = posDifferent.normalized;

            RaycastHit hit;
            int layerMask0 = 1;  // Set to something that will only hit your object
            float raycastDistance = 10.0f; // something greater than your object's largest radius, 
                                           // so that the ray doesn't start inside of your object
            Vector3 rayStart = this.transform.position + overlapDirection * raycastDistance;
            Vector3 rayDirection = -overlapDirection;

            if (Physics.Raycast(rayStart, rayDirection, out hit, Mathf.Infinity, layerMask0))
            {
                Player.instance.statsSystem.basicTakeDamages(stats.attack);
            }
            else
            {
                // The ray missed your object, somehow. 
                // Most likely it started inside your object 
                // or there is a mistake in the layerMask
            }
        }
        
    }
    public void WaitToAttack() 
    {
        StartCoroutine(enemyWolf.CanAttack(enemyWolf.delayAttack));
    }
    public void EventFinishAnimInfo() 
    {
       // enemyWolf.hasDoneAnim = true;
    }
   
}
