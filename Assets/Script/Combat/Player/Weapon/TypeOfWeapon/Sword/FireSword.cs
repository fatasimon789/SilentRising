using System.Collections;
using System.Collections.Generic;
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
    public override void triggerAbilitySkill(PlayerTriggerEventAnim.AnimationTriggerType triggerAbility)
    {
        base.triggerAbilitySkill(triggerAbility);
    }
    #endregion

    #region Resauble Method
    #endregion
}
