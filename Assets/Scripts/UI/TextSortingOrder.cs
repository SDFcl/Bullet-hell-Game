using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TextSortingOrder : MonoBehaviour
{
    [SerializeField] private string sortingLayerName = "Obj in scene";
    [SerializeField] private int orderInLayer = 10;

    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = sortingLayerName;
        meshRenderer.sortingOrder = orderInLayer;
    }
}
