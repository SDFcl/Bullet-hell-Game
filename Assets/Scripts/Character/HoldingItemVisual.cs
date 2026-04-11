using UnityEngine;

public class HoldingItemVisual : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    private Health health;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        health = GetComponentInParent<Health>();
    }

    private void OnEnable()
    {
        health.OnDead += Hide;
    }
    private void OnDisable()
    {
        health.OnDead -= Hide;
    }

    public void Show()
    {
        SetVisible(true);
    }

    public void Hide()
    {
        SetVisible(false);
    }

    private void SetVisible(bool visible)
    {
        if (spriteRenderers == null || spriteRenderers.Length == 0)
            return;

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.enabled = visible;
        }
    }
}
