using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Random Item/RateDrop")]
public class RateDrop : ScriptableObject
{
    [Header("Normal")]
    public float NormalRate = 0.7f;

    [Header("Rare")]
    public float RareRate = 0.25f;

    [Header("Legendary")]
    public float LegendaryRate = 0.05f;
}
