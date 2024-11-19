using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : IEnemyHP
{
    public float hpValue { get ; set; }
    public EnemyHP(float HP) 
    {
        hpValue = HP;
    }
    public void takeDamages(int TAKE_DAMAGES)
    {
        hpValue -= TAKE_DAMAGES;
      //  Debug.Log("current hp = " + hpValue);
    }

   
}
