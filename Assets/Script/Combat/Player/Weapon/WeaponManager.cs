using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public SystemSkillWeapon SystemSkillWeapon;
    public int damages { get; set; }
    public CombatTypeManager.DamegesType DamagesType { get; set; }
    public string nameWeapon { get;  set; }


    #region Weapon Type Machine
    public WeaponTypeMachine  WeaponMachine { get; set; }
    public FireSword FireSword { get; set; }
    public IcePunch IcePunch { get; set; }
    #endregion
    private void Awake()
    {
        WeaponMachine = new WeaponTypeMachine();

        FireSword = new FireSword(this,WeaponMachine);
        IcePunch  = new IcePunch(this,WeaponMachine);
    }
    private void Start()
    {
        WeaponMachine.Initialize(FireSword);
    }
    private void Update()
    {
      
        // lay name lam ui thong bao ten weapon
        nameWeapon = SystemSkillWeapon.name;
        // lay damges type de thong qua code damages enemy  phu thuoc vao he
        DamagesType = SystemSkillWeapon.DamagesType;
        // base dame co ban se dua vao thuoc tinh chinh cua nhan vat 
        damages = SystemSkillWeapon.dameges;
  
    }
    private void ChangeWeapon()
    {
      //  WeaponMachine.ChanceWeapon(IcePunch);
      // tao 1 system rieng chuyen swap va xoa method nay  neu chon weapon  ngau nhien ( vi du Ice Punch thi se thay systemskill weapon) 
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
}
