using UnityEngine;

public class StringOnFogus : OnFogus
{
    public GameObject GameObject;

    private void Start()
    {
        GameObject.SetActive(false);
    }

    public override void Execute()
    {
        GameObject.SetActive(onFogus);
    }
}
