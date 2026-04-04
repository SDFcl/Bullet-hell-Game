using UnityEngine;

public class RoomDoorController : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;

    private void Start()
    {
        CloseDoors();
    }

    public void OpenDoors()
    {
        SetDoorState(false);
    }

    public void CloseDoors()
    {
        SetDoorState(true);
    }

    void SetDoorState(bool state)
    {
        foreach (var door in doors)
        {
            door.SetActive(state);
        }
    }
}
