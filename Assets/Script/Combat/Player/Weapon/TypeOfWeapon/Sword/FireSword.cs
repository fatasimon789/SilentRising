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

    public override void ChanceNewWeapon()
    {
        base.ChanceNewWeapon();
        // them vao day firesword
    }

    public override void Dashing()
    {
        base.Dashing();
    }

    public override void DeleteOldWeapon()
    {
        base.DeleteOldWeapon();
        // xoa vao day firesword
    }

    public override void FirstSkill()
    {
        base.FirstSkill();
    }

    public override void Healing()
    {
        base.Healing();
    }

    public override void NormalAttack()
    {
        base.NormalAttack();
    }

    public override void Passive()
    {
        base.Passive();
    }

    public override void SecondSkill()
    {
        base.SecondSkill();
    }

    public override void UltimateSkill()
    {
        base.UltimateSkill();
    }
}
