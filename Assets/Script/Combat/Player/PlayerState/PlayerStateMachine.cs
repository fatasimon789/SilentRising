using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateMachine
{
    public IState currentState { get; set; }
    public void ChanceState(IState newState) 
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
