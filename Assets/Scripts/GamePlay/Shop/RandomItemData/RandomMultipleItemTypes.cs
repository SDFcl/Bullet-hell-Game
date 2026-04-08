using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Random Item/RandomMultipleItemTypes")]
public class RandomMultipleItemTypes : ClassRandom
{
    [Header("สุ่มหลายประเภทไอเทม")]
    public List<int> RateDropItems = new List<int>();
    public List<ListRateDropItem> listRateDropItems = new List<ListRateDropItem>();

    public override GameObject GetRandomItem()
    {
        // เช็คก่อน
        if (RateDropItems.Count == 0 || listRateDropItems.Count == 0)
        {
            Debug.LogWarning("RateDropItems หรือ listRateDropItems ว่าง");
            return null;
        }

        if (RateDropItems.Count != listRateDropItems.Count)
        {
            Debug.LogError("จำนวน RateDropItems กับ listRateDropItems ไม่เท่ากัน");
            return null;
        }

        // รวมเรททั้งหมด
        int totalRate = 0;
        foreach (int rate in RateDropItems)
        {
            totalRate += rate;
        }

        // สุ่มค่า
        int randomValue = Random.Range(0, totalRate);

        // หาประเภทที่สุ่มได้
        int selectedIndex = 0;
        int cumulative = 0;

        for (int i = 0; i < RateDropItems.Count; i++)
        {
            cumulative += RateDropItems[i];
            if (randomValue < cumulative)
            {
                selectedIndex = i;
                break;
            }
        }

        // ได้ประเภทแล้ว → ไปสุ่ม item ในประเภทนั้น
        ListRateDropItem selectedType = listRateDropItems[selectedIndex];

        float rand = Random.value;

        if (rand < selectedType.NormalRate)
        {
            return selectedType.Normal[Random.Range(0, selectedType.Normal.Count)];
        }
        else if (rand < selectedType.NormalRate + selectedType.RareRate)
        {
            return selectedType.Rare[Random.Range(0, selectedType.Rare.Count)];
        }
        else
        {
            return selectedType.Legendary[Random.Range(0, selectedType.Legendary.Count)];
        }
    }
}
