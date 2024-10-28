using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public SystemSkillWeapon SystemSkillWeapon;

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
       
        // Weapon Ability
        PassivePlayer();
        NormalAttackPlayer();
        FirstSkillPlayer();
        SecondSkillPlayer();
        UltimateSkillPlayer();
        DashingPlayer();
        HealingPlayer();
    }
    #region All Skill
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
        // left mouse input
    }

    public void DashingPlayer()
    {
        WeaponMachine.Dashing();
        // left ctrl input
    }

    public void PassivePlayer()
    {
        WeaponMachine.passive();
    }
    public void FirstSkillPlayer()
    {
        WeaponMachine.FirstSkill();
        // Q input
    }


    public void SecondSkillPlayer()
    {
        WeaponMachine.SecondSkill();
        // E input
    }

    public void UltimateSkillPlayer()
    {
        WeaponMachine.Ultimate();
        // R input
    }
    #endregion
    public void StatsWeaponUpdate()
    {
        weaponDamages = SystemSkillWeapon.dameges;
        weaponHP = SystemSkillWeapon.heal;
        weaponDEF = SystemSkillWeapon.defense;
        weaponCRIT = SystemSkillWeapon.crit;
    }
}
