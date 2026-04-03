using UnityEngine;

public class DebugDistanceRange : MonoBehaviour
{
    [SerializeField] private DistanceSO distanceSO;
    [SerializeField] private bool showGizmo = true;

    private void OnDrawGizmos()
    {
        if (!showGizmo || distanceSO == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceSO.range);
    }
}