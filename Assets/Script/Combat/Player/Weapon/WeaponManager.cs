using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public SystemSkillWeapon SystemSkillWeapon;

    [field: Header("PosColliderAbi")]
    [field:SerializeField] public PostionAbilityCollider posAbilityCollider;
    [field: SerializeField] public LayerMask layerMask { get; set; }

    [field : SerializeField] public Rigidbody rgb { get; set; }
    public int weaponDamages { get;private  set; }
    public int weaponHP { get;private  set; }
    public int weaponDEF { get;private set; }
    public float weaponCRIT { get;private set; }
    public CombatTypeManager.TypeElement DamagesType { get;private set; }
    public string nameWeapon { get;  set; }

    public bool IsCDAbiQ { get; private set; }
    public bool IsCDAbiE { get ; private set; }
    public bool IsCDAbiR { get ; private set; }
    public bool IsCDDashing { get; private set; }
   
    #region Weapon Type Machine
    public WeaponTypeMachine  WeaponMachine { get; set; }
    public FireSword FireSword { get; set; }
    public IcePunch IcePunch { get; set; }
    #endregion
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(instance);
        }
        WeaponMachine = new WeaponTypeMachine();

        FireSword = new FireSword(this,WeaponMachine);
        IcePunch  = new IcePunch(this,WeaponMachine);
        

    }
    private void Start()
    {
        // give the value from weapon 
        WeaponMachine.Initialize(FireSword);
        nameWeapon = SystemSkillWeapon.name;
        DamagesType = SystemSkillWeapon.DamagesType;
        // InitlizeFirstWeapon Start
        weaponDamages = SystemSkillWeapon.dameges;
        weaponHP = SystemSkillWeapon.heal;
        weaponDEF = SystemSkillWeapon.defense;
        weaponCRIT = SystemSkillWeapon.crit;
      
    }
    private void Update()
    {
        // Weapon INFO
        nameWeapon = SystemSkillWeapon.name;
        DamagesType = SystemSkillWeapon.DamagesType;
        // stats value
        StatsWeaponUpdate();
        WeaponMachine.currentWeapon.UpdateEvent();
       
      //  VirtualBox.DisplayBox(posAbilityCollider.colliderPosQ.transform.position,
      //  SystemSkillWeapon._abilityPostionQ[0].rangeExtendBoxCollider,this.transform.rotation);
    }
    private void FixedUpdate()
    {
        // Weapon Ability
        PassivePlayer();
        NormalAttackPlayer();
        Ability1Input();
        Ability2Input();
        AbilityUltimate();
        DashingIpnut();
        HealingIpnut();
        // event
        WeaponMachine.currentWeapon.FixUpdateEvent();

    }
    #region All Skill
    // H
    public  void HealingPlayer()
    {
        // refrence normal healling
        WeaponMachine.Healing();
        // H input
        // neu muon no co 1 chut dac biet thi se + - vao day
    }
    public  void NormalAttackPlayer()
    {
        WeaponMachine.normalAttack();
    }

    // left CTRL
    public void DashingPlayer()
    {
        if (!IsCDDashing) 
        {
            WeaponMachine.Dashing();
            StartCoroutine(CdDashing());
        }
    }

    public void PassivePlayer()
    {
        WeaponMachine.passive();
    }
    // Q 
    public void FirstSkillPlayer()
    {
        if (!IsCDAbiQ) 
        {
            WeaponMachine.FirstSkill();
            StartCoroutine(CDAbiQ());
        }
    }

    // E
    public void SecondSkillPlayer()
    {
        if (!IsCDAbiE) 
        {
            WeaponMachine.SecondSkill();
            StartCoroutine(CDAbiE());
        }
    }
    // R
    public void UltimateSkillPlayer()
    {
        if (!IsCDAbiR) 
        {
            WeaponMachine.Ultimate();
            StartCoroutine(CDAbiR());
        }
    }
    #endregion
    #region Updating event 
    public void StatsWeaponUpdate()
    {
        weaponDamages = SystemSkillWeapon.dameges;
        weaponHP = SystemSkillWeapon.heal;
        weaponDEF = SystemSkillWeapon.defense;
        weaponCRIT = SystemSkillWeapon.crit;
    }
    #endregion
    #region Input Player
    public void Ability1Input()
    {
         Player.instance.playerInput.playerActions.AbilityQ.performed += ctx => FirstSkillPlayer();
    }
    public void Ability2Input()
    {
      
          Player.instance.playerInput.playerActions.AbilityE.performed += ctx => SecondSkillPlayer();
    }
    public void AbilityUltimate()
    {
       
          Player.instance.playerInput.playerActions.AbilityR.performed += ctx => UltimateSkillPlayer();
    }
    public void HealingIpnut() 
    {
       Player.instance.playerInput.playerActions.Healing.performed+= ctx => HealingPlayer();
    }
    public void DashingIpnut()
    {
        Player.instance.playerInput.playerActions.Dash.performed += ctx => DashingPlayer();
    }


    #endregion
    #region Clone Event
    public GameObject CreateInstantitate(GameObject PROJECTILE,GameObject PARENT_OBJECT) 
    {
        var projectilePosx = this.transform.localPosition.x + PROJECTILE.transform.localPosition.x;
        var projectilePosy = this.transform.localPosition.y + PROJECTILE.transform.localPosition.y;
        var projectilePosz = this.transform.localPosition.z + PROJECTILE.transform.localPosition.z;
        var projectTilePostion = new Vector3(projectilePosx,projectilePosy,projectilePosz);
        Quaternion rotationProjectile = Quaternion.Euler
        (PROJECTILE.transform.eulerAngles.x,PROJECTILE.transform.eulerAngles.y + this.transform.eulerAngles.y,PROJECTILE.transform.eulerAngles.z);
        var projectileObj = Instantiate(PROJECTILE,PARENT_OBJECT.transform.position + projectTilePostion,rotationProjectile,PARENT_OBJECT.transform);
        return projectileObj;
    }
    public GameObject CreateInstantitateWithoutParent(GameObject PROJECTILE)
    {
        var projectileObj = Instantiate(PROJECTILE, this.transform.position, Quaternion.identity);
        return projectileObj;
    }
    public void DestroyInstantiate(GameObject GAMEOBJECT,float TIME_DESTROY) 
    {
        DelayCoroutineEvent(delayVfxDestroy(GAMEOBJECT,TIME_DESTROY));
    }
    public void DestroyInstantiate(GameObject GAMEOBJECT)
    {
        Destroy(GAMEOBJECT);
    }
    #endregion
    #region Ability CoolDown
    private IEnumerator CDAbiQ() 
    {
        IsCDAbiQ = true;
        yield return new WaitForSeconds(SystemSkillWeapon.AbiCoolDownQ);
        IsCDAbiQ = false;
    }
    private IEnumerator CDAbiE()
    {
        IsCDAbiE = true;
        yield return new WaitForSeconds(SystemSkillWeapon.AbiCoolDownE);
        IsCDAbiE = false;
    }
    private IEnumerator CDAbiR()
    {
        IsCDAbiR = true;
        yield return new WaitForSeconds(SystemSkillWeapon.AbiCoolDownR);
        IsCDAbiR = false;
    }
    private IEnumerator CdDashing() 
    {
        IsCDDashing = true;
        yield return new WaitForSeconds(1f);
        IsCDDashing = false;
    }
    private IEnumerator delayVfxDestroy(GameObject GET_OBJECT,float TIME_DELAY) 
    {
        yield return new WaitForSeconds(TIME_DELAY);
        Destroy(GET_OBJECT);
    }
    #endregion
    #region Coroutine Event && Delay Event
    private void DelayCoroutineEvent(IEnumerator TIME_DELAY) 
    {
        StartCoroutine(TIME_DELAY);
    }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
     //   Gizmos.DrawWireSphere(transform.position, testRadius);
    }

    #endregion
}
