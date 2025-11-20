using UnityEngine;

public interface IPlatformFactory
{
    PlatformInstance Create(string type, Vector2 position, int difficulty);
}