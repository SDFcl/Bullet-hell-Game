using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;

    public GameObject WorldPrefab;

    [Header("Item Type")]
    public ItemType itemType;

    public GameObject HoldingPrefab;
    public List<ItemEffect> effects;
}