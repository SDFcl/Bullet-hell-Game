using UnityEngine;

public class TestSetDamageHItbox : MonoBehaviour
{
    public float Damage = 1;
    Hitbox hitbox;
    void Awake()
    {
        hitbox = GetComponent<Hitbox>();
    }
    void Start()
    {
        hitbox.SetDamage(Damage);
    }
}
