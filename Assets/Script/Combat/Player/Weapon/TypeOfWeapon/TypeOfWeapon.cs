using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class TypeOfWeapon : IWeapon
{
   protected WeaponManager WeaponManager;
   protected WeaponTypeMachine WeaponTypeMachine;

   public TypeOfWeapon (WeaponManager WEAPON_MANAGER , WeaponTypeMachine WEAPON_TYPE_MACHINE) 
    {
        this.WeaponManager= WEAPON_MANAGER;
        this.WeaponTypeMachine= WEAPON_TYPE_MACHINE;
    }
    public virtual void ChanceNewWeapon()
    {

    }
    public virtual void DeleteOldWeapon()
    {

    }
    public virtual void Healing()
    {

    }
    public virtual void NormalAttack()
    {

    }

    public virtual void Dashing()
    {

    }

    public virtual void Passive()
    {
    }
    public virtual void FirstSkill()
    {
        StartAnimation(Player.instance.playerAnimatorData.S_FirstAbi);
        Debug.Log("1");
    }


    public virtual void SecondSkill()
    {
        StartAnimation(Player.instance.playerAnimatorData.S_SecondAbi);
    }

    public virtual void UltimateSkill()
    {
       // StartAnimation(Player.instance.playerAnimatorData.S_UltimateAbi);
    }
    public virtual void triggerAbilitySkill(PlayerTriggerEventAnim.AbilityTriggerType triggerAbility) 
    {
    
    }
    public  void StartAnimation(string ANIM) 
    {
        Player.instance.animator.SetTrigger(ANIM);
    }
}
