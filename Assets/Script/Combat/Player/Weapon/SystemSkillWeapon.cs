using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New SystemSkill",menuName = "SystemSkill")]
public class SystemSkillWeapon : ScriptableObject
{
    #region  Field Data
    // Bao gom thong tin  : Weapon , DamgesType ,  Skill 
    public string nameOfWeapon;
    public GameObject weaponPrefap;
    public int attackRange;
    public CombatTypeManager.DamegesType DamagesType;
    public RuntimeAnimatorController animatorPlayer;

    [Header(" Basic Stats")]
    public int heal;
    public int dameges;
    public int defense;
    public float crit;

    [Header("CD")]
    public float AbiCoolDownQ;
    public float AbiCoolDownE;
    public float AbiCoolDownR;

    [Header("Projectile")]
    public GameObject projectile;

    [Header("Basic Damages")]
    public List<int> basicDmgQ;
    public List<int> basicDmgE;
    public List<int> basicDmgR;

    [Header("Multiply Damages")]
    public List<float> multiQ;
    public List<float> multiE;
    public List<float> multiR;

    [Header("User Percent Damages")]
    public float percentDmgQ;
    public float percentDmgE;
    public float percentDmgR;

    [Header("DiscriptionFirstAbility")]
    public string nameFirstAbility;
    [TextArea(10,15)]
    public string baseFirstAbility;
    [TextArea(3, 8)]
    public string perfectFirstAbility;
    public Sprite[] abilityIcon1;

    [Header("DiscriptionSecondAbility")]
    public string nameSecondAbility;
    [TextArea(10, 15)]
    public string baseSecondAbility;
    [TextArea(3, 8)]
    public string perfectSecondAbility;
    public Sprite[] abilityIcon2;

    [Header("DiscriptionUltimateAbility")]
    public string nameUltimateAbility;
    [TextArea(10, 15)]
    public string baseUltimateAbility;
    [TextArea(3, 8)]
    public string perfectUltimateAbility;
    public Sprite[] abilityIcon3;

    [Header("Material Upgrade ")]
    public List<ItemSystem> requiredItemUpgrade;
    #endregion

    #region Total Main  Stats
    // hp    lv 1 
    public int PlayerHP() 
    {
       
        int currentHP = heal  ;// more
        return currentHP;
    }

    // heal   lv 1 

    // FIRE SWORD
    // (Base DMG LV 1   + [User DMG/100 * USER PERCENTDMG LV 1]) * multiplyDMG LV 1   
    public float NormalAttackSword() 
    {
        var percentUserDMG = dameges / 100 * 65;
        var totalDMG = percentUserDMG ;
        return totalDMG;
    }
    //                  S2  
    public int AbilityQSword(int  BASE_DMG , float MULTI_Q) 
    {
      
        var percentUserDMG = dameges / 100 * percentDmgQ;
        var resultDMG = BASE_DMG + percentUserDMG;
        var totalDMG =  resultDMG * MULTI_Q;
        Debug.Log(totalDMG);
        return (int)totalDMG;
    }
    public int AbilityESword(int BASE_DMG, float MULTI_E)
    {
        var percentUserDMG = dameges / 100 * percentDmgE;
        var resultDMG = BASE_DMG + percentUserDMG;
        var totalDMG = resultDMG * MULTI_E;
        return (int)totalDMG;
    }
    public int AbilityRSword(int BASE_DMG, float MULTI_R)
    {
        var percentUserDMG = dameges / 100 * percentDmgR;
        var resultDMG = BASE_DMG + percentUserDMG;
        var totalDMG = resultDMG * MULTI_R;
        return (int)totalDMG;
    }
    // Ice fist , Q , E ,R  // attack
    #endregion

    #region Updating Resauable Stats
    public int BonusHP(int levelHP = 0,int newBonusHP = 100) 
    {

        levelHP++;
        if (levelHP <= 1) 
        {
          
        }
        if (levelHP == 10) 
        {
            Debug.Log("full level update");
        }
  
        return newBonusHP;
    }
    // S0 ADD ARRAY VALUE 
    // -------------- Q ------------------
    public void LevelCDQ() 
    {
    
    }
    public int LevelBaseDmgQ(int I_BASE_DMG) 
    {
         return basicDmgQ[I_BASE_DMG];
    }
    public float LevelMultiQ(int I_MULTI_Q)
    {
        return multiQ[I_MULTI_Q];
    }
    // --------------- E -------------------
    public int LevelBaseDmgE(int I_BASE_DMG)
    {
        return basicDmgE[I_BASE_DMG];
    }
    public float LevelMultiE(int I_MULTI_E)
    {
        return multiE[I_MULTI_E];
    } 
    // --------------- R -------------------
    public int LevelBaseDmgR(int I_BASE_DMG)
    {
        return basicDmgR[I_BASE_DMG];
    }
    public float LevelMultiR(int I_MULTI_R)
    {
        return multiR[I_MULTI_R];
    }
    #endregion
}
