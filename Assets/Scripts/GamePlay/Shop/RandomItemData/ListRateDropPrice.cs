using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListRatePrice", menuName = "Random/ListRatePrice", order = 1)]
public class ListRatePrice : ScriptableObject
{
    [Header("Normal")]
    public int NormalMinPrice = 20;
    public int NormalMaxPrice = 100;

    [Header("Rare")]
    public int RareMinRate = 150;
    public int RareMaxRate = 400;

    [Header("Legendary")]
    public int LegendaryMinRate = 700;
    public int LegendaryMaxRate = 2000;
}
