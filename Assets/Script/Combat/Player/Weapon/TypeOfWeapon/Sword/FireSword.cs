using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.VFX;

public class FireSword : TypeOfWeapon
{
    public FireSword(WeaponManager WEAPON_MANAGER, WeaponTypeMachine WEAPON_TYPE_MACHINE) : base(WEAPON_MANAGER, WEAPON_TYPE_MACHINE)
    {
    }
    private EnemyTargetInfo enemyInfo;
    //Dot
    private float timeDotDuration { get; set; }
    private float minBeginDot { get; set; }
    private bool activeDOT1,activeDOT2 ;
    // Perfect Q
    private bool isActivePerfectQ;
    private float timeDuration = 3f;
    private int stealHP = 15;

    public float radiusSwing = 4.5f;
    public CombatTypeManager.TypeElement g ;
    // Perfect E
    private VisualEffect visualEffect;
    private bool isActivePerfectE;
    // Perfect R
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
        PerfectAttackE();
     
    }
    public override void FixUpdateEvent()
    {
        base.FixUpdateEvent();
        if (activeDOT1) 
        {
           BurningTarget();
        }
        if (activeDOT2)
        {
            BurningSlash();
        }

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
            case PlayerTriggerEventAnim.AbilityTriggerType.EndAnimAbility:
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxFirstAbiSword:
                var getParentAbilityQ = findParentAbility("AbilityQ");
                var abilityInstantiate= weaponManager.CreateInstantitate(Player.instance.playerDataEffect.G_AbilityQ,getParentAbilityQ);
                DestroyClone(abilityInstantiate,2);
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxSecondAbiSword:
                var getParentAbilityE = findParentAbility("AbilityE");
                var abilityIstantiate1 = weaponManager.CreateInstantitate(Player.instance.playerDataEffect.G_AbilityE,getParentAbilityE);
                DestroyClone(abilityIstantiate1,2);

           
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.vfxUltimateAbiSword:
                break;
            // PerfectAbility
            case PlayerTriggerEventAnim.AbilityTriggerType.firstPerfectAbility:
                if (isOnPerfectAbilityQ[0])
                {
                    //  code : time duration on :  animation on , target on , disable normal attack , onable perfect attack 
                    // taget animation : duration off straightaway ,- heal enemy , + heal player ...
                    isActivePerfectQ= true;
                    timeDuration = 3;
                    var perfectAbilityIndicator = weaponManager.CreateInstantitate(weaponManager.SystemSkillWeapon.projectilePerfectIndicator[0],weaponManager.gameObject);
                    perfectAbilityIndicator.SetActive(true);
                    Player.instance.playerInput.playerActions.SpecialAttack.Enable();
                }
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.secondPerfectAbility:
                if (isOnPerfectAbilityE[1]) 
                {
                    isActivePerfectE= true;
                }
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.UltimatePerfectAbility:
                if (isOnPerfectAbilityR[2]) 
                {
                
                }
                break;
        }
    }

    public void FirstAbilityCollider()
    {
        AttackColliderAbility(ColliderBox(weaponManager.posAbilityCollider.colliderPosQ.transform.position,
                             weaponManager.SystemSkillWeapon._abilityPostionQ[0].rangeExtendBoxCollider
                            ,weaponManager.transform.rotation, weaponManager.layerMask),UpdatingAbility.instance.FirstAbilityDMG());
       
    }
    public void SecondAbilityCollider()
    {
        AttackColliderAbility(ColliderBox(weaponManager.posAbilityCollider.colliderPosE.transform.position,
                              weaponManager.SystemSkillWeapon._abilityPostionE[0].rangeExtendBoxCollider
                             ,weaponManager.transform.rotation, weaponManager.layerMask),UpdatingAbility.instance.SecondAbilityDMG());
       
    }
    public void UltimateAbilityCollider()
    {
        var projectObjSword = weaponManager.CreateInstantitateWithoutParent(weaponManager.SystemSkillWeapon.projectile[0]);
        GroundSlash groundSlash = projectObjSword.GetComponent<GroundSlash>();
        //lay object clone tham chieu vao rigidbody  va nhap van toc bay ;

        projectObjSword.GetComponent<Rigidbody>().velocity = Player.instance.transform.forward * groundSlash.speed;
    }

    public void DestroyClone(GameObject VFX_OBJECT,float TIME_END) 
    {
          weaponManager.DestroyInstantiate(VFX_OBJECT,TIME_END);
    }
    #endregion
   
    #region DOT
    public void BurningTarget() 
    {
        CombatTypeManager.DamegesOverTime(enemyInfo.LoadTargetInfo(),UpdatingAbility.instance.PerfectDMGV2("Q"),
        timeDotDuration = 4f, minBeginDot = 1f,weaponManager.DamagesType);
        if (CombatTypeManager._liveTime.LiveTimeOut() >= timeDotDuration) 
        {
            activeDOT1 = false;
            CombatTypeManager._liveTime.ResetTimeOut();
            CombatTypeManager._liveTime.ResetMinTimeOut();
        }
    }
    public void BurningSlash()
    {
        CombatTypeManager.DamegesOverTime(enemyInfo.LoadTargetInfo(), UpdatingAbility.instance.PerfectDMGV2("E"),
        timeDotDuration = 5,  minBeginDot = 0.5f, weaponManager.DamagesType);

        if (CombatTypeManager._liveTime.LiveTimeOut() >= timeDotDuration)
        {
            activeDOT2 = false;
            CombatTypeManager._liveTime.ResetTimeOut();
            CombatTypeManager._liveTime.ResetMinTimeOut();
        }
    }
 
    #endregion
    #region Perfect Ability Q
    private void perfectAttackQ(float MINIMUM_TARGET_RANGE = 1f,float MINIMUM_INDIACATOR_RANGE = 0.172f,float INDEX_MODIFY = 6f) 
    {
      
       if (isActivePerfectQ) 
       {
          float timeStarting = Time.deltaTime;
          timeDuration -= timeStarting;
          var indicatorObject = weaponManager.transform.Find("Indicator_PerfectQ(Clone)").gameObject;
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
              isActivePerfectQ = false;
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
            Player.instance.statsSystem.takeHealing(stealHP);
            // dame dot
            activeDOT1 = true;
        }

        enemy.enemyHP.takeDamages(UpdatingAbility.instance.PerfectDMGV1("Q"),weaponManager.DamagesType);

        // gay sat thuong + heal + effect

    }
    public void CreateSlash() 
    {
        if (isOnPerfectAbilityQ[2]) 
        {
         var projectVfx = weaponManager.CreateInstantitateWithoutParent(weaponManager.SystemSkillWeapon.projectilePerfect[0]);
         Vector3 editScale = new Vector3(2,2,2);
         projectVfx.transform.localScale = editScale;
         AttackColliderAbility(PerfectSwingQ(),UpdatingAbility.instance.PerfectDMGV3("Q"));
            DestroyClone(projectVfx, 2);
         
        
        }

    }
    private Collider[] PerfectSwingQ() 
    {
        Collider[] colliderInfo = Physics.OverlapSphere(weaponManager.transform.position,
                                             radiusSwing, weaponManager.layerMask);
        return colliderInfo;
    }
    #endregion
    #region Perfect Ability E
    private void PerfectAttackE() 
    {
        if (isActivePerfectE) 
        {
            var findParentEffect = findParentAbility("AbilityE");
            var getTargetEffect = findParentEffect.GetComponentInChildren<VisualEffect>();
            visualEffect = getTargetEffect;
           if (visualEffect.aliveParticleCount <= 0.1f )
           { 
                // make a double slash on here
                   CreatingBigSlash(); 
           }
        
        }
    }
    private void CreatingBigSlash()
    { 
        var vfxProject = weaponManager.CreateInstantitateWithoutParent(weaponManager.SystemSkillWeapon.projectilePerfect[1]);
        vfxProject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        isActivePerfectE = false;
        DestroyClone(vfxProject, 1.5f);
        AttackColliderAbility(ColliderBox(weaponManager.posAbilityCollider.colliderPosE.transform.position,
                             weaponManager.SystemSkillWeapon._abilityPostionE[1].rangeExtendBoxCollider
                            , weaponManager.transform.rotation, weaponManager.layerMask), UpdatingAbility.instance.PerfectDMGV2("E"));
        if (isOnPerfectAbilityE[1]) 
        {
        //    activeDOT2 = true;
        }
        CreatingSlash4Direction();
    }
    private void CreatingSlash4Direction() 
    {
        var vfxProject = weaponManager.CreateInstantitateWithoutParent(weaponManager.SystemSkillWeapon.projectilePerfect[2]);
        DestroyClone(vfxProject, 2f);
    }
    #endregion
    #region Collider Damages
   
   
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