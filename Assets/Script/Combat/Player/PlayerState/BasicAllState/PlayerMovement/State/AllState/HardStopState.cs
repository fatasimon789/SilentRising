using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardStopState : PlayerActionState
{
    public HardStopState(PlayerMovementStateMachine PLAYER_MOVEMENT_STATE_MACHINE) : base(PLAYER_MOVEMENT_STATE_MACHINE)
    {
    }
    #region IState
    public override void EnterState()
    {
        base.EnterState();
        StartAnimator(playerMovementStateMachine.player.playerAnimatorData.S_StopingString);
    }
    public override void ExitState()
    {
        base.ExitState();
        StopAnimator(playerMovementStateMachine.player.playerAnimatorData.S_StopingString);

    }
    public override void Update()
    {
        base.Update();
        speedModifer= 0;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!IsMovingHorizontally()) 
        {
            playerMovementStateMachine.ChanceState(playerMovementStateMachine.runState);
        }
        else 
        {
            playerMovementStateMachine.ChanceState(playerMovementStateMachine.idleState);
        }
    }
    #endregion
}
