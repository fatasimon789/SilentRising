using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class WeaponTypeMachine 
{
    public TypeOfWeapon currentWeapon { get; set; }
   

    // dat defaul vu khi nao  player sai 
    public void Initialize(TypeOfWeapon STARTING_WEAPON) 
    {
           currentWeapon = STARTING_WEAPON;
           currentWeapon.ChanceNewWeapon();
           
    }
    // Chance vu khi moi Delete vu khi cu
 
    public void ChanceWeapon(TypeOfWeapon CHANCE_NEW_WEAPON) 
    {
         currentWeapon.DeleteOldWeapon();
         currentWeapon = CHANCE_NEW_WEAPON;
         currentWeapon.ChanceNewWeapon();
    }
    
}