using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyHP 
{
    float hpValue { get; set; }
    float critValue { get; set; }
    float multiplyCrit { get; set; }
    bool isTakingDamages { get; set; }
    float damageReceive { get; set; }
    void takeDamages(float TAKE_DAMAGES);

}
