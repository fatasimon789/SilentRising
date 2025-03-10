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
    public CombatTypeManager.TypeElement DamagesType;
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
    public  GameObject [] projectile;

    [Header("Projectile Perfect")]
    public GameObject[] projectilePerfect;

    [Header("Indicator")]
    public GameObject[] projectilePerfectIndicator;
 

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

    [Header("Perfect Damages Q1")]
    public float basePerfectValueQ1;
    public float multiPerfectValueQ1;
    public float[] percentPerfectValueQ;

    [Header("Perfect Damages Q2")]
    public float basePerfectValueQ2;
    public float multiPerfectValueQ2;
    public float percentPerfectValueQ2;

    [Header("Perfect Damages Q3")]
    public float basePerfectValueQ3;
    public float multiPerfectValueQ3;
    public float percentPerfectValueQ3;

    [Header("Perfect Damages E1")]
    public float basePerfectValueE1;
    public float multiPerfectValueE1;
    public float[] percentPerfectValueE;

    [Header("Perfect Damages E2")]
    public float basePerfectValueE2;
    public float multiPerfectValueE2;
    public float percentPerfectValueE2;

    [Header("Perfect Damages E3")]
    public float basePerfectValueE3;
    public float multiPerfectValueE3;
    public float percentPerfectValueE3;

    [Header("Perfect Damages R1")]
    public float basePerfectValueR1;
    public float multiPerfectValueR1;
    public float[] percentPerfectValueR;

    [Header("Perfect Damages R2")]
    public float basePerfectValueR2;
    public float multiPerfectValueR2;
    public float percentPerfectValueR2;

    [Header("Perfect Damages R3")]
    public float basePerfectValueR3;
    public float multiPerfectValueR3;
    public float percentPerfectValueR3;

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

    [SerializeField]
    [AbilityPostion]
    public AbilityPostion[] _abilityPostionQ;
    [SerializeField]
    [AbilityPostion]
    public AbilityPostion[] _abilityPostionE;
    [SerializeField]
    [AbilityPostion]
    public AbilityPostion[] _abilityPostionR;
    [SerializeField]
    [AbilityPostion]
    public AbilityPostion[] _abilityPostionAttack;

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
        var percentUserDMG = Player.instance.statsSystem.attackValue/ 100 * percentDmgQ;
        var resultDMG = BASE_DMG + percentUserDMG;
        var totalDMG =  resultDMG * MULTI_Q;
     //   Debug.Log(totalDMG);
        return (int)totalDMG;
    }
    public int AbilityESword(int BASE_DMG, float MULTI_E)
    {
        var percentUserDMG = Player.instance.statsSystem.attackValue / 100 * percentDmgE;
        var resultDMG = BASE_DMG + percentUserDMG;
        var totalDMG = resultDMG * MULTI_E;
        return (int)totalDMG;
    }
    public int AbilityRSword(int BASE_DMG, float MULTI_R)
    {
        var percentUserDMG = Player.instance.statsSystem.attackValue / 100 * percentDmgR;
        var resultDMG = BASE_DMG + percentUserDMG;
        var totalDMG = resultDMG * MULTI_R;
        return (int)totalDMG;
    }
    public int PerfectDamagesV1(float BASE_VALUE ,float MULTI_VALUE,float PERCENT_PERFECT_ATK) 
    {
        var resultDMG = BASE_VALUE * MULTI_VALUE;
        var percentDMG  =  Player.instance.statsSystem.attackValue/100 * PERCENT_PERFECT_ATK;
        var totalDMG = resultDMG + percentDMG;
        return(int)totalDMG;
    }
    public int PerfectDamagesV2(float BASE_VALUE, float MULTI_VALUE, float PERCENT_PERFECT_ATK) 
    {
        var resultDMG = BASE_VALUE * MULTI_VALUE;
        var percentDMG = Player.instance.statsSystem.attackValue / 100 * PERCENT_PERFECT_ATK;
        var totalDMG = resultDMG + percentDMG;
        return (int)totalDMG;
    }
    public int PerfectDamagesV3(float BASE_VALUE, float MULTI_VALUE, float PERCENT_PERFECT_ATK)
    {
        var resultDMG = BASE_VALUE * MULTI_VALUE;
        var percentDMG = Player.instance.statsSystem.attackValue / 100 * PERCENT_PERFECT_ATK;
        var totalDMG = resultDMG + percentDMG;
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
    public List<float> LevelPerfectV1(int I_PERFECT,string NAME_ABILITY) 
    {
        List<float> values = new List<float>();
        switch (NAME_ABILITY) 
        {
            case "Q":
                values.Add(basePerfectValueQ1);
                values.Add(multiPerfectValueQ1);
                values.Add(percentPerfectValueQ[I_PERFECT]);
                break;
            case "E":
                values.Add(basePerfectValueE1);
                values.Add(multiPerfectValueE1);
                values.Add(percentPerfectValueE[I_PERFECT]);
                break;
            case "R":
                values.Add(basePerfectValueR1);
                values.Add(multiPerfectValueR1);
                values.Add(percentPerfectValueR[I_PERFECT]);
                break;
        }
        return values;
    }
    public List<float> LevelPerfectV2(string NAME_ABILITY)
    {
        List<float> values = new List<float>();
        switch (NAME_ABILITY)
        {
            case "Q":
                values.Add(basePerfectValueQ2);
                values.Add(multiPerfectValueQ2);
                values.Add(percentPerfectValueQ2);
                break;
            case "E":
                values.Add(basePerfectValueE2);
                values.Add(multiPerfectValueE2);
                values.Add(percentPerfectValueE2);
                break;
            case "R":
                values.Add(basePerfectValueR2);
                values.Add(multiPerfectValueR2);
                values.Add(percentPerfectValueR2);
                break;
        }
        return values;
    }
    public List<float> LevelPerfectV3(string NAME_ABILITY)
    {
        List<float> values = new List<float>();
        switch (NAME_ABILITY)
        {
            case "Q":
                values.Add(basePerfectValueQ3);
                values.Add(multiPerfectValueQ3);
                values.Add(percentPerfectValueQ3);
                break;
            case "E":
                values.Add(basePerfectValueE3);
                values.Add(multiPerfectValueE3);
                values.Add(percentPerfectValueE3);
                break;
            case "R":
                values.Add(basePerfectValueR3);
                values.Add(multiPerfectValueR3);
                values.Add(percentPerfectValueR3);
                break;
        }
        return values;
    }
    #endregion
   
}
