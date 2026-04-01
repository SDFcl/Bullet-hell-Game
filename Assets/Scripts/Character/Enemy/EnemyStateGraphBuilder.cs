using System.Collections.Generic;
using UnityEngine;

public static class EnemyStateGraphBuilder
{
    public static IState<EnemyContext> Build(EnemyStateGraphSO graph)
    {
        if (graph == null)
        {
            Debug.LogError("EnemyStateGraphSO is null.");
            return null;
        }

        if (graph.entryState == null)
        {
            Debug.LogError("Entry state is null.");
            return null;
        }

        var stateMap = new Dictionary<EnemyStateSO, State<EnemyContext>>();

        // 1) สร้าง runtime state ก่อน
        foreach (var stateSO in graph.allStates)
        {
            if (stateSO == null || stateMap.ContainsKey(stateSO))
                continue;

            var runtimeState = new State<EnemyContext>();

            foreach (var actionSO in stateSO.actions)
            {
                if (actionSO == null)
                    continue;

                runtimeState.AddAction(actionSO.CreateAction());
            }

            stateMap[stateSO] = runtimeState;
        }

        // กันกรณี entryState ไม่อยู่ใน list
        if (!stateMap.ContainsKey(graph.entryState))
        {
            var runtimeEntry = new State<EnemyContext>();

            foreach (var actionSO in graph.entryState.actions)
            {
                if (actionSO == null)
                    continue;

                runtimeEntry.AddAction(actionSO.CreateAction());
            }

            stateMap[graph.entryState] = runtimeEntry;
        }

        // 2) ต่อ transitions
        foreach (var pair in stateMap)
        {
            EnemyStateSO fromSO = pair.Key;
            State<EnemyContext> fromRuntime = pair.Value;

            foreach (var transitionSO in fromSO.transitions)
            {
                if (transitionSO == null || transitionSO.condition == null || transitionSO.targetState == null)
                    continue;

                if (!stateMap.TryGetValue(transitionSO.targetState, out var targetRuntime))
                {
                    Debug.LogError($"Target state not found in graph: {transitionSO.targetState.name}");
                    continue;
                }

                fromRuntime.AddTransition(new Transition<EnemyContext>(
                    transitionSO.condition.CreateCondition(),
                    targetRuntime
                ));
            }
        }

        return stateMap[graph.entryState];
    }
}