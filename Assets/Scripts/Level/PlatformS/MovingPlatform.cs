using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector2 localPointA;
    public Vector2 localPointB;
    public float speed = 2f;

    private Vector2 worldA;
    private Vector2 worldB;
    private Vector2 currentTarget;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }

    private void Start()
    {
        worldA = transform.TransformPoint(localPointA);
        worldB = transform.TransformPoint(localPointB);

        float t = Random.Range(0f, 1f);
        Vector2 randomStartPos = Vector2.Lerp(worldA, worldB, t);

        transform.position = randomStartPos;

        currentTarget = worldB;
    }

    private void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (rb == null) return;

        Vector2 newPos = Vector2.MoveTowards(rb.position, currentTarget, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(newPos, currentTarget) < 0.05f)
            currentTarget = (currentTarget == worldA) ? worldB : worldA;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            var playerRb = col.collider.attachedRigidbody;

            if (playerRb != null)
                playerRb.interpolation = RigidbodyInterpolation2D.None;

            col.collider.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.transform.SetParent(null);

            var playerRb = col.collider.attachedRigidbody;
            if (playerRb != null)
                playerRb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}
