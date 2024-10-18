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
        //Debug.Log("attack state 000 " + canAttack);
    }
    public override void ExitState()
    {
        base.ExitState();
     
    }
    public override void Update()
    {
        base.Update();
   //     Debug.Log(canAttack + "attackstate");
       
    }
    public override void HandleInput()
    {
        base.HandleInput();
        // tuy loi vu khi se chuyen dang tan cong khac nhau
       
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
        canAttack = true;
        playerMovementStateMachine.ChanceState(playerMovementStateMachine.idleState);
       
    }
    private void ComboTab() 
    {
        canAttack = true;
    }
    #endregion
}
