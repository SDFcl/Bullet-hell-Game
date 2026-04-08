using System.Collections;
using UnityEngine;

public class EffectCoroutineRunner : MonoBehaviour
{
    private static EffectCoroutineRunner instance;

    public static EffectCoroutineRunner Instance
    {
        get
        {
            if (instance == null)
            {
                var runnerObject = new GameObject("EffectCoroutineRunner");
                instance = runnerObject.AddComponent<EffectCoroutineRunner>();
                DontDestroyOnLoad(runnerObject);
            }
            return instance;
        }
    }

    public static Coroutine Run(IEnumerator coroutine)
    {
        return Instance.StartCoroutine(coroutine);
    }
}
