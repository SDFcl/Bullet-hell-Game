using System.Collections.Generic;
using UnityEngine;

public class SpriteOrderInLayer : MonoBehaviour
{
    public GameObject spriteParent;
    public List<SpriteChilden> childen;

    public void Awake()
    {
        List<SpriteChilden> Newchilden;
        if (spriteParent == null)
        {
            spriteParent = gameObject;
            Newchilden = new List<SpriteChilden>(spriteParent.GetComponentsInChildren<SpriteChilden>());
        }
        else
        {
            Newchilden = new List<SpriteChilden>(spriteParent.GetComponentsInChildren<SpriteChilden>());
        }
       
        childen.AddRange(Newchilden);
    }

    public void Update()
    {
        for (int i = 0; i < childen.Count; i++)
        {
            childen[i].GetComponent<SpriteRenderer>().sortingOrder = -(int)(spriteParent.transform.position.y *100 - childen[i].offsetY);
        }
    }
}
