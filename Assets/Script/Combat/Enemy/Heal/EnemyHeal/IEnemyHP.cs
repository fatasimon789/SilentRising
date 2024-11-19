using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyHP 
{
    float hpValue { get; set; }

    void takeDamages(int TAKE_DAMAGES);

}
