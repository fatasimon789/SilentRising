using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateMachine
{
    protected IState currentState;
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
