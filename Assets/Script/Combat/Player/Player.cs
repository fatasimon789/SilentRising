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
    public PlayerStatsSystem healSystem { get; private set; }
    
   
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
        healSystem = new PlayerStatsSystem(WeaponManager.instance.weaponHP, WeaponManager.instance.weaponDamages,
                                          WeaponManager.instance.weaponDEF, WeaponManager.instance.weaponCRIT);
       
        playerMovementStateMachine.ChanceState(playerMovementStateMachine.idleState);
        healSystem.StartHealSystem();
    }
    private void Update()
    {
       
        playerMovementStateMachine.HandleInput();
        playerMovementStateMachine.Update();
        healSystem.UIUpdateHealthBar();
       
    }
    private void FixedUpdate()
    {
        playerMovementStateMachine.PhysicUpdate();   
    }
    
}
