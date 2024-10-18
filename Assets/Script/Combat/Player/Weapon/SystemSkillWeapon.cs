using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SystemSkill",menuName = "SystemSkill")]
public class SystemSkillWeapon : ScriptableObject
{
    // Bao gom thong tin  : Weapon , DamgesType ,  Skill 
    public string nameOfWeapon;
    public GameObject weaponPrefap;
    public int attackRange;
    public CombatTypeManager.DamegesType DamagesType;
    public Animator animatorPlayer;

    [Header(" Basic Stats")]
    public int heal;
    public int dameges;
    public int defense;
    public float crit; 
}
