using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatingAbility : MonoBehaviour
{
    public static UpdatingAbility instance;
    private int _abilityLevelQ,_abilityLevelE,_abilityLevelR;
    private bool test;
    #region Main monobehaviour
    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
        else 
        {
            Destroy(instance);
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.U) && !test) 
        {
            _abilityLevelQ++;
            test = true;
        }
    }
    #endregion
    #region Call Upgrade Ability 
    public void UpdatingAbilityTreeQ() 
    {
        _abilityLevelQ++;
        Debug.Log("You LevelQ is " + _abilityLevelQ.ToString());
    }
    public void UpdatingAbilityTreeE()
    {
        _abilityLevelE++;
        Debug.Log("You LevelE is " + _abilityLevelE.ToString());
    }
    public void UpdatingAbilityTreeR()
    {
        _abilityLevelR++;
        Debug.Log("You LevelR is " + _abilityLevelR.ToString());
    }
    #endregion
    #region Ability Damages Caculation
    // DMG Q 
    // S3    ADD VALUE AND  CALL THE DAMAGES 
    public int FirstAbilityDMG() 
    {
        var resultDMG =  WeaponManager.instance.SystemSkillWeapon.AbilityQSword(LevelBaseDmgQUpdating(),LevelMultiQUpdating());
        return resultDMG;
    }
    public int SecondAbilityDMG()
    {
        var resultDMG = WeaponManager.instance.SystemSkillWeapon.AbilityESword(LevelBaseDmgEUpdating(), LevelMultiEUpdating());
        return resultDMG;
    }
    public int UltimateAbilityDMG()
    {
        var resultDMG = WeaponManager.instance.SystemSkillWeapon.AbilityRSword(LevelBaseDmgRUpdating(), LevelMultiRUpdating());
        return resultDMG;
    }
    // DMG E
    #endregion
    #region  Resauble method (Damages paramater)
    //     --------------------- Q -----------------------
    // S1 MAKE  THE UPDATING VALUE FROM  ARRAY     
    private int  LevelBaseDmgQUpdating() 
    {
        if (WeaponManager.instance.SystemSkillWeapon.basicDmgQ.Count == _abilityLevelQ) 
        {
            Debug.Log("Ability Q is full");
        }
        var totalBaseDMG=  WeaponManager.instance.SystemSkillWeapon.LevelBaseDmgQ(_abilityLevelQ);
        return totalBaseDMG;
    }
    private float LevelMultiQUpdating() 
    {
        var totalMultiQ = WeaponManager.instance.SystemSkillWeapon.LevelMultiQ(_abilityLevelQ);
        return totalMultiQ;
    }
    // ------------------------- E ---------------------------
    private int LevelBaseDmgEUpdating()
    {
        if (WeaponManager.instance.SystemSkillWeapon.basicDmgE.Count == _abilityLevelE)
        {
            Debug.Log("Ability E is full");
        }
        var totalBaseDMG = WeaponManager.instance.SystemSkillWeapon.LevelBaseDmgE(_abilityLevelE);
        return totalBaseDMG;
    }
    private float LevelMultiEUpdating()
    {
        var totalMultiE = WeaponManager.instance.SystemSkillWeapon.LevelMultiE(_abilityLevelE);
        return totalMultiE;
    }
    // ------------------------- R ---------------------------
    private int LevelBaseDmgRUpdating()
    {
        if (WeaponManager.instance.SystemSkillWeapon.basicDmgR.Count == _abilityLevelR)
        {
            Debug.Log("Ability R is full");
        }
        var totalBaseDMG = WeaponManager.instance.SystemSkillWeapon.LevelBaseDmgR(_abilityLevelR);
        return totalBaseDMG;
    }
    private float LevelMultiRUpdating()
    {
        var totalMultiR = WeaponManager.instance.SystemSkillWeapon.LevelMultiR(_abilityLevelR);
        return totalMultiR;
    }
    #endregion
    #region Item Upgrade
    public List<int> GetItemRequiredUpgrade(int LEVEL,int amountMaterial1 = 0 , int amountMaterial2 = 0 ) 
    {
        // get value required 
        List<int> amountMaterial= new List<int>();
        switch (LEVEL) 
        {
            case 0:
                amountMaterial1 = 1;
                break;
            case 1:
                amountMaterial1 = 2;
                break;
            case 2:
                amountMaterial1 = 3;
                break;
            case 3:
                amountMaterial1 = 5;
                amountMaterial2 = 1;
                break;
            case 4:
                amountMaterial1 = 8;
                amountMaterial2 = 2;
                break;
            case 5:
                amountMaterial1 = 12;
                amountMaterial2 = 4;
                break;
            case 6:
                amountMaterial1 = 15;
                amountMaterial2 = 6;
                break;
            case 7:
                amountMaterial1 = 20;
                amountMaterial2 = 10;
                break;
            default:
                break;
        }
        amountMaterial.Add(amountMaterial1);
        amountMaterial.Add(amountMaterial2);
        return amountMaterial;
    }
    
    #endregion
}
