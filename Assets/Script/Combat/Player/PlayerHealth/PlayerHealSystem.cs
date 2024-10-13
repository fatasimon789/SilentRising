
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealSystem : IPlayerHeal
{
    public static PlayerHealSystem instance;
    public int currentHeal { get ; set ; }
    public int maxHeal { get; set; }
    public float lerpSpeed { get; set; } = 0.05f;
 

    public PlayerHealSystem(int MAXHEAL, int CURRENT_HEAL)
    {
        maxHeal = MAXHEAL;
        currentHeal = CURRENT_HEAL;
    }
  
    public void StartHealSystem()
    {
        currentHeal = maxHeal;
    }
    
    public  void UIUpdateHealthBar()
    {
        if (PlayerUI.instance.healBar.value != currentHeal)
        {
            PlayerUI.instance.healBar.value = currentHeal;
        }
        if (PlayerUI.instance.healBar.value != PlayerUI.instance.easeHealBar.value)
        {
            PlayerUI.instance.easeHealBar.value = Mathf.Lerp(PlayerUI.instance.easeHealBar.value, currentHeal, lerpSpeed);
        }
        // test take damages
        if (Input.GetKeyDown(KeyCode.L))
        {
            takeDamages(10);
        }
    }
    // enemy call
    public void takeDamages(int DAMAGES)
    {
        currentHeal -= DAMAGES;
        Debug.Log(currentHeal);
    }
    // player call
    public void takeHealing(int DAMAGES)
    {
       currentHeal += DAMAGES;
    }
}
