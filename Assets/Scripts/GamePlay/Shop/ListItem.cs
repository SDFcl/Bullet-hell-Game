using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Random Item/List Item")]
public class ListItem : ScriptableObject
{
    [Header("Normal")]
    public List<GameObject> Normal = new List<GameObject>();

    [Header("Rare")]
    public List<GameObject> Rare = new List<GameObject>();

    [Header("Legendary")]
    public List<GameObject> Legendary = new List<GameObject>();
}
