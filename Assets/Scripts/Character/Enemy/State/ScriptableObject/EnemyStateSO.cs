using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyStateTransitionSO
{
    public EnemyConditionSO condition;
    public EnemyStateSO targetState;
}

[CreateAssetMenu(menuName = "AI/UOP1 Style/State")]
public class EnemyStateSO : ScriptableObject
{
    public List<EnemyActionSO> actions = new();
    public List<EnemyStateTransitionSO> transitions = new();
}



