using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class FireSword : TypeOfWeapon
{
    public FireSword(WeaponManager WEAPON_MANAGER, WeaponTypeMachine WEAPON_TYPE_MACHINE) : base(WEAPON_MANAGER, WEAPON_TYPE_MACHINE)
    {
    }
    private EnemyTargetInfo enemyInfo;
    private bool isStrengthenAttack;
   
    private float timeDuration = 3f;
    #region  Weapon Chance / Delete
    public override void ChanceNewWeapon()
    {
        base.ChanceNewWeapon();
        enemyInfo= new EnemyTargetInfo();
        // them vao day firesword
       // Debug.Log("Fire sword ");
       
    }
    public override void DeleteOldWeapon()
    {
        base.DeleteOldWeapon();
        // xoa vao day firesword
    }
    #endregion
    #region Basic Ability
    public override void Dashing()
    {
        base.Dashing();
    }


    public override void Healing()
    {
        base.Healing();
    }

    public override void NormalAttack()
    {
        base.NormalAttack();
    }
    #endregion
    #region Ability Weapon
    public override void Passive()
    {
        base.Passive();
        // crease 30 crit rate value 
    }
    public override void FirstSkill()
    {
        base.FirstSkill();
        // anim
        // CD  5S
    }

    public override void SecondSkill()
    {
        base.SecondSkill();
        // anim
        // 6S
    }

    public override void UltimateSkill()
    {
        base.UltimateSkill();
        // anim
    
        //20S
    }
    #endregion

    #region Main Method
    // update event
    public override void UpdateEvent()
    {
        base.UpdateEvent();
        perfectAttackQ();
    }

    // animation event 
    public override void triggerAbilitySkill(PlayerTriggerEventAnim.AbilityTriggerType triggerAbility)
    {
        base.triggerAbilitySkill(triggerAbility);
        switch (triggerAbility)
        {
            // COLLIDER
            case PlayerTriggerEventAnim.AbilityTriggerType.FirstAbi:
                FirstAbilityCollider();
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.SecondAbi:
                SecondAbilityCollider();
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.UltimateAbi:
                UltimateAbilityCollider();
                break;
            // VFX
            case PlayerTriggerEventAnim.AbilityTriggerType.EndAnimAbility:
                OffVfx();
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxFirstAbiSword:
                Player.instance.playerDataEffect.G_AbilityQ.SetActive(true);
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxSecondAbiSword:
                Player.instance.playerDataEffect.G_AbilityE.SetActive(true);
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxUltimateAbiSword:
                break;
            // PerfectAbility
            case PlayerTriggerEventAnim.AbilityTriggerType.firstPerfectAbility:
                if (isOnPerfectAbilityQ[0])
                {
                    //  code : time duration on :  animation on , target on , disable normal attack , onable perfect attack 
                    // taget animation : duration off straightaway ,- heal enemy , + heal player ...
                    isStrengthenAttack= true;
                    timeDuration = 3;
                    var perfectAbilityIndicator = weaponManager.CreateInstantitate(weaponManager.SystemSkillWeapon.perfectAbilityProjectile[0]);
                    perfectAbilityIndicator.SetActive(true);
                }
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.secondPerfectAbility:
                if (isOnPerfectAbilityE[0]) 
                {
                
                }
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.UltimatePerfectAbility:
                if (isOnPerfectAbilityR[0]) 
                {
                
                }
                break;
        }
    }

    public void FirstAbilityCollider()
    {
        AttackColliderAbility(ColiderDamagesQ(),UpdatingAbility.instance.FirstAbilityDMG());
       
    }
    public void SecondAbilityCollider()
    {
        AttackColliderAbility(ColiderDamagesE(),UpdatingAbility.instance.SecondAbilityDMG());
       
    }
    public void UltimateAbilityCollider()
    {
        var projectObjSword = weaponManager.CreateInstantitateWithoutParent(weaponManager.SystemSkillWeapon.projectile[0]);
        GroundSlash groundSlash = projectObjSword.GetComponent<GroundSlash>();
        //lay object clone tham chieu vao rigidbody  va nhap van toc bay ;

        projectObjSword.GetComponent<Rigidbody>().velocity = Player.instance.transform.forward * groundSlash.speed;
    }
    #endregion
    public void OffVfx()
    {
        // Q VFX
        Player.instance.playerDataEffect.G_AbilityQ.SetActive(false);
        // E VFX
        Player.instance.playerDataEffect.G_AbilityE.SetActive(false);
        // R VFX

        // Attack VFX
        Player.instance.playerMovementStateMachine.attackSwordState.vfxAllOff();
    }
    #region Resauble Method
    private Collider[] ColiderDamagesQ()
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapBox(weaponManager.updatingPosAbi.colliderPosQ.position,
                                        weaponManager.updatingPosAbi.localColliderHalfExtendQ, Quaternion.identity,weaponManager.layerMask);
       
        return colliderInfo;
    }
    private Collider[] ColiderDamagesE()
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapBox(weaponManager.updatingPosAbi.colliderPosE.position,
                                         weaponManager.updatingPosAbi.localColliderHalfExtendE, Quaternion.identity,weaponManager.layerMask);
        return colliderInfo;
    }
    private Collider[] ColiderDamagesR()
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapBox(weaponManager.updatingPosAbi.colliderPosR.position,
                                         weaponManager.updatingPosAbi.localColliderHalfExtendR, Quaternion.identity, weaponManager.layerMask);
        return colliderInfo;
    }
    private void AttackColliderAbility(Collider[] ABILITY_COL,float DMG)
    {
        foreach (Collider col in ABILITY_COL)
        {
           
            Vector3 closetPoint0 = col.ClosestPoint(Player.instance.transform.position); // diem collider gan nhat

            Vector3 posDifferent = (closetPoint0 - Player.instance.transform.position); //  chi ra huong khi va cham lan dau
                                                                                        //  va den trung tam collider
            Vector3 overlapDirection = posDifferent.normalized;

            RaycastHit hit;
           
            float raycastDistance = 10.0f; // something greater than your object's largest radius, 
                                           // so that the ray doesn't start inside of your object
            Vector3 rayStart = Player.instance.transform.position + overlapDirection * raycastDistance;
            Vector3 rayDirection = -overlapDirection;

            if (Physics.Raycast(rayStart, rayDirection, out hit, Mathf.Infinity, weaponManager.layerMask))
            {
                var targetInfo = hit.collider.GetComponent<IEnemy>();
                targetInfo.enemyHP.takeDamages(DMG);
            }
            else
            {
                // The ray missed your object, somehow. 
                // Most likely it started inside your object 
                // or there is a mistake in the layerMask
            }
        }
        
    }
    #region Perfect Ability Q
    private void perfectAttackQ(float MINIMUM_TARGET_RANGE = 1f,float MINIMUM_INDIACATOR_RANGE = 0.172f,float INDEX_MODIFY = 6f) 
    {
      
       if (isStrengthenAttack) 
       {
          float timeStarting = Time.deltaTime;
          timeDuration -= timeStarting;
          var indicatorObject = weaponManager.transform.Find("FireSwordPerfectQ(Clone)").gameObject;
            if (timeDuration > 0 ) 
            {
                var targetRange = MINIMUM_TARGET_RANGE * INDEX_MODIFY;
                var indicatorRange = MINIMUM_INDIACATOR_RANGE * INDEX_MODIFY ;
                var indicatorTransform =  indicatorObject.GetComponent<RectTransform>();
                indicatorTransform.localScale = IndicatorScale(indicatorRange);

                Player.instance.playerInput.playerActions.AttackSword.Disable();
                GameObject [] allEnemysInfo = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < allEnemysInfo.Length; i++) 
                {
                     if (distancePerfectAttackQ(allEnemysInfo[i]) <= targetRange) 
                     {
                        // enable perfect attack 
                 
                        Player.instance.playerInput.playerActions.SpecialAttack.performed += ctx => PerAttacking(out timeDuration);
                        enemyInfo.StoreTargetInfo(allEnemysInfo[i]);
                        
                    }
                }
            }
            else if (timeDuration <= 0) 
            {
              weaponManager.DestroyInstantiate(indicatorObject);
              isStrengthenAttack = false;
              Player.instance.playerInput.playerActions.SpecialAttack.Disable();
              Player.instance.playerInput.playerActions.AttackSword.Enable();
              return;
              // off all 
            }
        
        }
    }
    private void PerAttacking(out float TIME_UP) 
    {
        // run animation
        StartAnimation("PerfectAttack");
        TIME_UP = 0;
         
    }
    public void enemyTarget() 
    {
        Player.instance.transform.position = enemyInfo.LoadTargetInfo().transform.position;
        var enemy =  enemyInfo.LoadTargetInfo().GetComponent<IEnemy>();
        Player.instance.playerDataEffect.G_AbilityQ.SetActive(true);
        if (isOnPerfectAbilityQ[1]) 
        {
            Player.instance.statsSystem.takeHealing(30);
        }

        enemy.enemyHP.takeDamages(UpdatingAbility.instance.PerfectDMG());

        // gay sat thuong + heal + effect

    }
    #endregion
    #endregion
    #region Resauble Method
    private Vector3 IndicatorScale(float INTDICATOR_RANGE) 
    {
        Vector3 setScale = new Vector3(INTDICATOR_RANGE, INTDICATOR_RANGE, INTDICATOR_RANGE);
        return setScale;
    }
    private Vector2 PlayerPos() 
    {
        Vector2 playerCurrentPos = new Vector2(weaponManager.gameObject.transform.position.x, weaponManager.gameObject.transform.position.z);
        return playerCurrentPos;
    }
    private Vector3 EnemyrPos(GameObject FIND_ENEMY_POS)
    {
        Vector2 EnemyCurrentPos = new Vector2(FIND_ENEMY_POS.transform.position.x, FIND_ENEMY_POS.transform.position.z);
        return EnemyCurrentPos;
    }
    private float distancePerfectAttackQ(GameObject ENEMY) 
    {
        var distanceValue = Vector2.Distance(PlayerPos(), EnemyrPos(ENEMY));
        return distanceValue;
    }
   
#endregion
    public class EnemyTargetInfo
    {
        GameObject currentEnemy = new GameObject();
       public void StoreTargetInfo(GameObject ENEMY) 
       {
            currentEnemy = ENEMY;
       }
       public GameObject LoadTargetInfo() 
       {
            return currentEnemy;
       }
    }
}