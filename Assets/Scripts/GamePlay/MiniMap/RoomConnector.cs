using System.Collections.Generic;
using UnityEngine;

public class RoomConnector : MonoBehaviour
{
    public List<RoomMiniMapController> roomMiniMapController = new List<RoomMiniMapController>();

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;

        // Detect rooms using OverlapAreaAll with BoxCollider2D bounds
        Collider2D[] colliders = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);
        foreach (Collider2D collider in colliders)
        {
            RoomMiniMapController controller = collider.gameObject.GetComponentInChildren<RoomMiniMapController>();
            if (controller != null)
            {
                controller.roomConnectors.Add(this); // Add this connector to the room's list
                roomMiniMapController.Add(controller);
            }
        }

        gameObject.SetActive(false);
    }

    public void Execute()
    {
        foreach (var controller in roomMiniMapController)
        {
            if (controller.state != RoomMiniMapState.Explored)
            {
                controller.ChangeRoomState(RoomMiniMapState.Discover);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (boxCollider != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, boxCollider.size);
        }
    }
}
