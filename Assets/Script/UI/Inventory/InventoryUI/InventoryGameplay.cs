using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGameplay
{
    public List<ItemSystem> items { get; set; }
    UI_Inventory inventoryGameplay { get; set; }
   
    public InventoryGameplay(UI_Inventory I_MANAGER) 
    {
          items = new List<ItemSystem>();
          inventoryGameplay = I_MANAGER;
    }
    
    public void AddItem(ItemSystem ITEM)
    {
        items.Add(ITEM);
    }
    public void RemoveItem(ItemSystem ITEM)
    {
        items.Remove(ITEM);
    }
    public List<ItemSystem> GetItemList()
    {
        return items;
    }
}
