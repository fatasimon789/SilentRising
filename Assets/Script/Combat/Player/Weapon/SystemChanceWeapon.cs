using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemChanceWeapon : MonoBehaviour
{
    public static SystemChanceWeapon instance;
    private Player _player { get; set; }
    public List<SystemSkillWeapon> chanceSystemSkillWeapon;
    public bool isSwitch { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _player = FindAnyObjectByType<Player>().GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.M)) 
        {
            ChanceWeapon();
        }
    }

    private void ChanceWeapon()
    {
        // click so 1
        // WeaponManager.instance.WeaponMachine.ChanceWeapon(WeaponManager.instance.FireSword);
        // WeaponManager.instance.SystemSkillWeapon = chanceSystemSkillWeapon[0];

        // click so 2
        if (!isSwitch) 
        {
        isSwitch = true;
        WeaponManager.instance.SystemSkillWeapon = chanceSystemSkillWeapon[1];
        WeaponManager.instance.WeaponMachine.ChanceWeapon(WeaponManager.instance.IcePunch);
        WeaponManager.instance.StatsWeaponUpdate();
       
        StartCoroutine(IsSwitchOff());
        }
        // click so 3
        // click so 4
        // click so 5
    }
    IEnumerator IsSwitchOff() 
    {
        yield return new WaitForSeconds(1f);
        isSwitch= false;
    }
}
