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
    private bool isStrengthenAttack;
   
    private float timeDuration = 3f;
    #region  Weapon Chance / Delete
    public override void ChanceNewWeapon()
    {
        base.ChanceNewWeapon();
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
                if (isOnPerfectAbilityQ)
                {
                    //  code : time duration on :  animation on , target on , disable normal attack , onable perfect attack 
                    // taget animation : duration off straightaway ,- heal enemy , + heal player ...
                    isStrengthenAttack= true;
                    timeDuration = 3;
                    var perfectAbilityIndicator = WeaponManager.CreateInstantitate(WeaponManager.SystemSkillWeapon.perfectAbilityProjectile[0]);
                    perfectAbilityIndicator.SetActive(true);
                }
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.secondPerfectAbility:
                if(isOnPerfectAbilityE) 
                {
                
                }
                break;
            case PlayerTriggerEventAnim.AbilityTriggerType.UltimatePerfectAbility:
                if(isOnPerfectAbilityR) 
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
        var projectObjSword = WeaponManager.CreateInstantitateWithoutParent(WeaponManager.SystemSkillWeapon.projectile[0]);
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

        Collider[] colliderInfo = Physics.OverlapBox(WeaponManager.updatingPosAbi.colliderPosQ.position,
                                        WeaponManager.updatingPosAbi.localColliderHalfExtendQ, Quaternion.identity,WeaponManager.layerMask);
       
        return colliderInfo;
    }
    private Collider[] ColiderDamagesE()
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapBox(WeaponManager.updatingPosAbi.colliderPosE.position,
                                         WeaponManager.updatingPosAbi.localColliderHalfExtendE, Quaternion.identity,WeaponManager.layerMask);
        return colliderInfo;
    }
    private Collider[] ColiderDamagesR()
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapBox(WeaponManager.updatingPosAbi.colliderPosR.position,
                                         WeaponManager.updatingPosAbi.localColliderHalfExtendR, Quaternion.identity, WeaponManager.layerMask);
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

            if (Physics.Raycast(rayStart, rayDirection, out hit, Mathf.Infinity, WeaponManager.layerMask))
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
    private void perfectAttackQ() 
    {
      
       if (isStrengthenAttack) 
       {
          float timeStarting = Time.deltaTime;
          timeDuration -= timeStarting;
      //    Transform indicatorRectTransform =
            if (timeDuration > 0) 
            {
                // attack per fect 
               
            }
            else 
            {
              var indicatorObject =  WeaponManager.transform.Find("FireSwordPerfectQ(Clone)").gameObject;
              WeaponManager.DestroyInstantiate(indicatorObject);
              isStrengthenAttack = false;
                // off all 
            }
        
        }
    }
    #endregion
#region Resauble Method
  
#endregion
}