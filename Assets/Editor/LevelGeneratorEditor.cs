using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelGenerator gen = (LevelGenerator)target;

        GUILayout.Space(15);
        GUI.backgroundColor = Color.green;

        if (GUILayout.Button("Generate In Editor", GUILayout.Height(30)))
        {
#if UNITY_EDITOR
            ServiceProvider.EditorRegisterServices();
#endif
            gen.GenerateLevelInEditor();
        }

        GUI.backgroundColor = Color.white;
    }
}
