using UnityEngine;

public class AdjustDamagePlayer : MonoBehaviour
{
    public Attack playerAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAttack.AddDamagePercent(0.5f); // Increase damage by 50%
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
