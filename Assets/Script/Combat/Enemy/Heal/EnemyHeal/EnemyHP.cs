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
    public float oldHP { get; set; }
    public Color colorElement { get ; set ; }

    public EnemyHP(float HP) 
    {
        hpValue = HP;
        oldHP = HP;
    }
    public void takeDamages(float TAKE_DAMAGES,CombatTypeManager.TypeElement ELEMENTAL_INFO)
    {
        var elementalInfo = CombatTypeManager.TypeElementInfo(ELEMENTAL_INFO);
        
        if (CritRandom()) 
        {
            multiplyCrit= 2;
        }
        else 
        {
            multiplyCrit= 1;
        }
        damageReceive = TAKE_DAMAGES * multiplyCrit;
        hpValue -= damageReceive;
        isTakingDamages = true;
        colorElement = elementalInfo.colorWeapon;
        
    }
    private bool CritRandom() 
    {
        critValue = Player.instance.statsSystem.critRateValue / 100;
        var critRandom = Random.value <= critValue;
      //  Debug.Log(critRandom);
        return critRandom;
    }
   
}
