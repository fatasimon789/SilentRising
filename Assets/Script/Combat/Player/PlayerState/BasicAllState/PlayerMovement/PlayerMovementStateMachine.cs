using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : PlayerStateMachine
{
    // add Player vo de co lien ket giua  tat ca he thong manager vs nhau
    public Player player { get; private set; }
    public RunState runState { get; private set; }
    public IdleState idleState { get; private set; }
    public HardStopState hardStopState { get; private set; }

    public HealState healState { get; private set; }
    public AttackState attackSwordState { get; private set; }
    public FirstAbility firstAbility { get; private set; }
    public SecondAbility secondAbility { get; private set; }
    public UltimateAbility ultimateAbility { get; private set; }
    //   struct  chua cac khoi tao state nho va dat Player vao de  initlize tat ca trang thai
    public PlayerMovementStateMachine(Player PLAYER) 
    {
       player = PLAYER;
       runState= new RunState(this);
       idleState= new IdleState(this);
       hardStopState = new HardStopState(this);
       healState= new HealState(this);
       attackSwordState= new AttackState(this);
       firstAbility= new FirstAbility(this);
      //  secondAbility = new SecondAbility(this);
    }
}
