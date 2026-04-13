using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Random Item/RandomGameObj")]
public class RandomGameObj : ClassRandom
{
    [Header("สุ่มหลายประเภทไอเทม")]
    public List<int> RateDrop = new List<int>();
    public List<GameObject> listGameObj = new List<GameObject>();

    public override GameObject GetRandomItem()
    {
        // เช็คก่อน
        if (RateDrop.Count == 0 || listGameObj.Count == 0)
        {
            Debug.LogWarning("RateDrop หรือ listGameObj ว่าง");
            return null;
        }

        if (RateDrop.Count != listGameObj.Count)
        {
            Debug.LogError("จำนวน RateDrop กับ listGameObj ไม่เท่ากัน");
            return null;
        }

        // รวมเรททั้งหมด
        int totalRate = 0;
        foreach (int rate in RateDrop)
        {
            totalRate += rate;
        }

        // สุ่มค่า
        int randomValue = Random.Range(0, totalRate);

        // หาประเภทที่สุ่มได้
        int selectedIndex = 0;
        int cumulative = 0;

        for (int i = 0; i < RateDrop.Count; i++)
        {
            cumulative += RateDrop[i];
            if (randomValue < cumulative)
            {
                selectedIndex = i;
                break;
            }
        }
        return listGameObj[selectedIndex];
    }
}
