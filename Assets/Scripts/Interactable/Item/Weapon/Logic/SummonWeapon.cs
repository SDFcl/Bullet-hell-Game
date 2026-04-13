using UnityEngine;

public class SummonWeapon : BaseWeapon
{
    [System.Serializable]
    public class SummonSpawnPoint
    {
        public string[] summonPoolTags;
        public Transform point;
    }
    [Header("Mana")]
    [SerializeField] private float manaCost = 20f;

    [Header("Minion")]
    [SerializeField] private SummonSpawnPoint[] spawnPoints;
    
    private Mana mana;

    public override void SetOwner(GameObject owner)
    {
        base.SetOwner(owner);
        mana = owner != null ? owner.GetComponent<Mana>() : null;
    }
    protected override bool CanAttack()
    {
        if (mana != null && mana.CurrentMana < manaCost)
        {
            Debug.Log("Not enough mana to shoot!");
            return false;
        }

        if (ObjectPooler.Instance == null)
        {
            Debug.LogError("ObjectPooler.Instance is null");
            return false;
        }

        return true;
    }
    protected override void PerformAttack()
    {
        foreach (SummonSpawnPoint spawnPoint in spawnPoints)
        {
            if (spawnPoint == null || spawnPoint.point == null) continue;
            string randomTag = spawnPoint.summonPoolTags[Random.Range(0, spawnPoint.summonPoolTags.Length)];

            ObjectPooler.Instance.SpawnFromPool(
                randomTag,
                spawnPoint.point.position,
                Quaternion.identity,
                obj =>
                {
                    var minion = obj.GetComponent<Attack>();
                    {
                        if(minion != null)
                        {
                            minion.AddFlatDamage(GetDamage());
                        }
                    }
                }
            );
        }

        if (mana != null)
            mana.ConsumeMana(manaCost);
    }
}
