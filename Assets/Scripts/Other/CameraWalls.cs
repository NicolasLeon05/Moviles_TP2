using UnityEngine;

public class CameraWalls : MonoBehaviour
{
    public Camera cam;
    public Transform leftWall;
    public Transform rightWall;

    public float wallThickness = 1f;

    private void Start()
    {
        if (cam == null)
            cam = Camera.main;

        UpdateWallPositions();
    }

    private void Update()
    {
        UpdateWallPositions();
    }

    private void UpdateWallPositions()
    {
        float left = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;
        float right = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x;

        leftWall.position = new Vector3(left - wallThickness * 0.5f, cam.transform.position.y, 0);
        rightWall.position = new Vector3(right + wallThickness * 0.5f, cam.transform.position.y, 0);

        BoxCollider2D leftCol = leftWall.GetComponent<BoxCollider2D>();
        BoxCollider2D rightCol = rightWall.GetComponent<BoxCollider2D>();

        if (leftCol != null)
            leftCol.size = new Vector2(wallThickness, 1000f);

        if (rightCol != null)
            rightCol.size = new Vector2(wallThickness, 1000f);
    }
}
