using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  IProjectile 
{
    float speed { get; set; }
    public float slowRate { get; set; }
    public float detectingDistance { get; set; }
    public float disapearTime { get; set; }
    public LayerMask layerMask { get; set; }

    public Rigidbody rgb { get; set; }


}
