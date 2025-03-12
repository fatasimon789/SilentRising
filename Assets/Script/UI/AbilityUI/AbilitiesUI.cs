using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AbilitiesUI : MonoBehaviour
{
    public static AbilitiesUI instance;
    // ability 1
    [Header("Ability1")]
    [SerializeField] private Image _currentImage1;
    [SerializeField] private Image _abilityImageCD1;
    [SerializeField] private Text _abilityText1;
    private float _abilityCD1;
    private float _currentAbilityCD1;
    private bool _isAbility1CD;

    // ability 2
    [Header("Ability2")]
    [SerializeField] private Image _currentImage2;
    [SerializeField] private Image _abilityImageCD2;
    [SerializeField] private Text _abilityText2;
    private float _abilityCD2;
    private float _currentAbilityCD2;
    private bool _isAbility2CD;

    [Header("Ability3")]
    [SerializeField] private Image _currentImage3;
    [SerializeField] private Image _abilityImageCD3;
    [SerializeField] private Text _abilityText3;
    private float _abilityCD3;
    private float _currentAbilityCD3;
    private bool _isAbility3CD;
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }
    void Start()
    {
        RefreshData();
    }

    private void FixedUpdate()
    {
        FirstAbilityInput();
        SecondAbilityInput();
        UltimateAbilityInput();

        AbilityCoolDownUI(ref _currentAbilityCD1, _abilityCD1, ref _isAbility1CD, _abilityImageCD1, _abilityText1);
        AbilityCoolDownUI(ref _currentAbilityCD2, _abilityCD2, ref _isAbility2CD, _abilityImageCD2, _abilityText2);
        AbilityCoolDownUI(ref _currentAbilityCD3, _abilityCD3, ref _isAbility3CD, _abilityImageCD3, _abilityText3);
    }
    private void FirstAbilityInput() 
    {
        Player.instance.playerInput.playerActions.AbilityQ.performed += ctx => FirstAbilityUI();
    }

    private void FirstAbilityUI()
    {
        _isAbility1CD = true;
        _currentAbilityCD1 = _abilityCD1;
       
    }

    private void SecondAbilityInput()
    {
        Player.instance.playerInput.playerActions.AbilityE.performed += ctx => SecondAbilityUI();
        
    }

    private void SecondAbilityUI()
    {
        _isAbility2CD = true;
        _currentAbilityCD2 = _abilityCD2;
    }

    private void UltimateAbilityInput()
    {
        Player.instance.playerInput.playerActions.AbilityR.performed += ctx => UltimateAbilityUI();
        _currentAbilityCD3 = _abilityCD3;
    }

    private void UltimateAbilityUI()
    {
        _isAbility3CD = true;
    }
    private void AbilityCoolDownUI(ref float CURRENT_CD,float MAX_CD,ref bool IS_CD,Image ABILITY_IMG,Text ABILITY_TEXT) 
    {
        if (IS_CD) 
        {
            CURRENT_CD -= Time.deltaTime;
            if (CURRENT_CD <= 0 ) 
            {
                IS_CD= false;
                CURRENT_CD = 0;
                if (ABILITY_IMG != null) 
                {
                   ABILITY_IMG.fillAmount= 0;
                }
                if (ABILITY_IMG != null) 
                {
                    ABILITY_TEXT.text = "";
                }
            }
            else 
            {
                if (ABILITY_IMG != null)
                {
                    ABILITY_IMG.fillAmount = CURRENT_CD / MAX_CD;
                }
                if (ABILITY_IMG != null)
                {
                    ABILITY_TEXT.text = Mathf.Ceil(CURRENT_CD).ToString();
                }
            }
        }
    }
    private void RefreshData() 
    {
        _currentImage1.sprite = WeaponManager.instance.SystemSkillWeapon.abilityIcon1[0];
        _currentImage2.sprite = WeaponManager.instance.SystemSkillWeapon.abilityIcon2[0];
        _currentImage3.sprite = WeaponManager.instance.SystemSkillWeapon.abilityIcon3[0];

        _abilityImageCD1.sprite = WeaponManager.instance.SystemSkillWeapon.abilityIcon1[0];
        _abilityImageCD2.sprite = WeaponManager.instance.SystemSkillWeapon.abilityIcon2[0];
        _abilityImageCD3.sprite = WeaponManager.instance.SystemSkillWeapon.abilityIcon3[0];

        _abilityCD1 = WeaponManager.instance.SystemSkillWeapon.AbiCoolDownQ;
        _abilityCD2 = WeaponManager.instance.SystemSkillWeapon.AbiCoolDownE;
        _abilityCD3 = WeaponManager.instance.SystemSkillWeapon.AbiCoolDownR;

        _abilityImageCD1.fillAmount = 0;
        _abilityImageCD2.fillAmount = 0;
        _abilityImageCD3.fillAmount = 0;

        _abilityText1.text = "";
        _abilityText2.text = "";
        _abilityText3.text = "";
    }
    public  void ResetAbilityUI() 
    {
       RefreshData();
    }
}
