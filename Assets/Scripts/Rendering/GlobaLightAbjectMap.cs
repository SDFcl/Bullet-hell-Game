using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobaLightAbjectMap : MonoBehaviour
{
    [SerializeField] private GlobalLightData globalLightData;
    private Light2D light2D;
    void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    void Start()
    {
        globalLightData.UpdateLightValue(out float globalLightvalue);
        light2D.intensity = globalLightvalue;
    }
}
