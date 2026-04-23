using UnityEngine;

public class OnFogus : MonoBehaviour
{
    public bool onFogus;

    public void SetFogus(bool value)
    {
        onFogus = value;
        Execute();
    }

    public virtual void Execute()
    {

    }
}
