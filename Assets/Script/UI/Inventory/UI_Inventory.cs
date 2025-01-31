using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_Inventory : MonoBehaviour
{
    public static UI_Inventory instance;
    public Dictionary<ItemSystem,int> _items;
    public Transform itemContent;
    public GameObject inventoryItem;
    public GameObject inventory;
    #region Main Monobehaivour
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
        _items= new Dictionary<ItemSystem, int>();
    }
    private void Update()
    {
        InputIventory();
        RefreshItemList();
    }
    #endregion
    #region UI INPUT 
    private void InputIventory()
    {
        Player.instance.playerInput.playerActions.Inventory.performed += ctx => InventoryUIActive();
    }
    private void InventoryUIActive()
    {
        inventory.SetActive(true);
    }
    #endregion
    #region Main Method
    public void AddItem(ItemSystem ITEM)
    {
        foreach (var item in GetItemList()) 
        {
            if (ITEM.id == item.Key.id) 
            {
                // set value moi vao day
                if (GetItemList().TryGetValue(item.Key,out int amount)) 
                {
                   _items[item.Key] = amount + 1;
                }
                return;
            
            }
        }
        _items.Add(ITEM,1);
      // se tao ra list amount va setup sao cho List [index] amount = List[index] list
      // neu co chung` id se lay id(index) = 1  index cua List amount ( cung same )
     
    }
    public void RemoveItem(ItemSystem ITEM)
    {
        _items.Remove(ITEM);
    }
    public void RefreshItemList()
    {
         // clear content before  open ;
         foreach (Transform item in itemContent)
         {
            Destroy(item.gameObject);
         }

        foreach (var item in GetItemList())
        {
            var objTransform = Instantiate(inventoryItem, itemContent);
            RectTransform objItemSlot = objTransform.GetComponent<RectTransform>();
           
            objItemSlot.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.Key.itemName;
            objItemSlot.Find("ItemIcon").GetComponent<Image>().sprite = item.Key.icon;
            var amountText = objItemSlot.Find("ItemStack").GetComponent<TextMeshProUGUI>();
            var renderText = item.Value ;// 1     va =
            if (item.Value > 1) 
            {
                amountText.SetText(renderText.ToString());
            }
            else 
            {
                amountText.SetText("");
            }

        }
    }
    #endregion
    #region Resauble Method
    public Dictionary<ItemSystem,int> GetItemList()
    {
        return _items;
    }
    // kiem tra xem id cua item phia ngoai co trung` vs id trong inventory k , neu co se tra lai. data co'
    public Tuple<int,int> CheckingIdItemInventory(int ID) 
    {
         foreach(var item in GetItemList()) 
         {
            if (ID == item.Key.id) 
            {
                return new Tuple<int,int>(item.Key.id,item.Value);
                
            }
        }
        return new Tuple<int, int>(0 , 0);
    }
    public void SetItemValueUpdating(int ID, int DEGREE_VALUE) 
    {
        for (int i = 0; i < GetItemList().Count; i++) 
        {
            if (GetItemList().ElementAt(i).Key.id == ID) 
            {
                if (GetItemList().TryGetValue(GetItemList().ElementAt(i).Key, out int amount))
                {
                    // set lai data 
                    var newValue = amount - DEGREE_VALUE;
                    _items[GetItemList().ElementAt(i).Key] = newValue;
                    Debug.Log("NEW VALUE  " + newValue);
                }
            }
        }
    }
    #endregion 
}
