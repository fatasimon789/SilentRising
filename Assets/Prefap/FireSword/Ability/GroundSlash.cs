using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlash : MonoBehaviour
{
    [field: SerializeField] public float speed = 5f;
    [field: SerializeField] public float slowRate = 0.6f;
    [field: SerializeField] public float detectingDistance = 0.1f;
    [field: SerializeField] public float disapearTime = 5f;
    [field: SerializeField] public LayerMask layerMask { get; set; }

    //public WeaponManager weaponManager { get; set; }
   
    public Rigidbody rgb { get; set; }
   
    private bool isStopped;
    void Start()
    {
        
        rgb = GetComponent<Rigidbody>();
      //  weaponManager = GetComponentInParent<WeaponManager>();
       
        StartCoroutine(SlowDown());
        Destroy(this.gameObject,disapearTime);
    }

   
    void Update()
    {
       
        CheckTheGround(); 
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy")) 
        { 
          var targetInfo = collision.gameObject.GetComponent<IEnemy>();
          targetInfo.enemyHP.takeDamages(WeaponManager.instance.SystemSkillWeapon.dameges * WeaponManager.instance.SystemSkillWeapon.multiR);
        }
    }
   
    private void CheckTheGround() 
    {
        if (!isStopped) 
        {
            RaycastHit hit;
              Vector3 distance = new Vector3(transform.position.x,transform.position.y + 1,transform.position.z);
           
            if (Physics.Raycast(distance,transform.TransformDirection(-Vector3.up),out hit,detectingDistance,layerMask)) 
            {
               transform.position = new Vector3(transform.position.x,hit.point.y,transform.position.z);
                
            }
            else 
            {
               transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
               
            }
            Debug.DrawRay(distance, transform.TransformDirection(-Vector3.up * detectingDistance),Color.red);
        }
    }
    IEnumerator SlowDown() 
    {
        float t = 1;
        while (t > 0 ) 
        {
            rgb.velocity = Vector3.Lerp(Vector3.zero,rgb.velocity,t);
            t -= slowRate;
            yield return new WaitForSeconds(0.1f);
        }
        isStopped= true;
    }
    
}
