using System.Collections;
using UnityEngine;

public class ImpulseController : MonoBehaviour,IImpulseMover
{
    private IMovement movement;
    private Coroutine currentCoroutine;

    private void Awake()
    {
        movement = GetComponent<IMovement>();
    }

    public void Play(Vector2 direction, float force, float duration)
    {
        StopCurrent();
        currentCoroutine = StartCoroutine(PlayRoutine(direction, force, duration));
    }

    public void StopCurrent()
    {
        if (currentCoroutine == null) return;

        StopCoroutine(currentCoroutine);
        currentCoroutine = null;

        Cleanup();
    }

    private IEnumerator PlayRoutine(Vector2 direction, float force, float duration)
    {
        movement.DisableMovement();
        movement.ResetVelocity();
        movement.ApplyImpulse(direction, force);

        yield return new WaitForSeconds(duration);

        Cleanup();
        currentCoroutine = null;
    }

    private void Cleanup()
    {
        movement.ResetVelocity();
        movement.EnableMovement();
    }
}
