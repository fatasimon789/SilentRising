
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsSystem : IPlayerHeal
{
    public static PlayerStatsSystem instance;
    public float lerpSpeed { get; set; } = 0.05f;
    // ---------- STATS VALUE ----------------
    public int maxHPValue { get; set; }
    public float currentHPValue { get ; set ; }
    public int attackValue { get ; set; }
    public float defenseValue { get ; set ; }
    public float critRateValue { get; set ; }

    private float enemyTestDamages = 80f;

    public PlayerStatsSystem( int HP,int ATTACK , float DEF , float CRITRATE)
    {
        maxHPValue = HP;
        attackValue= ATTACK;
        defenseValue = DEF;
        critRateValue = CRITRATE;
        
    }

    public void StartHealSystem()
    {
        currentHPValue = maxHPValue;
      
    }
  
    public  void UIUpdateHealthBar()
    {
        if (SystemChanceWeapon.instance.isSwitch) 
        {
            StartHealSystem();
            Debug.Log("current hp from start heal = " + currentHPValue);
        }
        if (PlayerUI.instance.healBar.value != currentHPValue)
        {
            PlayerUI.instance.healBar.value = currentHPValue;
        }
        if (PlayerUI.instance.healBar.value != PlayerUI.instance.easeHealBar.value)
        {
            PlayerUI.instance.easeHealBar.value = Mathf.Lerp(PlayerUI.instance.easeHealBar.value, currentHPValue, lerpSpeed);
        }
        // test take damages
        if (Input.GetKeyDown(KeyCode.L))
        {
            //3 
            basicTakeDamages(takeDamegesValue(enemyTestDamages));
        }
        
    }
    // enemy call
    // HP va DEF VALUE se bi tru phu thuoc vao damages enemy
    // 2
    private float takeDamegesValue(float DAMAGES) 
    {
      float damagesValue = DAMAGES - damagesReduceDEF(DAMAGES);
        return damagesValue;
    }
    // 1 
    private float damagesReduceDEF(float DAMEGES_TAKEN) 
    {
        float damagesReduce = DAMEGES_TAKEN * multiplyDEF();
        return damagesReduce;
    }
    //0
    public void basicTakeDamages(float DAMAGES)
    {
        currentHPValue -= takeDamegesValue(DAMAGES);
      //  Debug.Log(currentHPValue + "take damages basic");
    }
    // player call
    public void takeHealing(int DAMAGES)
    {
       currentHPValue += DAMAGES;
    }

    public void takeFullHealing()
    {
        currentHPValue = maxHPValue;
    }
    // 0
    private float multiplyDEF(float averageValue = 200f) 
    {
        float baseDef =  defenseValue;
        float averageDEF = defenseValue + averageValue;
        float multiplyDEF = baseDef / averageDEF;
        if (multiplyDEF == 0) 
        {
            multiplyDEF = 1f;
        }
        return multiplyDEF;
    }

}
