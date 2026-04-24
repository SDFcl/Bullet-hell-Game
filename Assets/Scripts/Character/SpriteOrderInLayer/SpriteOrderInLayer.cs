using System.Collections.Generic;
using UnityEngine;

public class SpriteOrderInLayer : MonoBehaviour
{
    public List<SpriteChilden> childen;

    public void Awake()
    {
        List<SpriteChilden> Newchilden = new List<SpriteChilden>(GetComponentsInChildren<SpriteChilden>());
        childen.AddRange(Newchilden);
    }

    public void Update()
    {
        for (int i = 0; i < childen.Count; i++)
        {
            childen[i].GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y *100 + childen[i].offsetY);
        }
    }
}
