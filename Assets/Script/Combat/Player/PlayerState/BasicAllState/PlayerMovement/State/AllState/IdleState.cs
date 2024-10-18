using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : PlayerActionState
{
    // thua ke tuong tu playerActionState
    public IdleState(PlayerMovementStateMachine PLAYER_MOVEMENT_STATE_MACHINE) : base(PLAYER_MOVEMENT_STATE_MACHINE)
    {
    }
    #region IState
    public override void EnterState()
    {
        base.EnterState();
        StartAnimator(playerMovementStateMachine.player.playerAnimatorData.S_idleString);
         
    }
    public override void ExitState()
    {
        base.ExitState();
        StopAnimator(playerMovementStateMachine.player.playerAnimatorData.S_idleString);
    }
    public override void Update()
    {
        base.Update();
        if (movementInput == Vector2.zero) 
        {
            return; 
        }
        onMove();
    }
    #endregion

    #region Main Method
    private void onMove()
    {
        playerMovementStateMachine.ChanceState(playerMovementStateMachine.runState);
    }
    #endregion
}
