using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemShoppingUI : MonoBehaviour
{
    [field:SerializeField] public Transform nameItem { get; set; }
    [field:SerializeField] public Transform discriptionItem { get; set; }
    [field:SerializeField] public Transform selectionItem { get; set; }
    [field:SerializeField] public Button buyButton { get; set; }

    private Transform _container;
    private Transform _shopTemplate;
 
    public List<ItemSystem> Items;
    private int testMoneyValue = 5000;

    #region Main Monobehaviour
    private void Awake()
    {
        _container = transform.Find("Container");
        _shopTemplate = _container.Find("Template");
    }
    private void Start()
    {
         for (int i = 0; i < Items.Count; i++) 
         {
            CreateItemButton(Items[i],Items[i].icon, Items[i].cost,i);
         }
    }
    private void Update()
    {
        // turn off a template
        _shopTemplate.gameObject.SetActive(false);
    }
    #endregion
    #region Main Method
    // button click event 
    public void ButtonUpdateSelection(int ITEM_INDEX, int ITEM_COST) 
    {
        var RectTransform = EventSystem.current.currentSelectedGameObject.gameObject;
       
        selectionItem.transform.position = RectTransform.transform.position;
        selectionItem.gameObject.SetActive(true);
        nameItem.GetComponent<TextMeshProUGUI>().SetText(Items[ITEM_INDEX].name);
        discriptionItem.GetComponent<TextMeshProUGUI>().SetText(Items[ITEM_INDEX].itemDisciption);
        // buy item button  (button nay sai chung )
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(delegate { BuyItem(ITEM_INDEX,ITEM_COST); });
    }
    // create item button
    public void CreateItemButton(ItemSystem ITEM,Sprite ITEM_SPRITE, int ITEM_COST,int POSITION_INDEX) 
    {
        Transform ShopItemTransform = Instantiate(_shopTemplate,_container);
   
        RectTransform ShopItemRectTransform  = ShopItemTransform.GetComponent<RectTransform>();
        

        // create new item cost and more 
         float shopItemHeight = 100f;
         float shopItemWidth = 5f;
         ShopItemRectTransform.anchoredPosition = new Vector2(shopItemWidth,- shopItemHeight * POSITION_INDEX);
      

        ShopItemRectTransform.Find("Cost").GetComponent<TextMeshProUGUI>().SetText(ITEM_COST.ToString());
        ShopItemRectTransform.Find("Item Icon").GetComponent<Image>().sprite = ITEM_SPRITE;
        // add them button vao
        var OnClickFunction = ShopItemRectTransform.AddComponent<Button>();
        // thay vi bo onclick o ngoai inspector thi minh code vao day luon  / muc dich : phan biet 2 instantiate giong nhau
        // 1 method rieng biet 
        OnClickFunction.onClick.AddListener(delegate { ButtonUpdateSelection(POSITION_INDEX,ITEM_COST);});
   
        
    }

    // Buy botton my self
    public void BuyItem(int ITEM_ID,int COST_INFO ) 
    {
        if (testMoneyValue <= COST_INFO)
        {
            Debug.Log("not enought money");
            return;

        }
        testMoneyValue -= COST_INFO;
     //   Debug.Log(ITEM_ID + " ITEM ID" + "  " + COST_INFO.ToString());
          UI_Inventory.instance.AddItem(Items[ITEM_ID]);
    }
    #endregion
    #region Resauble Method

    #endregion
}
