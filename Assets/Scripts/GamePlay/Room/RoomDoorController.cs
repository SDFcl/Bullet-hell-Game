using UnityEngine;

public class RoomDoorController : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;
    private void Awake()
    {
        doors = GameObject.FindGameObjectsWithTag("Barrier");
        AddDoorAnimationComponents();
    }

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
            //Debug.Log("Setting door " + door.name + " active: " + state);
            door.SetActive(state);
        }
    }

    void AddDoorAnimationComponents()
    {
        foreach (var door in doors)
        {
            if (door.GetComponent<DoorAnimation>() == null)
            {
                door.AddComponent<DoorAnimation>();
                door.AddComponent<DoorScriptLight>();
            }
        }
    }
}
