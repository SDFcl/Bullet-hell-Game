using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private RoomEnemysController enemysController;
    private RoomDoorController doorController;
    private RoomRewardController rewardController;
    private RoomMiniMapController miniMapController;

    private bool playerInsideNeverBefore = false;

    private void Awake()
    {
        if (TryGetComponent(out RoomEnemysController controller))
        {
            enemysController = controller;
            enemysController.OnRoomCleared += RoomCleared;
        }

        if (TryGetComponent(out RoomDoorController door)) doorController = door;

        if (TryGetComponent(out RoomRewardController reward)) rewardController = reward;
        
        miniMapController = GetComponentInChildren<RoomMiniMapController>();
    }

    private void Start()
    {
        if (enemysController == null)
        {
            rewardController.SetRewardActive(true);
        }
    }

    public void PlayerEnterRoom()
    {
        miniMapController.ChangeRoomState(RoomMiniMapState.Explored);
        if (enemysController == null || doorController == null) 
        {
            return;
        }
        playerInsideNeverBefore = true;
        doorController.CloseDoors();
        enemysController.SetEnemiesActive(true);
        rewardController.SetRewardActive(false);
        if (TryGetComponent(out Collider2D collider))
        {
            collider.enabled = false; // Disable trigger after player enters
        }
    }

    public void RoomCleared()
    {
        doorController.OpenDoors();
        rewardController.SetRewardActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !playerInsideNeverBefore)
        {
            PlayerEnterRoom();
        }
    }
}