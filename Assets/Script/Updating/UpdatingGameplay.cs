using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatingGameplay : MonoBehaviour
{
    public static UpdatingGameplay instance;
    private int _abilityLevelQ;
    private bool test;

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
    // DMG Q 
    // S3    ADD VALUE AND  CALL THE DAMAGES 
    public int FirstAbilityDMG() 
    {
        var resultDMG =  WeaponManager.instance.SystemSkillWeapon.AbilityQSword(LevelBaseDmgUpdating(),LevelMultiQUpdating());
        return resultDMG;
    }
    // DMG E 
    #region  Resauble method 
    //     --------------------- Q -----------------------
    // S1 MAKE  THE UPDATING VALUE FROM  ARRAY     
    private int  LevelBaseDmgUpdating() 
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
    #endregion
}
