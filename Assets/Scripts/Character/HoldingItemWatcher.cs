using UnityEngine;
using System;

public class HoldingItemWatcher : MonoBehaviour
{
    public event Action OnHoldingItemChanged;
    private void OnTransformChildrenChanged()
    {
        //Debug.Log("HoldingItem children changed");
        OnHoldingItemChanged?.Invoke();
    }
}
