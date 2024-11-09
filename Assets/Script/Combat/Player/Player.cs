using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour

{
    public static Player instance { get; private set; }

    [field : Header("Animation")]
    [field : SerializeField ] public PlayerAnimData playerAnimatorData { get; private set; }
    [field : Header("Vfx")]
    [field : SerializeField]public PlayerDataEffect playerDataEffect { get; private set; }
    public Rigidbody rgb { get; private set; }
    public PlayerInput playerInput { get; private set; }
    public PlayerMovementStateMachine playerMovementStateMachine;
    public Camera _mainCamera { get; private set; }
    public Animator animator { get; private set;}
    public PlayerStatsSystem statsSystem { get;  set; }
    private bool isCheckingSwitch;
    
    private void Awake()
    {
        if(instance == null) { instance = this; } else { Destroy(instance); }
       
        playerMovementStateMachine = new PlayerMovementStateMachine(this);
       
        
        rgb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerAnimatorData.Initilized();
        playerDataEffect.InitilizedVfx();
        animator = GetComponentInChildren<Animator>();
      
    }
    private void Start()
    {
       
        statsSystem = new PlayerStatsSystem(WeaponManager.instance.weaponHP, WeaponManager.instance.weaponDamages,
                                          WeaponManager.instance.weaponDEF, WeaponManager.instance.weaponCRIT);
        statsSystem.StartHealSystem();
        playerMovementStateMachine.ChanceState(playerMovementStateMachine.idleState);
    }
    private void Update()
    {
        playerMovementStateMachine.HandleInput();
        playerMovementStateMachine.Update();
        statsSystem.UIUpdateHealthBar();
        
        if (SystemChanceWeapon.instance.isSwitch && !isCheckingSwitch ) 
        {
            isCheckingSwitch = true;
            statsSystem = new PlayerStatsSystem(WeaponManager.instance.weaponHP, WeaponManager.instance.weaponDamages,
                                                WeaponManager.instance.weaponDEF, WeaponManager.instance.weaponCRIT);
            StartCoroutine(checkingOff());
        }
        animator.runtimeAnimatorController = WeaponManager.instance.SystemSkillWeapon.animatorPlayer;
    }
    private void FixedUpdate()
    {
        playerMovementStateMachine.PhysicUpdate();   
    }
    IEnumerator checkingOff() 
    {
      yield return new WaitForSeconds(1);
        isCheckingSwitch = false;
    }  
}
