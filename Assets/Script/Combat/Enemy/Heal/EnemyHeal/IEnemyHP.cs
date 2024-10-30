using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyHP 
{
    float hpValue { get; set; }

    bool isDead { get; set; }

    void takeDamages();

}
