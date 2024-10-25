using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerHeal 
{
    float lerpSpeed { get; set; }
    // Stats 
    int attackValue { get; set; }
    int maxHPValue { get; set; }
    float currentHPValue { get; set; }
    float defenseValue { get; set; }
    float critRateValue { get; set; }

    void basicTakeDamages(float DAMAGES);
    void takeHealing(int DAMAGES);
    void UIUpdateHealthBar();
}
