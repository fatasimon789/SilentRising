using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTriggerEventAnim : MonoBehaviour
{
    public void AnimationTriggerEvent(AnimationTriggerType type) 
    {
      Player.instance.playerMovementStateMachine.currentState.AnimationTriggerEventBase(type);
    }
    public void AbilityTriggerEvent(AbilityTriggerType ABITYPE) 
    {
        WeaponManager.instance.WeaponMachine.currentWeapon.triggerAbilitySkill(ABITYPE);
    }
    public enum AnimationTriggerType 
    {
       // Attack Type
       EndAnim,ComboTo0,OffComboTo0,
       //+ Effect Sword NormalAttack
       vfxSlash1,vfxSlash2,vfxThursh,
       //+ Effect Firt NormalAttack
       vfxPunch1,vfxPunch2,vfxPunch3,vfxPunch4,
       //+ Effect Axe NormalAttack
       v,v2,v3,v4,v5,
       CancelAnim
     
    }
    public enum AbilityTriggerType 
    {
        // Ending Ability Anim
        EndAnimAbility,
        // Ability Q  Box collider
        FirstAbi,
        // Ability E  Box Collider 
        SecondAbi,
        // Ability R  Box Collider
        UltimateAbi,
        //+ Effect Sword Ability
        vfxFirstAbiSword, vfxSecondAbiSword, vfxUltimateAbiSword,
        //+ Effect Sword Ability
        vfxFirstAbiFist, vfxSecondAbiFist, vfxUltimateAbiFist,
        //+ Effect Sword Ability
        vfxFirstAbiAxe, vfxSecondAbiAxe, vfxUltimateAbiAxe,
    }
   

}
