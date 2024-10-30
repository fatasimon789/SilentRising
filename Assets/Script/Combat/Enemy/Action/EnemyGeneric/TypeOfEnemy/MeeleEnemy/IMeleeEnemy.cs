using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeEnemy
{
    float distanceEnemy { get; set; }
    float damages { get; set; }
    bool isAttack { get; set; }
    bool isDead { get; set; }
    float delayAttack { get; set; }
    Rigidbody RGB { get; set; }
    Transform playerPos { get; set; }
    float attackRange { get; set; }
    float chasingSpeed { get; set; }
    float visionRange { get; set; }
    Animator animator { get; set; }

    void OnVision(float VISION_RANGES);
    void OnRange(float CHASING_SPEED);
    void Attack(bool INTO_RANGE);


}
