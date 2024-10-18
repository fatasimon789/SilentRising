using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void ChanceNewWeapon();
    public void DeleteOldWeapon();
   
    public  void Healing();
    public  void NormalAttack();

    public  void Dashing();

    public  void Passive();
    public  void FirstSkill();


    public  void SecondSkill();

    public  void UltimateSkill();
}
