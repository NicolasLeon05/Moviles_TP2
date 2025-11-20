#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject Spawn(PlatformInstance instance)
    {
        var go = Instantiate(instance.flyweight.prefab);
        go.transform.position = instance.position;
        go.GetComponent<PlatformBehaviour>()?.Init(instance.flyweight);
        return go;
    }

#if UNITY_EDITOR
    public GameObject SpawnEditor(PlatformInstance instance, Transform parent)
    {
        GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(instance.flyweight.prefab);
        go.transform.position = instance.position;

        if (parent != null)
            go.transform.SetParent(parent);

        go.GetComponent<PlatformBehaviour>()?.Init(instance.flyweight);

        return go;
    }
#endif
}
