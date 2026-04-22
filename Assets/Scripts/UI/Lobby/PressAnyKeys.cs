using UnityEngine;
using UnityEngine.Events;

public class PressAnyKeys : MonoBehaviour
{
    [SerializeField] private UnityEvent onAnyInputPressed;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            onAnyInputPressed?.Invoke();
        }
    }
}
