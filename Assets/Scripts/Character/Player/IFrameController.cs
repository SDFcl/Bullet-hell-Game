using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class IFrameController : MonoBehaviour, IDamageBlocker
{
    private Coroutine routine;
    private float endTime;
    private Collider2D hitCollider;

    public bool IsDamageBlocked { get; private set; }
    private void Awake()
    {
        hitCollider = GetComponent<Collider2D>();
    }

    public void AddDuration(float duration)
    {
        endTime = Mathf.Max(endTime, Time.time + duration);

        if (routine == null)
        {
            routine = StartCoroutine(IFrameRoutine());
        }
    }

    private IEnumerator IFrameRoutine()
    {
        IsDamageBlocked = true;
        hitCollider.enabled = false;

        while (Time.time < endTime)
        {
            yield return null;
        }

        IsDamageBlocked = false;
        hitCollider.enabled = true;
        routine = null;
    }
}
