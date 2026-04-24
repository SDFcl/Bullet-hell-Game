using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorScriptLight : MonoBehaviour
{
    [Header("Player Detect")]
    [SerializeField] private Transform player;
    [SerializeField] private float detectDistance = 1.5f;
    [SerializeField] private float fadeSpeed = 2f;

    [Header("Light Color")]
    [SerializeField] private Color hiddenColor = new Color(1f, 0f, 0f, 0f);
    [SerializeField] private Color visibleColor = new Color(1f, 0f, 0f, 1f);

    [Header("Light Settings")]
    [SerializeField] private float visibleIntensity = 1f;
    [SerializeField] private float hiddenIntensity = 0f;
    [SerializeField] private float falloff = 2f;
    [SerializeField] private float falloffStrength = 0.25f;
    [SerializeField] private int lightOrder = 0;

    private Light2D light2D;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();

        if (light2D == null)
            light2D = gameObject.AddComponent<Light2D>();

        ApplyBaseSettings();

        light2D.color = hiddenColor;
        light2D.intensity = hiddenIntensity;
    }

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
                player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        bool isVisible = distance <= detectDistance;

        Color targetColor = isVisible ? visibleColor : hiddenColor;
        float targetIntensity = isVisible ? visibleIntensity : hiddenIntensity;

        light2D.color = Color.Lerp(
            light2D.color,
            targetColor,
            fadeSpeed * Time.deltaTime
        );

        light2D.intensity = Mathf.MoveTowards(
            light2D.intensity,
            targetIntensity,
            fadeSpeed * Time.deltaTime
        );
    }

    private void OnValidate()
    {
        light2D = GetComponent<Light2D>();

        if (light2D == null)
            return;

        ApplyBaseSettings();
    }

    private void ApplyBaseSettings()
    {
        if (light2D == null) return;

        light2D.lightType = Light2D.LightType.Point;
        light2D.shapeLightFalloffSize = falloff;
        light2D.falloffIntensity = falloffStrength;
        light2D.lightOrder = lightOrder;
        
        light2D.pointLightInnerRadius = 0f;
        light2D.pointLightOuterRadius = 3f;
        // ต้องให้ Renderer2D Blend Style index 0 เป็น Additive เองใน Renderer Data
        light2D.blendStyleIndex = 0;
    }
}