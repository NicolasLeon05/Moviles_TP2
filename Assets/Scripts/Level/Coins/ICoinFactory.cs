using UnityEngine;

public enum CoinType
{
    Silver,
    Gold
}
public interface ICoinFactory
{
    GameObject Create(CoinType type, Transform parent);
}