using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelData levelData;

    private IPlatformFactory factory;
    private PlatformSpawner spawner;
    private List<Transform> platforms = new List<Transform>();
    [SerializeField] private GameObject flagPrefab;

    private void Start()
    {
        ServiceProvider.TryGetService(out factory);
        ServiceProvider.TryGetService(out spawner);

        GenerateLevel();
    }

    private void GenerateLevel()
    {
        foreach (var seg in levelData.segments)
            GenerateSegment(seg, spawner, factory);

        SpawnCoins(platforms);
        SpawnFinishFlag(platforms);
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
            GenerateSegment(seg, spawner, factory);
    }

    private void GenerateSegment(
        PlatformSegmentData segment,
        PlatformSpawner spawner,
        IPlatformFactory factory)
    {
        float y = segment.startY;

        while (y < segment.endY)
        {
            float x = Random.Range(-segment.platformDistanceX, segment.platformDistanceX);
            var pos = new Vector2(x, y);

            var type = GetPlatformTypeForSegment(segment);
            var inst = factory.Create(type, pos);

            if (inst != null)
            {
                GameObject go;
#if UNITY_EDITOR
                go = spawner.SpawnEditor(inst, transform);
#else
                go = spawner.Spawn(inst, transform);
#endif
                platforms.Add(go.transform);
            }
            y += segment.platformDistanceY;
        }
    }

    private PlatformType GetPlatformTypeForSegment(PlatformSegmentData segment)
    {
        return segment.platformType;
    }

    private List<CoinType> CalculateCoins(int totalCurrency)
    {
        List<CoinType> result = new List<CoinType>();

        int goldValue = CoinValues.GetValue(CoinType.Gold);
        int silverValue = CoinValues.GetValue(CoinType.Silver);

        int goldCount = totalCurrency / goldValue;
        int remainder = totalCurrency % goldValue;

        for (int i = 0; i < goldCount; i++)
            result.Add(CoinType.Gold);

        for (int i = 0; i < remainder / silverValue; i++)
            result.Add(CoinType.Silver);

        return result;
    }

    private void SpawnCoins(List<Transform> platforms)
    {
        ServiceProvider.TryGetService(out ICoinFactory coinFactory);

        var coins = CalculateCoins(levelData.totalCurrency);

        if (coins.Count > platforms.Count)
        {
            Debug.LogWarning("There are more coins than platforms, some coins won't spawn");
            coins = coins.GetRange(0, platforms.Count);
        }

        List<Transform> selectedPlatforms = new List<Transform>(platforms);
        selectedPlatforms.Remove(selectedPlatforms[selectedPlatforms.Count - 1]);
        Shuffle(selectedPlatforms);

        for (int i = 0; i < coins.Count; i++)
        {
            var platform = selectedPlatforms[i];
            coinFactory.Create(coins[i], platform);
        }
    }

    private void SpawnFinishFlag(List<Transform> platforms)
    {
        if (platforms == null || platforms.Count == 0)
        {
            Debug.LogWarning("No platforms to place finish flag");
            return;
        }
        Transform last = platforms[platforms.Count - 1];

        Vector3 pos = last.position + new Vector3(0f, 0.75f, 0f);
        Instantiate(flagPrefab, pos, Quaternion.identity, last);
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

}
