using UnityEngine;

public class Timer
{
    public float TimeElapsed { get; private set; }

    public void Tick(float dt)
    {
        TimeElapsed += dt;
    }

    public void ResetTimer()
    {
        TimeElapsed = 0f;
    }
}
