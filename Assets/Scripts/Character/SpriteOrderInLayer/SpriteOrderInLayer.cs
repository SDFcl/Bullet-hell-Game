using System.Collections.Generic;
using UnityEngine;

public class SpriteOrderInLayer : MonoBehaviour
{
    public GameObject spriteParent;
    public List<SpriteChilden> childen;

    public void Awake()
    {
        FindSpriteChilden();
    }

    public void FindSpriteChilden()
    {
        if (spriteParent == null)
        {
            spriteParent = gameObject;
            childen = new List<SpriteChilden>(spriteParent.GetComponentsInChildren<SpriteChilden>());
        }
        else
        {
            childen = new List<SpriteChilden>(spriteParent.GetComponentsInChildren<SpriteChilden>());
        }
    }

    public void Update()
    {
        for (int i = 0; i < childen.Count; i++)
        {
            if (childen[i] == null)
            {
                FindSpriteChilden();
            }
            else
            {
                childen[i].GetComponent<SpriteRenderer>().sortingOrder = -(int)(spriteParent.transform.position.y *10 - childen[i].offsetY);
            }
        }
    }
}
