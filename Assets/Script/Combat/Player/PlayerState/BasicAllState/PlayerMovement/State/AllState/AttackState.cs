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
        isCancel = false;
    }
    public override void ExitState()
    {
        base.ExitState();
     
    }
    public override void Update()
    {
        base.Update();

        if (movementInput != Vector2.zero && isCancel) 
        {
            playerMovementStateMachine.ChanceState(playerMovementStateMachine.runState);
            OnableControls();
            vfxAllOff();
        }
        isCancel = false;
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
            case PlayerTriggerEventAnim.AnimationTriggerType.EndAnim:
                OnableControls();
                vfxAllOff();
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.ComboTo0:
                ComboTab();
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.OffComboTo0:
                OffComboTab();
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.CancelAnim:
                isCancel = true;
                break;
         // ++++++++++++++++++++++++++ ATTACK SWORD +++++++++++++++++++++++
            case PlayerTriggerEventAnim.AnimationTriggerType.vfxSlash1:
                vfxNormalAttack(1);
                AttackCollider(ColiderDamagesSlash());
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.vfxThursh:
                vfxNormalAttack(2);
                AttackCollider(ColiderDamagesSlash());
                break;
            case PlayerTriggerEventAnim.AnimationTriggerType.vfxSlash2:
                vfxNormalAttack(3);
                AttackCollider(ColiderDamagesSlash());
                break;
            // ++++++++++++++++++++++++++ ATTACK Fist +++++++++++++++++++++++

            default:
                break;
        }
    }
    #endregion
    #region Main Method
    public void OnableControls() 
    {
        playerMovementStateMachine.ChanceState(playerMovementStateMachine.idleState);
        IsPlayerMoving(true);
       
    }
    public void DisableControls() 
    {
        IsPlayerMoving(false);
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
     // +++++++++++++++++++ ATTACK SWORD ++++++++++++++++
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
     // ++++++++++++++++++++ ATTACK FIST +++++++++++++++++
    }
    public void vfxAllOff() 
    {
        // +++++++++++++++++++ ATTACK SWORD ++++++++++++++++
        playerMovementStateMachine.player.playerDataEffect.G_Slash1.SetActive(false);
        playerMovementStateMachine.player.playerDataEffect.G_Thursh1.SetActive(false);
        playerMovementStateMachine.player.playerDataEffect.G_Slash2.SetActive(false);
        // +++++++++++++++++++ ATTACK FIST ++++++++++++++++
    }
    private Collider[] ColiderDamagesSlash()
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapBox(WeaponManager.instance.posAbilityCollider.colliderPosAttack.transform.position,
                                         WeaponManager.instance.SystemSkillWeapon._abilityPostionAttack[0].rangeExtendBoxCollider
                                         , Quaternion.identity, WeaponManager.instance.layerMask);
        return colliderInfo;
    }
    private void AttackCollider(Collider[] ABILITY_COL)
    {
        foreach (Collider col in ABILITY_COL)
        {

            Vector3 closetPoint0 = col.ClosestPoint(Player.instance.transform.position); // diem collider gan nhat

            Vector3 posDifferent = (closetPoint0 - Player.instance.transform.position); //  chi ra huong khi va cham lan dau
                                                                                        //  va den trung tam collider
            Vector3 overlapDirection = posDifferent.normalized;

            RaycastHit hit;

            float raycastDistance = 10.0f; // something greater than your object's largest radius, 
                                           // so that the ray doesn't start inside of your object
            Vector3 rayStart = Player.instance.transform.position + overlapDirection * raycastDistance;
            Vector3 rayDirection = -overlapDirection;

            if (Physics.Raycast(rayStart, rayDirection, out hit, Mathf.Infinity, WeaponManager.instance.layerMask))
            {
                var targetInfo = hit.collider.GetComponent<IEnemy>();
                targetInfo.enemyHP.takeDamages(WeaponManager.instance.SystemSkillWeapon.NormalAttackSword(),WeaponManager.instance.DamagesType);
            }
            else
            {
                // The ray missed your object, somehow. 
                // Most likely it started inside your object 
                // or there is a mistake in the layerMask
            }
        }

    }
    #endregion
}
