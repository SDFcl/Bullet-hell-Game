using UnityEngine;
using System.Collections;

public class ProjectilesBurstCount : MonoBehaviour
{

    [SerializeField] private int burstCount = 1;
    [SerializeField] private float burstInterval = 0.1f;

    private IFirePattern firePattern;
    private ProjectileWeapon projectileWeapon;

    void Awake()
    {
        firePattern = GetComponent<IFirePattern>();
        projectileWeapon = GetComponent<ProjectileWeapon>();
    }
    void OnEnable()
    {
        projectileWeapon.OnAttack += PerformAttack;
    }
    void OnDisable()
    {
        projectileWeapon.OnAttack -= PerformAttack;
    }
    
    private void PerformAttack()
    {
        if (firePattern == null)
        return;
        StartCoroutine(FireBurst());
    }

    private IEnumerator FireBurst()
{
    for (int i = 0; i < burstCount; i++)
    {
        firePattern.Execute(projectileWeapon);

        if (i < burstCount - 1)
            yield return new WaitForSeconds(burstInterval);
    }
}
}
