using TMPro;
using UnityEngine;

public class RGBCycleText : MonoBehaviour
{
    public float speed = 1.5f;

    private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        // Hue cycles over time
        float h = Mathf.Repeat(Time.time * speed, 1f);

        // Add variation to saturation & brightness
        float s = 0.8f + Mathf.Sin(Time.time * 2f) * 0.2f; // 0.6 - 1.0
        float v = 0.9f + Mathf.Cos(Time.time * 3f) * 0.1f; // 0.8 - 1.0

        text.color = Color.HSVToRGB(h, s, v);
    }
}