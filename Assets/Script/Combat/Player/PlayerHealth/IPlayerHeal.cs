using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerHeal 
{
    float lerpSpeed { get; set; }
    // Stats 
    int attackValue { get; set; }
    int maxHPValue { get; set; }
    int currentHPValue { get; set; }
    int defenseValue { get; set; }
    float critRateValue { get; set; }

    void takeDamages(int DAMAGES);
    void takeHealing(int DAMAGES);
    void UIUpdateHealthBar();
}
