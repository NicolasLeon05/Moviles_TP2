using System.Collections.Generic;
using UnityEngine;

public class PlatformFactory : IPlatformFactory
{
    private readonly Dictionary<string, PlatformFlyweight> _flyweights;

    public PlatformFactory(Dictionary<string, PlatformFlyweight> flyweights)
    {
        _flyweights = flyweights;
    }

    public PlatformInstance Create(string type, Vector2 position, int difficulty)
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
            difficulty = difficulty
        };
    }
}