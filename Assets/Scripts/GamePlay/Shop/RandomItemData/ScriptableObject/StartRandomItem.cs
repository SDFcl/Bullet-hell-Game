using UnityEngine;

public class StartRandomItem : MonoBehaviour
{
    public ClassRandom randomItem;

    public GameObject GetRandomItem()
    {
        return randomItem.GetRandomItem();
    }
}
