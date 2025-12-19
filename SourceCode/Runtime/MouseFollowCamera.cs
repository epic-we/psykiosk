using UnityEngine;

public class MouseFollowCamera : MonoBehaviour {
    private bool cameraMovementOn = true;

    [Header("Parallax")]
    public float strength = 0.12f;
    public float smoothTime = 0.08f;

    [Header("Background Bounds")]
    public Transform background; // should have SpriteRenderer
    public Vector2 padding = new Vector2(0.05f, 0.05f);
    public bool useSpriteBounds = true;

    [Header("Manual Bounds (used when useSpriteBounds = false)")]
    public Vector2 manualMin = new Vector2(-10f, -6f);
    public Vector2 manualMax = new Vector2(10f, 6f);

    [Header("Dead Zone")]
    public bool useDeadZone = true;
    public Rect deadZone = new Rect(0.45f, 0f, 0.1f, 0.2f);

    [Header("Mouse movement requirement")]
    public bool requireMouseMovement = true;
    public float movementThreshold = 0.002f;

    Vector3 velocity = Vector3.zero;
    Vector2 previousMouseViewport = new Vector2(-1f, -1f);

    Camera cam;

    void OnValidate(){
        if (strength < 0f) strength = 0f;
        if (smoothTime < 0f) smoothTime = 0f;
        if (movementThreshold < 0f) movementThreshold = 0f;
    }


    void Start() {
        cam = GetComponent<Camera>();
        if (cam == null) cam = Camera.main;
    }

    void LateUpdate(){
        if (!cameraMovementOn) { return; }
        Vector2 mouseViewport = cam.ScreenToViewportPoint(Input.mousePosition);

        // Dead zone check
        if (useDeadZone && deadZone.Contains(mouseViewport)){
            previousMouseViewport = mouseViewport;
            return;
        }

        if (requireMouseMovement) {
            if (previousMouseViewport.x >= 0f) {
                float delta = (mouseViewport - previousMouseViewport).magnitude;
                if (delta < movementThreshold) {
                    return;
                }
            }
        }

        Vector2 centered = new Vector2(mouseViewport.x - 0.5f, mouseViewport.y - 0.5f);
        Vector2 curved = centered * centered.magnitude;
        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * ((float)Screen.width / (float)Screen.height);
        Vector3 offset = new Vector3(curved.x * horzExtent * strength, curved.y * vertExtent * strength, 0f);
        Vector3 target = cam.transform.position + offset;
        Vector2 min, max;

        if (useSpriteBounds && background != null) {
            SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
            if (sr != null) {
                Bounds b = sr.bounds;
                min = new Vector2(b.min.x + horzExtent + padding.x, b.min.y + vertExtent + padding.y);
                max = new Vector2(b.max.x - horzExtent - padding.x, b.max.y - vertExtent - padding.y);


                if (min.x > max.x) {
                    float centerX = (b.min.x + b.max.x) * 0.5f;
                    min.x = max.x = centerX;
                }
                if (min.y > max.y) {
                    float centerY = (b.min.y + b.max.y) * 0.5f;
                    min.y = max.y = centerY;
                }
            } else {
                min = manualMin;
                max = manualMax;
            }
        } else {
            min = manualMin;
            max = manualMax;
        }

        // Clamp target
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);
        target.z = transform.position.z;

        // Smoothly move
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
        previousMouseViewport = mouseViewport;
    }

    public void ToggleCameraMovement (bool toggle) {
        cameraMovementOn = toggle;
    }
}

