using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceProvider
{
    private static readonly Dictionary<Type, object> Services = new();

#if UNITY_EDITOR
    public static bool IsInitializedInEditor { get; private set; }

    public static void EditorRegisterServices()
    {
        if (IsInitializedInEditor)
            return;

        // Buscar ServiceRegister aunque esté desactivado o no en escena
        var reg = UnityEngine.Object.FindFirstObjectByType<ServiceRegister>(FindObjectsInactive.Include);

        if (reg == null)
        {
            Debug.LogError("ServiceRegister not found in Editor. You must place one in the Boot scene.");
            return;
        }

        reg.Register();
        IsInitializedInEditor = true;
    }
#endif

    public static void SetService<T>(T service, bool overwriteIfFound = false)
    {
        if (!Services.TryAdd(typeof(T), service) && overwriteIfFound)
            Services[typeof(T)] = service;
    }

    public static bool TryGetService<T>(out T service) where T : class
    {
        if (Services.TryGetValue(typeof(T), out var myService)
            && myService is T tService)
        {
            service = tService;
            return true;
        }

        service = null;
        return false;
    }

    public static T GetService<T>() where T : class
    {
        if (TryGetService<T>(out var service))
            return service;

        throw new Exception($"Service of type {typeof(T)} not found.");
    }
}
