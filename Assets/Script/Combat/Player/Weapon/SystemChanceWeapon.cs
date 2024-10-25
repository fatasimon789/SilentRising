using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemChanceWeapon : MonoBehaviour
{
    public List<SystemSkillWeapon> chanceSystemSkillWeapon;
  
    private void Update()
    {
        if (Input.GetKey(KeyCode.Backspace)) 
        {
            ChanceWeapon();
        }
    }

    private void ChanceWeapon()
    {
        // click so 1
        WeaponManager.instance.WeaponMachine.ChanceWeapon(WeaponManager.instance.FireSword);
        WeaponManager.instance.SystemSkillWeapon = chanceSystemSkillWeapon[0];
   
        // click so 2
        WeaponManager.instance.WeaponMachine.ChanceWeapon(WeaponManager.instance.IcePunch);
        WeaponManager.instance.SystemSkillWeapon = chanceSystemSkillWeapon[1];
       
        // click so 3
        // click so 4
        // click so 5
    }
}
