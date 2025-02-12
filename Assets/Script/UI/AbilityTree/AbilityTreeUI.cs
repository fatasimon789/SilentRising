using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using static UnityEditor.Progress;

public class AbilityTreeUI : MonoBehaviour
{
    [field: Header("The Weapon")]
    [field: SerializeField] public SystemSkillWeapon weapon { get; private set; }
    [field: Header("ItemUpgrade")]
    [field: SerializeField] public Transform materialContent { get; private set; }
    [field: SerializeField] public GameObject materialItemsPrefap { get; private set; }

    [field: SerializeField] Dictionary<ItemSystem, int> abilityMaterialsData;

    [field: Header("textAbility")]
    [field: SerializeField] public TextMeshProUGUI nameAbility { get; private set; }
    [field: SerializeField] public TextMeshProUGUI discriptionBase { get; private set; }
    [field: SerializeField] public TextMeshProUGUI discriptionPerfect { get; private set; }
    [field: SerializeField] public TextMeshProUGUI warningText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI materialRequireText { get; private set; }

    [field: Header("ButtonEvent")]
    [field: SerializeField] public Button buttonActive { get; private set; }

    [field : Header("AbilityMultiControllerList")]

    [field: SerializeField] public List <GameObject> _lineAbilityQ { get; private set; }
    [field: SerializeField] public List <GameObject> _iconAbilityQ { get; private set; }
    [field: SerializeField] public List <GameObject> _lineAbilityE { get; private set; }
    [field: SerializeField] public List <GameObject> _iconAbilityE { get; private set; }
    [field: SerializeField] public List <GameObject> _lineAbilityR { get; private set; }

    [field: SerializeField] public List <GameObject> _iconAbilityR { get; private set; }
    // field
    private bool conditionActive1, conditionActive2;

    private int _currentAbilityLevelQ { get; set; }
    private int _currentAbilityLevelE { get; set; }
    private int _currentAbilityLevelR { get; set; }

    private void Start()
    {
        abilityMaterialsData = new Dictionary<ItemSystem, int>();
        GetMaterialUpgrade();
    }
    private void Update()
    {
        _currentAbilityLevelQ = UpdatingAbility.instance.abilityLevelQ;
        _currentAbilityLevelE = UpdatingAbility.instance.abilityLevelE;
        _currentAbilityLevelR = UpdatingAbility.instance.abilityLevelR;
        if (Input.GetKey(KeyCode.P))
        {
            // open abity tree
            RenderInfoAbilityQ(0);
        }
    }
    #region Main Method
    public void ActiveUpdateQ(int LEVEL_VALUE)
    {
        UpdatingAbility.instance.UpdatingAbilityTreeQ();
        DegreeMaterials(LEVEL_VALUE);
        SkillUnlockPathQ(LEVEL_VALUE);
        // effect unlock ( )
    }
    public void ActiveUpdateE(int LEVEL_VALUE)
    {
        UpdatingAbility.instance.UpdatingAbilityTreeE();
        DegreeMaterials(LEVEL_VALUE);
        SkillUnlockPathE(LEVEL_VALUE);
    }
    public void ActiveUpdateR(int LEVEL_VALUE)
    {
        UpdatingAbility.instance.UpdatingAbilityTreeR();
        DegreeMaterials(LEVEL_VALUE);
        SkillUnlockPathR(LEVEL_VALUE);
    }
    public void RenderInfoAbilityQ(int LEVEL)
    {
        nameAbility.text = weapon.nameFirstAbility + "" + IndexNameAbility(LEVEL);
        discriptionBase.text = weapon.baseFirstAbility + IndexValueInfoAbilityQ(LEVEL);
        discriptionPerfect.text = CheckingPerfectLevel(LEVEL);
        RenderInfoMaterials(LEVEL);
        ActiveButtonUpgrade(LEVEL,_currentAbilityLevelQ,weapon.nameFirstAbility);
        buttonActive.onClick.RemoveAllListeners();
        buttonActive.onClick.AddListener(delegate { ActiveUpdateQ(LEVEL); });
    }
    public void RenderInfoAbilityE(int LEVEL)
    {
        nameAbility.text = weapon.nameSecondAbility + "" + IndexNameAbility(LEVEL);
        discriptionBase.text = weapon.baseSecondAbility + IndexValueInfoAbilityE(LEVEL);
       
        discriptionPerfect.text = CheckingPerfectLevel(LEVEL);   
        RenderInfoMaterials(LEVEL);
        ActiveButtonUpgrade(LEVEL,_currentAbilityLevelE,weapon.nameSecondAbility);
        buttonActive.onClick.RemoveAllListeners();
        buttonActive.onClick.AddListener(delegate { ActiveUpdateE(LEVEL); });
    }
    public void RenderInfoAbilityR(int LEVEL)
    {
        nameAbility.text = weapon.nameUltimateAbility + "" + IndexNameAbility(LEVEL);
        discriptionBase.text = weapon.baseUltimateAbility + IndexValueInfoAbilityR(LEVEL);
        discriptionPerfect.text = CheckingPerfectLevel(LEVEL); 
        RenderInfoMaterials(LEVEL);
        ActiveButtonUpgrade(LEVEL,_currentAbilityLevelR,weapon.nameUltimateAbility);
        buttonActive.onClick.RemoveAllListeners();
        buttonActive.onClick.AddListener(delegate { ActiveUpdateR(LEVEL); });
    }
    #region Material method
    public void RenderInfoMaterials(int LEVEL)
    {
        GetValueDataMaterial();
        RenderMaterialsText(LEVEL);
    }
    public void RenderMaterialsText(int LEVEL_MATERIAL_REQUIRED)
    {
        // clear old data then render new 
        foreach (Transform item in materialContent)
        {
            Destroy(item.gameObject);
        }
        for (int i = 0; i < abilityMaterialsData.Count; i++)
        {
            var objTransform = Instantiate(materialItemsPrefap, materialContent);
            RectTransform objItemSlot = objTransform.GetComponent<RectTransform>();
            objItemSlot.Find("ItemIcon").GetComponent<Image>().sprite = abilityMaterialsData.ElementAt(i).Key.icon;
            var materialRequired = UpdatingAbility.instance.GetItemRequiredUpgrade(LEVEL_MATERIAL_REQUIRED);
            // vi list bat dau = 0 con` logic gameplay la 1 
            var materialValue = abilityMaterialsData.ElementAt(i).Value;
            objItemSlot.Find("ItemStack").GetComponent<TextMeshProUGUI>().text
                 = materialValue.ToString() + "/" + materialRequired[i].ToString();
            GetInfoButtonUpgrade(materialValue, materialRequired[i], i);
            if (materialRequired[i] == 0)
            {
                objTransform.SetActive(false);
            }
        }
    }
    private void GetMaterialUpgrade()
    {
        // get data material (from weapon) which want to upgrade 
        foreach (var getMaterial in weapon.requiredItemUpgrade)
        {
            // RENDER ITEM MATERIAL AND VALUE START 0 
            abilityMaterialsData.Add(getMaterial, 0);

        }
    }
    private void GetValueDataMaterial()
    {
        // method check xem trong inventory co' item hay k 

        for (int i = 0; i < abilityMaterialsData.Count; i++)
        {
            var findIdItem = UI_Inventory.instance.CheckingIdItemInventory(abilityMaterialsData.ElementAt(i).Key.id);
            // neu co thi se set value data 
            if (abilityMaterialsData.ElementAt(i).Key.id == findIdItem.Item1)
            {
                // set new value 
                if (abilityMaterialsData.TryGetValue(abilityMaterialsData.ElementAt(i).Key, out int value))
                {
                    // chance value data dictionary 
                    abilityMaterialsData[abilityMaterialsData.ElementAt(i).Key] = findIdItem.Item2;
                }
            }
        }
    }
    public void GetInfoButtonUpgrade(int DATA_MATERIAL, int DATA_MATERIAL_REQUIRED, int INDEX_ITEM)
    {
        if (DATA_MATERIAL >= DATA_MATERIAL_REQUIRED && INDEX_ITEM == 0)
        {
            conditionActive1 = true;
        }
        if (DATA_MATERIAL >= DATA_MATERIAL_REQUIRED && INDEX_ITEM == 1)
        {
            conditionActive2 = true;
        }
        else if (DATA_MATERIAL < DATA_MATERIAL_REQUIRED)
        {
            conditionActive1 = false;
            conditionActive2 = false;
        }

    }
    public void ActiveButtonUpgrade(int LEVEL_INDEX,int _CURRENT_ABILITY_LEVEL,string NAME_ABILITY)
    {
        // default 
        buttonActive.gameObject.SetActive(true);
        warningText.gameObject.SetActive(false);
        materialRequireText.gameObject.SetActive(true);
        materialContent.gameObject.SetActive(true);
        // Actived (on limit  0 >= -1 ...)
        if (_currentAbilityLevelQ >= LEVEL_INDEX || _currentAbilityLevelE >= LEVEL_INDEX 
         || _currentAbilityLevelR >= LEVEL_INDEX) 
        {
            buttonActive.GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
            buttonActive.GetComponentInChildren<TextMeshProUGUI>().text = "Actived";
            buttonActive.enabled= false;
            materialRequireText.gameObject.SetActive(false);
            materialContent.gameObject.SetActive(false);
            return;
        }
        // too far to Active (out limit is  2 < )
        else if (!LimitUpgradeAbility(LEVEL_INDEX,_CURRENT_ABILITY_LEVEL)) 
        {
            buttonActive.gameObject.SetActive(false);
            buttonActive.enabled = false;
            warningText.gameObject.SetActive(true);
            warningText.SetText("Need to Active"  + "" + NAME_ABILITY + "" + IndexNameAbility(LEVEL_INDEX - 1));
            // text warning 
        }
        // limit is 1 (cant upgraded , ran out materials )
        else if (!conditionActive1 || !conditionActive2 )
        {
            buttonActive.GetComponent<Image>().color = new Color(1,1,1,0.25f);
            buttonActive.enabled = false;
            buttonActive.GetComponentInChildren<TextMeshProUGUI>().text = "Active";
        }
        //limit is 1 (can upgrade)
        else if (conditionActive1 && conditionActive2 ) 
        {
            buttonActive.GetComponent<Image>().color = new Color (1,1,1,1);
            buttonActive.enabled = true;
            buttonActive.GetComponentInChildren<TextMeshProUGUI>().text = "Active";
        }
    }
    public void DegreeMaterials(int LEVEL) 
    {
        // degree materials (material required value)
        for(int i = 0; i < weapon.requiredItemUpgrade.Count; i++) 
        {
            var getRequiredValue = UpdatingAbility.instance.GetItemRequiredUpgrade(LEVEL);
            UI_Inventory.instance.SetItemValueUpdating(weapon.requiredItemUpgrade[i].id, getRequiredValue[i]);
        }
    }
    public void SkillUnlockPathQ(int LEVEL) 
    {
        var lineUnlock= _lineAbilityQ.ElementAt(LEVEL);
        var iconUnlock = _iconAbilityQ.ElementAt(LEVEL);
      
        StartCoroutine(LerpUnlockLineEffect(lineUnlock));
      
        StartCoroutine(LerpUnlockIconEffect(iconUnlock));
        EffectIcon(iconUnlock);
    }
    public void SkillUnlockPathE(int LEVEL)
    {
        var lineUnlock = _lineAbilityE.ElementAt(LEVEL);
        lineUnlock.GetComponent<UILineRenderer>().LineThickness = 40f;
        var iconUnlock = _iconAbilityE.ElementAt(LEVEL);
        iconUnlock.GetComponent<UICircle>().color = new Color(1, 1, 1, 1);
    }
    public void SkillUnlockPathR(int LEVEL)
    {
        var lineUnlock = _lineAbilityR.ElementAt(LEVEL);
        lineUnlock.GetComponent<UILineRenderer>().LineThickness = 40f;
        var iconUnlock = _iconAbilityR.ElementAt(LEVEL);
        iconUnlock.GetComponent<UICircle>().color = new Color(1, 1, 1, 1);
    }
    private void EffectIcon(GameObject PARENT_EFFECT) 
    {
        GameObject[] allChildrent = new GameObject[PARENT_EFFECT.transform.childCount];
        for(int i = 0; i < allChildrent.Length; i++) 
        {
            allChildrent[i] = PARENT_EFFECT.transform.GetChild(i).gameObject;
            allChildrent[i].SetActive(true);
        }
    }
    public void removeOldDataMaterials() 
    {
       // khi doi vu khi se xoa' cai cu~ 
    }
    #endregion
    #endregion
    #region Excecuting method out side 
    private IEnumerator LerpUnlockLineEffect(GameObject LINE_UNLOCK,float DURATION = 10f) 
    {
        float timeElapsedLine = 0f;
        var getLineUnlock = LINE_UNLOCK.GetComponent<UILineRenderer>();
        var getStartThickNess = getLineUnlock.LineThickness;
        var getEndThickNess = 40f;
        while (timeElapsedLine < DURATION) 
        {
           float t = timeElapsedLine / DURATION;
           var lineGlowTransform = Mathf.Lerp(getStartThickNess, getEndThickNess, t);
   
           getLineUnlock.LineThickness = lineGlowTransform;
           timeElapsedLine += Time.deltaTime;
        }
        yield return null;
    }
    private IEnumerator LerpUnlockIconEffect(GameObject ICON_UNLOCK,float DURATION = 3F) 
    {
        
        if (ICON_UNLOCK.TryGetComponent<Image>(out Image objectColor)) 
        {
            var IconColor  =  ICON_UNLOCK.GetComponent<Image>();
            float timeElapsedIcon = 0f;
            while (timeElapsedIcon < DURATION)
            {
                float t = timeElapsedIcon / DURATION;
                var iconColorLerp = Mathf.Lerp(IconColor.color.a, 1, t);
                IconColor.color = new Color(1, 1, 1, iconColorLerp);
                timeElapsedIcon += Time.deltaTime;
            }

        }
        else if (ICON_UNLOCK.TryGetComponent<UICircle>(out UICircle objectColor2)) 
        {
            var IconColor = ICON_UNLOCK.GetComponent<UICircle>();
            float timeElapsedIcon = 0f;
            while (timeElapsedIcon < DURATION)
            {
                float t = timeElapsedIcon / DURATION;
                var iconColorLerp = Mathf.Lerp(IconColor.color.a, 1, t);
                IconColor.color = new Color(1, 1, 1, iconColorLerp);
                timeElapsedIcon += Time.deltaTime;
            }
        }
        yield return null;
    }

    #endregion
    #region Resauble Method
    private string IndexValueInfoAbilityQ(int LEVEL) 
    {
        string valueDMG = weapon.basicDmgQ[LEVEL].ToString();
        return valueDMG;
    }
    private string IndexValueInfoAbilityE(int LEVEL)
    {
        string valueDMG = weapon.basicDmgE[LEVEL].ToString();
        return valueDMG;
    }
    private string IndexValueInfoAbilityR(int LEVEL)
    {
        string valueDMG = weapon.basicDmgR[LEVEL].ToString();
        return valueDMG;
    }
    private string IndexNameAbility(int LEVEL) 
    {
        var nameIndex = LEVEL + 1;
        return "(" + "+" + nameIndex.ToString() + ")";
    }
   
    private bool LimitUpgradeAbility(int GET_LEVEL,int GET_CURRENT_LEVEL) 
    {
        var limitValue = GET_LEVEL - GET_CURRENT_LEVEL;
        if (limitValue >= 2 ) 
        {
           return false;
        }
        else  
        {
          return true;
        }
    }
    
    private string CheckingPerfectLevel(int LEVEL) 
    {
        string perFectText = "" ;
        switch(LEVEL) 
        {
            default:
                perFectText= string.Empty;
                break;
            case 5:
                perFectText = string.Empty;
                break;
            case 6:
                // weapon perfect text + 1;
                break;
            case 7:
                // weapon perfect text + 1;
                break;
        }
        return perFectText;
    }
    #endregion
}
