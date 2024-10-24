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
                vfxAllOff();
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.ComboTo0:
                ComboTab();
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.OffComboTo0:
                OffComboTab();
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.ThurshSlash:
                WaitToEndSlashAnimation();
                vfxAllOff();
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.vfxSlash1:
                vfxNormalAttack(1);
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.vfxThursh:
                vfxNormalAttack(2);
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.vfxSlash2:
                vfxNormalAttack(3);
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
    private void vfxNormalAttack(int COMBO) 
    {
        if (COMBO == 1) 
        {
            playerMovementStateMachine.player.playerDataEffect.G_Slash1.SetActive(true);
        }
        if (COMBO == 2)
        {
            playerMovementStateMachine.player.playerDataEffect.G_Thursh1.SetActive(true);
        }
        if (COMBO == 3)
        {
            playerMovementStateMachine.player.playerDataEffect.G_Slash2.SetActive(true);
        }
    }
    private void vfxAllOff() 
    {
        playerMovementStateMachine.player.playerDataEffect.G_Slash1.SetActive(false);
        playerMovementStateMachine.player.playerDataEffect.G_Thursh1.SetActive(false);
        playerMovementStateMachine.player.playerDataEffect.G_Slash2.SetActive(false);
    }
    #endregion
}
