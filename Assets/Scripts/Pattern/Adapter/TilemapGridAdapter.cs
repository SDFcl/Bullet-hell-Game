using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGridAdapter : MonoBehaviour, IGridScanSource
{
    [SerializeField] private Tilemap floorTilemap;

    public Bounds GetBounds()
    {
        floorTilemap.CompressBounds();
        var localBounds = floorTilemap.localBounds;
        var worldCenter = floorTilemap.transform.TransformPoint(localBounds.center);
        return new Bounds(worldCenter, localBounds.size);
    }

    public float GetCellSize()
    {
        return floorTilemap.layoutGrid.cellSize.x;
    }
}
