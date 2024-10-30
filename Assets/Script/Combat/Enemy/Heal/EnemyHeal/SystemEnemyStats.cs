using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "EnemyInfo")]
public class SystemEnemyStats : ScriptableObject 
{ 
        public string nameOfEnemy;
        public float hp;
        public float attack;
        public float attackRange;
        public float delayAttack;
        public float chasingSpeed;
        public float visionRange;
}
