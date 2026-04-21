using UnityEngine;
using UnityEngine.UI;

public class ManaSliderPHUD : MonoBehaviour
{
    Mana mana;
    Slider manaSlider;
    void Awake()
    {
        if(mana == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            mana = player.GetComponent<Mana>();
        }
        manaSlider = GetComponent<Slider>();
    }
    void Update()
    {
        if (mana.MaxMana <= 0f)
        {
            manaSlider.value = 0f;
            return;
        }

        manaSlider.value = Mathf.Clamp01(mana.CurrentMana / mana.MaxMana);
    }
}
