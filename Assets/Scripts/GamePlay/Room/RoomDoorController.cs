using UnityEngine;

public class RoomDoorController : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;

    private void Start()
    {
        OpenDoors();
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
            Debug.Log("Setting door " + door.name + " active: " + state);
            door.SetActive(state);
        }
    }
}
