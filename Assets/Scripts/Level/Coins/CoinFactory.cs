using System.Collections.Generic;
using UnityEngine;

public class CoinFactory : ICoinFactory
{
    private readonly Dictionary<CoinType, GameObject> prefabs;

    public CoinFactory(Dictionary<CoinType, GameObject> prefabs)
    {
        this.prefabs = prefabs;
    }

    public GameObject Create(CoinType type, Transform parent)
    {
        if (!prefabs.TryGetValue(type, out var prefab))
        {
            Debug.LogError($"Coin prefab not found for type: {type}");
            return null;
        }

        var coin = Object.Instantiate(prefab, parent);
        coin.transform.localPosition = Vector3.up * 0.2f;

        if (!coin.TryGetComponent<Coin>(out var coinComponent))
            coinComponent = coin.AddComponent<Coin>();

        coinComponent.type = type;

        return coin;
    }

}
