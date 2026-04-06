using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Random Item/RandomItem")]
public class RandomItem : ScriptableObject
{
    //สุ่มแต่ประเภทไอเทมเดียว
    [Header("สุ่มแต่ประเภทไอเทมเดียว")]
    public ListRateDropItem listRateDropItem;

    //สุ่มหลายประเภทไอเทม
    [Header("สุ่มหลายประเภทไอเทม")]
    public List<int> RateDropItems = new List<int>();
    public List<ListRateDropItem> listRateDropItems = new List<ListRateDropItem>();

    public GameObject GetRandomItem()
    {
        if (listRateDropItem == null)
        {
            GameObject randomitem = GetRandomMultiItemType();
            if (randomitem != null)
            {
                return randomitem;
            }
            return null;
        }
        float randomValue = Random.value;
        if (randomValue < listRateDropItem.NormalRate)
        {
            return listRateDropItem.Normal[Random.Range(0, listRateDropItem.Normal.Count)];
        }
        else if (randomValue < listRateDropItem.NormalRate + listRateDropItem.RareRate)
        {
            return listRateDropItem.Rare[Random.Range(0, listRateDropItem.Rare.Count)];
        }
        else
        {
            return listRateDropItem.Legendary[Random.Range(0, listRateDropItem.Legendary.Count)];
        }
    }

    public GameObject GetRandomMultiItemType()
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
