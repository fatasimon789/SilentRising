using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerActionState
{
   
    public AttackState(PlayerMovementStateMachine PLAYER_MOVEMENT_STATE_MACHINE) : base(PLAYER_MOVEMENT_STATE_MACHINE)
    {
    }
    #region IState
    public override void EnterState()
    {
        base.EnterState();
        StartAnimatorTrigger(playerMovementStateMachine.player.playerAnimatorData.S_attackString);
    }
    public override void ExitState()
    {
        base.ExitState();
     
    }
    public override void Update()
    {
        base.Update();
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void AnimationTriggerEventBase(PlayerTriggerEventAnim.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEventBase(triggerType);
        switch (triggerType) 
        {
            case PlayerTriggerEventAnim.AnimationTriggerType.Slash:
                   WaitToEndSlashAnimation();
                
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.ComboTo0:
                ComboTab();
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.OffComboTo0:
                OffComboTab();
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.ThurshSlash:
                WaitToEndSlashAnimation();
                break;
            default:
                break;
        }
    }
    #endregion
    #region Main Method
    private void WaitToEndSlashAnimation() 
    {
        playerMovementStateMachine.ChanceState(playerMovementStateMachine.idleState);
        IsPlayerMoving(true);
    }
    
    private void ComboTab() 
    {
        canAttack = true;
      
    }
    private void OffComboTab() 
    {
        canAttack = false;
    }
    #endregion
}
