using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRoblin : MonoBehaviour
{
    public SystemEnemyStats statsRobilin;
    protected EnemyRoblin enemyRoblin;

    private void Awake()
    {
        enemyRoblin = new EnemyRoblin(this);
    }
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    public void WaitToAttack()
    {
        StartCoroutine(enemyRoblin.CanAttack(enemyRoblin.delayAttack));
    }
}
