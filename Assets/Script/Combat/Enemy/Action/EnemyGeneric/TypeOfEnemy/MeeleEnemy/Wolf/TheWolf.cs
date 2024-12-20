using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TheWolf : MonoBehaviour,IEnemy
{
    protected EnemyWolf enemyWolf;
    [field:SerializeField] public SystemEnemyStats stats { get; set; }
    public EnemyHP enemyHP { get; set; }

    [field:SerializeField] public Transform colliderPos { get; set; }
    [field:SerializeField] public Vector3 localColliderHalfExtend { get; set; }
    [field:SerializeField] public LayerMask layerMask { get; set; }
    [field:SerializeField]public GameObject floatingDamages { get ; set ; }

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
        // Initlize Basic Value
        enemyHP = new EnemyHP(enemyWolf.hp);
       
    }

    // Update is called once per frame
    void Update()
    {
        enemyWolf.playerPos = GameObject.FindGameObjectWithTag("Player").transform;
     
        enemyWolf.UpdateAction();
        CheckingDamages();
        VirtualBox.DisplayBox(colliderPos.position, localColliderHalfExtend, Quaternion.identity);
        CheckHP();
       
    }
    public void AttackCollider0()
    {
        foreach (Collider col in enemyWolf.ColiderDamages1())
        {
           
            Vector3 closetPoint = col.ClosestPoint(this.transform.position); // diem collider gan nhat

            Vector3 posDifferent = (closetPoint - this.transform.position); //  chi ra huong khi va cham lan dau
                                                                            //  va den trung tam collider
            Vector3 overlapDirection = posDifferent.normalized;

            RaycastHit hit;
          //  int layerMask0 = 1;  // Set to something that will only hit your object
            float raycastDistance = 10.0f; // something greater than your object's largest radius, 
                                           // so that the ray doesn't start inside of your object
            Vector3 rayStart = this.transform.position + overlapDirection * raycastDistance;
            Vector3 rayDirection = -overlapDirection;

            if (Physics.Raycast(rayStart, rayDirection, out hit, Mathf.Infinity, layerMask))
            {
                Player.instance.statsSystem.basicTakeDamages(this.stats.attack);
               
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

    public void CheckingDamages()
    {
        if (floatingDamages && enemyHP.isTakingDamages) 
        {
            FloatingDamagesUI();
        }
    }
    public void CheckHP() 
    {
        if (enemyHP.hpValue <= 0  && !enemyWolf.isDead) 
        {
            enemyWolf.animator.SetTrigger("Death");
            enemyWolf.isDead= true;
            Destroy(this.gameObject,3f);
        }
        else if (enemyHP.hpValue != enemyHP.oldHP && !enemyWolf.isDead) 
        {
            enemyHP.oldHP = enemyHP.hpValue;
            enemyWolf.animator.SetTrigger("GotDamages");
        }
    }
    public void FloatingDamagesUI()
    {
       
       var  addDamagesText = Instantiate(floatingDamages,this.transform.position,Quaternion.identity);
        addDamagesText.GetComponent<TextMesh>().text = enemyHP.damageReceive.ToString();
        
        enemyHP.isTakingDamages = false;
    }
    
    
}
