using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    string Name { get; set; }
    int damages { get; set; }
    CombatTypeManager.DamegesType DamagesType { get; set; }
   
}
