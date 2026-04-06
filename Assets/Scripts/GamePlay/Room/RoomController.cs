using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private RoomEnemysController enemysController;
    private RoomDoorController doorController;
    public GameObject chest;

    private bool playerInsideNeverBefore = false;

    private void Awake()
    {
        if (TryGetComponent(out RoomEnemysController controller)) enemysController = controller;
        enemysController.OnRoomCleared += RoomCleared;

        if (TryGetComponent(out RoomDoorController door)) doorController = door;

        if (chest != null) chest.SetActive(false);
    }

    public void PlayerEnterRoom()
    {
        if (enemysController == null || doorController == null) return;
        playerInsideNeverBefore = true;
        doorController.CloseDoors();
        enemysController.SetEnemiesActive(true);
    }

    public void RoomCleared()
    {
        doorController.OpenDoors();
        chest.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !playerInsideNeverBefore)
        {
            PlayerEnterRoom();
        }
    }
}