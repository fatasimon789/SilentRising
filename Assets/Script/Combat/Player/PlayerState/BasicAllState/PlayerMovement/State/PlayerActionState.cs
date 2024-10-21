using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerActionState : IState
{
    // goi  v de lien ket giua cac class PlayermovementSate --- PlayerMovementStateMachine ---- Player
    protected PlayerMovementStateMachine playerMovementStateMachine { get; private set; }
    protected Vector2 movementInput;
    protected Vector3 mouseInput;
    protected float baseSpeed = 5f;
    protected float speedModifer = 1f;
    protected float rotateSpeed = 18f;
    // Onenable||disable controls 
    protected bool isAttack;
    protected static bool canAttack = true;
    protected static bool canMoving = true;

    // dat struct   ao~ de khoi tao ra new cua cac state nho~ thong qua PlayerMovementStatemachine
    // cac state duoc thua ke se co struct tuong tu cai nay
    public PlayerActionState(PlayerMovementStateMachine PLAYER_MOVEMENT_STATE_MACHINE)
    {
        playerMovementStateMachine = PLAYER_MOVEMENT_STATE_MACHINE;
    }
    #region IState Method 
    public virtual void EnterState()
    {
        Debug.Log("State: " + GetType().Name);
    }

    public virtual void ExitState()
    {

    }

    public virtual void Update()
    {
    }
    public virtual void FixedUpdate()
    {
        Move();
        PlayerRotate();
        if (isAttack)
        {
            PlayerLookAtMouse();
        }
    }


    public virtual void HandleInput()
    {
        PlayerMovement();
        AttackSword();
    }
    public virtual void AnimationTriggerEventBase(PlayerTriggerEventAnim.AnimationTriggerType triggerType)
    {

    }
    #endregion
    //------------------------------- Main Method--------------------------
    #region Main Method
    public void PlayerMovement()
    {
        movementInput = playerMovementStateMachine.player.playerInput.playerActions.Movement.ReadValue<Vector2>();
    }
    public void AttackSword()
    {
        playerMovementStateMachine.player.playerInput.playerActions.AttackSword.performed += ctx => Attack();
    }

    private void PlayerLookAtMouse()
    {
        Ray ray = playerMovementStateMachine.player._mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = playerMovementStateMachine.player.transform.position.y;
            playerMovementStateMachine.player.transform.LookAt(target);
            // ++ dua vao chi so target ma chia thanh 8 huong' de lam UI va an chuot di 
        }

    }
    private void Move()
    {

        if (movementInput == Vector2.zero || speedModifer == 0f || !canMoving)
        {
            playerMovementStateMachine.player.rgb.velocity = Vector3.zero;
            return;
        }
       
        Vector3 movementDirection = GetInputDirection();

        float movementSpeed = GetMovementSpeed();
        // -+ vector nay neu muon nhan vat move theo huong rotation cua nhan vat ( tuy option game)
        Vector3 mouseDirection = playerMovementStateMachine.player.transform.rotation * movementDirection;
        // tao de tranh luc di chuyen bi cong don
        // check player co ngung moving ko neu tu phia trc ve phia lui se vector2 = 0 v nen can them bool de check

        Vector3 currentPlayerVelocity = GetPlayerHorizontalVelocity();
        playerMovementStateMachine.player.rgb.AddForce(movementDirection * movementSpeed - currentPlayerVelocity, ForceMode.VelocityChange);
    }

    private void PlayerRotate()
    {
        if (movementInput == Vector2.zero || !canMoving)
        {
            return;
        }
      
        Vector3 directionPlayer = GetInputDirection() ;
        var rotation = Quaternion.LookRotation(directionPlayer);
        playerMovementStateMachine.player.transform.rotation = Quaternion.RotateTowards(playerMovementStateMachine.player.transform.rotation
                                                                                        , rotation, rotateSpeed);  
    }
    private void Attack()
    {
        if(canAttack)
        {
          IsPlayerMoving(false);
         
          playerMovementStateMachine.ChanceState(playerMovementStateMachine.attackSwordState);
        }
      
    }
    #endregion
    // -------------------- Resauble Method ----------------------
    #region Resauble Method
    protected void StartAnimator(string ANIMATION_NAME) 
    {
        playerMovementStateMachine.player.animator.SetBool(ANIMATION_NAME,true);
    }
    protected void StopAnimator(string ANIMATION_NAME)
    {
        playerMovementStateMachine.player.animator.SetBool(ANIMATION_NAME, false);
    }
    protected void StartAnimatorTrigger(string ANIMATION_NAME)
    {
        playerMovementStateMachine.player.animator.SetTrigger(ANIMATION_NAME);
    }
    protected Vector3 GetInputDirection()
    {
        return new Vector3(movementInput.x, 0, movementInput.y);
    }
    protected float GetMovementSpeed()
    {
        return  baseSpeed * speedModifer;
    }
    // tao vector 3 co luc = vs van toc rigidbory muc dich de luc = luc chu k phai luc += luc
    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = playerMovementStateMachine.player.rgb.velocity;
        playerHorizontalVelocity.y = 0f;
      
        return playerHorizontalVelocity;
    }
    protected void ResetVelocity() 
    {
        playerMovementStateMachine.player.rgb.velocity = Vector3.zero;
    }
    protected bool IsMovingHorizontally(float MINIMUM_MAGNITUDE = 0.1f) 
    { 
      Vector3 playerHorizontalVelocity = GetInputDirection();
       
        Vector2 playerHorizontalMovement = new Vector2(playerHorizontalVelocity.x,playerHorizontalVelocity.y);
       
        return playerHorizontalMovement.magnitude < MINIMUM_MAGNITUDE;
    }
    protected void IsPlayerMoving (bool isMoving) 
    {
        canMoving= isMoving;
        canAttack = isMoving;
    }
    #endregion
}
