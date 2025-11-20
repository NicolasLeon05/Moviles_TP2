using UnityEngine;

[CreateAssetMenu(menuName = "Game/PlatformFlyweight")]
public class PlatformFlyweight : ScriptableObject
{
    public string id;            // "Basic", "Breaking", "Moving"
    public GameObject prefab;    // Modelo compartido
    public float width;
    public float height;
}