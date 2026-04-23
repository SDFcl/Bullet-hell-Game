using UnityEngine;
using TMPro;

public class UpdateText_Enemy : MonoBehaviour
{
    TextMeshProUGUI timeCountText;
    void Awake()
    {
        timeCountText = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        timeCountText.text = GameSession.enemyCount.ToString();
    }
}
