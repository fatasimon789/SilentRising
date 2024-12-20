using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    public static UI_Inventory instance;
    private InventoryGameplay _inventoryGameplay;
    public GameObject inventoryObj;
    public GameObject inventory;
    public Transform itemContent;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _inventoryGameplay = new InventoryGameplay(this);
    }
    private void Update()
    {
        InputIventory();
    }
    #region UI INPUT 
    private void InputIventory() 
    {
         Player.instance.playerInput.playerActions.Inventory.performed += ctx => InventoryUIActive();
    }
    private void InventoryUIActive() 
    {
       inventory.SetActive(true);
        RefreshItemList();
    }
    #endregion
    public void RefreshItemList()
    {
        // clear content before  open ;
        foreach (Transform item in itemContent) 
        {
            Destroy(item.gameObject);
        }
        foreach (ItemSystem item in _inventoryGameplay.GetItemList())
        {
           GameObject obj = Instantiate(inventoryObj,itemContent);
            var itemName = obj.transform.Find("Item Name").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }
}
