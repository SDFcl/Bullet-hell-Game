using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Dodge Modify")]
public class Ef_DodgeModify : ItemEffect
{
    public float DistanceMultiplier = 0.25f; // ԭ1.5֮ǰ
    public float iFrameDuration = 0.1f; // ԭ0.3֮ǰ

    public bool IsActive = false;

    public override void Apply(GameObject target)
    {
        var dodge = target.GetComponent<Dodge>();
        if (!IsActive)
        {
            if (dodge != null)
            {
                dodge.AddDistanceMultiplier(DistanceMultiplier);
                dodge.AddIFrameDuration(iFrameDuration);
                IsActive = true;
            }
        }
        else
        {
            if (dodge != null)
            {
                dodge.RemoveDistanceMultiplier(DistanceMultiplier);
                dodge.RemoveIFrameDuration(iFrameDuration);
                IsActive = false;
            }
        }
    }
}
