using UnityEngine;

public class FloatingMovement : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0f, 0.25f, 0f);
    [SerializeField] private float duration = 4f;
    [SerializeField] private float startTimeOffset = 0f;
    [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private Vector3 startLocalPosition;
    private float timer;

    private void Awake()
    {
        startLocalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        timer = startTimeOffset;
        transform.localPosition = startLocalPosition;
    }

    private void Update()
    {
        if (duration <= 0f)
            return;

        timer += Time.deltaTime;

        float normalizedTime = (timer % duration) / duration;
        float pingPongTime = Mathf.PingPong(normalizedTime * 2f, 1f);
        float curveValue = curve.Evaluate(pingPongTime);

        transform.localPosition = startLocalPosition + offset * curveValue;
    }
}
