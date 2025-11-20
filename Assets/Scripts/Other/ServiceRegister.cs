using UnityEngine;
using System.Collections.Generic;

public class ServiceRegister : MonoBehaviour
{
    [Header("Flyweights")]
    [SerializeField] private PlatformFlyweight[] flyweights;

    [Header("Scene References")]
    [SerializeField] private PlatformSpawner spawner;

    private void Awake()
    {
        Register();
    }

    public void Register()
    {
        var dict = new Dictionary<string, PlatformFlyweight>();
        foreach (var f in flyweights)
            dict[f.id] = f;

        ServiceProvider.SetService<IPlatformFactory>(new PlatformFactory(dict), true);
        ServiceProvider.SetService(spawner, true);
    }
}
