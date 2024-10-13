using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfWeapon 
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

    }


    public virtual void SecondSkill()
    {

    }

    public virtual void UltimateSkill()
    {

    }
}
