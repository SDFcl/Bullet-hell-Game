using UnityEngine;
using UnityEngine.UI;

public class SpecialSkillPHUD : MonoBehaviour
{
    SpecialAbility playeerSpecialAbility;
    Slider specialAbilitySlider;
    void Awake()
    {
        if(playeerSpecialAbility == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playeerSpecialAbility = player.GetComponent<SpecialAbility>();
        }
        specialAbilitySlider = GetComponent<Slider>();
    }
    void Update()
    {
        specialAbilitySlider.value = 1f - playeerSpecialAbility.CurrentCooldown;
    }
}
