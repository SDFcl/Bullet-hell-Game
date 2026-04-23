using UnityEngine;
using System;

public class DebugDonDestoryOnLoad : Singleton<DebugDonDestoryOnLoad>
{
    public float CurrentGC;
    void Update()
    {
        CurrentGC = GameSession.CurrentReward;
    }
}
