using UnityEngine;

[CreateAssetMenu(fileName = "RandomPrice", menuName = "Random/RandomPrice", order = 1)]
public class RandomPrice : ClassRandom
{
    [Header("สุ่มราคา")]
    public ListRatePrice listRatePrice;

    public override int GetRandomInt(RareType rareType)
    {
        switch(rareType)
        {
            case RareType.Normal:
                return Random.Range(listRatePrice.NormalMinPrice, listRatePrice.NormalMaxPrice);
            case RareType.Rare:
                return Random.Range(listRatePrice.RareMinRate, listRatePrice.RareMaxRate);
            case RareType.Legendary:
                return Random.Range(listRatePrice.RareMinRate, listRatePrice.RareMaxRate);
            default:
                return 0;
        }
    }
}
