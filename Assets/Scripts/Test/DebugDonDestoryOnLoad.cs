using UnityEngine;
using System;

public class DebugDonDestoryOnLoad : Singleton<DebugDonDestoryOnLoad>
{
    public float CurrentGC;
    [SerializeField] private GameState debugCurrentState;
    void Update()
    {
        CurrentGC = GameSession.CurrentReward;
        debugCurrentState = GameStateManager.CurrentState;
    }
}
