using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateMachine
{
    public PlayerActionState currentState { get; set; }
    public void ChanceState(PlayerActionState newState) 
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
    public void HandleInput() 
    {
       currentState?.HandleInput();
    }
    public void Update()
    {
        currentState?.Update();
    }
    public void PhysicUpdate()
    {
        currentState?.FixedUpdate();
    }
   
}
