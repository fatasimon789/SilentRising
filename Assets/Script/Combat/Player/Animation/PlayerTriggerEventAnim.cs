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
       // Sword Type
       Slash,ComboTo0,OffComboTo0,
       ThurshSlash,
       //+ Effect Sword Type
       vfxSlash1,vfxSlash2,vfxThursh
    }
    
}
