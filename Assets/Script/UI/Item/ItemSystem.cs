using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Item/Create New Item")]
public class ItemSystem : ScriptableObject
{
    public int id;
    public string itemName;
    [TextArea(3,8)]public string itemDisciption;
    public int cost;
    public Sprite icon;

}
