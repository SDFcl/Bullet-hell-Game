using UnityEngine;

public class Startpoint : MonoBehaviour
{
    [SerializeField] private bool disableTP = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //test
        MovePlayerToStartpoint();
    }

    public void MovePlayerToStartpoint()
    {
        if (disableTP) return;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        player.position = transform.position;
    }

    
}
