using System;
using UnityEngine;

[Serializable]
public class PlayerAnimData
{

    [Header("attack")]
    [SerializeField] private string _attacking = "IsAttack";
    [Header("Ability Skill")]
    [SerializeField] private string _FirstAbility = "IsQ";
    [SerializeField] private string _SecondAbility = "IsE";
    [SerializeField] private string _Ultimate = "IsR";
    [Header("Default Skill")]
    [SerializeField] private string _healing = "IsHealing";
    [SerializeField] private string _dashing = "IsDashing";
    [Header("GroundMovement")]
    [SerializeField] private string _idling = "IsIdling";
    [SerializeField] private string _running = "IsMoving";
    [SerializeField] private string _hardStop = "IsStoping";

    public string S_idleString { get; private set; }
    public string S_movingString{ get; private set; }
    public string S_StopingString { get; private set; }
    public string S_attackString{ get; private set; }
    public string S_healString{ get; private set; }
    public string S_dashString{ get; private set; }
    public string S_FirstAbi { get; private set; }
    public string S_SecondAbi { get; private set; }
    public string S_UltimateAbi { get; private set; }

    public void Initilized() 
    {
        S_movingString = _running;
        S_attackString = _attacking;
        S_healString = _healing;
        S_dashString = _dashing;
        S_StopingString = _hardStop;
        S_idleString = _idling;
        S_FirstAbi = _FirstAbility;
        S_SecondAbi = _SecondAbility;
        S_UltimateAbi = _Ultimate;
    }
}
