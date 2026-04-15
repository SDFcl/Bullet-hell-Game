using UnityEngine;

[ExecuteAlways]
public class CombatUICornerLayout : MonoBehaviour
{
    [Header("Corner Targets")]
    [SerializeField] private RectTransform topLeft;
    [SerializeField] private RectTransform topRight;
    [SerializeField] private RectTransform bottomLeft;
    [SerializeField] private RectTransform bottomRight;

    [Header("Padding")]
    [SerializeField] private bool linkHorizontalPadding = true;
    [SerializeField] private bool linkVerticalPadding = true;
    [SerializeField] private float leftPadding = 24f;
    [SerializeField] private float rightPadding = 24f;
    [SerializeField] private float topPadding = 24f;
    [SerializeField] private float bottomPadding = 24f;

    private RectTransform cachedRectTransform;
    private float previousLeftPadding = 24f;
    private float previousRightPadding = 24f;
    private float previousTopPadding = 24f;
    private float previousBottomPadding = 24f;

    private RectTransform RootRectTransform
    {
        get
        {
            if (cachedRectTransform == null)
            {
                cachedRectTransform = GetComponent<RectTransform>();
            }

            return cachedRectTransform;
        }
    }

    private void Reset()
    {
        AutoAssignCorners();
        ApplyLayout();
    }

    private void Awake()
    {
        AutoAssignMissingCorners();
        ApplyLayout();
    }

    private void OnEnable()
    {
        ApplyLayout();
    }

    private void OnValidate()
    {
        leftPadding = Mathf.Max(0f, leftPadding);
        rightPadding = Mathf.Max(0f, rightPadding);
        topPadding = Mathf.Max(0f, topPadding);
        bottomPadding = Mathf.Max(0f, bottomPadding);

        SyncLinkedPadding();

        AutoAssignMissingCorners();
        ApplyLayout();
        CachePaddingValues();
    }

    private void OnRectTransformDimensionsChange()
    {
        ApplyLayout();
    }

    [ContextMenu("Auto Assign Corners")]
    public void AutoAssignCorners()
    {
        topLeft = FindChildRect("TopLeft");
        topRight = FindChildRect("TopRight");
        bottomLeft = FindChildRect("BottomLeft");
        bottomRight = FindChildRect("BottomRight");
    }

    [ContextMenu("Apply Layout")]
    public void ApplyLayout()
    {
        PlaceCorner(topLeft, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(leftPadding, -topPadding));
        PlaceCorner(topRight, new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(-rightPadding, -topPadding));
        PlaceCorner(bottomLeft, new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(leftPadding, bottomPadding));
        PlaceCorner(bottomRight, new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-rightPadding, bottomPadding));
    }

    private void SyncLinkedPadding()
    {
        if (linkHorizontalPadding)
        {
            bool leftChanged = !Mathf.Approximately(leftPadding, previousLeftPadding);
            bool rightChanged = !Mathf.Approximately(rightPadding, previousRightPadding);

            if (leftChanged)
            {
                rightPadding = leftPadding;
            }
            else if (rightChanged)
            {
                leftPadding = rightPadding;
            }
            else
            {
                rightPadding = leftPadding;
            }
        }

        if (linkVerticalPadding)
        {
            bool topChanged = !Mathf.Approximately(topPadding, previousTopPadding);
            bool bottomChanged = !Mathf.Approximately(bottomPadding, previousBottomPadding);

            if (topChanged)
            {
                bottomPadding = topPadding;
            }
            else if (bottomChanged)
            {
                topPadding = bottomPadding;
            }
            else
            {
                bottomPadding = topPadding;
            }
        }
    }

    private void CachePaddingValues()
    {
        previousLeftPadding = leftPadding;
        previousRightPadding = rightPadding;
        previousTopPadding = topPadding;
        previousBottomPadding = bottomPadding;
    }

    private void AutoAssignMissingCorners()
    {
        if (topLeft == null)
        {
            topLeft = FindChildRect("TopLeft");
        }

        if (topRight == null)
        {
            topRight = FindChildRect("TopRight");
        }

        if (bottomLeft == null)
        {
            bottomLeft = FindChildRect("BottomLeft");
        }

        if (bottomRight == null)
        {
            bottomRight = FindChildRect("BottomRight");
        }
    }

    private RectTransform FindChildRect(string childName)
    {
        Transform child = transform.Find(childName);
        return child != null ? child as RectTransform : null;
    }

    private void PlaceCorner(RectTransform target, Vector2 anchor, Vector2 pivot, Vector2 anchoredPosition)
    {
        if (target == null || RootRectTransform == null)
        {
            return;
        }

        target.anchorMin = anchor;
        target.anchorMax = anchor;
        target.pivot = pivot;
        target.anchoredPosition = anchoredPosition;
    }
}
