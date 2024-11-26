using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePunch : TypeOfWeapon
{
    public IcePunch(WeaponManager WEAPON_MANAGER, WeaponTypeMachine WEAPON_TYPE_MACHINE) : base(WEAPON_MANAGER, WEAPON_TYPE_MACHINE)
    {
    }

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

    }
    public override void FirstSkill()
    {
        base.FirstSkill();
      
    }

    public override void SecondSkill()
    {
        base.SecondSkill();
    }

    public override void UltimateSkill()
    {
        base.UltimateSkill();
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
              //  OffVfx();
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxFirstAbiSword:
            //    Player.instance.playerDataEffect.G_AbilityQ.SetActive(true);
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxSecondAbiSword:
             //   Player.instance.playerDataEffect.G_AbilityE.SetActive(true);
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxUltimateAbiSword:
                break;
        }
    }
    public void FirstAbilityCollider()
    {
     
    }
    public void SecondAbilityCollider()
    {
      
    }
    public void UltimateAbilityCollider()
    {
     
    }
    #endregion

    #region Resauble Method

    #endregion
}
