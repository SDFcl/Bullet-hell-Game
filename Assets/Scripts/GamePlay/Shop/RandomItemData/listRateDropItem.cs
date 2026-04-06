using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Random Item/List RateDrop Item")]
public class ListRateDropItem : ScriptableObject
{
    [Header("Normal")]
    public int NormalRate = 70;
    public List<GameObject> Normal = new List<GameObject>();

    [Header("Rare")]
    public int RareRate = 25;
    public List<GameObject> Rare = new List<GameObject>();

    [Header("Legendary")]
    public int LegendaryRate = 5;
    public List<GameObject> Legendary = new List<GameObject>();
}
