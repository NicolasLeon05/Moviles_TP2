using UnityEngine;
using System.Collections.Generic;

public class ServiceRegister : MonoBehaviour
{
    [Header("Flyweights")]
    [SerializeField] private PlatformFlyweight[] flyweights;

    [Header("Scene References")]
    [SerializeField] private PlatformSpawner spawner;

    [SerializeField] private GameObject silverCoinPrefab;
    [SerializeField] private GameObject goldCoinPrefab;

    private void Awake()
    {
        Register();
    }

    public void Register()
    {
        var coinDict = new Dictionary<CoinType, GameObject>
        {
            { CoinType.Silver, silverCoinPrefab },
            { CoinType.Gold, goldCoinPrefab }
        };
        ServiceProvider.SetService<ICoinFactory>(new CoinFactory(coinDict), true);

        var platDict = new Dictionary<PlatformType, PlatformFlyweight>();
        foreach (var f in flyweights)
            platDict[f.platformType] = f;

        ServiceProvider.SetService<IPlatformFactory>(new PlatformFactory(platDict), true);
        ServiceProvider.SetService(spawner, true);
    }
}
