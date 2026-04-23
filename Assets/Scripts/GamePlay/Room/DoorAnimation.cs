using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float detectDistance = 1.5f;
    [SerializeField] float fadeSpeed = 2f;
    [SerializeField] Color _color = new Color(1f, 0f, 0f, 0.7f);
    [SerializeField, Range(0f, 1f)] float hiddenAlpha = 0f;
    [SerializeField, Range(0f, 1f)] float visibleAlpha = 0.7f;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetAlpha(hiddenAlpha);
    }

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
        
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        float targetAlpha = distance <= detectDistance ? visibleAlpha : hiddenAlpha;

        Color color = spriteRenderer.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
        spriteRenderer.color = color;
    }

    void SetAlpha(float alpha)
    {
        Color color = _color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
