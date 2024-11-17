using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public SystemSkillWeapon SystemSkillWeapon;

    [field: Header("PosColliderAbi")]
    [field: SerializeField] public UpdatingPositionAbility updatingPosAbi { get; private set; }
    public int weaponDamages { get;private  set; }
    public int weaponHP { get;private  set; }
    public int weaponDEF { get;private set; }
    public float weaponCRIT { get;private set; }
    public CombatTypeManager.DamegesType DamagesType { get;private set; }
    public string nameWeapon { get;  set; }

    #region Weapon Type Machine
    public WeaponTypeMachine  WeaponMachine { get; set; }
    public FireSword FireSword { get; set; }
    public IcePunch IcePunch { get; set; }
    #endregion
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(instance);
        }
        WeaponMachine = new WeaponTypeMachine();

        FireSword = new FireSword(this,WeaponMachine);
        IcePunch  = new IcePunch(this,WeaponMachine);
        

    }
    private void Start()
    {
        // give the value from weapon 
        WeaponMachine.Initialize(FireSword);
        nameWeapon = SystemSkillWeapon.name;
        DamagesType = SystemSkillWeapon.DamagesType;
        // InitlizeFirstWeapon Start
        weaponDamages = SystemSkillWeapon.dameges;
        weaponHP = SystemSkillWeapon.heal;
        weaponDEF = SystemSkillWeapon.defense;
        weaponCRIT = SystemSkillWeapon.crit;
    }
    private void Update()
    {
        // Weapon INFO
        nameWeapon = SystemSkillWeapon.name;
        DamagesType = SystemSkillWeapon.DamagesType;
        // stats value
        StatsWeaponUpdate();

        VirtualBox.DisplayBox(updatingPosAbi.colliderPosQ.position, updatingPosAbi.localColliderHalfExtendQ, Quaternion.identity);
        VirtualBox.DisplayBox(updatingPosAbi.colliderPosE.position, updatingPosAbi.localColliderHalfExtendE, Quaternion.identity);
        VirtualBox.DisplayBox(updatingPosAbi.colliderPosR.position, updatingPosAbi.localColliderHalfExtendR, Quaternion.identity);
    }
    private void FixedUpdate()
    {
        // Weapon Ability
        PassivePlayer();
        NormalAttackPlayer();
        Ability1Input();
        Ability2Input();
        AbilityUltimate();
        DashingIpnut();
        HealingIpnut();
    }
    #region All Skill
    // H
    public  void HealingPlayer()
    {
        // refrence normal healling
        WeaponMachine.Healing();
        // H input
        // neu muon no co 1 chut dac biet thi se + - vao day
    }
    public  void NormalAttackPlayer()
    {
        WeaponMachine.normalAttack();
    }

    // left CTRL
    public void DashingPlayer()
    {
        WeaponMachine.Dashing();
    }

    public void PassivePlayer()
    {
        WeaponMachine.passive();
    }
    // Q 
    public void FirstSkillPlayer()
    {
        WeaponMachine.FirstSkill();
    }

    // E
    public void SecondSkillPlayer()
    {
        WeaponMachine.SecondSkill();
    }
    // R
    public void UltimateSkillPlayer()
    {
        WeaponMachine.Ultimate();
    }
    #endregion
    #region Updating event 
    public void StatsWeaponUpdate()
    {
        weaponDamages = SystemSkillWeapon.dameges;
        weaponHP = SystemSkillWeapon.heal;
        weaponDEF = SystemSkillWeapon.defense;
        weaponCRIT = SystemSkillWeapon.crit;
    }
    #endregion
    #region Input Player
    public void Ability1Input()
    {
        Player.instance.playerInput.playerActions.AbilityQ.performed += ctx => FirstSkillPlayer();
    }
    public void Ability2Input()
    {
        Player.instance.playerInput.playerActions.AbilityE.performed += ctx => SecondSkillPlayer();
    }
    public void AbilityUltimate()
    {
        Player.instance.playerInput.playerActions.AbilityR.performed += ctx => UltimateSkillPlayer();
    }
    public void HealingIpnut() 
    {
       Player.instance.playerInput.playerActions.Healing.performed+= ctx => HealingPlayer();
    }
    public void DashingIpnut()
    {
        Player.instance.playerInput.playerActions.Dash.performed += ctx => DashingPlayer();
    }
    #endregion
}
