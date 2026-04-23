using UnityEngine;
using TMPro;

public class UpdateText_Time : MonoBehaviour
{
    TextMeshProUGUI timeCountText;
    void Awake()
    {
        timeCountText = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        float timeCount = GameSession.timeCount;

        int hours = Mathf.FloorToInt(timeCount / 3600f);
        int minutes = Mathf.FloorToInt((timeCount % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(timeCount % 60f);

        timeCountText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }
}
