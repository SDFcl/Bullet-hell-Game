using UnityEngine;

public class TestAttack : MonoBehaviour
{
    public GameObject prefab;
    private IWeapon weapon;
    public Mana mana;

    private void Awake()
    {
        weapon = prefab.GetComponent<IWeapon>();
    }
    private void Start()
    {
        weapon.SetOwner(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.ExecuteAttack();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
           ChangeWeapon();
        }
    }

    private void ChangeWeapon()
    {
        weapon = prefab.GetComponent<IWeapon>();
        weapon.SetOwner(gameObject);
    }
}
