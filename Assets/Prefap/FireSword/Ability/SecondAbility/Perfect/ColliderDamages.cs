using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColliderDamages : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var targetInfo = other.gameObject.GetComponent<IEnemy>();
            targetInfo.enemyHP.takeDamages(UpdatingAbility.instance.PerfectDMGV3("E"), WeaponManager.instance.DamagesType);
        }
    }
}
