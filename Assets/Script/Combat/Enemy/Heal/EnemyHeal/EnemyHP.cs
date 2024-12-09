using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : IEnemyHP
{
    public float hpValue { get ; set; }
    public float critValue { get ; set ; }
    public float multiplyCrit { get ; set ; }
    public float damageReceive { get; set; }
    public bool isTakingDamages { get; set; }

    public EnemyHP(float HP) 
    {
        hpValue = HP;
       
    }
    public void takeDamages(float TAKE_DAMAGES)
    {
        if (CritRandom()) 
        {
            multiplyCrit= 1;
        }
        else 
        {
            multiplyCrit= 2;
        }
        damageReceive = TAKE_DAMAGES * multiplyCrit;
        hpValue -= damageReceive;
        isTakingDamages = true;
    }
    private bool CritRandom() 
    {
        critValue = WeaponManager.instance.weaponCRIT / 100;
        var critRandom = Random.value <= critValue;
      //  Debug.Log(critRandom);
        return critRandom;
    }
   
}
