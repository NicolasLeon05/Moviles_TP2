using UnityEngine;

public interface IPlatformFactory
{
    PlatformInstance Create(PlatformType type, Vector2 position);
}