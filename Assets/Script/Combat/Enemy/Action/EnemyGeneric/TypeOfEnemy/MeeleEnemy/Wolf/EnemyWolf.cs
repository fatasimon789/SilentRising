using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWolf : EnemyAction
{
    
    public EnemyWolf(TheWolf ENEMY) : base(ENEMY)
    {
        wolf = ENEMY;
       
    }
    #region Main Method
    public override void Attack(bool INTO_RANGES)
    {
        base.Attack(INTO_RANGES);
        // effect
        // colider
    }
   
    #endregion
    #region Method Event
    public Collider[] ColiderDamages1() 
    {
        // tao collider damags
        Collider[] colliderInfo = Physics.OverlapBox(wolf.colliderPos,wolf.colliderHalfExtend, Quaternion.identity);

        return colliderInfo;
        
        // neu collider tra ve k null thi se lay script player take daamges
    }
    #endregion
    #region Resauble Method

    #endregion


}
