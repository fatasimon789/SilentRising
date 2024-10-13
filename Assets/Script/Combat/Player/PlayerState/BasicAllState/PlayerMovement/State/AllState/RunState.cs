using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerMovementState
{
    public RunState(PlayerMovementStateMachine PLAYER_MOVEMENT_STATE_MACHINE) : base(PLAYER_MOVEMENT_STATE_MACHINE)
    {
    }
    #region IState
    public override void EnterState()
    {
        base.EnterState();
        // StartAnimator(playerMovementStateMachine.player.playerAnimator.runningParamaterHash);
        StartAnimator(playerMovementStateMachine.player.playerAnimatorData.S_movingString);
    }
    public override void ExitState()
    {
        base.ExitState();
        StopAnimator(playerMovementStateMachine.player.playerAnimatorData.S_movingString);
    }
    public override void Update()
    {
        base.Update();
        if(movementInput == Vector2.zero) 
        {
            playerMovementStateMachine.ChanceState(playerMovementStateMachine.hardStopState);
        }

    }
    #endregion
}
