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
       Slash,ComboTo0,OffComboTo0,
       ThurshSlash,
    }
    
}
