using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour,IEnemyHP
{
    public  SystemEnemyStats systemStats ;
    public float hpValue { get ; set; }
    public bool isDead { get ; set; }

    public void takeDamages()
    {
        
    }

   
}
