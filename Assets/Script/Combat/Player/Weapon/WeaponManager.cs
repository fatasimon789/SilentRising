using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour, IWeapon
{
    public SystemSkillWeapon SystemSkillWeapon;
    public int damages { get; set; }
    public CombatTypeManager.DamegesType DamagesType { get; set; }
    public string Name { get; set; }


    #region Weapon Type Machine
    public WeaponTypeMachine  WeaponMachine { get; set; }
    public FireSword FireSword { get; set; }
    #endregion
    private void Awake()
    {
        WeaponMachine = new WeaponTypeMachine();

        FireSword = new FireSword(this,WeaponMachine);
    }
    private void Start()
    {
        WeaponMachine.Initialize(FireSword);
    }
    private void Update()
    {
        if (SystemSkillWeapon != null  && DamagesType ==  SystemSkillWeapon.DamagesType ) 
        {
             
        }

        Name = SystemSkillWeapon.name;
        DamagesType = SystemSkillWeapon.DamagesType;
        damages = SystemSkillWeapon.dameges;
       
    }

}
