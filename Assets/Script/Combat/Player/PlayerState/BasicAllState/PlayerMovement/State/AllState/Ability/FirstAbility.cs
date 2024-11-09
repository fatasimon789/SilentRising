using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAbility : PlayerActionState
{
    public FirstAbility(PlayerMovementStateMachine PLAYER_MOVEMENT_STATE_MACHINE) : base(PLAYER_MOVEMENT_STATE_MACHINE)
    {
    }


    public override void EnterState()
    {
        base.EnterState();
        StartAnimator(playerMovementStateMachine.player.playerAnimatorData.S_FirstAbi);
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void AnimationTriggerEventBase(PlayerTriggerEventAnim.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEventBase(triggerType);
       
    }
}
