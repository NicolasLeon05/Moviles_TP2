using UnityEngine;

[CreateAssetMenu(menuName = "Game/PlatformFlyweight")]
public class PlatformFlyweight : ScriptableObject
{
    public PlatformType platformType;
    public GameObject prefab;
    public float width;
    public float height;

    [Header("Movement offset")]
    public bool isMoving;
    public float offsetX;
    public float offsetY;
    public float moveSpeed;
}