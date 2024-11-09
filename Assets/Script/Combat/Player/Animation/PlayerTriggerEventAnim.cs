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
    public enum AnimationTriggerType 
    {
       // Attack Type
       EndAnim,ComboTo0,OffComboTo0,
       //+ Effect Sword Type
       vfxSlash1,vfxSlash2,vfxThursh,
       //+ Effect Firt Type
       vfxPunch1,vfxPunch2,vfxPunch3,vfxPunch4,
       //+ Effect Axe Type 
       
       // Ability Q 
       FirstAbi,
       // Ability E
       SecondAbi,
       // Ability R
       UltimateAbi,
    }
    
}
