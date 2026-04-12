using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AttackEntry
{
    public string attackName;
    public GameObject prefab;
    public float cooldown;
    [HideInInspector] public float cooldownTimer;

    public bool IsReady => cooldownTimer <= 0f;
}

public class EnemyAttackSelector : MonoBehaviour
{
    [SerializeField] private Transform holdingItemPoint;
    [SerializeField] private List<AttackEntry> attacks = new();

    private GameObject currentAttackObject;
    private int currentIndex = -1;

    private void Update()
    {
        for (int i = 0; i < attacks.Count; i++)
        {
            if (attacks[i].cooldownTimer > 0f)
            {
                attacks[i].cooldownTimer -= Time.deltaTime;
            }
        }
    }

    public bool SelectAttack(int index)
    {
        if (index < 0 || index >= attacks.Count) return false;
        if (holdingItemPoint == null) return false;
        if (!attacks[index].IsReady) return false;

        if (currentAttackObject != null)
        {
            Destroy(currentAttackObject);
        }

        currentIndex = index;
        currentAttackObject = Instantiate(attacks[index].prefab, holdingItemPoint);
        attacks[index].cooldownTimer = attacks[index].cooldown;
        return true;
    }

    public bool SelectRandomAttack()
    {
        List<int> availableIndices = new List<int>();

        for (int i = 0; i < attacks.Count; i++)
        {
            if (attacks[i].IsReady)
            {
                availableIndices.Add(i);
            }
        }

        if (availableIndices.Count == 0) return false;

        int randomIndex = availableIndices[Random.Range(0, availableIndices.Count)];
        return SelectAttack(randomIndex);
    }
}
