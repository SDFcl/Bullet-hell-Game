using UnityEngine;

public class EnemyMoveTextController : MonoBehaviour
{
    public MonoBehaviour movementreference;
    public PathToDir pathToDir;
    public Transform target;
    IMovement movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = movementreference as IMovement;
    }

    // Update is called once per frame
    void Update()
    {
        pathToDir.SetDestination(target.position);
        movement.SetMoveInput(pathToDir.GetDirection().normalized);
    }
}
