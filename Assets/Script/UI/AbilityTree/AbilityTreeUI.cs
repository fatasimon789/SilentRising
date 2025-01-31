using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
    [field: Header("ButtonEvent")]
    [field: SerializeField] public Button buttonActive { get; private set; }
    // field
    private bool conditionActive1, conditionActive2;

    private int _currentAbilityLevelQ { get; set; }
    private int _currentAbilityLevelE { get; set; }
    private int _currentAbilityLevelR { get; set; }

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
    private void Start()
    {
        abilityMaterialsData = new Dictionary<ItemSystem, int>();
        GetMaterialUpgrade();
    }
    #region Main Method
    public void ActiveUpdateQ(int LEVEL_VALUE)
    {
        UpdatingAbility.instance.UpdatingAbilityTreeQ();
        DegreeMaterials(LEVEL_VALUE);
    }
    public void ActiveUpdateE(int LEVEL_VALUE)
    {
        UpdatingAbility.instance.UpdatingAbilityTreeE();
        DegreeMaterials(LEVEL_VALUE);
    }
    public void ActiveUpdateR(int LEVEL_VALUE)
    {
        UpdatingAbility.instance.UpdatingAbilityTreeR();
        DegreeMaterials(LEVEL_VALUE);
    }
    public void RenderInfoAbilityQ(int LEVEL)
    {
        nameAbility.text = weapon.nameFirstAbility;
        discriptionBase.text = weapon.baseFirstAbility + IndexValueInfoAbilityQ(LEVEL);
        discriptionPerfect.text = weapon.perfectFirstAbility + "";
        RenderInfoMaterials(LEVEL);
        ActiveButtonUpgrade(LEVEL);
        buttonActive.onClick.RemoveAllListeners();
        buttonActive.onClick.AddListener(delegate { ActiveUpdateQ(LEVEL); });
    }
    public void RenderInfoAbilityE(int LEVEL)
    {
        nameAbility.text = weapon.nameSecondAbility;
        discriptionBase.text = weapon.baseSecondAbility + IndexValueInfoAbilityE(LEVEL);
        discriptionPerfect.text = weapon.perfectSecondAbility + "";
        RenderInfoMaterials(LEVEL);
        ActiveButtonUpgrade(LEVEL);
        buttonActive.onClick.RemoveAllListeners();
        buttonActive.onClick.AddListener(delegate { ActiveUpdateE(LEVEL); });
    }
    public void RenderInfoAbilityR(int LEVEL)
    {
        nameAbility.text = weapon.nameUltimateAbility;
        discriptionBase.text = weapon.baseUltimateAbility + IndexValueInfoAbilityR(LEVEL);
        discriptionPerfect.text = weapon.perfectUltimateAbility + "";
        RenderInfoMaterials(LEVEL);
        ActiveButtonUpgrade(LEVEL);
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
    public void ActiveButtonUpgrade(int LEVEL_INDEX)
    {
        // Actived
        if (_currentAbilityLevelQ >= LEVEL_INDEX || _currentAbilityLevelE >= LEVEL_INDEX 
         || _currentAbilityLevelR >= LEVEL_INDEX) 
        {
            buttonActive.GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
            buttonActive.GetComponentInChildren<TextMeshProUGUI>().text = "Actived";
            buttonActive.enabled= false;
            return;
        }
        // too far to Active (out limit is 2 )
        else if (!LimitUpgradeAbility(LEVEL_INDEX)) 
        {
           //  make its need to upgrade something first 
        }
        // limit is 1 (cant upgraded , ran out materials )
        else if (!conditionActive1 || !conditionActive2 )
        {
            buttonActive.GetComponent<Image>().color = new Color(1,1,1,0.25f);
            buttonActive.enabled = false;
        }
        //limit is 1 (can upgraed)
        else if (conditionActive1 && conditionActive2 ) 
        {
            buttonActive.GetComponent<Image>().color = new Color (1,1,1,1);
            buttonActive.enabled = true;
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
   
    public void removeOldDataMaterials() 
    {
       // khi doi vu khi se xoa' cai cu~ 
    }
    #endregion
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

    private bool LimitUpgradeAbility(int GET_LEVEL) 
    {
        var limitValue = GET_LEVEL - _currentAbilityLevelQ;
        if (limitValue >= 2 ) 
        {
           return false;
        }
        else  
        {
          return true;
        }
    }
    // method neu button vao skill se hien ra dong` text cho ca 3 cai tren


    // method neu sau khi active thi so luong material - 1 va - trong iventory 
    // icon se sang len lai va line se sang len (theo thu tu se la -0- )


    // + tao them icon , discription ability cho moi loai vu khi 
    // mac dinh se la icon trang' 
    // tao 1 method de add cac icon trang' thanh icon ability vs moi loai vu khi 
    // tuong tu thay material  nang cap tung loai vu khi 
    #endregion
}
