using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatingAbility : MonoBehaviour
{
    public static UpdatingAbility instance;
    public int abilityLevelQ { get;private set; }
    public int abilityLevelE { get;private set; }
    public int abilityLevelR { get;private set; }
    public int perfectAbilityLevelQ { get;private set; }
    public int perfectAbilityLevelE { get; private set; }
    public int perfectAbilityLevelR { get; private set; }
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
        PerfectAbilityQ();
        if (Input.GetKey(KeyCode.U) && !test) 
        {
            abilityLevelQ++;
            test = true;
        }
    }
    #endregion
    #region Call Upgrade Ability 
    public void UpdatingAbilityTreeQ() 
    {
        abilityLevelQ++;
        Debug.Log("You LevelQ is " + abilityLevelQ.ToString());
    }
    public void UpdatingAbilityTreeE()
    {
        abilityLevelE++;
        Debug.Log("You LevelE is " + abilityLevelE.ToString());
    }
    public void UpdatingAbilityTreeR()
    {
        abilityLevelR++;
        Debug.Log("You LevelR is " + abilityLevelR.ToString());
    }
    public void PerfectAbilityQ() 
    {
        if (abilityLevelQ >= 0 ) 
        {
            WeaponManager.instance.FireSword.isOnPerfectAbilityQ[0] = true;
        }
        if (abilityLevelQ >= 0)
        {
            WeaponManager.instance.FireSword.isOnPerfectAbilityQ[1] = true;
        }
        if (abilityLevelQ >= 7)
        {
            WeaponManager.instance.FireSword.isOnPerfectAbilityQ[2] = true;
        }
    }
    public void PerfectAbilityE()
    {
        if (abilityLevelE == 5)
        {

        }
    }
    public void PerfectAbilityR()
    {
        if (abilityLevelR == 5)
        {

        }
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
    public int PerfectDMGV1(string ABILITY_NAME) 
    {
        var resultDMG = WeaponManager.instance.SystemSkillWeapon.PerfectDamagesV1(LevelPerfectPercentDamagesV1(ABILITY_NAME).Item1,
        LevelPerfectPercentDamagesV1(ABILITY_NAME).Item2,LevelPerfectPercentDamagesV1(ABILITY_NAME).Item3);
        return resultDMG;
    }
    public int PerfectDMGV2(string ABILITY_NAME) 
    {
        var resultDMG = WeaponManager.instance.SystemSkillWeapon.PerfectDamagesV2(LevelPerfectPercentDamagesV2(ABILITY_NAME).Item1,
        LevelPerfectPercentDamagesV2(ABILITY_NAME).Item2,LevelPerfectPercentDamagesV2(ABILITY_NAME).Item3);
        return resultDMG;
    }
    public int PerfectDMGV3(string ABILITY_NAME)
    {
        var resultDMG = WeaponManager.instance.SystemSkillWeapon.PerfectDamagesV3(LevelPerfectPercentDamagesV3(ABILITY_NAME).Item1,
        LevelPerfectPercentDamagesV3(ABILITY_NAME).Item2, LevelPerfectPercentDamagesV3(ABILITY_NAME).Item3);
        return resultDMG;
    }
    // DMG E
    #endregion
    #region  Resauble method (Damages paramater)
    //     --------------------- Q -----------------------
    // S1 MAKE  THE UPDATING VALUE FROM  ARRAY     
    private int  LevelBaseDmgQUpdating() 
    {
        if (WeaponManager.instance.SystemSkillWeapon.basicDmgQ.Count == abilityLevelQ) 
        {
            Debug.Log("Ability Q is full");
        }
        var totalBaseDMG=  WeaponManager.instance.SystemSkillWeapon.LevelBaseDmgQ(abilityLevelQ);
        return totalBaseDMG;
    }
    private float LevelMultiQUpdating() 
    {
        var totalMultiQ = WeaponManager.instance.SystemSkillWeapon.LevelMultiQ(abilityLevelQ);
        return totalMultiQ;
    }

    // ------------------------- E ---------------------------
    private int LevelBaseDmgEUpdating()
    {
        if (WeaponManager.instance.SystemSkillWeapon.basicDmgE.Count == abilityLevelE)
        {
            Debug.Log("Ability E is full");
        }
        var totalBaseDMG = WeaponManager.instance.SystemSkillWeapon.LevelBaseDmgE(abilityLevelE);
        return totalBaseDMG;
    }
    private float LevelMultiEUpdating()
    {
        var totalMultiE = WeaponManager.instance.SystemSkillWeapon.LevelMultiE(abilityLevelE);
        return totalMultiE;
    }
    // ------------------------- R ---------------------------
    private int LevelBaseDmgRUpdating()
    {
        if (WeaponManager.instance.SystemSkillWeapon.basicDmgR.Count == abilityLevelR)
        {
            Debug.Log("Ability R is full");
        }
        var totalBaseDMG = WeaponManager.instance.SystemSkillWeapon.LevelBaseDmgR(abilityLevelR);
        return totalBaseDMG;
    }
    private float LevelMultiRUpdating()
    {
        var totalMultiR = WeaponManager.instance.SystemSkillWeapon.LevelMultiR(abilityLevelR);
        return totalMultiR;
    }
    // --------------------------Perfect ----------------------
    private Tuple<float,float,float> LevelPerfectPercentDamagesV1(string ABILITY_NAME) 
    {
        var getMultiValue = WeaponManager.instance.SystemSkillWeapon.LevelPerfectV1(perfectAbilityLevelQ, ABILITY_NAME);
        var basePerfectValue = getMultiValue[0];
        var multiPerfectValue = getMultiValue[1];
        var percentPerfectValue = getMultiValue[2];
        return new Tuple<float,float,float>(basePerfectValue,multiPerfectValue,percentPerfectValue);
    }
    private Tuple<float,float,float> LevelPerfectPercentDamagesV2(string ABILITY_NAME)
    {
        var getMultiValue = WeaponManager.instance.SystemSkillWeapon.LevelPerfectV2(ABILITY_NAME);
        var basePerfectValue = getMultiValue[0];
        var multiPerfectValue = getMultiValue[1];
        var percentPerfectValue = getMultiValue[2];
        return new Tuple<float, float, float>(basePerfectValue, multiPerfectValue, percentPerfectValue);
    }
    private Tuple<float,float,float> LevelPerfectPercentDamagesV3(string ABILITY_NAME)
    {
        var getMultiValue = WeaponManager.instance.SystemSkillWeapon.LevelPerfectV2(ABILITY_NAME);
        var basePerfectValue = getMultiValue[0];
        var multiPerfectValue = getMultiValue[1];
        var percentPerfectValue = getMultiValue[2];
        return new Tuple<float, float, float>(basePerfectValue, multiPerfectValue, percentPerfectValue);
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
