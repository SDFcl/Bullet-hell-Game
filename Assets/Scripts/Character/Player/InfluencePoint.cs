using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluencePoints : MonoBehaviour
{
    public List<Vector2> influencePoints = new List<Vector2>();
    [SerializeField] private float radius = 3f;
    [SerializeField] private int resolution = 36;
    [SerializeField] private LayerMask obstacleLayer;

    void Update()
    {
        UpdateInfluence();
    }
    private void UpdateInfluence()
    {
        influencePoints.Clear();
        for (int i = 0; i < resolution; i++)
        {
            float angle = i * Mathf.PI * 2 / resolution;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            Vector2 pos = (Vector2)transform.position + dir * radius;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, radius, obstacleLayer);
            if (!hit) influencePoints.Add(pos);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (var p in influencePoints)
            Gizmos.DrawSphere(p, 0.05f);
    }
}
