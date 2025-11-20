using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    private PlatformFlyweight flyweight;

    public void Init(PlatformFlyweight f)
    {
        flyweight = f;

        ApplyVisual();
        ApplyPhysics();
    }

    private void ApplyVisual()
    {
        var sr = GetComponent<SpriteRenderer>();
    }

    private void ApplyPhysics()
    {
        var col = GetComponent<Collider2D>();
    }
}
