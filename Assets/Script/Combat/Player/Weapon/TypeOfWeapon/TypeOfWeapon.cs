using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class TypeOfWeapon : IWeapon
{
    #region Generic Manager
    protected WeaponManager weaponManager;
   protected WeaponTypeMachine WeaponTypeMachine;
   public bool[] isOnPerfectAbilityQ { get; set; }
   public bool[] isOnPerfectAbilityE { get; set; }
   public bool[] isOnPerfectAbilityR { get; set; }

   protected static bool isAnimRunning = true;
    #endregion
    #region Field of skill
    // Dashing
    private Transform orientation;
    private float dashForce = 100;
    private float dashDuration = 0.5f;
    #endregion
    public TypeOfWeapon (WeaponManager WEAPON_MANAGER , WeaponTypeMachine WEAPON_TYPE_MACHINE) 
    {
        this.weaponManager= WEAPON_MANAGER;
        this.WeaponTypeMachine= WEAPON_TYPE_MACHINE;
    }
    #region Base Method
    public virtual void ChanceNewWeapon()
    {
       isOnPerfectAbilityQ = new bool[3];
       isOnPerfectAbilityE = new bool[3];
       isOnPerfectAbilityR = new bool[3];
    }
    public virtual void DeleteOldWeapon()
    {

    }
    public virtual void Healing()
    {

    }
    public virtual void NormalAttack()
    {

    }

    public virtual void Dashing()
    {
        DashingSkill();
        Debug.Log("is dash");
    }

    public virtual void Passive()
    {
    }
    #endregion
    #region Generic Ability Virtual
    public virtual void FirstSkill()
    {
        if (isAnimRunning) 
        {
          StartAnimation(Player.instance.playerAnimatorData.S_FirstAbi);
          ControllBehaviourDisable();
            Debug.Log("use Q");
        }
    }


    public virtual void SecondSkill()
    {
        if (isAnimRunning)
        {
          StartAnimation(Player.instance.playerAnimatorData.S_SecondAbi);
          ControllBehaviourDisable();
            Debug.Log("use E");

        }
    }

    public virtual void UltimateSkill()
    {
        if (isAnimRunning)
        {
          StartAnimation(Player.instance.playerAnimatorData.S_UltimateAbi);
          ControllBehaviourDisable();
            Debug.Log("use R");
        }
    }
    #endregion
    #region Collider Event
    protected Collider[] ColliderBox(Vector3 CENTER,Vector3 HALF_EXETENDS,Quaternion OREANATION ,LayerMask LAYER_MASK)
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapBox(CENTER,HALF_EXETENDS,OREANATION, LAYER_MASK);

        return colliderInfo;
    }
    protected Collider[] ColliderSquere(Vector3 POSTION, float RADIUS, LayerMask LAYER_MASK)
    {
        // tao collider damags

        Collider[] colliderInfo = Physics.OverlapSphere(POSTION, RADIUS, LAYER_MASK);

        return colliderInfo;
    }
    protected void AttackColliderAbility(Collider[] ABILITY_COL, float DMG)
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
                targetInfo.enemyHP.takeDamages(DMG, weaponManager.DamagesType);
            }
            else
            {
                // The ray missed your object, somehow. 
                // Most likely it started inside your object 
                // or there is a mistake in the layerMask
            }
        }

    }
    public virtual void triggerAbilitySkill(PlayerTriggerEventAnim.AbilityTriggerType triggerAbility) 
    {
        switch(triggerAbility) 
        {
            case PlayerTriggerEventAnim.AbilityTriggerType.EndAnimAbility:
                ControllBehaviourEnable();
                isAnimRunning = true;
                break;
        }
    }
    #endregion
    #region Properties and etc
    public virtual void UpdateEvent() 
    {
        
    }
    public virtual void FixUpdateEvent() 
    {
       
    }
    protected GameObject findParentAbility(string ABILITY_NAME) 
    {
        GameObject parent = GameObject.Find(ABILITY_NAME);
        return parent;
    }
    public  void StartAnimation(string ANIM) 
    {
        Player.instance.animator.SetTrigger(ANIM);
        isAnimRunning = false;
    }
    private void ControllBehaviourEnable() 
    {
      Player.instance.playerMovementStateMachine.attackSwordState.OnableControls();
    }
    private void ControllBehaviourDisable() 
    {
     Player.instance.playerMovementStateMachine.attackSwordState.DisableControls();
    }
    #endregion
    #region Generic Ability
    private void DashingSkill() 
    {
        orientation = Player.instance.transform;
        
        weaponManager.rgb.AddForce(orientation.forward * dashForce,ForceMode.Impulse);
        Debug.Log("force");
    }
  
    #endregion
}
