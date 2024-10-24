
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsSystem : IPlayerHeal
{
    public static PlayerStatsSystem instance;
    public float lerpSpeed { get; set; } = 0.05f;
    // ---------- STATS VALUE ----------------
    public int maxHPValue { get; set; }
    public int currentHPValue { get ; set ; }
    public int attackValue { get ; set; }
    public int defenseValue { get ; set ; }
    public float critRateValue { get; set ; }

    public PlayerStatsSystem( int HP,int ATTACK , int DEF , float CRITRATE)
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
            takeDamages(10);
        }
    }
    // enemy call
    // HP va DEF VALUE se bi tru phu thuoc vao damages enemy
    public void takeDamages(int DAMAGES)
    {
        currentHPValue -= DAMAGES;
        Debug.Log(currentHPValue);
    }
    // player call
    public void takeHealing(int DAMAGES)
    {
       currentHPValue += DAMAGES;
    }
}
