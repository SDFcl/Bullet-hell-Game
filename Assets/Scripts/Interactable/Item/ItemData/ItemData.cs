using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable
}
public enum ConsumableType
{
    Useable,
    passive
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;

    public GameObject WorldPrefab;

    public RareType rareType;

    [Header("Item Type")]
    public ItemType itemType;
    /// 🔥 ถ้าเป็น Weapon ให้โชว์ HoldingPrefab
    public GameObject HoldingPrefab;
    /// 🔥 ถ้าเป็น Consumable ให้โชว์ Effects
    public ConsumableType consumableType;
    public List<ItemEffect> effects;
}