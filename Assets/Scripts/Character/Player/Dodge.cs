using UnityEngine;
using System.Collections;
using System;

public class Dodge : MonoBehaviour
{
    [Header("Dodge Settings")]
    public float dodgeForce;
    public float cooldown;

    [Header("Dodge Animation")]
    public AnimationClip dodgeAnimation;

    public event Action OnDodge;

    private bool canDodge = true;

    public void TryDodge()
    {
        if (!canDodge) return;
        StartCoroutine(dodge());
    }


    IEnumerator dodge()
    {
        canDodge = false;
        OnDodge?.Invoke();
        yield return new WaitForSeconds(dodgeAnimation.length + cooldown);
        canDodge = true;
    }
}
