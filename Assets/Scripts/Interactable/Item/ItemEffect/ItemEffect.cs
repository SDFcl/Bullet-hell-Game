using System.Collections;
using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    public abstract bool IsActive { get; set; }
    public abstract void Apply(GameObject target,bool IsActive = false);
}