using UnityEngine;

public class StartRandomItem : MonoBehaviour
{
    public ClassRandom randomItem;

    public GameObject Start()
    {
        return randomItem.GetRandomItem();
    }
}
