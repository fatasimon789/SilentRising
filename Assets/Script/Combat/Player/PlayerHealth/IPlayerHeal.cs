using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerHeal 
{
    float lerpSpeed { get; set; }
    int currentHeal { get; set; }
    int maxHeal { get; set; }
    void takeDamages(int DAMAGES);
    void takeHealing(int DAMAGES);
    void UIUpdateHealthBar();
}
