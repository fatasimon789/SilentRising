using System;
using UnityEngine;

[Serializable]
public class PlayerAnimData
{

    [Header("Type of attack : Sword")]
    [SerializeField] private string _attackSword = "IsAttackingSword";
    [SerializeField] private string _slash = "IsSlashing";
    [SerializeField] private string _slashAndThursh = "IsThurshSlashing";
    [Header("Type of attack : Fist")]
    [SerializeField] private string _attackPunch = "IsAttackingPunch";
    [Header("Default Skill")]
    [SerializeField] private string _attack = "IsAtacking";    
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
    public string S_attackSwordString{ get; private set; }
    public string S_attackPunchString{ get; private set; }
    public string S_attackGunString{ get; private set; }
    public string S_healString{ get; private set; }
    public string S_dashString{ get; private set; }
    public string S_slashString{ get; private set; }
    public string S_thrushSlashString{ get; private set; }

    public void Initilized() 
    {
        S_movingString = _running;
        S_attackString = _attack;
        S_attackSwordString = _attackSword;
        S_healString = _healing;
        S_dashString = _dashing;
        S_StopingString = _hardStop;
        S_idleString = _idling;
        S_attackPunchString = _attackPunch;
        S_slashString= _slash;
        S_thrushSlashString = _slashAndThursh;
    }
}
