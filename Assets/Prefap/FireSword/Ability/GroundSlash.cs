using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlash : MonoBehaviour
{
    [field: SerializeField] public float speed = 30f;
    [field: SerializeField] public float slowRate = 0.6f;
    [field: SerializeField] public float detectingDistance = 0.1f;
    [field: SerializeField] public float disapearTime = 5f;
    [field: SerializeField] public LayerMask layerMask { get; set; }

    private Rigidbody _rgb;
    private bool isStopped;
    void Start()
    {
        transform.position = new Vector3(this.transform.position.x, 0 ,this.transform.position.z);
        _rgb = GetComponent<Rigidbody>();
        StartCoroutine(SlowDown());
        Destroy(this.gameObject,disapearTime);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTheGround();
    }
    private void CheckTheGround() 
    {
        if (!isStopped) 
        {
            RaycastHit hit;
            Vector3 distance = new Vector3(transform.position.x,transform.position.y + 1,transform.position.z);
            if (Physics.Raycast(distance,transform.TransformDirection(-Vector3.up),out hit,detectingDistance,layerMask)) 
            {
               transform.position = new Vector3 (transform.position.x,hit.point.y,transform.position.z);
            }
            else 
            {
               transform.position = new Vector3 (transform.position.x,0,transform.position.z);
            }
            Debug.DrawRay(distance, transform.TransformDirection(-Vector3.up * detectingDistance),Color.red);
        }
    }
    IEnumerator SlowDown() 
    {
        float t = 1;
        while (t > 0 ) 
        {
            _rgb.velocity = Vector3.Lerp(Vector3.zero,_rgb.velocity,t);
            t -= slowRate;
            yield return new WaitForSeconds(0.1f);
        }
        isStopped= true;
    }
    
}
