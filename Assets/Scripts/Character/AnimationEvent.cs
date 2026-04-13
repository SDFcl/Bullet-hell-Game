using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public event Action OnPrepareAttack;
    public event Action OnAttack;
    public event Action OnAttackExit;

    public void RaisePrepareAttack() => OnPrepareAttack?.Invoke();
    public void RaiseAttack() => OnAttack?.Invoke();
    public void RaiseAttackExit() => OnAttackExit?.Invoke();
}
