using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class FireSword : TypeOfWeapon
{
    public FireSword(WeaponManager WEAPON_MANAGER, WeaponTypeMachine WEAPON_TYPE_MACHINE) : base(WEAPON_MANAGER, WEAPON_TYPE_MACHINE)
    {
    }

    public float critValue { get ; set ; }
    public int attackRange { get ; set ; }
    public int cd { get; set; }
    #region  Weapon Chance / Delete
    public override void ChanceNewWeapon()
    {
        base.ChanceNewWeapon();
        // them vao day firesword
        Debug.Log("Fire sword ");
    }
    public override void DeleteOldWeapon()
    {
        base.DeleteOldWeapon();
        // xoa vao day firesword
    }
    #endregion
    #region Basic Ability
    public override void Dashing()
    {
        base.Dashing();
    }


    public override void Healing()
    {
        base.Healing();
    }

    public override void NormalAttack()
    {
        base.NormalAttack();
    }
    #endregion
    #region Ability Weapon
    public override void Passive()
    {
        base.Passive();
        // crease 30 crit rate value 
    }
    public override void FirstSkill()
    {
        base.FirstSkill();
      // anim
      // CD  5S
    }

    public override void SecondSkill()
    {
        base.SecondSkill();
       // anim
       // 6S
    }

    public override void UltimateSkill()
    {
        base.UltimateSkill();
       // anim
       //20S
    }
    #endregion

    #region Main Method
    public override void triggerAbilitySkill(PlayerTriggerEventAnim.AbilityTriggerType triggerAbility)
    {
        base.triggerAbilitySkill(triggerAbility);
        switch (triggerAbility) 
        {
            // COLLIDER
            case PlayerTriggerEventAnim.AbilityTriggerType.FirstAbi:
                FirstAbilityCollider();
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.SecondAbi:
                SecondAbilityCollider();
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.UltimateAbi:
                UltimateAbilityCollider();
                break;
            // VFX
            case PlayerTriggerEventAnim.AbilityTriggerType.EndAnimAbility:
                OffVfx();
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxFirstAbiSword:
                Player.instance.playerDataEffect.G_AbilityQ.SetActive(true);
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxSecondAbiSword:
                Player.instance.playerDataEffect.G_AbilityE.SetActive(true);
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxUltimateAbiSword:
                break;
        }
    }

    public void FirstAbilityCollider() 
    {
        AttackColliderAbility(ColiderDamagesQ());
    }
    public void SecondAbilityCollider() 
    {
        AttackColliderAbility(ColiderDamagesE());
    }
    public void UltimateAbilityCollider() 
    {
        AttackColliderAbility(ColiderDamagesR());
    }
    #endregion
    public void OffVfx() 
    {
        // Q VFX
           Player.instance.playerDataEffect.G_AbilityQ.SetActive(false);
        // E VFX
           Player.instance.playerDataEffect.G_AbilityE.SetActive(false);
        // R VFX

        // Attack VFX
        Player.instance.playerMovementStateMachine.attackSwordState.vfxAllOff();
    }
    #region Resauble Method
    private Collider[] ColiderDamagesQ()
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapBox(WeaponManager.updatingPosAbi.colliderPosQ.position,
                                                     WeaponManager.updatingPosAbi.localColliderHalfExtendQ, Quaternion.identity);
        return colliderInfo;
    }
    private Collider[] ColiderDamagesE()
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapBox(WeaponManager.updatingPosAbi.colliderPosE.position,
                                                     WeaponManager.updatingPosAbi.localColliderHalfExtendE, Quaternion.identity);
        return colliderInfo;
    }
    private Collider[] ColiderDamagesR()
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapBox(WeaponManager.updatingPosAbi.colliderPosR.position,
                                                     WeaponManager.updatingPosAbi.localColliderHalfExtendR, Quaternion.identity);
        return colliderInfo;
    }
    private void AttackColliderAbility(Collider[] ABILITY_COL)
    {
        foreach (Collider col in ABILITY_COL)
        {
            Vector3 closetPoint = col.ClosestPoint(Player.instance.transform.position); // diem collider gan nhat

            Vector3 posDifferent = (closetPoint - Player.instance.transform.position); //  chi ra huong khi va cham lan dau
                                                                                          //  va den trung tam collider
            Vector3 overlapDirection = posDifferent.normalized;

            RaycastHit hit;
            int layerMask0 = 1;  // Set to something that will only hit your object
            float raycastDistance = 10.0f; // something greater than your object's largest radius, 
                                           // so that the ray doesn't start inside of your object
            Vector3 rayStart = Player.instance.transform.position + overlapDirection * raycastDistance;
            Vector3 rayDirection = -overlapDirection;

            if (Physics.Raycast(rayStart, rayDirection, out hit, Mathf.Infinity, layerMask0))
            {
                //   Player.instance.statsSystem.basicTakeDamages(stats.attack);
            }
            else
            {
                // The ray missed your object, somehow. 
                // Most likely it started inside your object 
                // or there is a mistake in the layerMask
            }
        }
    }
    #endregion
}
