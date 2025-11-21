using UnityEngine;

public class VerticalCameraFollow : MonoBehaviour
{
    public Transform target;
    public float verticalOffset = 3f;
    public float smooth = 5f;

    private void LateUpdate()
    {
        if (!target) return;

        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, target.position.y + verticalOffset, Time.deltaTime * smooth);
        if (pos.y < 0) pos.y = 0;
        transform.position = pos;
    }
}