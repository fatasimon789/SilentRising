using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class TypeOfWeapon : IWeapon
{
   protected WeaponManager WeaponManager;
   protected WeaponTypeMachine WeaponTypeMachine;
   public bool isOnPerfectAbilityQ { get; set; }
   public bool isOnPerfectAbilityE { get; set; }
   public bool isOnPerfectAbilityR { get; set; }

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
        ControllBehaviourDisable();
    }


    public virtual void SecondSkill()
    {
        StartAnimation(Player.instance.playerAnimatorData.S_SecondAbi);
        ControllBehaviourDisable();
    }

    public virtual void UltimateSkill()
    {
        StartAnimation(Player.instance.playerAnimatorData.S_UltimateAbi);
        ControllBehaviourDisable();
    }
    public virtual void triggerAbilitySkill(PlayerTriggerEventAnim.AbilityTriggerType triggerAbility) 
    {
        switch(triggerAbility) 
        {
            case PlayerTriggerEventAnim.AbilityTriggerType.EndAnimAbility:
                ControllBehaviourEnable();
                break;
        }
    }
    public virtual void UpdateEvent() 
    {
        
    }
    public  void StartAnimation(string ANIM) 
    {
        Player.instance.animator.SetTrigger(ANIM);
    }
    private void ControllBehaviourEnable() 
    {
      Player.instance.playerMovementStateMachine.attackSwordState.OnableControls();
    }
    private void ControllBehaviourDisable() 
    {
     Player.instance.playerMovementStateMachine.attackSwordState.DisableControls();
    }
}
