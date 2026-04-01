using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "AI/UOP1 Style/State Graph")]
public class EnemyStateGraphSO : ScriptableObject
{
    public EnemyStateSO entryState;
    public List<EnemyStateSO> allStates = new();
}
