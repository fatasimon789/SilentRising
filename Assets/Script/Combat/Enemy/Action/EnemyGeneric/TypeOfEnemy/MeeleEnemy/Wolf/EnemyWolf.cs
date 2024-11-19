using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyWolf : EnemyAction
{

    public EnemyWolf(TheWolf ENEMY) : base(ENEMY)
    {
        wolf = ENEMY;

    }
    #region Main Method
    public override void Attack(bool INTO_RANGE)
    {
        base.Attack(isAttack);
        // effect
        // colider
    }

    #endregion
    #region Method Event
    public Collider[] ColiderDamages1()
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapBox(wolf.colliderPos.position, wolf.localColliderHalfExtend, Quaternion.identity, wolf.layerMask);
        return colliderInfo;
    }
    #endregion
   
    #region Resauble Method
    #endregion
}
