
using UnityEngine;

[CreateAssetMenu(menuName = "Random Item/RandomItem")]
public class RandomItem : ClassRandom
{
    //สุ่มแต่ประเภทไอเทมเดียว
    [Header("สุ่มแต่ประเภทไอเทมเดียว")]
    public ListRateDropItem listRateDropItem;


    public override GameObject GetRandomItem()
    {
        float randomValue = Random.Range(0f, listRateDropItem.NormalRate + listRateDropItem.RareRate + listRateDropItem.LegendaryRate + 1);
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

    

}
