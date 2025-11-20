using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelData levelData;

    private IPlatformFactory factory;
    private PlatformSpawner spawner;

    private void Start()
    {
        ServiceProvider.TryGetService(out factory);
        ServiceProvider.TryGetService(out spawner);

        GenerateLevel();
    }

    private void GenerateLevel()
    {
        foreach (var seg in levelData.segments)
            GenerateSegment(seg, levelData.difficulty, spawner, factory);
    }

    public void GenerateLevelInEditor()
    {
#if UNITY_EDITOR
        ServiceProvider.EditorRegisterServices();
#endif

        ServiceProvider.TryGetService(out IPlatformFactory factory);
        ServiceProvider.TryGetService(out PlatformSpawner spawner);

        if (factory == null || spawner == null)
        {
            Debug.LogError("Factory or Spawner not registered!");
            return;
        }

        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);

        foreach (var seg in levelData.segments)
            GenerateSegment(seg, levelData.difficulty, spawner, factory);
    }

    private void GenerateSegment(
        PlatformSegmentData segment,
        int difficulty,
        PlatformSpawner spawner,
        IPlatformFactory factory)
    {
        float y = segment.startY;

        while (y < segment.endY)
        {
            float x = Random.Range(-segment.platformDistanceX, segment.platformDistanceX);
            var pos = new Vector2(x, y);

            string type = PickPlatformType(difficulty);
            var inst = factory.Create(type, pos, difficulty);

            if (inst != null)
            {
                spawner.SpawnEditor(inst, this.transform);
            }

            y += segment.platformDistanceY;
        }
    }

    private string PickPlatformType(int difficulty)
    {
        return difficulty > 3 ? "Breaking" : "Basic";
    }
}
