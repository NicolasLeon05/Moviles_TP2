using System.Collections.Generic;
using UnityEngine;

public class PlatformFactory : IPlatformFactory
{
    private readonly Dictionary<PlatformType, PlatformFlyweight> _flyweights;

    public PlatformFactory(Dictionary<PlatformType, PlatformFlyweight> flyweights)
    {
        _flyweights = flyweights;
    }

    public PlatformInstance Create(PlatformType type, Vector2 position)
    {
        if (!_flyweights.TryGetValue(type, out var fly))
        {
            Debug.LogError($"No flyweight found: {type}");
            return null;
        }

        return new PlatformInstance
        {
            flyweight = fly,
            position = position,
        };
    }
}