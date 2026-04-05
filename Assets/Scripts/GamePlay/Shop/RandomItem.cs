using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public ListItem listItem;
    public RateDrop rateDrop;

    public GameObject GetRandomItem()
    {
        float randomValue = Random.value;
        if (randomValue < rateDrop.NormalRate)
        {
            return listItem.Normal[Random.Range(0, listItem.Normal.Count)];
        }
        else if (randomValue < rateDrop.NormalRate + rateDrop.RareRate)
        {
            return listItem.Rare[Random.Range(0, listItem.Rare.Count)];
        }
        else
        {
            return listItem.Legendary[Random.Range(0, listItem.Legendary.Count)];
        }
    }
}
