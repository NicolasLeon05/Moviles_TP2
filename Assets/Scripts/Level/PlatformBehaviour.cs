using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    private PlatformFlyweight flyweight;

    public void Init(PlatformFlyweight f)
    {
        flyweight = f;

        Debug.Log("Init called for: " + gameObject.name + " | moving=" + f.isMoving);

        ApplyVisual();
        ApplyPhysics();

        if (f.isMoving)
        {
            Debug.Log("Adding MovingPlatform to: " + gameObject.name);
            SetupMovingPlatform();
        }
    }


    private void SetupMovingPlatform()
    {
        var mp = gameObject.AddComponent<MovingPlatform>();
        Debug.Log(gameObject.name);

        float halfX = flyweight.offsetX * 0.5f;
        float halfY = flyweight.offsetY * 0.5f;

        mp.localPointA = new Vector2(-halfX, -halfY);
        mp.localPointB = new Vector2(+halfX, +halfY);

        mp.speed = flyweight.moveSpeed;
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
